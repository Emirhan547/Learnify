using Learnify.Business.DependencyResolvers;
using Learnify.Business.MappingProfiles;
using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Learnify.DataAccess.Extensions;
using Learnify.DataAccess.Repositories;
using Learnify.DataAccess.Seeds;
using Learnify.Entity.Concrete;

using Learnify.UI.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ DbContext
builder.Services.AddDbContext<LearnifyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Identity
builder.Services.AddIdentity<AppUser, IdentityRole<int>>()
    .AddEntityFrameworkStores<LearnifyContext>()
    .AddDefaultTokenProviders();

// ✅ Servisler
builder.Services.AddBusinessLayerServices();
builder.Services.AddAutoMapper(typeof(GeneralMapping));
builder.Services.AddDataAccess(builder.Configuration);


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ✅ Controllers with Views
builder.Services.AddControllersWithViews();

// ✅ Cookie Configuration
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin/Account/Login";
    options.LogoutPath = "/Admin/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;

    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.SameSite = SameSiteMode.Lax;
});

// ✅ AutoMapper
builder.Services.AddAutoMapper(typeof(GeneralMapping));

// ✅ HttpContextAccessor & Session
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// ✅ Rolleri seed et (Admin kullanıcı yok)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
    await RoleSeed.EnsureRolesAsync(roleManager);
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

app.UseAuthentication();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();


app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
    

// ✅ Area Routing
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

// ✅ Default Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
