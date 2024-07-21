var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.OpenTelemetryDemo_ApiService>("apiservice");

builder.AddProject<Projects.OpenTelemetryDemo_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();