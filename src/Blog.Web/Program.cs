using Application;
using Infrastructure;
using Microsoft.AspNetCore.RateLimiting;
using Web;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddPersistence(builder.Configuration)
        .AddWeb(builder.Configuration);
}

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
app.UseRateLimiter(new RateLimiterOptions { RejectionStatusCode = 429 }
    .AddFixedWindowLimiter("Comments", options =>
    {
        options.Window = TimeSpan.FromSeconds(15);
        options.PermitLimit = 2;
    }));
app.MapControllers();

app.Run();