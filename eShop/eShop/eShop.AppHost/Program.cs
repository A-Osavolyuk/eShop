var builder = DistributedApplication.CreateBuilder(args);

var azureStorage = builder.AddAzureStorage("AzureStorage")
    .AddBlobs("AvatarsStorage");

var sqlServer = builder.AddSqlServerContainer("SqlServer", "Password_2024", 8500)
    .AddDatabase("AuthDB")
    .AddDatabase("ProductDB");

var authApi = builder.AddProject<Projects.eShop_AuthWebApi>("AuthWebApi")
    .WithReference(sqlServer)

var productApi = builder.AddProject<Projects.eShop_ProductWebApi>("ProductWebApi")
    .WithReference(sqlServer)
    .WithReference(authApi);

builder.AddProject<Projects.eShop_BlazorWebUI>("BlazorWebUI")
    .WithReference(authApi);

builder.AddProject<Projects.eShop_EmailSenderWebApi>("eshop-emailsenderwebapi");

builder.Build().Run();
