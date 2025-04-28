using Microsoft.EntityFrameworkCore;
using TeammateApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();

string connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/Login");

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Main",
    pattern: "Home/Index");

app.MapControllerRoute(
    name: "Profile",
    pattern: "Authorized/Profile");

app.Run();
