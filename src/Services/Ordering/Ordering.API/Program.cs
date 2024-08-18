var builder = WebApplication.CreateBuilder(args);

// builder.Services
//     .AddpApplicationServices()
//     .AddInfrastructureServices(builder.Configuration)
//     .AddWebServices();

var app = builder.Build();

app.Run();
