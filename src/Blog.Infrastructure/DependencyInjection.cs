using Application.Interfaces.Persistence;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DefaultConnection") 
                         ?? throw new Exception("Unable to parse DbConnection configuration");
        services.AddDbContext<IApplicationContext, ApplicationContext>(options => options.UseSqlite(connection));
        
        
        return services;
    }
    
    public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
    {
        var connection = connectionString;
        services.AddDbContext<IApplicationContext, ApplicationContext>(options => options.UseSqlite(connection));

        return services;
    }
}