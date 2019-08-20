using System;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

namespace App.Basic.API.Infrastructure.Libraries
{
    public class StatementBuilder
    {
        public static Func<TSource, TSource> NewStatement<TSource>(string fields)
                    where TSource : class
        {
            var entityType = typeof(TSource);
            var properties = entityType.GetProperties();

            // input parameter "o"
            var xParameter = Expression.Parameter(entityType, "o");

            //dynamic MyDynamic = new System.Dynamic.ExpandoObject();

            // new statement "new Data()"
            var xNew = Expression.New(typeof(ExpandoObject));

            // create initializers
            var bindings = fields.Split(',').Select(o => o.Trim())
                .Select(o =>
                {

                    //因为客户端不一定使用帕斯卡法则的字段名称,所以需要自己匹配正确的字段名称
                    var realPropName = string.Empty;

                    for (int idx = properties.Length - 1; idx >= 0; idx--)
                    {
                        var propName = properties[idx].Name.ToString();
                        if (propName.ToLower() == o.ToLower())
                        {
                            realPropName = propName;
                            break;
                        }
                    }//for


                    // property "Field1"
                    var mi = entityType.GetProperty(realPropName);

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

        //public static Func<TSource, TSource> NewStatement1<TSource>(TSource x, string fields)
        //      where TSource : class
        //{
        //    dynamic expando = new ExpandoObject();
        //    expando.someProperty = x.prop1;
        //    expando.someOtherProperty = x.prop2;
        //    return (ExpandoObject)expando;
        //}
    }
}
