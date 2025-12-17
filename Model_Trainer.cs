using System.ComponentModel.DataAnnotations;

namespace fitness_app.Models
{
    public class Trainer
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ad Soyad")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Uzmanlık Alanı")]
        public string ExpertiseArea { get; set; }

        public string ImageUrl { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public int GymServiceId { get; set; }
        public GymService GymService { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
