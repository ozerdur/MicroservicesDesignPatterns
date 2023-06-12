using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Shared;
using Shared.Interfaces;

namespace Payment.API.Consumers
{
    public class StockReservedRequestPaymentConsumer : IConsumer<IStockReservedRequestPayment>
    {
        private ILogger<StockReservedRequestPaymentConsumer> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public StockReservedRequestPaymentConsumer(ILogger<StockReservedRequestPaymentConsumer> logger, IPublishEndpoint publishEndpoint )
        {
         _logger = logger;
         _publishEndpoint = publishEndpoint;     
        }

        public async Task  Consume(ConsumeContext<IStockReservedRequestPayment> context)
        {
            var balance = 3000m;
            if(balance > context.Message.Payment.TotalPrice){
                _logger.LogInformation($"{context.Message.Payment.TotalPrice} TL was withdrawn from credit card for user id ={ context.Message.BuyerId}");
                await _publishEndpoint.Publish(new PaymentCompletedEvent(context.Message.CorrelationId));
            }
            else{
                _logger.LogInformation($"{context.Message.Payment.TotalPrice} TL was NOT withdrawn from credit card for user id ={ context.Message.BuyerId}");
                await _publishEndpoint.Publish(new PaymentFailedEvent(context.Message.CorrelationId) { BuyerId = context.Message.BuyerId, OrderItems = context.Message.OrderItems, Reason="Not enough balance"});
            }
        }
    }
}