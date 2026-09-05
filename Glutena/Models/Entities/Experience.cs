using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Glutena.Models.Entities
{
    public class Experience : BaseEntity   
        //her ürün için özel, detaylı deneyimler
    {

        [DisplayName("Başlık")]
        [Required(ErrorMessage = "Başlık zorunludur.")]
        [StringLength(100, ErrorMessage = "En fazla 100 karakter girilebilir.")]
        public string Title { get; set; } = string.Empty;

        [DisplayName("İçerik")]
        [Required(ErrorMessage = "İçerik zorunludur.")]
        [StringLength(1000, ErrorMessage = "En fazla 1000 karakter girilebilir.")]
        public string Content { get; set; } = string.Empty;

        [DisplayName("Puan")]
        [Range(1, 5, ErrorMessage = "Puan 1 ile 5 arasında olmalıdır.")]
        public int Rating { get; set; }

        [DisplayName("Onaylı mı?")]
        public bool IsApproved { get; set; } = false;

        [DisplayName("Aktif/Pasif")]
        public bool IsActive { get; set; } = true;

        // İlişkiler
        [DisplayName("Ürün")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [DisplayName("Yazar")]
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }
    }
}
