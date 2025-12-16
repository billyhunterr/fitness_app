using fitness_app.Data;
using fitness_app.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. AYAR: Veritabanı Adresini (Connection String) okuyoruz
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. AYAR: Köprüyü (DbContext) sisteme ekliyoruz
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. AYAR: Üyelik Sistemi ve Şifre Kuralları
// Burası "sau" şifresini kabul etmesi için 
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;           // Rakam zorunlu değil
    options.Password.RequireLowercase = false;       // Küçük harf zorunlu değil
    options.Password.RequireUppercase = false;       // Büyük harf zorunlu değil
    options.Password.RequireNonAlphanumeric = false; // Sembol (!, @) zorunlu değil
    options.Password.RequiredLength = 3;             // En az 3 karakter olsun ("sau" 3 harfli)
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Hata yönetimi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Önce kimlik kontrolü , sonra yetki kontrolü 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// --- SEEDING İŞLEMİ BAŞLANGICI ---
// Uygulama her başladığında Admin var mı diye kontrol et
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // DbSeeder sınıfındaki metodu çağırıyoruz
        await DbSeeder.SeedRolesAndAdminAsync(services);
    }
    catch (Exception)
    {
        // Hata olursa şimdilik boşver (Loglama yapılabilir)
    }
}

app.Run();
