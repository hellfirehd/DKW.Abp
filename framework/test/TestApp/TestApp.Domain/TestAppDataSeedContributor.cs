using Bogus;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;

namespace TestApp;

public class TestAppDataSeedContributor(IRepository<Person, Guid> personRepository) : IDataSeedContributor
{
    protected readonly IRepository<Person, Guid> PersonRepository = personRepository;

    public async Task SeedAsync(DataSeedContext context)
    {
        var fakePerson = new Faker<Person>()
            .RuleFor(p => p.TenantId, f => default)
            .RuleFor(p => p.CityId, f => default)
            .RuleFor(p => p.Name, f => f.Person.FullName)
            .RuleFor(p => p.Age, f => f.Random.Int())
            .RuleFor(p => p.Birthday, f => default)
            .RuleFor(p => p.LastActive, f => default)
            .RuleFor(p => p.NotMappedDateTime, f => default)
            .RuleFor(p => p.Phones, f => default)
            .RuleFor(p => p.LastActiveTime, f => f.Date.Past())
            .RuleFor(p => p.HasDefaultValue, f => f.Date.Past())
            .RuleFor(p => p.EntityVersion, f => f.Random.Int())
            .RuleFor(p => p.IsDeleted, f => f.Random.Bool())
            .RuleFor(p => p.DeleterId, f => default)
            .RuleFor(p => p.DeletionTime, f => default)
            .RuleFor(p => p.LastModificationTime, f => default)
            .RuleFor(p => p.LastModifierId, f => default)
            .RuleFor(p => p.CreationTime, f => f.Date.Past())
            .RuleFor(p => p.CreatorId, f => default)
            .RuleFor(p => p.ExtraProperties, f => default)
            .RuleFor(p => p.ConcurrencyStamp, f => f.Lorem.Word())
            .RuleFor(p => p.Id, f => f.Random.Uuid());

        await PersonRepository.InsertAsync(fakePerson.Generate(), true);
    }
}
