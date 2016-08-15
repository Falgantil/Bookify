using System;
using System.Linq;
using System.Linq.Expressions;

namespace Bookify.Core.Extensions
{
    public static class Extensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string ordering, bool? desc)//, params object[] values
        {

            var type = typeof(T);
            
            var property = type.GetProperty(ordering);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            string tempDesc = "OrderBy";
            if (desc != null && desc.Value == true)
                tempDesc = "OrderByDescending";

            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), tempDesc, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }

    }
}
