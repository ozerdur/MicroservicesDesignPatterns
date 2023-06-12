using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public interface IStockRollbackMessage
    {
        public List<OrderItemMessage> OrderItems { get; set; }
    }
}