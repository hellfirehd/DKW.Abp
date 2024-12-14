using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace TestApp.EntityFrameworkCore;

public class PersonRepository : EfCoreRepository<TestAppDbContext, Person, Guid>, IPersonRepository
{
    public PersonRepository(IDbContextProvider<TestAppDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<PersonView> GetViewAsync(string name)
    {
        return await (await GetDbContextAsync()).PersonView.Where(x => x.Name == name).FirstOrDefaultAsync();
    }
}
