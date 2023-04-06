using Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ApplicationContext = Application.Persistence.ApplicationContext;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DefaultConnection") 
                         ?? throw new Exception("Unable to parse DbConnection configuration");
        services.AddDbContext<ApplicationContext, Application.Persistence.ApplicationContext>(options => options.UseSqlite(connection));
        
        
        return services;
    }
    
    public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
    {
        var connection = connectionString;
        services.AddDbContext<ApplicationContext, Application.Persistence.ApplicationContext>(options => options.UseSqlite(connection));

        return services;
    }
}