using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Stock.API.Models;
using Shared;
using Microsoft.EntityFrameworkCore;

namespace Stock.API.Consumers
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
                foreach(var item in context.Message.orderItems){
                    var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId);
                    if(stock != null){
                        stock.Count += item.Count;
                    }

                    await _context.SaveChangesAsync();
                }

                _logger.LogInformation($"Stock was released for Buyer Id({context.Message.orderId})");

        }
    }
}