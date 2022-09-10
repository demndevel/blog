using Blog;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllersWithViews();
builder.Services.AddLogging(loggingBuilder => {
    loggingBuilder.AddFile("app.log", append:true);
});
builder.Services.Configure<TokenConfig>(builder.Configuration.GetSection("AdminToken"));
builder.Services.Configure<HomeConfig>(builder.Configuration.GetSection("Home"));
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));
builder.Services.AddMvc().AddXmlDataContractSerializerFormatters();
builder.Services.AddMvc().AddXmlSerializerFormatters();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");
app.MapControllerRoute(
    name: "rss",
    pattern: "/rss", new {controller = "Rss", action = "Rss"});
app.MapControllerRoute(
    name: "blog",
    pattern: "/blog", new {controller = "Notes", action = "BlogByPage", page = 1});
app.MapControllerRoute(
    name: "blog",
    pattern: "/blog/{page}", new {controller = "Notes", action = "BlogByPage"});
app.MapControllerRoute(
    name: "projects",
    pattern: "/projects", new {controller = "Projects", action = "Projects"});
app.MapControllerRoute(
    name: "tags",
    pattern: "/tags", new {controller = "Tags", action = "Tags"});
app.MapControllerRoute(
    name: "tag",
    pattern: "/tag/{id}", new {controller = "Tags", action = "Tag"});
app.MapControllerRoute(
    name: "note",
    pattern: "/note/{id}", new {controller = "Notes", action = "Note"});

app.Run();

public class TokenConfig
{
    public string Token { get; set; } = "";
}
public class HomeConfig
{
    public string Text { get; set; } = "";
}