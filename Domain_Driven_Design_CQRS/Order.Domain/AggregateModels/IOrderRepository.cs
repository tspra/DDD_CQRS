using Order.Domain.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.AggregateModels
{
    public interface IOrderRepository : IRepository<Order>
    {
    }
}
