using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Infrastructure.Data;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // services.AddControllers();wa
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        return app;
    }
}