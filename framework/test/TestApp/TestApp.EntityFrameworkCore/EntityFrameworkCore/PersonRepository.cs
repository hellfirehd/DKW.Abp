using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace TestApp.EntityFrameworkCore;

public class PersonRepository(IDbContextProvider<TestAppDbContext> dbContextProvider)
    : EfCoreRepository<TestAppDbContext, Person, Guid>(dbContextProvider), IPersonRepository
{
    public async Task<PersonView?> GetViewAsync(String name)
    {
        return await (await GetDbContextAsync()).PersonView.Where(x => x.Name == name).FirstOrDefaultAsync();
    }
}
