using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace SagaStateMachineWorkerService.Models
{
    public class OrderStateDbContext : SagaDbContext
    {

        public OrderStateDbContext(DbContextOptions<OrderStateDbContext> options) : base(options)
        { 
        }
        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get{
                yield return new OrderStateMap();
            }
        }
    }
}