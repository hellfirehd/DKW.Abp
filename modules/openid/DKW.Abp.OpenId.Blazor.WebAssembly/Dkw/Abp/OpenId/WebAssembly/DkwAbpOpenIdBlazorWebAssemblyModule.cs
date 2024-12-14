using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.Modularity;

namespace DKW.Abp.OpenId.WebAssembly;

[DependsOn(typeof(AbpAutofacWebAssemblyModule))]
[DependsOn(typeof(DkwOpenIdApplicationContractsModule))]
public class DkwAbpOpenIdBlazorWebAssemblyModule : AbpModule
{
    public override async Task PreConfigureServicesAsync(ServiceConfigurationContext context)
    {
        var builder = context.Services.GetHostBuilder();

        await builder.AddOpenIdEndpointsAsync();
    }
}
