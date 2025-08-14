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
