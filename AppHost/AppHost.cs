var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ERPBlazorApp>("webfrontend");

builder.Build().Run();
