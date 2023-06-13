using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using EventSourcing.API.DTOs;

namespace EventSourcing.API.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}