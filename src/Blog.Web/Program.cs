using Web.Configs;
using Web.Repository.Implementations;
using Web.Repository.Interfaces;
using Web.Unit_of_work;
using Domain.Entities;
using Infrastructure.Persistence;
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
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));
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
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
