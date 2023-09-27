using EmployeeManagement.Models;
using EmployeeManagement.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

// DI service
// create one instace
builder.Services.AddScoped<ICompanyRepository<Employee>, SqlEmployeeRepository>();
// create instance if u need
//builder.Services.AddScoped<ICompanyRepository<Employee>, EmployeeRepository>();
// each HTTP Request will be create new instance
//builder.Services.AddTransient<ICompanyRepository<Employee>, EmployeeRepository>();

//DbContext Service
string strCon = builder.Configuration.GetConnectionString("EmployeeDbConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(strCon));



// Identity

builder.Services.AddIdentity<AppUser, IdentityRole>(options => { 
  options.Password.RequiredLength = 8;
  options.Password.RequireUppercase = false;
  options.Password.RequireLowercase = false;

}).AddEntityFrameworkStores<AppDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
