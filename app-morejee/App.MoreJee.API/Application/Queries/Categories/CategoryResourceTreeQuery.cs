using MediatR;

namespace App.MoreJee.API.Application.Queries.Categories
{
    public class CategoryResourceTreeQuery : IRequest<string>
    {
        public string Resource { get; set; }

    }
}
