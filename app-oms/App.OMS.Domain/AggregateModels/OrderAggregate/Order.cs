using App.Base.Domain.Common;
using App.OMS.Domain.AggregateModels.CustomerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.OMS.Domain.AggregateModels.OrderAggregate
{
    public class Order : Entity
    {
        private readonly List<OrderItem> _ownOrderItems;
        public IReadOnlyCollection<OrderItem> OwnOrderItems => _ownOrderItems;
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Creator { get; protected set; }
        public string Modifier { get; protected set; }
        public DateTime CreatedTime { get; protected set; }
        public DateTime ModifiedTime { get; protected set; }
        public string OrganizationId { get; protected set; }
        public string OrderNo { get; protected set; }
        public int TotalNum { get; protected set; }
        public decimal TotalPrice { get; protected set; }
        public string CustomerId { get; protected set; }
        public Customer Customer { get; protected set; }
        public string ContactName { get; protected set; }
        public string ContactPhone { get; protected set; }
        public string ContactMail { get; protected set; }
        public string ShippingAddress { get; protected set; }

        #region ctor
        protected Order()
        {
            _ownOrderItems = new List<OrderItem>();
        }

        public Order(string name, string description, string organizationId, string creator)
            : this()
        {
            Id = GuidGen.NewGUID();
            Name = name;
            Description = description;
            OrganizationId = organizationId;
            Creator = creator;
            Modifier = Creator;
            CreatedTime = DateTime.UtcNow;
            ModifiedTime = CreatedTime;
        }
        #endregion

        public void OrderItemSummary()
        {
            TotalNum = _ownOrderItems.Sum(x => x.Num);
            TotalPrice = _ownOrderItems.Sum(x => x.TotalPrice);
        }

        public void SetOrderNo(string orderNo)
        {
            OrderNo = orderNo;
        }

        public void UpdateContactInfo(string customerId, string contactName, string contactPhone, string contactMail, string shippingAddress)
        {
            CustomerId = customerId;
            ContactName = contactName;
            ContactPhone = contactPhone;
            ContactMail = contactMail;
            CustomerId = customerId;
            ShippingAddress = shippingAddress;
        }

        public void AddItem(string productId, string productName, string productDes, string productIcon, string productBrand, string productUnit, string productSpecId, string productSpecName, string productSpecDes, string productSpecIcon, int num, decimal unitPrice, string remark)
        {
            var exist = _ownOrderItems.Any(x => x.ProductSpecId == productSpecId);
            if (!exist)
            {
                var item = new OrderItem(productId, productName, productDes, productIcon, productBrand, productUnit, productSpecId, productSpecName, productSpecDes, productSpecIcon, num, unitPrice, remark);
                _ownOrderItems.Add(item);
            }
            else
            {
                for (int idx = _ownOrderItems.Count - 1; idx >= 0; idx--)
                {
                    var item = _ownOrderItems[idx];
                    if (item.ProductSpecId == productSpecId)
                    {
                        item.ChangeNum(item.Num + num);
                        break;
                    }
                }
            }
        }
    }
}
