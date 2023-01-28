using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        AddQueries(services);
        AddCommands(services);
        
        return services;
    }

    private static void AddQueries(IServiceCollection services)
    {
        services.Scan(selector =>
        {
            selector.FromCallingAssembly()
                .AddClasses(filter => { filter.AssignableTo(typeof(IQueryHandler<,>)); })
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });
    }
    
    private static void AddCommands(IServiceCollection services)
    {
        services.Scan(selector =>
        {
            selector.FromCallingAssembly()
                .AddClasses(filter => { filter.AssignableTo(typeof(ICommandHandler<,>)); })
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });
    }
}