using SchoolAdmission.BLL;
using SchoolAdmission.BLL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

builder.Services.AddScoped<IAdministrator, AdministratorBLL>();
builder.Services.AddScoped<IVerificator, VerificatorBLL>();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
