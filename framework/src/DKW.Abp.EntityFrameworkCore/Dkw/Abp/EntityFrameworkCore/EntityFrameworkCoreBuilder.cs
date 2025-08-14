// DKW ABP Framework Extensions
// Copyright (C) 2025 Doug Wilson
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

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dkw.Abp.EntityFrameworkCore;

public class EntityFrameworkCoreBuilder(WebApplicationBuilder Builder, DbContextOptionsBuilder dbContextOptionsBuilder)
{
    private readonly WebApplicationBuilder _builder = Builder;

    public IConfiguration Configuration => _builder.Configuration;
    public IServiceCollection Services => _builder.Services;
    public DbContextOptionsBuilder DbContextOptions { get; } = dbContextOptionsBuilder;
}
