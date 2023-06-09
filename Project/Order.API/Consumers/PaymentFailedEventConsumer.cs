using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Order.API.Models;
using Shared;

namespace Order.API.Consumers
{
    public class PaymentFailedEventConsumer : IConsumer<PaymentFailedEvent>
    {
        private readonly AppDbContext _context;
        private ILogger<PaymentFailedEventConsumer> _logger;
        public PaymentFailedEventConsumer(AppDbContext context, ILogger<PaymentFailedEventConsumer> logger)
        {
            _logger = logger;
            _context = context;
        }
        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            var order = await _context.Orders.FindAsync(context.Message.orderId);

            if(order != null){
                order.Status = OrderStatus.Fail;
                order.FailMessage = context.Message.Message;
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Order (Id={context.Message.orderId}) status changed to {order.Status}");
            }
            else{
                _logger.LogError($"Order (Id={context.Message.orderId}) not found");
            }
        }
    }
}