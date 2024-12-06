using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace Inara.Abp.OpenId;

[DependsOn(typeof(AbpDataModule))]
public class InaraOpenIdApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var hostBuilder = context.Services.GetSingletonInstanceOrNull<IHostBuilder>();
        hostBuilder?.AddInaraOpenIdEndpoints();
    }
}