using Blog;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllersWithViews();
builder.Services.AddLogging(loggingBuilder => {
    loggingBuilder.AddFile("app.log", append:true);
});
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");
app.MapControllerRoute(
    name: "rss",
    pattern: "/rss", new {controller = "Home", action = "Rss"});
app.MapControllerRoute(
    name: "blog",
    pattern: "/blog", new {controller = "Home", action = "Blog"});
app.MapControllerRoute(
    name: "blog",
    pattern: "/blog/{page}", new {controller = "Home", action = "Blog"});
app.MapControllerRoute(
    name: "projects",
    pattern: "/projects", new {controller = "Home", action = "Projects"});
app.MapControllerRoute(
    name: "tags",
    pattern: "/tags", new {controller = "Home", action = "Tags"});

app.Run();
