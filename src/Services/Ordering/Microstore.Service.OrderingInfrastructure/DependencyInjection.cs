namespace Microstore.Service.OrderingInfrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database")!;

        //Add services to the container
        services.AddDbContext<ApplicationDbContext>
        (
            options =>
            {
                options.AddInterceptors(new AuditableEntityInterceptor());
                options.UseSqlServer(connectionString);
            }
        );

        //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}