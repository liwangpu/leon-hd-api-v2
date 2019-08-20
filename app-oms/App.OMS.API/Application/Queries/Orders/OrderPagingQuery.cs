using App.Base.API.Application.Queries;
using App.Base.Domain.Extentions;
using App.OMS.Domain.AggregateModels.OrderAggregate;
using MediatR;

namespace App.OMS.API.Application.Queries.Orders
{
    public class OrderPagingQuery : PagingQueryRequest, IRequest<PagingQueryResult<OrderPagingQueryDTO>>
    {

    }

    public class OrderPagingQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
        public string Modifier { get; set; }
        public long CreatedTime { get; set; }
        public long ModifiedTime { get; set; }
        public string OrderNo { get; set; }
        public int TotalNum { get; set; }
        public decimal TotalPrice { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactMail { get; set; }
        public string ShippingAddress { get; set; }

        public static OrderPagingQueryDTO From(Order data)
        {
            return new OrderPagingQueryDTO
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                Creator = data.Creator,
                Modifier = data.Modifier,
                CreatedTime = data.CreatedTime.ToUnixTimeSeconds(),
                ModifiedTime = data.ModifiedTime.ToUnixTimeSeconds(),
                OrderNo = data.OrderNo,
                TotalNum = data.TotalNum,
                TotalPrice = data.TotalPrice,
                ContactName = data.ContactName,
                ContactPhone = data.ContactPhone,
                ContactMail = data.ContactMail,
                ShippingAddress = data.ShippingAddress
            };
        }
    }
}
