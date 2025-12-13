#  Fitness App - Spor Salonu Yönetim Sistemi

Bu proje, **Sakarya Üniversitesi Web Programlama** dersi kapsamında geliştirilmiş; kapsamlı bir Spor Salonu Yönetim Sistemi'dir. ASP.NET Core MVC mimarisi kullanılarak tasarlanan uygulama, modern web standartlarına uygun, güvenli ve kullanıcı dostu bir deneyim sunar.

## 🚀 Proje Özellikleri

### Kimlik ve Yetkilendirme (Identity)
* **Rol Bazlı Erişim:** Admin ve Üye (Member) olmak üzere iki farklı rol yapısı.
* **Güvenli Giriş:** Şifreli üyelik sistemi ve "Beni Hatırla" özelliği.
* **Admin Hesabı:** Sistem ilk açıldığında `ogrencinumarasi@sakarya.edu.tr` / `sau` admin hesabı otomatik oluşturulur (Data Seeding).

###  Yönetim Paneli (Admin)
* **Hizmet Yönetimi (CRUD):** Spor salonu hizmetlerini ekleme, düzenleme, silme ve listeleme.
* **Antrenör Yönetimi (CRUD):** Eğitmen kadrosunu yönetme, fotoğraf ve uzmanlık alanı atama.
* **Tam Yetki:** Sadece Admin yetkisine sahip kullanıcılar kritik verilere müdahale edebilir.

###  Randevu Sistemi
* **Akıllı Çakışma Kontrolü:** Aynı antrenöre, aynı saatte mükerrer randevu alınmasını engelleyen iş mantığı (Business Logic).
* **Kişisel Takvim:** Kullanıcılar aldıkları randevuları kendi panellerinde görüntüleyebilir.

###  Yapay Zeka (AI) Desteği
* **AI Antrenör:** Kullanıcının Boy/Kilo endeksine (BMI) göre otomatik egzersiz ve program önerisi sunan Uzman Sistem (Expert System).

###  Kullanıcı Gelişim Takibi
* **Profil Yönetimi:** Kullanıcılar kişisel bilgilerini güncelleyebilir.
* **İlerleme Tarihçesi:** Kullanıcının girdiği her kilo/boy verisi tarihli olarak veritabanına kaydedilir ve tablo halinde gösterilir.

###  REST API Hizmeti
* **Mobil Entegrasyon:** Antrenör verilerini JSON formatında dış dünyaya sunan API uç noktası (`/api/trainers`).
* **LINQ Sorguları:** API içerisinde veritabanı filtreleme işlemleri LINQ ile optimize edilmiştir.

---

##  Teknolojiler

* **Framework:** .NET 8.0 (ASP.NET Core MVC)
* **Veritabanı:** MSSQL / Entity Framework Core (Code First)
* **Front-End:** Bootstrap 5, HTML5, CSS3, JavaScript
* **Versiyon Kontrol:** Git & GitHub

---



