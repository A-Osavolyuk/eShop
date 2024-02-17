var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServerContainer("eShopSqlServer", "Password_2024", 8500)
    .AddDatabase("AuthDB");

builder.AddProject<Projects.eShop_AuthWebApi>("eShopAuthWebApi")
    .WithReference(sqlServer);

builder.Build().Run();
