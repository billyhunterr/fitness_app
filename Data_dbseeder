using fitness_app.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace fitness_app.Data
{
    // IdentityDbContext'ten miras alıyoruz ki üyelik tabloları da otomatik gelsin
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       

        public DbSet<GymService> GymServices { get; set; } // Veritabanında 'GymServices' tablosu olacak
        public DbSet<Trainer> Trainers { get; set; }       // Veritabanında 'Trainers' tablosu olacak
        public DbSet<Appointment> Appointments { get; set; } // Veritabanında 'Appointments' tablosu olacak
        public DbSet<UserProgress> UserProgresses { get; set; }

        // Not: Users (Kullanıcılar) tablosunu yazmamıza gerek yok,
        // çünkü IdentityDbContext bunu bizim için gizli bir şekilde hallediyor.
    }
}

-------------------------------------------------------------------------------------------------------------------------

//dbseeder

using fitness_app.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace fitness_app.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            // Veritabanı context'ini de çağırıyoruz
            var context = service.GetService<ApplicationDbContext>();

            // 1. Rolleri Oluştur
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("Member"))
                await roleManager.CreateAsync(new IdentityRole("Member"));

            // 2. Admin Kullanıcısını Oluştur
            var adminEmail = "ogrencinumarasi@sakarya.edu.tr";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Sistem",
                    LastName = "Yöneticisi",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(newAdmin, "sau");
                if (result.Succeeded) await userManager.AddToRoleAsync(newAdmin, "Admin");
            }

            // --- YENİ KISIM: HİZMET VE ANTRENÖR EKLEME --- //

            // 3. Hizmet Kontrolü (Hiç hizmet yoksa ekle)
            if (!context.GymServices.Any())
            {
                var fitnessService = new GymService
                {
                    Name = "Fitness",
                    Description = "Genel Vücut Geliştirme",
                    DurationMinutes = 60,
                    Price = 500
                };
                var pilatesService = new GymService
                {
                    Name = "Pilates",
                    Description = "Esneklik ve Denge",
                    DurationMinutes = 45,
                    Price = 600
                };

                context.GymServices.AddRange(fitnessService, pilatesService);
                await context.SaveChangesAsync(); // Kaydet ki ID oluşsun

                
            }
        }
    }
}
