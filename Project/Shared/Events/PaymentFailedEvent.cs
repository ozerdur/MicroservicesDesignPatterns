using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Interfaces;

namespace Shared
{
    public class PaymentFailedEvent : IPaymentFailedEvent
    {

        public PaymentFailedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        
        public string BuyerId { get; set; }

        public List<OrderItemMessage> OrderItems {get; set; }
        public string Reason { get; set; }

        public Guid CorrelationId  { get; }
    }
}