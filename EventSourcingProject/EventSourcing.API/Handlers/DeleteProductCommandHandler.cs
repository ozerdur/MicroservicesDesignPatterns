using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using EventSourcing.API.Commands;
using EventSourcing.API.EventStores;

namespace EventSourcing.API.Handlers
{
    public class DeleteProductCommandHandler:IRequestHandler<DeleteProductCommand>
    {
        
        private readonly ProductStream _productStream;

        public DeleteProductCommandHandler(ProductStream productStream)
        {
            _productStream = productStream;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken){
            _productStream.Deleted(request.Id);
            await _productStream.SaveAsync();
            return Unit.Value;
        }
    }
}