using fitness_app.Data;
using fitness_app.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace fitness_app.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppointmentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // 1. LİSTELEME
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var appointments = await _context.Appointments
                .Include(a => a.Trainer)
                .Where(a => a.MemberId == user.Id)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();

            return View(appointments);
        }

        // 2. EKLEME (Sayfa Açılışı)
        public IActionResult Create()
        {
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "FullName");
            return View();
        }

        // 3. KAYDETME 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            
  
            ModelState.Remove("Member");
            ModelState.Remove("MemberId");
            ModelState.Remove("Trainer");
            
            var user = await _userManager.GetUserAsync(User);
            appointment.MemberId = user.Id;
            appointment.CreatedDate = DateTime.Now;

            // Çakışma Kontrolü
            bool isConflict = _context.Appointments.Any(a =>
                a.TrainerId == appointment.TrainerId &&
                a.AppointmentDate == appointment.AppointmentDate);

            if (isConflict)
            {
                ModelState.AddModelError("", "Seçtiğiniz saatte bu antrenör dolu.");
            }

            // Geçmiş Tarih Kontrolü
            if (appointment.AppointmentDate < DateTime.Now)
            {
                ModelState.AddModelError("", "Geçmiş bir tarihe randevu alamazsınız.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Hata varsa sayfayı tekrar doldur
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "FullName", appointment.TrainerId);
            return View(appointment);
        }

        // 4. SİLME
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (appointment.MemberId == user.Id)
                {
                    _context.Appointments.Remove(appointment);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
