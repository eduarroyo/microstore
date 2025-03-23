var builder = WebApplication.CreateBuilder(args);

// Add revices to the container
builder.Services.AddCarter();
System.Reflection.Assembly assembly = typeof(Program).Assembly;
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

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis")!;
});

// Grpc Services
builder.Services
    .AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
    {
        options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
    })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        return new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
    });

// Async Communication Services
builder.Services.AddMessageBroker(builder.Configuration);

// Cross-Cutting services
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(setup => setup.SwaggerEndpoint("/openapi/v1.json", "Basket API"));
}

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();