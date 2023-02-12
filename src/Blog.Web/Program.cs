using Application;
using Web.Configs;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddPersistence(builder.Configuration);
}

#region SERVICES

builder.Services.AddControllersWithViews();
builder.Services.AddLogging(loggingBuilder => {
    loggingBuilder.AddFile("app.log", append:true);
});
builder.Services.Configure<AdminPasswordConfig>(builder.Configuration.GetSection("Admin"));
builder.Services.Configure<HomeConfig>(builder.Configuration.GetSection("Home"));
builder.Services.AddMvc().AddXmlDataContractSerializerFormatters();
builder.Services.AddMvc().AddXmlSerializerFormatters();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/admin/login");
builder.Services.AddOptions();

builder.Services.Configure<AdminPasswordConfig>(options =>
{
    options.Password = builder.Configuration.GetSection("Admin").GetSection("Password").Value 
                    ?? throw new Exception("AdminPassword not found in appsettings.json");
});

#endregion

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
