using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DKW.Abp.EntityFrameworkCore;

public class EntityFrameworkCoreBuilder(WebApplicationBuilder Builder, DbContextOptionsBuilder dbContextOptionsBuilder)
{
    private readonly WebApplicationBuilder _builder = Builder;

    public IConfiguration Configuration => _builder.Configuration;
    public IServiceCollection Services => _builder.Services;
    public DbContextOptionsBuilder DbContextOptions { get; } = dbContextOptionsBuilder;
}