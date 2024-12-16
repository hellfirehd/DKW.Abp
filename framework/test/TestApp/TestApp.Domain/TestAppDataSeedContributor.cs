using Bogus;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;

namespace TestApp;

public class TestAppDataSeedContributor(IRepository<Person, Guid> personRepository) : IDataSeedContributor
{
    protected readonly IRepository<Person, Guid> PersonRepository = personRepository;

    public async Task SeedAsync(DataSeedContext context)
    {
        var faker = new Faker();

        var fakePerson = new Person(Guid.NewGuid(), faker.Person.FullName, faker.Random.Int(10, 110));

        await PersonRepository.InsertAsync(fakePerson, true);
    }
}
