using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Glutena.Models.Entities
{
    // Bilgi sayfası: Yeni Tanı Rehberi, Çölyak, Gizlilik Politikası gibi
    public class ContentPage : BaseEntity
    {
        [DisplayName("Adres")]
        [Required(ErrorMessage = "Adres zorunludur.")]
        [MaxLength(100)]
        [RegularExpression("^[a-z0-9-]+$", ErrorMessage = "Sadece küçük harf, rakam ve tire (-) kullanın. Örnek: yeni-tani")]
        public string Slug { get; set; } = string.Empty;

        [DisplayName("Başlık")]
        [Required(ErrorMessage = "Başlık zorunludur.")]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [DisplayName("Alt başlık")]
        [MaxLength(300)]
        public string? Subtitle { get; set; }

        [DisplayName("Simge")]
        [MaxLength(10)]
        public string? Icon { get; set; }

        [DisplayName("Giriş metni")]
        public string? Intro { get; set; }

        [DisplayName("Sayfa sonu notu")]
        [MaxLength(500)]
        public string? Footnote { get; set; }

        [DisplayName("Yayında")]
        public bool IsActive { get; set; } = true;

        public List<ContentSection> Sections { get; set; } = new();
    }
}