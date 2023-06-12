using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Consumers;
using Order.API.Models;
using Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddMassTransit(x=>
{
    x.AddConsumer<OrderRequestCompletedEventConsumer>();
    x.AddConsumer<OrderRequestFailedEventConsumer>();
    x.UsingRabbitMq((context, cfg)=>{
        cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"));
        // cfg.Host("localhost", 5672, "/", h=> {
        //     h.Username("guest");
        //     h.Password("guest");    
        // });
        cfg.ReceiveEndpoint(RabbitMQSettingsConst.OrderRequestCompletedEventQueueName, e => 
            e.ConfigureConsumer<OrderRequestCompletedEventConsumer>(context));
        cfg.ReceiveEndpoint(RabbitMQSettingsConst.OrderRequestFailedEventQueueName, e => 
            e.ConfigureConsumer<OrderRequestFailedEventConsumer>(context));
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.Configure<MassTransitHostOptions>(options =>
{
    options.WaitUntilStarted = true;
    options.StartTimeout = TimeSpan.FromSeconds(30);
    options.StopTimeout = TimeSpan.FromMinutes(1);
});


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"))
);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
