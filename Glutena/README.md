# Glutena

Çölyak hastaları için ürün tanıtım ve bilgilendirme platformu.

## Proje Hakkında
Glutena, çölyak hastalarının güvenle tüketebileceği ürünleri keşfetmelerine,
kullanıcı yorumlarını okumalarına ve hastalıkla ilgili güvenilir bilgiye
ulaşmalarına yardımcı olan bir ASP.NET Core MVC web platformudur.

## Özellikler
- Ürün içerikleri ve kullanıcı yorumları
- Çölyak hastalığıyla ilgili küçük testler
- Ne zaman doktora başvurulması gerektiğine dair bilgilendirme
- Blog yazıları
- Rol bazlı (Admin / User) kimlik doğrulama ve yetkilendirme
- Ayrı bir yönetim paneli (Admin Area): ürün, blog, marka, kategori,
  deneyim (experience) ve kullanıcı yönetimi

## Planlanan Özellikler
- Kullanıcı ve yorum sayısı arttıkça devreye alınacak yapay zekâ destekli
  öneri / analiz sistemi

## Kullanılan Teknolojiler
- ASP.NET Core MVC, C#
- Entity Framework Core + SQL Server
- ASP.NET Core Identity (rol bazlı kimlik doğrulama, seed edilmiş admin hesabı)
- Generic Repository Pattern (IGenericRepository / GenericRepository)
- Güvenlik header middleware'leri (X-Content-Type-Options, X-Frame-Options,
  X-XSS-Protection, Referrer-Policy), HSTS ve production hata yönetimi

## Mimari
- `Data/Abstract` ve `Data/Concrete`: generic repository pattern ile veri
  erişim katmanı
- `Areas/Admin`: yönetim paneli controller ve view'ları
- `Models`: Product, Category, Brand, Blog, Comment, Experience, Feature,
  Testimonial, User, BlacklistWord vb. entity'ler
- `Migrations`: Entity Framework Core code-first migration geçmişi

## Durum
Bu proje aktif geliştirme aşamasındadır; bazı sayfalarda düzeltme
çalışmaları devam etmektedir. Yapay zekâ destekli öneri sistemi henüz
devreye alınmamıştır, kullanıcı tabanı büyüdükçe eklenmesi planlanmaktadır.
