/*#FIXME:� ���� �������� View ������ �������� ������. � ������: 
 * 1) ��������� �� �� ������� ����������� OrganizationRepository, 
    ����� ����� ���� RegionDivisionRepository, �.�. ������ ��� �������� �� ��������� � Explanation, � �������� � Form 
   2) � RegionDivision ������ ���� �������� �������, ������� ����� ��������� �� �� �� ������, � � ����� Organization, ������ ����� �� �������� ��� ����������� ������, ����� ��� �� ������������
   3) ��� � ������ ����� � ������, � ��� ������� �������, ������ ������ ������ View, �������� ������ ��������, ������� ����� ��������� ��� ��������� 8( */

using DiplomMetod.Data;
using DiplomMetod.Data.Identity;
using DiplomMetod.Repositories;
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

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IFormRepository, FormRepository>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IReferenceBookRepository, ReferenceBookRepository>();
builder.Services.AddScoped<IRegionDivisionRepository, RegionDivisionRepository>();
builder.Services.AddScoped<IFormTypeRepository, FormTypeRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Diplom Metod API", Version = "v1" });
});


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  
    app.UseSwagger();

    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Diplom Metod API");
        options.RoutePrefix = "swagger";
    });
}
else
{
    app.UseExceptionHandler("/Form/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//������������� ����� � ������������ �������������
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    IdentityInitializer.Initialize(userManager, roleManager);
}


app.Run();
