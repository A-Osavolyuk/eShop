using eShop.AppHost.Extensions.Mongo;
using eShop.AppHost.Extensions.RabbitMq;
using eShop.AppHost.Extensions.Sql;

var builder = DistributedApplication.CreateBuilder(args);

#region DataLayer

var redisCache = builder.AddRedis("eShopRedis", 60001);

var sqlServer = builder.AddSqlServer("eShopSqlServer", port: 60002)
    .WithAuthentication("Password_1234");

var authDb = sqlServer.AddDatabase("AuthDB");
var reviewsDb = sqlServer.AddDatabase("ReviewsDB");
var productDb = sqlServer.AddDatabase("ProductDB");

var mongo = builder.AddMongoDB("eShopMongo")
    .WithAuthentication("admin", "Password_1234")
    .WithMongoExpress(cfg =>
    {
        cfg.WithAuthentication();
        cfg.WithMongoCredentials("admin", "Password_1234");
        cfg.WithMongoServer("eShopMongo");
        cfg.WithMongoUrl("mongodb://eShopMongo:27017");
    }, "eShopMongoExpress");

var cartDb = mongo.AddDatabase("CartDB");

#endregion

#region ServicesLayer

var rabbitMq = builder.AddRabbitMQ("eShopRabbitMQ", port: 60003)
    .WithAuthentication("user", "b2ce482e-9678-43b9-82e3-3b5ec7148355")
    .WithManagementPlugin();

var emailService = builder.AddProject<Projects.eShop_EmailSenderApi>("eshop-email-sender-api")
    .WithReference(rabbitMq);

var authApi = builder.AddProject<Projects.eShop_AuthApi>("eshop-auth-api")
    .WithReference(authDb)
    .WithReference(emailService);

var productApi = builder.AddProject<Projects.eShop_ProductApi>("eshop-product-api")
    .WithReference(authApi)
    .WithReference(productDb);

var reviewsApi = builder.AddProject<Projects.eShop_ReviewsApi>("eshop-reviews-api")
    .WithReference(authApi)
    .WithReference(reviewsDb);

var cartApi = builder.AddProject<Projects.eShop_CartApi>("eshop-cart-api")
    .WithReference(cartDb)
    .WithReference(authApi);

var filesStorageApi = builder.AddProject<Projects.eShop_FilesStorageApi>("eshop-file-store-api")
    .WithReference(authApi);

var gateway = builder.AddProject<Projects.eShop_Gateway>("eshop-gateway");

#endregion

#region PresentationLayer

var blazorClient = builder.AddProject<Projects.eShop_BlazorWebUI>("eshop-blazor-webui")
    .WithReference(authApi)
    .WithReference(gateway);

var angularClient = builder.AddNpmApp("eshop-angular-webui",
        "../eShop.AngularWebUI")
    .WithReference(authApi)
    .WithReference(gateway)
    .WithHttpEndpoint(port: 60502, targetPort: 4200, env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

#endregion

builder.Build().Run();