using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Basic.API.Infrastructure.Extensions
{
    public static class QueryableExtension
    {
        //
        // 摘要:
        //     Projects each element of a sequence into a new form.
        //
        // 参数:
        //   source:
        //     A sequence of values to project.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // 类型参数:
        //   TSource:
        //     The type of the elements of source.
        //
        //   TResult:
        //     The type of the value returned by the function represented by selector.
        //
        // 返回结果:
        //     An System.Linq.IQueryable`1 whose elements are the result of invoking a projection
        //     function on each element of source.
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     source or selector is null.
        //public static IQueryable<TResult> Select<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector);

        public static IQueryable<TResult> Select<TSource, TResult>(this IQueryable<TSource> source, string fields)
        {
            // input parameter "o"
            var xParameter = Expression.Parameter(typeof(TSource), "o");

            // new statement "new Data()"
            var xNew = Expression.New(typeof(TSource));

            // create initializers
            var bindings = fields.Split(',').Select(o => o.Trim())
                .Select(o => {

            // property "Field1"
            var mi = typeof(TSource).GetProperty(o);

            // original value "o.Field1"
            var xOriginal = Expression.Property(xParameter, mi);

            // set value "Field1 = o.Field1"
            return Expression.Bind(mi, xOriginal);
                }
            );

            // initialization "new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var xInit = Expression.MemberInit(xNew, bindings);

            // expression "o => new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var lambda = Expression.Lambda<Func<TSource, TSource>>(xInit, xParameter);

            // compile to Func<Data, Data>
            return lambda.Compile();
        }

    }
}
