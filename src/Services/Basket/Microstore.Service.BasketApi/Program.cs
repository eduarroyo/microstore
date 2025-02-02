var builder = WebApplication.CreateBuilder(args);

// Add revices to the container
builder.Services.AddCarter();
System.Reflection.Assembly assembly = typeof(Program).Assembly;
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});
builder.Services.AddOpenApi();

builder.Services
    .AddMarten(opts =>
    {
        opts.Connection(builder.Configuration.GetConnectionString("Database")!);
        opts.Schema.For<ShoppingCart>().Identity(sc => sc.UserName);
    })
    .UseLightweightSessions();

//if (builder.Environment.IsDevelopment())
//{
//    builder.Services.InitializeMartenWith<CatalogInitialData>();
//}

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(setup => setup.SwaggerEndpoint("/openapi/v1.json", "Basket API"));
}

app.Run();