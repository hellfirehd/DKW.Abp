// Canadian Professional Counsellors Association Application Suite
// Copyright (C) 2024 Doug Wilson
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

using DKW.Abp.Ddd.Querying;
using Microsoft.EntityFrameworkCore;

namespace Dkw.Abp.EntityFrameworkCore;

public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> GetQuery<TEntity>(this IQueryable<TEntity> query, IQuerySpecification<TEntity> spec)
        where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(query);
        if (spec.IsReadOnly)
        {
            query = query.AsNoTracking();
        }

        query = query.Where(spec.ToExpression());

        query = spec.Includes.Aggregate(query, (q, e) => q.Include(e));

        if (spec.OrderBy != null)
        {
            query = spec.ThenBy == null
                ? query.OrderBy(spec.OrderBy)
                : query.OrderBy(spec.OrderBy).ThenBy(spec.ThenBy);
        }
        else if (spec.OrderByDescending != null)
        {
            query = spec.ThenBy == null
                ? query.OrderByDescending(spec.OrderByDescending)
                : query.OrderByDescending(spec.OrderByDescending).ThenBy(spec.ThenBy);
        }

        if (spec.GroupBy != null)
        {
            query = query.GroupBy(spec.GroupBy).SelectMany(x => x);
        }

        if (spec.IsPagingEnabled)
        {
            query = query.Skip((spec.PageNo - 1) * spec.PageSize).Take(spec.PageSize);
        }

        return query;
    }
}
