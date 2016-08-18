using System;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Bookify.DataAccess.Extensions
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

            var tempDesc = "OrderBy";
            if (desc != null && desc.Value)
                tempDesc = "OrderByDescending";

            var resultExp = Expression.Call(typeof(Queryable), tempDesc, new[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }

    }
}
