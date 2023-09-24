
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
    }
}
