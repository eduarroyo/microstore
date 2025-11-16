var builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<ParameterResource> postgresUser = builder.AddParameterFromConfiguration("POSTGRESUSER", "POSTGRESUSER", secret: true);
IResourceBuilder<ParameterResource> postgresPassword = builder.AddParameterFromConfiguration("POSTGRESPASSWORD", "POSTGRESPASSWORD", secret: true);
IResourceBuilder<ParameterResource> rabbitUser = builder.AddParameterFromConfiguration("RABBITUSER", "RABBITUSER", secret: true);
IResourceBuilder<ParameterResource> rabbitPassword = builder.AddParameterFromConfiguration("RABBITPASSWORD", "RABBITPASSWORD", secret: true);

IResourceBuilder<PostgresServerResource> postgres = builder.AddPostgres("DbServer", postgresUser, postgresPassword)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithHostPort(5432)
    .WithDataVolume();

IResourceBuilder<PostgresDatabaseResource> catalogDatabase = postgres.AddDatabase("CatalogDatabase");
IResourceBuilder<PostgresDatabaseResource> basketDatabase = postgres.AddDatabase("BasketDatabase");

IResourceBuilder<SqlServerServerResource> sqlServer = builder.AddSqlServer("SqlServer")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithHostPort(1433)
    .WithDataVolume();

IResourceBuilder<SqlServerDatabaseResource> orderingDb = sqlServer.AddDatabase("OrderingDatabase");

IResourceBuilder<RabbitMQServerResource> rabbitMq = builder.AddRabbitMQ("RabbitMQ", rabbitUser, rabbitPassword)
    .WithManagementPlugin();

IResourceBuilder<RedisResource> redis = builder.AddRedis("RedisCache")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithHostPort(6379);

builder.AddProject<Projects.Microstore_Services_CatalogApi>("microstore-services-catalogapi")
    .WithReference(catalogDatabase)
    .WaitFor(catalogDatabase);

IResourceBuilder<ProjectResource> discountGrpc = builder.AddProject<Projects.Microstore_Service_DiscountGrpc>("microstore-service-discountgrpc")
    .WithEnvironment("ConnectionStrings:DiscountsDatabase", "Data Source=discounts.db")
    .WithEndpoint(port: 8080, targetPort: 9080, "grpc");

IResourceBuilder<ProjectResource> basketApi = builder.AddProject<Projects.Microstore_Service_BasketApi>("microstore-service-basketapi")
    .WithReference(redis)
    .WithReference(discountGrpc)
    .WithReference(rabbitMq)
    .WithReference(basketDatabase)
    .WaitFor(discountGrpc)
    .WaitFor(rabbitMq)
    .WaitFor(basketDatabase);

IResourceBuilder<ProjectResource> orderingApi = builder.AddProject<Projects.Microstore_Service_OrderingApi>("microstore-service-orderingapi")
    .WithReference(rabbitMq)
    .WithReference(orderingDb)
    .WaitFor(rabbitMq)
    .WaitFor(orderingDb);

IResourceBuilder<ProjectResource> apiGateway = builder.AddProject<Projects.Microstore_ApiGateways_YarpApiGateway>("microstore-apigateway")
    .WithReference(basketApi)
    .WithReference(orderingApi)
    .WithReference(catalogDatabase)
    .WaitFor(basketApi)
    .WaitFor(orderingApi)
    .WaitFor(catalogDatabase);

builder.Build().Run();
