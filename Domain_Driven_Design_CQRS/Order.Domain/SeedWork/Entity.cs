using MediatRAbstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.SeedWork
{
    public abstract class Entity
    {
        int _Id;
        public virtual int Id
        {
            get
            {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }

        private List<INotificationRequest> _domainEvents;
        public IReadOnlyCollection<INotificationRequest> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotificationRequest eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotificationRequest>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotificationRequest eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
