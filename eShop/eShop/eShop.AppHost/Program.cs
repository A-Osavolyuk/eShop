var builder = DistributedApplication.CreateBuilder(args);

var azureStorage = builder.AddAzureStorage("AzureStorage")
    .AddBlobs("AvatarsStorage");

var authApi = builder.AddProject<Projects.eShop_AuthWebApi>("AuthWebApi");

var productApi = builder.AddProject<Projects.eShop_ProductWebApi>("ProductWebApi")
    .WithReference(authApi);

builder.AddProject<Projects.eShop_BlazorWebUI>("BlazorWebUI")
    .WithReference(authApi);

builder.AddProject<Projects.eShop_EmailSenderWebApi>("eshop-emailsenderwebapi");

builder.Build().Run();
