using Microsoft.EntityFrameworkCore;
using ZadatakAKD.Data;
using ZadatakAKD.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ZadatakAKDContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ZadatakAKDContext") ?? throw new InvalidOperationException("Connection string 'ZadatakAKDContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ZadatakAKDContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();
