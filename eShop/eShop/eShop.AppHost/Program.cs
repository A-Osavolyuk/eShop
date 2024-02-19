var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServerContainer("eShopSqlServer", "Password_2024", 8500)
    .AddDatabase("AuthDB");

builder.AddProject<Projects.eShop_AuthWebApi>("eShopAuthWebApi")
    .WithReference(sqlServer);

builder.AddProject<Projects.eShop_BlazorWebUI>("eShopBlazorWebUI");

builder.AddProject<Projects.eShop_ProductWebApi>("eshop.productwebapi");

builder.Build().Run();
