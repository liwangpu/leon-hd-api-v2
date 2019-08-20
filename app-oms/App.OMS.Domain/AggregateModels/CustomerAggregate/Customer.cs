using App.Base.Domain.Common;
using App.OMS.Domain.AggregateModels.OrderAggregate;
using System;
using System.Collections.Generic;

namespace App.OMS.Domain.AggregateModels.CustomerAggregate
{
    public class Customer : Entity
    {
        private readonly List<Order> _ownOrders;
        public IReadOnlyCollection<Order> OwnOrders => _ownOrders;

        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Company { get; protected set; }
        public string Mail { get; protected set; }
        public string Phone { get; protected set; }
        public string Address { get; protected set; }
        public string Creator { get; protected set; }
        public string Modifier { get; protected set; }
        public DateTime CreatedTime { get; protected set; }
        public DateTime ModifiedTime { get; protected set; }
        public string OrganizationId { get; protected set; }


        #region ctor
        protected Customer()
        {
            _ownOrders = new List<Order>();
        }

        public Customer(string name, string description, string company, string phone, string mail, string address, string organizationId, string creator)
            : this()
        {
            Id = GuidGen.NewGUID();
            Name = name;
            Description = description;
            Company = company;
            Phone = phone;
            Mail = mail;
            Address = address;
            OrganizationId = organizationId;
            Creator = creator;
            Modifier = Creator;
            CreatedTime = DateTime.UtcNow;
            ModifiedTime = CreatedTime;
        }
        #endregion

        public void UpdateBasicInfo(string name, string description, string company, string phone, string mail, string address, string operatorId)
        {
            Name = name;
            Description = description;
            Company = company;
            Phone = phone;
            Mail = mail;
            Address = address;
            Modifier = operatorId;
            ModifiedTime = DateTime.UtcNow;
        }
    }
}
