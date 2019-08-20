using MediatR;

namespace App.MoreJee.API.Application.Commands.Solutions
{
    public class SolutionBatchDeleteCommand : IRequest
    {
        public string Ids { get; set; }
    }
}
