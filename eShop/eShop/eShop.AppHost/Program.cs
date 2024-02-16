var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServerContainer("eShop.SqlServer", "Password_2024", 8500)
    .AddDatabase("Auth_DB");

builder.AddProject<Projects.eShop_AuthWebApi>("eShop.AuthWebApi")
    .WithReference(sqlServer);

builder.Build().Run();
