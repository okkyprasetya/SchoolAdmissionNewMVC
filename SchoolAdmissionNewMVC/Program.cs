using SchoolAdmission.BLL;
using SchoolAdmission.BLL.Interfaces;
using SchoolAdmission.DAL.Interfaces;
using SchoolAdmission.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
}
);

builder.Services.AddScoped<IAdministrator, AdministratorBLL>();
builder.Services.AddScoped<IVerificator, VerificatorBLL>();

builder.Services.AddScoped<IAdministratorDAL, AdministratorDAL>();
builder.Services.AddScoped<IVerificatorsDAL, VerificatorDAL>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
