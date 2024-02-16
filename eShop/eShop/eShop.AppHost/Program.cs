var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.eShop_AuthWebApi>("eshop.authwebapi");

builder.Build().Run();
