using Bogus;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace TestApp;

public class PersonContributor(IRepository<Person, Guid> personRepository) : IDataSeedContributor, ITransientDependency
{
    protected readonly IRepository<Person, Guid> PersonRepository = personRepository;

    public async Task SeedAsync(DataSeedContext context)
    {
        var faker = new Faker();
        var person = new Person(Guid.NewGuid(), faker.Person.FullName, faker.Random.Int(18, 110));

        await PersonRepository.InsertAsync(person, true);
    }
}
