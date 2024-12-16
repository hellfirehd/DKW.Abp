using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;
using Volo.Abp.OpenIddict.EntityFrameworkCore;

namespace DKW.Abp.EntityFrameworkCore;

[IgnoreMultiTenancy]
[ConnectionStringName(DbConsts.ConnectionStringName)]
public class OpenIdDbContext(DbContextOptions<OpenIdDbContext> options)
    : AbpDbContext<OpenIdDbContext>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureOpenIddict();
        builder.ConfigureIdentity();
    }
}
