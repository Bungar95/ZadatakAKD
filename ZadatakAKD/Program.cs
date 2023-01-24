﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZadatakAKD.Data;
using ZadatakAKD.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ZadatakAKDContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ZadatakAKDContext") ?? throw new InvalidOperationException("Connection string 'ZadatakAKDContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();