using Microstore.Service.OrderingApi;
using Microstore.Service.OrderingApplication;
using Microstore.Service.OrderingInfrastructure;
using Microstore.Service.OrderingInfrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApiServices();

if(app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
