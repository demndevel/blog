using Blog;
using Blog.Configs;
using Blog.Models;
using Blog.Repository.Implementations;
using Blog.Repository.Interfaces;
using Blog.Unit_of_work;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

#region SERVICES

builder.Services.AddControllersWithViews();
builder.Services.AddLogging(loggingBuilder => {
    loggingBuilder.AddFile("app.log", append:true);
});
builder.Services.Configure<TokenConfig>(builder.Configuration.GetSection("AdminToken"));
builder.Services.Configure<HomeConfig>(builder.Configuration.GetSection("Home"));
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));
builder.Services.AddMvc().AddXmlDataContractSerializerFormatters();
builder.Services.AddMvc().AddXmlSerializerFormatters();

builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IRepository<Project>, ProjectRepository>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();



#endregion

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
