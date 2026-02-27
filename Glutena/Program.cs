using Glutena.Data;
using Glutena.Data.Abstract;
using Glutena.Data.Concrete;
using Glutena.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GlutenaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));//Controller IGenericRepository<Product> isterse,
//ona GenericRepository<Product> ver.

builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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

app.Run();
