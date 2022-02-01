
using MediatRAbstraction;
using Order.Application.IntegrationEvents;
using Order.Domain.AggregateModels;
using System.Threading;
using System.Threading.Tasks;


namespace Order.Application.Features.Commands.CreateOrder
{

    public class CreateOrderCommandHandler : IDispatcherRequestHandler<CreateOrderCommand,bool>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderIntegrationEventService orderIntegrationEventService;
        public CreateOrderCommandHandler(IOrderRepository repository, IOrderIntegrationEventService ingIntegrationEventService)
        {
            orderRepository = repository;
            orderIntegrationEventService = ingIntegrationEventService;
        }

        public Task<bool> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var address = new Address(command.Street, command.City, command.State, command.Country, command.ZipCode);
            var order = new Order.Domain.AggregateModels.Order(command.UserId, command.UserName, address, command.CardTypeId, command.CardNumber, command.CardSecurityNumber, command.CardHolderName, command.CardExpiration);

            if (command.OrderItems != null)
            {
                foreach (var item in command.OrderItems)
                {
                    order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Discount, item.Units);
                }
            }
            //var orderStatusChangedToPaidIntegrationEvent = new OrderPaymentSucceededIntegrationEvent(
            //      123
            //      );

            //orderIntegrationEventService.AddAndSaveEventAsync(orderStatusChangedToPaidIntegrationEvent);
             orderRepository.AddAsync(order);
            return Task.FromResult(true);
        }
    }

   
}
