using Learnify.Business.Abstract;
using Learnify.Business.Concrete;
using Learnify.Business.MappingProfiles;
using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;

using Learnify.DataAccess.Repositories;
using Learnify.UI.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServiceExtensions();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LearnifyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(GeneralMapping));
// Dependency Injection

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
