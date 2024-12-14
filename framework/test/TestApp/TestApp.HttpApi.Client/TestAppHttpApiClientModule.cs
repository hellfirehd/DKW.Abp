using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.VirtualFileSystem;

namespace TestApp;

[DependsOn(typeof(TestAppApplicationContractsModule))]
[DependsOn(typeof(AbpAccountHttpApiClientModule))]
[DependsOn(typeof(AbpIdentityHttpApiClientModule))]
[DependsOn(typeof(AbpPermissionManagementHttpApiClientModule))]
[DependsOn(typeof(AbpTenantManagementHttpApiClientModule))]
[DependsOn(typeof(AbpFeatureManagementHttpApiClientModule))]
[DependsOn(typeof(AbpSettingManagementHttpApiClientModule))]
public class TestAppHttpApiClientModule : AbpModule
{
    public const string RemoteServiceName = "Default";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(TestAppApplicationContractsModule).Assembly,
            RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TestAppHttpApiClientModule>();
        });
    }
}
