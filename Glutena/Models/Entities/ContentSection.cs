using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Glutena.Models.Entities
{
    // Bir bilgi sayfasının bölümü: "Bundan sonra ne yiyeceğim?" gibi
    public class ContentSection : BaseEntity
    {
        [DisplayName("Sayfa")]
        public int ContentPageId { get; set; }
        public ContentPage? ContentPage { get; set; }

        [DisplayName("Sıra")]
        public int Order { get; set; }

        [DisplayName("Simge")]
        [MaxLength(10)]
        public string? Icon { get; set; }

        [DisplayName("Başlık")]
        [Required(ErrorMessage = "Başlık zorunludur.")]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [DisplayName("Kısa başlık")]
        [MaxLength(40)]
        public string? ShortTitle { get; set; }

        [DisplayName("İçerik")]
        [Required(ErrorMessage = "İçerik zorunludur.")]
        public string Body { get; set; } = string.Empty;

        [DisplayName("Yayında")]
        public bool IsActive { get; set; } = true;
    }
}