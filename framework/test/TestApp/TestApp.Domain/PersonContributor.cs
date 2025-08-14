// DKW ABP Framework Extensions
// Copyright (C) 2025 Doug Wilson
//
// This program is free software: you can redistribute it and/or modify it under the terms of
// the GNU Affero General Public License as published by the Free Software Foundation, either
// version 3 of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License along with this
// program. If not, see <https://www.gnu.org/licenses/>.

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
