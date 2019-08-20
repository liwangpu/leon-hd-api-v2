using App.Base.Domain.Extentions;
using App.OMS.Domain.AggregateModels.OrderAggregate;
using MediatR;
using System.Collections.Generic;

namespace App.OMS.API.Application.Queries.Orders
{
    public class OrderIdentityQuery : IRequest<OrderIdentityQueryDTO>
    {
        public string Id { get; protected set; }
        public OrderIdentityQuery(string id)
        {
            Id = id;
        }
    }

    public class OrderIdentityQueryDTO
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

        public CustomerInfo Customer { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public static OrderIdentityQueryDTO From(Order data)
        {
            var dto = new OrderIdentityQueryDTO
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
                TotalPrice = data.TotalPrice
            };
            dto.Customer = new CustomerInfo()
            {
                Id = data.CustomerId,
                Name = data.ContactName,
                Phone = data.ContactPhone,
                Mail = data.ContactMail,
                Address = data.ShippingAddress
            };
            if (data.OwnOrderItems.Count > 0)
            {
                var list = new List<OrderItem>();
                foreach (var it in data.OwnOrderItems)
                {
                    list.Add(new OrderItem
                    {
                        Id = it.Id,
                        ProductId = it.ProductId,
                        ProductName = it.ProductName,
                        ProductIcon = it.ProductIcon,
                        ProductDescription = it.ProductDescription,
                        ProductBrand = it.ProductBrand,
                        ProductUnit = it.ProductUnit,
                        ProductSpecId = it.ProductSpecId,
                        ProductSpecName = it.ProductSpecName,
                        Icon = it.ProductSpecIcon,
                        ProductSpecDescription = it.ProductSpecDescription,
                        Num = it.Num,
                        UnitPrice = it.UnitPrice,
                        TotalPrice = it.TotalPrice,
                        Remark = it.Remark
                    });
                }
                dto.OrderItems = list;

            }
            return dto;
        }

        public class CustomerInfo
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Mail { get; set; }
            public string Address { get; set; }
        }

        public class OrderItem
        {
            public string Id { get; set; }
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductIcon { get; set; }
            public string ProductDescription { get; set; }
            public string ProductBrand { get; set; }
            public string ProductUnit { get; set; }
            public string ProductSpecId { get; set; }
            public string ProductSpecName { get; set; }
            public string Icon { get; set; }
            public string ProductSpecDescription { get; set; }
            public int Num { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal TotalPrice { get; set; }
            public string Remark { get; set; }
        }
    }
}
