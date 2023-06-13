using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EventSourcing.API.DTOs;
using EventSourcing.API.Commands;
using EventSourcing.API.Queries;

namespace EventSourcing.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllListByUserId(int userId)
        {
            return Ok(await _mediator.Send(new GetProductAllListByUserId(){UserId =userId}));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto createProductDto){
            await _mediator.Send(new CreateProductCommand() {
                CreateProductDto = createProductDto
            } );
            return NoContent();
        }


        
        [HttpPut]
        public async Task<IActionResult> ChangeName(ChangeProductNameDto changeProductNameDto){
            await _mediator.Send(new ChangeProductNameCommand() {
                ChangeProductNameDto = changeProductNameDto
            } );
            return NoContent();
        }

        
        
        [HttpPut]
        public async Task<IActionResult> ChangePrice(ChangeProductPriceDto changeProductPriceDto){
            await _mediator.Send(new ChangeProductPriceCommand() {
                ChangeProductPriceDto = changeProductPriceDto
            } );
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id){
            await _mediator.Send(new DeleteProductCommand() {
                Id = id
            } );
            return NoContent();
        }
        

    }
}