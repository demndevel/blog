using Application;
using Web.Configs;
using Web.Repository.Implementations;
using Web.Repository.Interfaces;
using Web.Unit_of_work;
using Domain.Entities.Project;
using Infrastructure;

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
builder.Services.Configure<TokenConfig>(builder.Configuration.GetSection("AdminToken"));
builder.Services.Configure<HomeConfig>(builder.Configuration.GetSection("Home"));
builder.Services.AddMvc().AddXmlDataContractSerializerFormatters();
builder.Services.AddMvc().AddXmlSerializerFormatters();

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
