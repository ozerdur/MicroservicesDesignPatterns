using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Order.API.Models;
using Shared;

namespace Order.API.Consumers
{
    public class StockNotReservedEventConsumer : IConsumer<StockNotReservedEvent>
    {
        private readonly AppDbContext _context;
        private ILogger<StockNotReservedEventConsumer> _logger;
        public StockNotReservedEventConsumer(AppDbContext context, ILogger<StockNotReservedEventConsumer> logger)
        {
            _logger = logger;
            _context = context;
        }
        public async Task Consume(ConsumeContext<StockNotReservedEvent> context)
        {
            var order = await _context.Orders.FindAsync(context.Message.OrderId);

            if(order != null){
                order.Status = OrderStatus.Fail;
                order.FailMessage = context.Message.Message;
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Order (Id={context.Message.OrderId}) status changed to {order.Status}");
            }
            else{
                _logger.LogError($"Order (Id={context.Message.OrderId}) not found");
            }
        }
    }
}