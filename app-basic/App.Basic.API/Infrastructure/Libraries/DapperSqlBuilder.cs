using App.Basic.Infrastructure.EntityMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace App.Basic.API.Infrastructure.Libraries
{
    public class DapperSqlBuilder
    {
        static string GetTableName<T>(string tableAlias = "o")
              where T : IEntityTypeMap
        {
            var entityType = typeof(T);
            var tableNameConst = entityType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Where(x => x.Name.ToLower() == "tablenamealias").FirstOrDefault();
            if (tableNameConst == null)
                throw new Exception($"在{entityType.Name}配置中没有发现TableNameAlias常量信息!");

            return tableNameConst.GetRawConstantValue().ToString();
        }

        /// <summary>
        /// select语句中的字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldsStr"></param>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        public static string FieldSelector<T>(string fieldsStr, string tableAlias = "o")
            where T : IEntityTypeMap
        {

            var selector = new List<string>();
            var entityType = typeof(T);
            var maps = entityType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            if (string.IsNullOrWhiteSpace(fieldsStr))
            {
                foreach (var item in maps)
                {
                    if (item.Name.ToLower() != "tablenamealias")
                        selector.Add($"{tableAlias}.{item.GetRawConstantValue()} as {item.Name}");
                }
            }
            else
            {
                fieldsStr = fieldsStr.ToLower();
                fieldsStr = fieldsStr.Replace("，", ",");//中文逗号防错
                var fieldArr = fieldsStr.Split(",", StringSplitOptions.RemoveEmptyEntries);
                foreach (var fieldName in fieldArr)
                {
                    var item = maps.Where(x => x.Name.ToLower() == fieldName).FirstOrDefault();
                    if (item != null)
                        selector.Add($"{tableAlias}.{item.GetRawConstantValue()} as {item.Name}");

                }
            }
            return string.Join(',', selector);
        }

        //public static string SimpleQuery<T>(string schema, string fieldStr)
        //      where T : class
        //{
        //    var tableAlias = "o";
        //    var tableName = GetTableName<T>(tableAlias);
        //    var selectorStr = FieldSelector<T>(fieldStr, tableAlias);

        //    var sql = $"select {selectorStr} from {schema}.{tableName} as {tableAlias}";

        //    return sql;
        //}

        //public static void SimplePagingQuery<T>(string schema, string fieldStr, int page, int pageSize, out string dataSelectSQL, out string dataCountSQL, string tableAlias = "o")
        //      where T : class
        //{
        //    var tableName = GetTableName<T>(tableAlias);
        //    var selectorStr = FieldSelector<T>(fieldStr, tableAlias);

        //    dataSelectSQL = $"select {selectorStr} from {schema}.{tableName} as {tableAlias}";
        //    dataCountSQL = $"select count(*) from {schema}.{tableName} as {tableAlias}";
        //}
    }
}
