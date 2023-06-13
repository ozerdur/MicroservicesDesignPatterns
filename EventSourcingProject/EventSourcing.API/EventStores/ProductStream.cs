using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using EventSourcing.Shared.Events;
using EventSourcing.API.DTOs;

namespace EventSourcing.API.EventStores
{
    public class ProductStream : AbstractStream
    {
        public static string StreamName => "ProductStream";
        public static string GroupName => "agroup";

        public ProductStream(IEventStoreConnection eventStoreConnection): base(StreamName, eventStoreConnection)
        {
            
        }

        public void Created(CreateProductDto createProductDto){
            Events.AddLast(new ProductCreatedEvent {
                Id= Guid.NewGuid(), Name=createProductDto.Name,Price= createProductDto.Price, Stock = createProductDto.Stock, UserId= createProductDto.UserId
                });
            
        }

        public void NameChanged(ChangeProductNameDto changeProductNameDto){
            Events.AddLast( new ProductNameChangedEvent{
                Id=changeProductNameDto.Id ,
                ChangedName = changeProductNameDto.Name
            });
        }
        public void PriceChanged(ChangeProductPriceDto changeProductPriceDto){
            Events.AddLast( new ProductPriceChangedEvent{
                Id=changeProductPriceDto.Id ,
                ChangedPrice = changeProductPriceDto.Price
            });
        }

        
        public void Deleted(Guid id){
            Events.AddLast( new ProductDeletedEvent{
                Id=id ,
            });
        }

    }
}