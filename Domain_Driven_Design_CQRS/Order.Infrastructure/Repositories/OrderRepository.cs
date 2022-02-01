using MediatRAbstraction;
using Order.Domain.AggregateModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        // Here we can use either EF or any no SQl databases container refernces.
        // For demo purpose we are using a colelction
        private List<Domain.AggregateModels.Order> orders = new List<Domain.AggregateModels.Order>();
        private readonly IDispatcher dispatcher;
        public OrderRepository(IDispatcher handler)
        {
            dispatcher = handler;
        }

        public Task<Domain.AggregateModels.Order> AddAsync(Domain.AggregateModels.Order entity)
        {
            orders.Add(entity);

            // The following code should move to context classes, once we save into DB , we will execute the domain events
            foreach (var item in entity.DomainEvents)
            {
          
                dispatcher.Publish(item);
            }
            return Task.FromResult(entity);
        }

        public Task DeleteAsync(Domain.AggregateModels.Order entity)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.AggregateModels.Order> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Domain.AggregateModels.Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
