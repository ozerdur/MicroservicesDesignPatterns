using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using EventStore.ClientAPI;
using System.Text;
using EventSourcing.API.EventStores;
using System.Text.Json;
using System.Text.Json.Serialization;
using EventSourcing.Shared.Events;
using EventSourcing.API.Models;

namespace EventSourcing.API.BackgroundServices
{
    public class ProductReadModelEventStore:BackgroundService
    {
        private readonly IEventStoreConnection _eventStoreConnection;
        private readonly ILogger<ProductReadModelEventStore> _logger;
        private readonly IServiceProvider _serviceProvider;

        
        public override Task StartAsync(CancellationToken cancellationToken){
            return base.StartAsync(cancellationToken);
        }

        
        public override Task StopAsync(CancellationToken cancellationToken){
            return base.StopAsync(cancellationToken);
            
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken){
            await _eventStoreConnection.ConnectToPersistentSubscriptionAsync(ProductStream.StreamName, ProductStream.GroupName, EventAppeared, autoAck: false);
            // true: If EventAppeared successfully finished then ack
        }

        private async Task EventAppeared(EventStorePersistentSubscriptionBase arg1, ResolvedEvent arg2){
            var type = Type.GetType($"{Encoding.UTF8.GetString(arg2.Event.Metadata)}, EventSourcing.Shared");
            _logger.LogInformation($"The Message processing ...: {type.ToString()}");
            var eventData = Encoding.UTF8.GetString(arg2.Event.Data);

            var @event = JsonSerializer.Deserialize(eventData, type);

            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            Product product = null;

            switch(@event){
                case ProductCreatedEvent productCreatedEvent:
                    product = new Product(){
                        Name = productCreatedEvent.Name,
                        Id = productCreatedEvent.Id,
                        Price = productCreatedEvent.Price,
                        Stock = productCreatedEvent.Stock,
                        UserId = productCreatedEvent.UserId
                    };
                    context.Products.Add(product);
                    break;

                case ProductNameChangedEvent productNameChangedEvent:
                    product = context.Products.Find(productNameChangedEvent.Id);
                    if (product != null){
                        product.Name = productNameChangedEvent.ChangedName;
                    }
                    break;

                case ProductPriceChangedEvent productPriceChangedEvent:
                    product = context.Products.Find(productPriceChangedEvent.Id);
                    if (product != null){
                        product.Price = productPriceChangedEvent.ChangedPrice;
                    }
                    break;

                
                case ProductDeletedEvent productDeletedEvent:
                    product = context.Products.Find(productDeletedEvent.Id);
                    if (product != null){
                        context.Products.Remove(product);
                    }
                    break;
            }
            await context.SaveChangesAsync();
            arg1.Acknowledge(arg2.Event.EventId); //Manually sending ack since set autoAck to false
        }
    }
}