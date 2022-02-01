using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRAbstraction
{
    public interface IDispatcherRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IDispatcherRequest<TResponse>
    {
    }
}
