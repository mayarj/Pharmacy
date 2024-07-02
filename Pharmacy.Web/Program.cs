using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pharmacy.Application.Interfaces;
using Pharmacy.Application.Services;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.IRepositories;
using Pharmacy.Infrastructure.Models;
using Pharmacy.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PharmacyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});
builder.Services.AddIdentity<User, IdentityRole>()
    .AddUserManager<UserManager<User>>() // Register UserManager<User>
    .AddEntityFrameworkStores<PharmacyContext>()
    .AddDefaultTokenProviders();

// Move the UserManager registration above the Configure<IdentityOptions> call




builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LoginPath = "/Account/Logout";
});


    
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Home/Indix";
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy =>
       policy.RequireRole("User"));
    options.AddPolicy("UserAndAdmin", policy =>

    policy.RequireRole("User", "Admin"));
});
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();



builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IFactoryService, FactoryService>();
builder.Services.AddScoped<IFactoryRepository, FactoryRepository>();

builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();

builder.Services.AddScoped<IMedicineService, MedicineService>();
builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();

builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();

builder.Services.AddScoped<IPrescriptionMedicineService, PrescriptionMedicineService>();
builder.Services.AddScoped<IPrescriptionMedicineRepository, PrescriptionMedicineRepository>();

builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();

builder.Services.AddScoped<IMedicineIngredientService, MedicineIngredientService>();
builder.Services.AddScoped<IMedicineIngredientRepository, MedicineIngredientRepository>();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
    if (!adminRoleExists)
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    var adminUser = await userManager.FindByEmailAsync("admin@example.com");
    if (adminUser == null)
    {
        adminUser = new User { UserName = "admin@example.com", Email = "admin@example.com", Id = "admin@example.com" };

        // Disable the default behavior of generating a new Id
        userManager.UserValidators.Clear();

        var result = await userManager.CreateAsync(adminUser, "AdminPassword123!");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
            
        }
    }
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStatusCodePagesWithRedirects("/Error/{0}");
app.UseStaticFiles();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Catchall",
     pattern: "{*catchall}", 
   new { controller = "Home", action = "Error" }
);

app.Run();
