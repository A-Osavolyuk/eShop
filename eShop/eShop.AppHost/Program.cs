using eShop.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

#region DataLayer

var redisCache = builder.AddRedis("eShopRedis", 25101);

var sqlServer = builder.AddSqlServer("eShopSqlServer", port: 25201)
    .WithEnvironment(EnvironmentVariables.MssqlSaPassword, "Password_1234");

var authDb = sqlServer.AddDatabase("AuthDB");
var productDb = sqlServer.AddDatabase("ProductDB");
var reviewsDb = sqlServer.AddDatabase("ReviewsDB");

var mongoServer = builder.AddMongoDB("eShopMongo", 25202)
    .WithMongoExpress();

var cartDb = mongoServer.AddDatabase("CartDB");

#endregion

#region ServicesLayer

var rabbitMq = builder.AddRabbitMQ("eShopRabbitMQ", port: 25001)
    .WithManagementPlugin()
    .WithEnvironment(EnvironmentVariables.RabbitmqDefaultUser, "user")
    .WithEnvironment(EnvironmentVariables.RabbitmqDefaultPass, "b2ce482e-9678-43b9-82e3-3b5ec7148355");

var emailService = builder.AddProject<Projects.eShop_EmailSenderWebApi>("eshop-email-sender-webapi")
    .WithReference(rabbitMq);

var authApi = builder.AddProject<Projects.eShop_AuthWebApi>("eshop-auth-webapi")
    .WithReference(authDb)
    .WithReference(emailService);

var productApi = builder.AddProject<Projects.eShop_ProductWebApi>("eshop-product-webapi")
    .WithReference(authApi)
    .WithReference(productDb);

var reviewsApi = builder.AddProject<Projects.eShop_ReviewsWebApi>("eshop-reviews-webapi")
    .WithReference(authApi)
    .WithReference(reviewsDb);

var cartApi = builder.AddProject<Projects.eShop_CartWebApi>("eshop-cart-webapi")
    .WithReference(cartDb)
    .WithReference(authApi);

var gateway = builder.AddProject<Projects.eShop_Gateway>("eshop-gateway");

#endregion

#region PresentationLayer

var blazorClient = builder.AddProject<Projects.eShop_BlazorWebUI>("eshop-blazor-webui")
    .WithReference(authApi)
    .WithReference(gateway);

var angularClient = builder.AddNpmApp("eshop-angular-webui",
        "C:/Users/sasha/source/repos/A-Osavolyuk/eShop/eShop.AngularWebUI")
    .WithReference(authApi)
    .WithReference(gateway)
    .WithHttpEndpoint(port: 5103, targetPort: 4200, env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

#endregion

builder.Build().Run();