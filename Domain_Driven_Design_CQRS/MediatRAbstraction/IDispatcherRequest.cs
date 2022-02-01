using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRAbstraction
{
    public interface IDispatcherRequest<T> : IRequest<T>
    {

    }
}
