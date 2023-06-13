using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EventStore.ClientAPI;

namespace EventSourcing.API.EventStores
{
    public static class EventStoreExtensions
    {
        public static void AddEventStore(this IServiceCollection services, IConfiguration configuration){
            var connection = EventStoreConnection.Create(configuration.GetConnectionString("EventStore"));
            connection.ConnectAsync().Wait();
            services.AddSingleton(connection);

            using(var logFactory = LoggerFactory.Create(builder => { 
                builder.SetMinimumLevel(LogLevel.Information);
                builder.AddConsole();
            })){
                var logger = logFactory.CreateLogger("Startup");
                connection.Connected += (sender, args) =>
                {
                    logger.LogInformation("Eventstore connection established");
                };

                connection.ErrorOccurred += (sender, args) =>
                {
                    logger.LogError(args.Exception.Message);
                };
            }
        }
    }
}