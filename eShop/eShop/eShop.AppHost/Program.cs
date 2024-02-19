var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServerContainer("eShopSqlServer", "Password_2024", 8500)
    .AddDatabase("AuthDB")
    .AddDatabase("ProductDB");

var authApi = builder.AddProject<Projects.eShop_AuthWebApi>("eShopAuthWebApi")
    .WithReference(sqlServer);

var productApi = builder.AddProject<Projects.eShop_ProductWebApi>("eShopProductWebApi")
    .WithReference(sqlServer)
    .WithReference(authApi);

builder.AddProject<Projects.eShop_BlazorWebUI>("eShopBlazorWebUI")
    .WithReference(authApi);

builder.Build().Run();
