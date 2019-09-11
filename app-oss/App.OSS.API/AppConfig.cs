
namespace App.OSS.API
{
    /// <summary>
    /// 应用程序配置文件
    /// </summary>
    public class AppConfig
    {
        public JwtSettings JwtSettings { get; set; }
        public DatabaseSettings DatabaseSettings { get; set; }
        public ConsulSettings ConsulSettings { get; set; }
    }

    /// <summary>
    /// Jwt配置
    /// </summary>
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public int ExpiresDay { get; set; }
    }

    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DatabaseSettings
    {
        public string Server { get; }
        public string Port { get; }
        public string Database { get; }
        public string DatabaseType { get; }
        public string UserId { get; }
        public string Password { get; }
    }


    public class ConsulSettings
    {
        public ConsulEndPoint Server { get; set; }
        public ConsulEndPoint Client { get; set; }
    }

    public class ConsulEndPoint
    {
        public string IP { get; set; }
        public int Port { get; set; }
    }

}
