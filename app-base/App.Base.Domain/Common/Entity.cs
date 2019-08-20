
using MediatR;
using System.Collections.Generic;

namespace App.Base.Domain.Common
{
    public abstract class Entity
    {
        public string Id { get; protected set; }
        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        /// <summary>
        /// 一般来说,id都是在构造函数里面定义或者自动生成
        /// 但是有些情况需要自己定义Id,可以使用该方法
        /// </summary>
        /// <param name="id"></param>
        public void CustomizeId(string id)
        {
            Id = id;
        }

        public bool IsTransient()
        {
            return !string.IsNullOrWhiteSpace(Id);
        }

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }


}
