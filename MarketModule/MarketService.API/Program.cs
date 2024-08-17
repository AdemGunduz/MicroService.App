using InventoryService.Data.Repositories;
using MarketService.Data.Contexts;
using MarketService.Data.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse("localhost:1453", true);
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddMassTransit( x =>
{
    x.UsingRabbitMq();
});

builder.Services.AddControllers();
builder.Services.AddDbContext<MarketContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Local"));
});
builder.Services.AddScoped<IMarketRepository, MarketRepository>();
builder.Services.AddScoped<ItemInventoryRepository>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
