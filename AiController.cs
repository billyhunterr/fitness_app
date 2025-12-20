using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fitness_app.Controllers
{
    [Authorize] // Sadece üyeler kullanabilsin
    public class AiController : Controller
    {
        // 1. GİRİŞ EKRANI (Formu Göster)
        public IActionResult Index()
        {
            return View();
        }

        // 2. YAPAY ZEKA İŞLEMİ (Hesapla ve Öner)
        [HttpPost]
        public IActionResult GeneratePlan(double weight, double height, string goal)
        {
            // Basit veri doğrulama
            if (weight <= 0 || height <= 0)
            {
                ViewBag.Error = "Lütfen geçerli değerler giriniz.";
                return View("Index");
            }

            // Vücut Kitle İndeksi (BMI) Hesaplama
            // Boyu santimetreden metreye çeviriyoruz (örn: 180 cm -> 1.80 m)
            double heightM = height / 100;
            double bmi = weight / (heightM * heightM);

            // AI Mantığı (Karar Ağacı Algoritması)
            string recommendation = "";
            string status = "";

            // Vücut tipini belirle
            if (bmi < 18.5) status = "Zayıf";
            else if (bmi < 25) status = "Normal";
            else if (bmi < 30) status = "Fazla Kilolu";
            else status = "Obez";

            // Hedefe ve Duruma göre öneri üret
            if (goal == "lose_weight") // Kilo Vermek İstiyorsa
            {
                if (status == "Obez" || status == "Fazla Kilolu")
                    recommendation = "Senin için 'Yüksek Yoğunluklu Kardiyo (HIIT)' programı hazırladım. Haftada 4 gün tempolu yürüyüş ve düşük karbonhidratlı diyet öneriyorum. Önceliğimiz yağ yakımı.";
                else
                    recommendation = "Kilon zaten dengeli. Sıkılaşmak için hafif tempolu koşu, yüzme ve Pilates öneriyorum.";
            }
            else // Kas Yapmak İstiyorsa
            {
                if (status == "Zayıf")
                    recommendation = "Hacim kazanman lazım! 'Hypertrophy (Büyüme)' odaklı ağırlık antrenmanı ve yüksek proteinli/karbonhidratlı beslenme planı senin için en iyisi.";
                else
                    recommendation = "Vücudun kas yapmaya müsait. '5x5 Güç Antrenmanı' programını ve dengeli protein alımını öneriyorum.";
            }

            // Sonuçları ekrana göndermek için paketle
            ViewBag.Bmi = Math.Round(bmi, 1);
            ViewBag.Status = status;
            ViewBag.Recommendation = recommendation;
            ViewBag.Goal = goal == "lose_weight" ? "Kilo Vermek" : "Kas Yapmak";

            return View("Result");
        }
    }
}
