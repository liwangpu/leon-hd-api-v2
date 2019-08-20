using MediatR;

namespace App.Basic.API.Application.Queries.AccessPoints
{
    public class CheckExistAssetPointQuery : IRequest<bool>
    {
        public string UserId { get; set; }
        public string PointKey { get; set; }
    }
}
