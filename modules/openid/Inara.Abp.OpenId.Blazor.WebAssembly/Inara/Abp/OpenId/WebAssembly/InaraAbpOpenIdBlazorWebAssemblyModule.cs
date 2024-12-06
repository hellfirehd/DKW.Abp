using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.Modularity;

namespace Inara.Abp.OpenId.WebAssembly;

[DependsOn(typeof(AbpAutofacWebAssemblyModule))]
[DependsOn(typeof(InaraOpenIdApplicationContractsModule))]
public class InaraAbpOpenIdBlazorWebAssemblyModule : AbpModule
{
    public override async Task PreConfigureServicesAsync(ServiceConfigurationContext context)
    {
        var builder = context.Services.GetHostBuilder();

        await builder.AddOpenIdEndpointsAsync();
    }
}