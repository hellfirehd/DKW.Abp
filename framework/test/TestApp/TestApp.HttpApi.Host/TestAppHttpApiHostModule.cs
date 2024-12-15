
using DKW.Abp.Logging;
using DKW.Abp.Microservices;
using TestApp.EntityFrameworkCore.Sqlite;
using TestApp.MultiTenancy;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace TestApp;

[DependsOn(typeof(TestAppApplicationModule))]
[DependsOn(typeof(TestAppEntityFrameworkCoreSqliteModule))]
[DependsOn(typeof(TestAppHttpApiModule))]
[DependsOn(typeof(DkwAbpLoggingModule))]
[DependsOn(typeof(DkwAbpMicroserviceModule))]
public class TestAppHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddDefaultLogger();
        // This is because we are defaulting to Sqlite which does not support transactions.
        //context.Services.AddAlwaysDisableUnitOfWorkTransaction();

        context.ConfigureMicroservice("TestApp");

        ConfigureConventionalControllers();
        ConfigureVirtualFileSystem(context);
    }

    private void ConfigureConventionalControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(TestAppApplicationModule).Assembly);
        });
    }

    private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<TestAppDomainSharedModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}TestApp.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<TestAppDomainModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}TestApp.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<TestAppApplicationContractsModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}TestApp.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<TestAppApplicationModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}TestApp.Application"));
            });
        }
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();
        app.UseCorrelationId();
        app.MapAbpStaticAssets();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseDynamicClaims();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "TestApp API");

            var configuration = context.GetConfiguration();
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            options.OAuthScopes("TestApp");
        });

        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
