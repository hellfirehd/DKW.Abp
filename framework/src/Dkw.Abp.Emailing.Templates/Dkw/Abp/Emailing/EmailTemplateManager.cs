// Canadian Professional Counsellors Association Application Suite
// Copyright (C) 2024 Doug Wilson
//
// This program is free software: you can redistribute it and/or modify it under the terms of
// the GNU Affero General Public License as published by the Free Software Foundation, either
// version 3 of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License along with this
// program. If not, see <https://www.gnu.org/licenses/>.

using Dkw.Abp.Security;
using Dkw.Abp.Templates;
using System.Security.Claims;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;

namespace Dkw.Abp.Emailing;

public class EmailTemplateManager(IEmailTemplateProvider provider, ITemplateRenderer renderer, IEmailSender sender, TimeProvider timeProvider) : IEmailTemplateManager, ITransientDependency
{
    private readonly IEmailTemplateProvider _provider = provider;
    private readonly ITemplateRenderer _renderer = renderer;
    private readonly IEmailSender _sender = sender;

    private DateTime Timestamp { get; } = timeProvider.GetUtcNow().DateTime;

    public T Get<T>(ClaimsPrincipal principal)
        where T : IEmailTemplateBuilder
        => Get<T>(principal.ZoneInfo()).To(principal);

    public T Get<T>(String timeZone = DkwDefaults.TimeZone)
        where T : IEmailTemplateBuilder
    {
        var template = _provider.GetTemplate<T>();

        if (template is IRequireEmailSender sender)
        {
            sender.SetSender(_sender);
        }

        if (template is ITimeStampedEmail ts)
        {
            ts.SetTimeStamp(Timestamp);
        }

        return template;
    }

    public async Task SendAsync<T>(T template, CancellationToken cancellationToken = default)
        where T : class, IEmailTemplateBuilder
    {
        await _renderer.RenderAsync(template, cancellationToken: cancellationToken);

        var message = template.Build();

        await _sender.SendAsync(message);
    }
}
