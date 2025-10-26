using Learnify.Business.DependencyResolvers;
using Learnify.Business.MappingProfiles;
using Learnify.DataAccess.Context;
using Learnify.Entity.Concrete;
using Learnify.UI.Extensions;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Service Extensions
builder.Services.AddServiceExtensions();

builder.Services.AddValidationRules();

builder.Services.AddIdentity<AppUser, IdentityRole<int>>()
    .AddEntityFrameworkStores<LearnifyContext>()
    .AddDefaultTokenProviders();

// ✅ Controllers with Views
builder.Services.AddControllersWithViews();

// ✅ Database Context
builder.Services.AddDbContext<LearnifyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Identity Configuration
builder.Services.AddIdentity<AppUser, IdentityRole<int>>(options =>
{
})
.AddEntityFrameworkStores<LearnifyContext>()
.AddDefaultTokenProviders();

// ✅ Cookie Configuration
builder.Services.ConfigureApplicationCookie(options =>
{
    // ✅ Admin paneli için özel login sayfası
    options.LoginPath = "/Admin/Login/Index";
    options.LogoutPath = "/Admin/Login/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;

    // Cookie ayarları
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.SameSite = SameSiteMode.Lax;
});

// ✅ AutoMapper
builder.Services.AddAutoMapper(typeof(GeneralMapping));

// ✅ HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// ✅ Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// ✅ Seed Roles and Admin User
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
    }

    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding roles");
    }
}

// ✅ Error Handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

// ✅ Authentication & Authorization (Sıralama önemli!)
app.UseAuthentication();
app.UseAuthorization();

// ✅ Area Routing
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);

// ✅ Default Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();