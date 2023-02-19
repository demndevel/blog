using Microsoft.AspNetCore.Authentication.Cookies;
using Web.Configs;

namespace Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();
        services.AddLogging(loggingBuilder => {
            loggingBuilder.AddFile("app.log", append:true);
        });
        services.Configure<AdminPasswordConfig>(configuration.GetSection("Admin"));
        services.Configure<HomeConfig>(configuration.GetSection("Home"));
        services.AddMvc().AddXmlDataContractSerializerFormatters();
        services.AddMvc().AddXmlSerializerFormatters();
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => options.LoginPath = "/admin/login");
        services.AddOptions();
        services.Configure<AdminPasswordConfig>(options =>
        {
            options.Password = configuration.GetSection("Admin").GetSection("Password").Value 
                               ?? throw new Exception("AdminPassword not found in appsettings.json");
        });
        
        return services;
    }
}