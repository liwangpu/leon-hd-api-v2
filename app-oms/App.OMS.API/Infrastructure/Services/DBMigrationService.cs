using App.OMS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace App.OMS.API.Infrastructure.Services
{
    public class DBMigrationService : IHostedService
    {
        private readonly OMSAppContext context;

        public DBMigrationService(OMSAppContext context)
        {
            this.context = context;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            context.Database.Migrate();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
