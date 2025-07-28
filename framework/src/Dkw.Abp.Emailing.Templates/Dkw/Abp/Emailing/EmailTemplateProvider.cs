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

using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace Dkw.Abp.Emailing;

public class EmailTemplateProvider(IServiceProvider serviceProvider) : IEmailTemplateProvider, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

    public T GetTemplate<T>()
        where T : IEmailTemplateBuilder
        => _serviceProvider.GetRequiredService<T>();
}
