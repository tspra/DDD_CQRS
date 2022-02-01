using MediatR;
using MediatRAbstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Application.Features.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IDispatcherRequest<Domain.AggregateModels.Order>
    {
        
    }
}
