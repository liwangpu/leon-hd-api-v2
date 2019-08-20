using App.OSS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace App.OSS.API.Infrastructure.Services
{
    public class DBMigrationService : IHostedService
    {
        private readonly OSSAppContext context;
        private readonly AppConfig appConfig;
        public DBMigrationService(OSSAppContext context, IOptions<AppConfig> options)
        {
            this.context = context;
            appConfig = options.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {

            //#if !DEBUG
            context.Database.Migrate();


            //#endif
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
