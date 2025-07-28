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

using System.Net.Mail;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;

namespace Dkw.Abp.Emailing;

/// <summary>
/// Public properties on this class and any derived classes will be used to
/// populate the email template. Placeholders in the template are in the forrm
/// of <c>{{ PropertyName }}</c>.
/// </summary>
/// <param name="emailMessageBuilder"></param>
/// <remarks>
/// The following properties are autoatically available but must be set by the
/// dereived class:
///     <list type="bullet">
///         <listheader>
///             <term>Subject</term>
///             <description>The text that will be used as the subject line of the email</description>
///         </listheader>
///         <item>
///             <term>Subject</term>
///             <description>The text that will be used as the subject line of the email</description>
///         </item>
///     </list>
/// </remarks>
public abstract class EmailTemplateBuilder(MailMessageBuilder emailMessageBuilder)
    : IEmailTemplateBuilder, IEmailTemplateSender, IRequireEmailSender, ITransientDependency
{
    private IEmailSender? _sender;

    public String Subject => Builder.Subject;
    public Boolean HasAttachments => Builder.HasAttachments;

    public MailMessageBuilder Builder { get; } = emailMessageBuilder;

    public virtual String TemplateName => GetType().Name;

    public void SetBody(String body) => Builder.WithHtmlBody(body);

    protected virtual void Apply() { }
    protected virtual void Validate() { }

    MailMessage IEmailTemplateBuilder.Build()
    {
        Apply();
        Validate();
        return Builder.Build();
    }

    void IRequireEmailSender.SetSender(IEmailSender sender)
        => _sender = sender ?? throw new ArgumentNullException(nameof(sender));

    async Task IEmailTemplateSender.SendAsync(CancellationToken cancelationToken)
    {
        if (_sender is null)
        {
            throw new InvalidOperationException("The IEmailSender has not been provided.");
        }

        await _sender.SendAsync(Builder.Build());
    }
}
