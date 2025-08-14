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
public class QuerySpecification<TEntity>(Expression<Func<TEntity, Boolean>> expression) : ExpressionSpecification<TEntity>(expression), IQuerySpecification<TEntity> where TEntity : class
{
    public QuerySpecification(ISpecification<TEntity> specification) : this(specification.ToExpression()) { }

    public ICollection<Expression<Func<TEntity, Object>>> Includes { get; } = [];
    public Expression<Func<TEntity, Object>>? OrderBy { get; private set; }
    public Expression<Func<TEntity, Object>>? ThenBy { get; private set; }
    public Expression<Func<TEntity, Object>>? OrderByDescending { get; private set; }
    public Expression<Func<TEntity, Object>>? GroupBy { get; private set; }

    public Int32 PageNo { get; private set; }
    public Int32 PageSize { get; private set; }
    public Boolean IsPagingEnabled => PageNo > 0 && PageSize > 0 && (OrderBy != null || OrderByDescending != null);
    public Boolean IsReadOnly { get; private set; }

    public virtual QuerySpecification<TEntity> SetReadOnly(Boolean isReadOnly = true)
    {
        IsReadOnly = isReadOnly;
        return this;
    }

    public virtual QuerySpecification<TEntity> ApplyGroupBy(Expression<Func<TEntity, Object>> groupByExpression)
    {
        GroupBy = groupByExpression;
        return this;
    }

    public virtual QuerySpecification<TEntity> AddInclude(Expression<Func<TEntity, Object>> includeExpression)
    {
        Includes.Add(includeExpression);
        return this;
    }

    public virtual QuerySpecification<TEntity> ApplyOrderBy(Expression<Func<TEntity, Object>> orderByExpression)
    {
        OrderBy = orderByExpression;
        return this;
    }

    public virtual QuerySpecification<TEntity> ApplyThenBy(Expression<Func<TEntity, Object>> orderByExpression)
    {
        ThenBy = orderByExpression;
        return this;
    }

    public virtual QuerySpecification<TEntity> ApplyOrderByDescending(Expression<Func<TEntity, Object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
        return this;
    }

    public virtual QuerySpecification<TEntity> ApplyPaging(Int32 pageNo, Int32 pageSize)
    {
        PageNo = pageNo;
        PageSize = pageSize;
        return this;
    }

    public virtual QuerySpecification<TEntity> ApplyPaging(Int32 pageNo, Int32 pageSize, Expression<Func<TEntity, Object>> orderByExpression, Expression<Func<TEntity, Object>>? thenByExpression = null)
    {
        PageNo = pageNo;
        PageSize = pageSize;
        OrderBy = orderByExpression;
        ThenBy = thenByExpression;
        return this;
    }
}
