using System;
using System.Linq;
using System.Linq.Expressions;

namespace EntityFramework.Spike
{
    public static class OrderByConstructor<T> where T : class, new()
    {
        public static Func<IQueryable<T>, IOrderedQueryable<T>> GenerateOrderBy(OrderByDescriptor orderByDescriptor)
        {
            if (orderByDescriptor == null)
                return null;

            Func<IQueryable<T>, IOrderedQueryable<T>> odby = (f =>
            {
                IOrderedQueryable<T> temOrderedQueryable = null;
                foreach (OrderByItem item in orderByDescriptor.OrderByList)
                {
                    if (!string.IsNullOrEmpty(item.ColumnName))
                    {
                        if (temOrderedQueryable == null)
                            temOrderedQueryable = f.OrderByField(item.ColumnName, item.Ascending, item.Ascending ? "OrderBy" : "OrderByDescending");
                        else
                            temOrderedQueryable = temOrderedQueryable.OrderByField(item.ColumnName, item.Ascending, item.Ascending ? "ThenBy" : "ThenByDescending");
                    }
                }
                return temOrderedQueryable;
            });

            return odby;
        }
    }

    internal static class Extends
    {
        internal static IOrderedQueryable<T> OrderByField<T>(this IQueryable<T> q, string SortField, bool Ascending, string orderbyMethod)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var prop = BuildPropertyExpression(param, SortField);
            var exp = Expression.Lambda(prop, param);
            var types = new Type[] { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), orderbyMethod, types, q.Expression, exp);
            return (IOrderedQueryable<T>)q.Provider.CreateQuery<T>(mce);
        }

        static MemberExpression BuildPropertyExpression(Expression source, string propertyNames)
        {
            var proArray = propertyNames.Split('.');
            return proArray.Aggregate<string, MemberExpression>(null, (current, pn) => Expression.Property(current ?? source, pn));
        }
    }
}