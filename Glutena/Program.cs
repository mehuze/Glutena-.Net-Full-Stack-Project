using Glutena.Data;
using Glutena.Data.Abstract;
using Glutena.Data.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlantýsý ayar dosyasýndan okunur
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
    throw new InvalidOperationException(
        "Veritabaný baðlantý cümlesi (ConnectionStrings:DefaultConnection) bulunamadý.");

builder.Services.AddDbContext<GlutenaDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<GlutenaDbContext>()
.AddErrorDescriber<Glutena.Models.TurkishIdentityErrorDescriber>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Login";
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// 404 gibi durumlarda boþ sayfa yerine hata sayfamýzý göster
app.UseStatusCodePagesWithReExecute("/Home/Error", "?code={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

// Güvenlik baþlýklarý
app.Use(async (context, next) =>
{
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["X-Frame-Options"] = "DENY";
    context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
    context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
    await next();
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Veritabanýný güncelle, rolleri ve admin hesabýný oluþtur
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    // Bekleyen migration'larý otomatik uygula
    var db = services.GetRequiredService<GlutenaDbContext>();
    await db.Database.MigrateAsync();

    // Bilgi sayfalarýnýn ilk içeriðini ekle (sayfa varsa dokunmaz)
    await ContentSeeder.SeedAsync(db);
    await ColyakPageSeeder.SeedAsync(db);
    await LegalPagesSeeder.SeedAsync(db);
    await TermsPageSeeder.SeedAsync(db);

    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    // Roller yoksa oluþtur
    foreach (var role in new[] { "Admin", "User" })
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

    // Admin bilgileri koddan deðil, ayar dosyasýndan okunur
    var adminEmail = app.Configuration["AdminSeed:Email"];
    var adminPassword = app.Configuration["AdminSeed:Password"];

    if (!string.IsNullOrWhiteSpace(adminEmail) && !string.IsNullOrWhiteSpace(adminPassword))
    {
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(adminUser, "Admin");
            else
                logger.LogWarning("Admin hesabý oluþturulamadý: {Errors}",
                    string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}

app.Run();