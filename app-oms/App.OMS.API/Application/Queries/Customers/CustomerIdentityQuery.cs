using App.Base.Domain.Extentions;
using App.OMS.Domain.AggregateModels.CustomerAggregate;
using MediatR;

namespace App.OMS.API.Application.Queries.Customers
{
    public class CustomerIdentityQuery : IRequest<CustomerIdentityQueryDTO>
    {
        public string Id { get; set; }
        public CustomerIdentityQuery(string id)
        {
            Id = id;
        }
    }

    public class CustomerIdentityQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public long CreatedTime { get; set; }
        public long ModifiedTime { get; set; }

        public static CustomerIdentityQueryDTO From(Customer data)
        {
            return new CustomerIdentityQueryDTO
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                Company = data.Company,
                Mail = data.Mail,
                Phone = data.Phone,
                Address = data.Address,
                CreatedTime = data.CreatedTime.ToUnixTimeSeconds(),
                ModifiedTime = data.ModifiedTime.ToUnixTimeSeconds()
            };
        }
    }
}
