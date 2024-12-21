using eShop.AppHost.Extensions;

var builder = DistributedApplication.CreateBuilder(args);

var defaultPassword = builder.AddParameter("password", "atpDWGvDb4jR5pE7rT59c7");
var defaultUser = builder.AddParameter("admin", "admin");

var redisCache = builder.AddRedis("redis", 60001)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume()
    .WithRedisInsight(containerName: "redis-insights");

var sqlServer = builder.AddSqlServer("mssql-server", port: 60002, password: defaultPassword)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume();

var mongo = builder.AddMongoDB("mongo", port:60004,  userName: defaultUser, password: defaultPassword)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume()
    .WithMongoExpress(cfg =>
    {
        cfg.WithAuthentication();
        cfg.WithMongoCredentials("admin", "atpDWGvDb4jR5pE7rT59c7");
        cfg.WithMongoServer("mongo");
        cfg.WithMongoUrl("mongodb://mongo:60004");
    }, "mongo-express");

var cartDb = mongo.AddDatabase("cart-db", "CartDB");
var authDb = sqlServer.AddDatabase("auth-db", "AuthDB");
var reviewsDb = sqlServer.AddDatabase("reviews-db", "ReviewsDB");
var productDb = sqlServer.AddDatabase("product-db", "ProductDB");

var rabbitMq = builder.AddRabbitMQ("rabbit-mq", port: 60003, userName: defaultUser, password: defaultPassword)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithManagementPlugin()
    .WithDataVolume();

var emailService = builder.AddProject<Projects.eShop_EmailSenderApi>("email-sender-api")
    .WithReference(rabbitMq);

var smsService = builder.AddProject<Projects.eShop_SmsSenderApi>("sms-service-api")
    .WaitForReference(rabbitMq);

var authApi = builder.AddProject<Projects.eShop_AuthApi>("auth-api")
    .WaitForReference(sqlServer)
    .WaitForReference(emailService)
    .WaitForReference(smsService);

var productApi = builder.AddProject<Projects.eShop_ProductApi>("product-api")
    .WaitForReference(sqlServer)
    .WaitForReference(authApi);

var reviewsApi = builder.AddProject<Projects.eShop_ReviewsApi>("reviews-api")
    .WaitForReference(sqlServer)
    .WaitForReference(authApi);

var cartApi = builder.AddProject<Projects.eShop_CartApi>("cart-api")
    .WaitForReference(sqlServer)
    .WaitForReference(authApi);

var filesStorageApi = builder.AddProject<Projects.eShop_FilesStorageApi>("file-store-api")
    .WaitForReference(authApi);

var gateway = builder.AddProject<Projects.eShop_Gateway>("gateway");

var blazorClient = builder.AddProject<Projects.eShop_BlazorWebUI>("blazor-webui")
    .WaitForReference(gateway)
    .WaitForReference(authApi);

var angularClient = builder.AddNpmApp("angular-webui",
        "../eShop.AngularWebUI")
    .WaitForReference(gateway)
    .WaitForReference(authApi)
    .WithHttpEndpoint(port: 60502, targetPort: 4200, env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();