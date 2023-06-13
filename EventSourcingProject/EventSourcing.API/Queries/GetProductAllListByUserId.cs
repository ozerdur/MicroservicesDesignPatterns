using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using EventSourcing.API.DTOs;

namespace EventSourcing.API.Queries
{
    public class GetProductAllListByUserId:IRequest<List<ProductDto>>
    {
        public int UserId { get; set; }
    }
}