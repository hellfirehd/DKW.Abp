using Volo.Abp.Domain.Repositories;

namespace TestApp;

public interface IPersonRepository : IBasicRepository<Person, Guid>
{
    Task<PersonView> GetViewAsync(string name);
}
