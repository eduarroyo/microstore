﻿namespace Microstore.Service.OrderingApi;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddCarter();
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication application)
    {
        application.MapCarter();
        return application;
    }
}
