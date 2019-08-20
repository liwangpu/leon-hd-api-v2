using MediatR;
using Newtonsoft.Json;

namespace App.OMS.API.Application.Commands.Orders
{
    public class OrderCustomerCreateCommand : IRequest
    {
        [JsonIgnore]
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
    }
}
