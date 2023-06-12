using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface IOrderRequestFailedEvent
    {
        public int OrderId { get; set; }
        public string Reason {get; set;}
    }
}