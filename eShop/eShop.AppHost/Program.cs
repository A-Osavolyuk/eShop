var builder = DistributedApplication.CreateBuilder(args);

var rabbitMQ = builder.AddRabbitMQ("eShopRabbitMQ", port: 25001)
    .WithManagementPlugin()
    .WithEnvironment("RABBITMQ_DEFAULT_USER", "user")
    .WithEnvironment("RABBITMQ_DEFAULT_PASS", "b2ce482e-9678-43b9-82e3-3b5ec7148355");

var redisCache = builder.AddRedis("eShopRedis", 25101);

var emailService = builder.AddProject<Projects.eShop_EmailSenderWebApi>("eshop-emailsenderwebapi")
    .WithReference(rabbitMQ);

var db = builder.AddSqlServer("eShopSqlServer", port: 25201).WithEnvironment("MSSQL_SA_PASSWORD", "Password_1234");

var authApi = builder.AddProject<Projects.eShop_AuthWebApi>("eshop-authwebapi")
    .WithReference(db)
    .WithReference(emailService);

builder.AddProject<Projects.eShop_ProductWebApi>("eshop-productwebapi")
    .WithReference(authApi);

builder.AddProject<Projects.eShop_BlazorWebUI>("eshop-blazorwebui")
    .WithReference(authApi);

builder.Build().Run();
