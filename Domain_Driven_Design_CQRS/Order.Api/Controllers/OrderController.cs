using MediatR;
using MediatRAbstraction;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Features;
using Order.Application.Features.Commands.CreateOrder;
using Order.Application.Features.Commands.DeleteOrder;
using Order.Application.Features.Queries.GetOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : Controller
    {
        private readonly IDispatcher dispatcher;
       
        public OrderController(IDispatcher dispatcherInstance)
        {
            dispatcher = dispatcherInstance;
        }
  
        [HttpPost(Name = "CheckoutOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> CheckoutOrder([FromBody] CreateOrderCommand command)
        {
            await dispatcher.Send<bool>(command);
            return Ok();
        }
        [HttpDelete(Name = "DeleteOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteOrder([FromBody] DeleteOrderCommand command)
        {
            await dispatcher.Send<Order.Domain.AggregateModels.Order>(command);
            return Ok();
        }
        [HttpGet(Name = "GetOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> GetOrder(Guid orderId)
        {
            var query = new GetOrderQuery(orderId);
            await dispatcher.Send<List<Order.Domain.AggregateModels.Order>>(query);
            return Ok();
        }
    }
}
