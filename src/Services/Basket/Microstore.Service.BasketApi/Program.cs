var builder = WebApplication.CreateBuilder(args);

// Add srevices to the container
builder.Services.AddCarter();
System.Reflection.Assembly assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(setup => setup.SwaggerEndpoint("/openapi/v1.json", "Basket API"));
}

app.Run();