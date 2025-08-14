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

using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace Dkw.Abp.Ddd.Querying;

public interface IQuerySpecification<TEntity> : ISpecification<TEntity>
{
    ICollection<Expression<Func<TEntity, Object>>> Includes { get; }
    Expression<Func<TEntity, Object>>? OrderBy { get; }
    Expression<Func<TEntity, Object>>? ThenBy { get; }
    Expression<Func<TEntity, Object>>? OrderByDescending { get; }
    Expression<Func<TEntity, Object>>? GroupBy { get; }

    Int32 PageSize { get; }

    /// <summary>
    /// Page number, starting at 1.
    /// </summary>
    Int32 PageNo { get; }
    Boolean IsPagingEnabled { get; }
    Boolean IsReadOnly { get; }
}
