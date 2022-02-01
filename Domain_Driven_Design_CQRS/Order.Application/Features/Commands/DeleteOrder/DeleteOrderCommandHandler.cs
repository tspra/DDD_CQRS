using MediatR;
using MediatRAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Features.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IDispatcherRequestHandler<DeleteOrderCommand,Order.Domain.AggregateModels.Order>
    {
        public Task<Domain.AggregateModels.Order> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

      
    }
}
