using System;
using System.Linq;
using System.Linq.Expressions;

namespace Bookify.Core.Extensions
{
    public static class QueryAbleExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string ordering, bool? desc)//, params object[] values
        {

            var type = typeof(T);
            
            var property = type.GetProperty(ordering);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            var tempDesc = "OrderBy";
            if (desc != null && desc.Value)
                tempDesc = "OrderByDescending";

            var resultExp = Expression.Call(typeof(Queryable), tempDesc, new[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}
