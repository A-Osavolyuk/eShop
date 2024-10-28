var builder = DistributedApplication.CreateBuilder(args);

var rabbitMQ = builder.AddRabbitMQ("eShopRabbitMQ", port: 25001)
    .WithManagementPlugin()
    .WithEnvironment("RABBITMQ_DEFAULT_USER", "user")
    .WithEnvironment("RABBITMQ_DEFAULT_PASS", "b2ce482e-9678-43b9-82e3-3b5ec7148355");

var redisCache = builder.AddRedis("eShopRedis", 25101);

var emailService = builder.AddProject<Projects.eShop_EmailSenderWebApi>("eshop-emailsenderwebapi")
    .WithReference(rabbitMQ);

var sqlServer = builder.AddSqlServer("eShopSqlServer", port: 25201)
    .WithEnvironment("MSSQL_SA_PASSWORD", "Password_1234");

var cardDb = builder.AddMongoDB("eShopMongo", 25202)
    .WithMongoExpress()
    .AddDatabase("CartDB");

var authApi = builder.AddProject<Projects.eShop_AuthWebApi>("eshop-authwebapi")
    .WithReference(sqlServer)
    .WithReference(emailService);

builder.AddProject<Projects.eShop_ProductWebApi>("eshop-productwebapi")
    .WithReference(authApi)
    .WithReference(sqlServer);

builder.AddProject<Projects.eShop_BlazorWebUI>("eshop-blazorwebui")
    .WithReference(authApi);

builder.AddProject<Projects.eShop_ReviewsWebApi>("eshop-reviewswebapi")
    .WithReference(authApi)
    .WithReference(sqlServer);

builder.AddProject<Projects.eShop_CartWebApi>("eshop-cartwebapi")
    .WithReference(cardDb)
    .WithReference(authApi);

builder.AddProject<Projects.eShop_Gateway>("eshop-gateway");

builder.AddNpmApp("eshop-angularwebui", "C:/Users/sasha/source/repos/A-Osavolyuk/eShop/eShop.AngularWebUI")
    .WithReference(authApi)
    .WithHttpEndpoint(port: 5103, targetPort:4200, env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();
