using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glutena.Models.Entities
{
    public class Feature : BaseEntity
    {//Evet, ürün özelliklerini göstermek için
      

        [DisplayName("Başlık")]
        [Required(ErrorMessage = "Başlık Zorunlu Alan")]
        [StringLength(150, ErrorMessage = "En Fazla 150 Karakter Girilebilir")]
        public string FeatureName { get; set; } = string.Empty;


        [DisplayName("Açıklama")]
        [Required(ErrorMessage = "Açıklama Zorunlu Alan")]
        [StringLength(150, ErrorMessage = "En Fazla 150 Karakter Girilebilir")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "İkon")]
        public string? IconUrl { get; set; }


        [DisplayName("Aktif/Pasif")]
        public bool IsActive { get; set; }
    }
}
