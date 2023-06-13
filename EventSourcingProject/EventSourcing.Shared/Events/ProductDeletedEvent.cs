using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcing.Shared.Events
{
    public class ProductDeletedEvent:IEvent
    {
        public Guid Id { get; set; }
    }
}