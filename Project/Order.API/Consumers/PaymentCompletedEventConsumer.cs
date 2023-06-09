using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Order.API.Models;
using Shared;

namespace Order.API.Consumers
{
    public class PaymentCompletedEventConsumer : IConsumer<PaymentCompletedEvent>
    {
        private readonly AppDbContext _context;
        private ILogger<PaymentCompletedEventConsumer> _logger;
        public PaymentCompletedEventConsumer(AppDbContext context, ILogger<PaymentCompletedEventConsumer> logger)
        {
            _logger = logger;
            _context = context;
        }
        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            var order = await _context.Orders.FindAsync(context.Message.orderId);

            if(order != null){
                order.Status = OrderStatus.Completed;
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Order (Id={context.Message.orderId}) status changed to {order.Status}");
            }
            else{
                _logger.LogError($"Order (Id={context.Message.orderId}) not found");
            }
        }
    }
}