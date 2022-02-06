using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace System.Linq
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> ApplyOrder<T>(this IQueryable<T> query, string field, bool? ascending, List<SortableField<T>> orders)
        {
            if (!string.IsNullOrEmpty(field) && ascending.HasValue)
            {
                Expression<Func<T, object>> order = orders.FirstOrDefault(o => o.Field.ToLowerInvariant() == field.ToLowerInvariant())?.OrderFunction;

                if (order != null)
                {
                    if (ascending.Value)
                    {
                        query = query.OrderBy(order);
                    }
                    else
                    {
                        query = query.OrderByDescending(order);
                    }
                }
            }
            return query;
        }

        public static IQueryable<T> ApplySkipAndTake<T>(this IQueryable<T> query, int? pageIndex, int? pageSize)
        {
            if (pageIndex != null && pageSize != null)
            {
                var t = Math.Max(0, pageSize.Value);
                var s = Math.Max(0, pageIndex.Value) * t;
                query = query.Skip(s).Take(t);
            }
            return query;
        }
    }
}

public class SortableField<T>
{
    public string Field { get; set; }
    public Expression<Func<T, object>> OrderFunction { get; set; }

    public SortableField(string field, Expression<Func<T, object>> func)
    {
        this.Field = field;
        this.OrderFunction = func;
    }
}