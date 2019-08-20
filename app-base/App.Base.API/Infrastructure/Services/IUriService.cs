namespace App.Base.API.Infrastructure.Services
{
    public interface IUriService
    {
        string GetUri(bool toLowerCase = true);
        string GetUriWithoutQuery(bool toLowerCase = true);
    }
}
