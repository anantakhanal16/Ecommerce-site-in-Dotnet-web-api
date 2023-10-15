﻿
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecifications<T> 
    {
        Expression<Func<T, bool>> Criteria { get; }

        List<Expression<Func<T, object>>> Includes { get; }

        //For Ordering 
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }

        int Take { get; }
        int Skip { get; }

        bool IsPagingEnabled { get; }
    }
}
