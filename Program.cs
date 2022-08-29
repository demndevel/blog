var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

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
    name: "notes",
    pattern: "/notes", new {controller = "Home", action = "Notes"});
app.MapControllerRoute(
    name: "projects",
    pattern: "/projects", new {controller = "Home", action = "Projects"});
app.MapControllerRoute(
    name: "tags",
    pattern: "/tags", new {controller = "Home", action = "Tags"});

app.Run();
