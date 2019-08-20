namespace App.Base.API.Infrastructure.Services
{
    public interface IIdentityService
    {
        string GetUserId();

        string GetUserName();

        string GetUserRole();

        string GetOrganizationId();

        string GetOrganizationTypeId();

        string GetToken();
    }
}
