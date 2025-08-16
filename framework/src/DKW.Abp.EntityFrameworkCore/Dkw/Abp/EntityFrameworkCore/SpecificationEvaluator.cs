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

using Dkw.Abp.Ddd.Querying;
using Microsoft.EntityFrameworkCore;

namespace Dkw.Abp.EntityFrameworkCore;

public static class SpecificationEvaluator
{
    /// <summary>
    /// Applies the given <paramref name="spec"/> to the <paramref name="query"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity in the query.</typeparam>
    /// <param name="query">The base query to which the specification will be applied. Must not be <see langword="null"/>.</param>
    /// <param name="spec">The query specification that defines filtering, sorting, grouping, and pagination rules. Cannot be
    ///     <see langword="null"/>.</param>
    /// <returns>A new <see cref="IQueryable{TEntity}"/> that represents the result of applying the specification to the base
    /// query.</returns>
    /// <remarks>The method applies the following transformations based on the provided <paramref name="spec"/>:
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///             If <see cref="IQuerySpecification{TEntity}.IsReadOnly"/> is <see langword="true"/>, the query is
    ///             marked as no-tracking using <see cref="EntityFrameworkQueryableExtensions.AsNoTracking{TEntity}(IQueryable{TEntity})"/>.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///             Filters the query using the expression returned by <see cref="IQuerySpecification{TEntity}.ToExpression"/>.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///             Includes navigation properties specified in <see cref="IQuerySpecification{TEntity}.Includes"/>.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///             Applies ordering based on <see cref="IQuerySpecification{TEntity}.OrderBy"/>, 
    ///             <see cref="IQuerySpecification{TEntity}.OrderByDescending"/>, and 
    ///             <see cref="IQuerySpecification{TEntity}.ThenBy"/>.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///             Groups the query using <see cref="IQuerySpecification{TEntity}.GroupBy"/> and flattens the result.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///             Applies pagination if <see cref="IQuerySpecification{TEntity}.IsPagingEnabled"/> is <see langword="true"/>.
    ///         </description>
    ///     </item>
    /// </list>
    /// </remarks>
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
