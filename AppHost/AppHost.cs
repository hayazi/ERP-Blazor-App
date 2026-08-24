var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Session);

var redis = builder.AddRedis("redis")
    .WithLifetime(ContainerLifetime.Session);

var db = sql.AddDatabase("ERPBlazorDb");

builder.AddProject<Projects.ERPBlazorApp>("webfrontend")
    .WithReference(db)
    .WithReference(redis);

builder.Build().Run();
