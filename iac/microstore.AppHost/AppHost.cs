using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<ParameterResource> postgresUser = builder.AddParameterFromConfiguration("POSTGRESUSER", "POSTGRESUSER", secret: true);
IResourceBuilder<ParameterResource> postgresPassword = builder.AddParameterFromConfiguration("POSTGRESPASSWORD", "POSTGRESPASSWORD", secret: true);

IResourceBuilder<PostgresServerResource> postgres = builder.AddPostgres("DbServer", postgresUser, postgresPassword)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithHostPort(5432)
    .WithDataVolume();

IResourceBuilder<PostgresDatabaseResource> catalogDatabase = postgres.AddDatabase("CatalogDatabase");
IResourceBuilder<PostgresDatabaseResource> basketDatabase = postgres.AddDatabase("BasketDatabase");

IResourceBuilder<RedisResource> redis = builder.AddRedis("RedisCache")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithHostPort(6379);

builder.AddProject<Projects.Microstore_Services_CatalogApi>("microstore-services-catalogapi")
    .WithReference(postgres)
    .WithReference(catalogDatabase)
    .WaitFor(catalogDatabase);

IResourceBuilder<ProjectResource> discountGrpc = builder.AddProject<Projects.Microstore_Service_DiscountGrpc>("microstore-service-discountgrpc")
    .WithEnvironment("ConnectionStrings:DiscountsDatabase", "Data Source=discounts.db")
    .WithEndpoint(port: 8080, targetPort: 9080, "grpc");

IResourceBuilder<ProjectResource> basketApi = builder.AddProject<Projects.Microstore_Service_BasketApi>("microstore-service-basketapi")
    .WithReference(redis)
    .WithReference(discountGrpc)
    .WithReference(basketDatabase)
    .WaitFor(basketDatabase);

//builder.AddProject<Projects.Microstore_Service_OrderingApi>("microstore-service-orderingapi");

builder.Build().Run();
