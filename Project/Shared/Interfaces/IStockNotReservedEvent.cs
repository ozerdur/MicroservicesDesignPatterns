using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;

namespace Shared.Interfaces
{
    public interface IStockNotReservedEvent: CorrelatedBy<Guid>
    {
        string Reason {get; set;}
    }
}