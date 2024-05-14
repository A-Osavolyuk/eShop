var builder = DistributedApplication.CreateBuilder(args);

var emailService = builder.AddProject<Projects.eShop_EmailSenderWebApi>("eshop-emailsenderwebapi");

var db = builder.AddSqlServer("eShopSqlServer", port: 29001).WithEnvironment("MSSQL_SA_PASSWORD", "Password_1234");

var authApi = builder.AddProject<Projects.eShop_AuthWebApi>("eshop-authwebapi")
    .WithReference(db)
    .WithReference(emailService);

builder.AddProject<Projects.eShop_ProductWebApi>("eshop-productwebapi")
    .WithReference(authApi);

builder.AddProject<Projects.eShop_BlazorWebUI>("eshop-blazorwebui")
    .WithReference(authApi);

builder.Build().Run();
