using Order.Domain.Events;
using Order.Domain.Persistance;
using Order.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Order.Domain.AggregateModels
{
    public enum OrderStatus
    {
        Submitted,
        AwaitingValidation,
        StockConfirmed,
        Paid,
        Shipped,
        Cancelled
    }
    public class Order : Entity, IAggregateRoot
    {
        private DateTime orderDate;

        public Address Address { get; private set; }

        public OrderStatus OrderStatus { get; private set; }
      
       
        private readonly List<OrderItem> orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => orderItems;

        private int? paymentMethodId;

        protected Order()
        {
            orderItems = new List<OrderItem>();
  
        }

        public Order(string userId, string userName, Address address, int cardTypeId, string cardNumber, string cardSecurityNumber,
                string cardHolderName, DateTime cardExpiration, int? buyerId = null, int? paymentMethodId = null) : this()
        {

           paymentMethodId = paymentMethodId;
            orderDate = DateTime.UtcNow;
            Address = address;

            // Add the OrderStarterDomainEvent to the domain events collection 
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            AddDomainEvent(new OrderCreatedEvent(this));
                                      
        }
      
        public void AddOrderItem(int productId, string productName, decimal unitPrice, decimal discount, int units = 1)
        {
            var existingOrderForProduct = orderItems.Where(o => o.ProductId == productId)
                .SingleOrDefault();

            if (existingOrderForProduct != null)
            {
        
                if (discount > existingOrderForProduct.GetCurrentDiscount())
                {
                    existingOrderForProduct.SetNewDiscount(discount);
                }

                existingOrderForProduct.AddUnits(units);
            }
            else
            {
              
                var orderItem = new OrderItem(productId, productName, unitPrice, discount, units);
                orderItems.Add(orderItem);
            }
        }

    }
}
