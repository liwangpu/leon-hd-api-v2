using App.MoreJee.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Infrastructure.Services
{
    public class DBMigrationService : IHostedService
    {
        private readonly MoreJeeAppContext context;
        private readonly AppConfig appConfig;
        public DBMigrationService(MoreJeeAppContext context, IOptions<AppConfig> options)
        {
            this.context = context;
            appConfig = options.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
//# if !DEBUG
            context.Database.Migrate();
//#endif




        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
