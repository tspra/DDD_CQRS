using Order.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.AggregateModels
{
    public class OrderItem
    {
      
        private string productName;
        private decimal unitPrice;
        private decimal discount;
        private int units;
        public int ProductId { get; private set; }

        protected OrderItem() { }

        public OrderItem(int productId, string name, decimal price, decimal offer,  int totalUnits = 1)
        {
            if (units <= 0)
            {
                throw new OrderingDomainException("Invalid number of units");
            }

            if ((unitPrice * units) < discount)
            {
                throw new OrderingDomainException("The total of order item is lower than applied discount");
            }

            ProductId = productId;
            productName = name;
            unitPrice = price;
            discount = offer;
            units = totalUnits;
            
        }
        public void SetNewDiscount(decimal discount)
        {
            if (discount < 0)
            {
                throw new OrderingDomainException("Discount is not valid");
            }

            discount = discount;
        }

        public void AddUnits(int units)
        {
            if (units < 0)
            {
                throw new OrderingDomainException("Invalid units");
            }

            units += units;
        }
        public decimal GetCurrentDiscount()
        {
            return discount;
        }

        public int GetUnits()
        {
            return units;
        }

        public decimal GetUnitPrice()
        {
            return unitPrice;
        }

    }
}
