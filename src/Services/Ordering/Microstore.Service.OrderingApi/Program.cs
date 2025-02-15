using Microstore.Service.OrderingApi;
using Microstore.Service.OrderingApplication;
using Microstore.Service.OrderingInfrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApiServices();

app.Run();
