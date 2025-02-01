var builder = WebApplication.CreateBuilder(args);

// Add srevices to the container

var app = builder.Build();

// Configure the HTTP request pipeline

app.Run();