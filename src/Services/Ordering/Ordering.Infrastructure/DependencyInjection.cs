using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        // services.AddDbContext<OrderContext>(options =>
        //     options.UseSqlServer(connectionString, sqlOptions =>
        //     {
        //         sqlOptions.MigrationsAssembly(typeof(OrderContext).Assembly.FullName);
        //     }));
        return services;
    }
}