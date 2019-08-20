using System;

namespace App.MoreJee.Export
{
    public class ServiceBase
    {
        public readonly string Server;
        public readonly string Token;
        public ServiceBase(string server, string auth)
        {
            if (!string.IsNullOrWhiteSpace(server))
            {
                var b = server[server.Length - 1];
                Server = b == '/' ? server.Substring(0, server.Length - 1) : server;
            }

            if (!string.IsNullOrWhiteSpace(auth))
            {
                var arr = auth.Split("bearer", StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length >= 1)
                    Token = arr[0].Trim();
            }
        }
    }
}
