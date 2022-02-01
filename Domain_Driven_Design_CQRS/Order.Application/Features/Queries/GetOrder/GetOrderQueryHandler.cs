using MediatR;
using MediatRAbstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Features.Queries.GetOrder
{
    public class GetOrderQueryHandler : IDispatcherRequestHandler<GetOrderQuery, List<Order.Domain.AggregateModels.Order>>
    {
        public Task<List<Domain.AggregateModels.Order>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
