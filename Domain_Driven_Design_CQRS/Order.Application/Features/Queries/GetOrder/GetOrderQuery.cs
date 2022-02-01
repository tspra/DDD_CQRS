using MediatRAbstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Application.Features.Queries.GetOrder
{
   public  class GetOrderQuery : IDispatcherRequest<List<Order.Domain.AggregateModels.Order>>
    {
        public Guid OrderId { get; private set; }
        public GetOrderQuery(Guid id)
        {
            OrderId = id;
        }
    }
}
