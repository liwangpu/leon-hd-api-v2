
using App.Base.API.Infrastructure.Consts;
using App.Base.API.Infrastructure.Libraries;
using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Data;
using System.Data.SqlClient;

namespace App.Base.API.Infrastructure.Services
{
    public class DapperConnection : IDapperConnection
    {
        public string ConnectionString { get; protected set; }
        protected string _databaseType;

        #region ctor
        public DapperConnection(string databaseType, string server, string port, string database, string userId, string password)
        {
            _databaseType = databaseType.ToLower();
            ConnectionString = DbConnectionStringBuilder.Build(databaseType, server, port, database, userId, password);
        }
        #endregion

        /// <summary>
        /// 创建IDbConnection连接实例
        /// </summary>
        /// <returns></returns>
        public IDbConnection Create()
        {
            if (_databaseType == AppDatabaseConst.Postgres)
                return new NpgsqlConnection(ConnectionString);
            else if (_databaseType == AppDatabaseConst.SQLServer)
                return new SqlConnection(ConnectionString);
            else if (_databaseType == AppDatabaseConst.MySQL)
                return new MySqlConnection(ConnectionString);
            else
                throw new Exception($"没有实施数据库类型为{_databaseType}的Dapper驱动");
        }
    }
}
