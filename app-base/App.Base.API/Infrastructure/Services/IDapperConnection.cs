using System.Data;

namespace App.Base.API.Infrastructure.Services
{
    public interface IDapperConnection
    {
        IDbConnection Create();
    }
}
