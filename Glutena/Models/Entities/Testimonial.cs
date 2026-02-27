using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glutena.Models.Entities
{
    public class Testimonial: BaseEntity
    {
        //genelde site veya marka hakkında genel yorumlar / referanslar
       


        [DisplayName("Ad Soyad")]
        [Required(ErrorMessage = "Ad Soyad Zorunlu Alan")]
        [StringLength(50, ErrorMessage = "En Fazla 50 Karakter Girilebilir")]
        public string TestimonialName { get; set; } = string.Empty;


        [DisplayName("Yorum")]
        [StringLength(500, ErrorMessage = "En Fazla 500 Karakter Girilebilir")]
        public string Comment { get; set; } = string.Empty;

        public bool IsApproved { get; set; }
    }
}
