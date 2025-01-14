using eShop.AppHost.Extensions;

var builder = DistributedApplication.CreateBuilder(args);

var defaultPassword = builder.AddParameter("password", "atpDWGvDb4jR5pE7rT59c7");
var defaultUser = builder.AddParameter("admin", "admin");

var redisCache = builder.AddRedis("redis", 40001)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume()
    .WithRedisInsight(containerName: "redis-insights");

var sqlServer = builder.AddSqlServer("mssql-server", port: 40002, password: defaultPassword)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume();

var mongo = builder.AddMongoDB("mongo", port: 40004, userName: defaultUser, password: defaultPassword)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume()
    .WithMongoExpress(cfg =>
    {
        cfg.WithAuthentication();
        cfg.WithMongoCredentials("admin", "atpDWGvDb4jR5pE7rT59c7");
        cfg.WithMongoServer("mongo");
        cfg.WithMongoUrl("mongodb://mongo:40004");
    }, "mongo-express");

var cartDb = mongo.AddDatabase("cart-db", "CartDB");
var authDb = sqlServer.AddDatabase("auth-db", "AuthDB");
var reviewsDb = sqlServer.AddDatabase("reviews-db", "ReviewsDB");
var productDb = sqlServer.AddDatabase("product-db", "ProductDB");

var rabbitMq = builder.AddRabbitMQ("rabbit-mq", port: 40003, userName: defaultUser, password: defaultPassword)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithManagementPlugin()
    .WithDataVolume();

var emailService = builder.AddProject<Projects.eShop_EmailSender_Api>("email-sender-api")
    .WithReference(rabbitMq);

var smsService = builder.AddProject<Projects.eShop_SmsSender_Api>("sms-service-api")
    .WaitForReference(rabbitMq);

var authApi = builder.AddProject<Projects.eShop_Auth_Api>("auth-api")
    .WaitForReference(sqlServer)
    .WaitForReference(emailService)
    .WaitForReference(smsService)
    .WaitForReference(redisCache);

var productApi = builder.AddProject<Projects.eShop_Product_Api>("product-api")
    .WaitForReference(authApi);

var reviewsApi = builder.AddProject<Projects.eShop_Reviews_Api>("reviews-api")
    .WaitForReference(authApi);

var cartApi = builder.AddProject<Projects.eShop_Cart_Api>("cart-api")
    .WaitForReference(authApi);

var filesStorageApi = builder.AddProject<Projects.eShop_FilesStorage_Api>("file-store-api")
    .WaitForReference(authApi);

var gateway = builder.AddProject<Projects.eShop_Gateway>("gateway");

var blazorClient = builder.AddProject<Projects.eShop_BlazorWebUI>("blazor-webui")
    .WaitForReference(gateway)
    .WaitForReference(authApi);

var angularClient = builder.AddNpmApp("angular-webui",
        "../eShop.AngularWebUI")
    .WaitForReference(gateway)
    .WaitForReference(authApi)
    .WithHttpEndpoint(port: 40502, targetPort: 4200, env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();