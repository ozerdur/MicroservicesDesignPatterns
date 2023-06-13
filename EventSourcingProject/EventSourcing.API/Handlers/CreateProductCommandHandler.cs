using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using EventSourcing.API.Commands;
using EventSourcing.API.EventStores;



namespace EventSourcing.API.Handlers
{
    public class CreateProductCommandHandler:IRequestHandler<CreateProductCommand>
    {
        private readonly ProductStream _productStream;

        public CreateProductCommandHandler(ProductStream productStream)
        {
            _productStream = productStream;
        }
        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken){
            _productStream.Created(request.CreateProductDto);
            await _productStream.SaveAsync();
            return Unit.Value;
        }
    }
}