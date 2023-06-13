using EventSourcing.API.EventStores;
using MediatR;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using EventSourcing.API.Models;
using EventSourcing.API.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"))
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEventStore(builder.Configuration);

builder.Services.AddSingleton<ProductStream>();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddHostedService<ProductReadModelEventStore>();
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
