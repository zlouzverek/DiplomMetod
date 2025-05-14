using DiplomMetod.Data;
using DiplomMetod.Data.Entites;
using DiplomMetod.Data.Identity;
using DiplomMetod.Repositories;
using DiplomMetod.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//   options.UseSqlite("Data Source=Local.db"));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IFormRepository, FormRepository>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IReferenceBookRepository, ReferenceBookRepository>();
builder.Services.AddScoped<IRegionDivisionRepository, RegionDivisionRepository>();
builder.Services.AddScoped<IFormTypeRepository, FormTypeRepository>();

builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IFormExportService, FormExportService>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//инициализация ролей и пользователя администратор
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    IdentityInitializer.Initialize(userManager, roleManager);
}


app.Run();
