using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glutena.Models.Entities
{
    public class Product : BaseEntity
    {//Evet, ürünleri göstermek için

        public ICollection<ProductCategory> ProductCategories { get; set; }
    = new List<ProductCategory>();
        public ICollection<Experience> Experiences { get; set; }
    = new List<Experience>();

        [DisplayName("Ürün Adı")]
        [Required(ErrorMessage = "Ürün Adı Zorunlu Alan")]
        [StringLength(50, ErrorMessage = "En Fazla 50 Karakter Girilebilir")]
        public string ProductName { get; set; } = string.Empty;

        [DisplayName("Açıklama")]
        [Required(ErrorMessage = "Açıklama Zorunlu Alan")]
        [StringLength(200, ErrorMessage = "En Fazla 200 Karakter Girilebilir")]
        public string Description { get; set; } = string.Empty;


        [Required(ErrorMessage = "Gluten durumu zorunludur.")]
        [DisplayName("Gluten Durumu")]
        [StringLength(50, ErrorMessage = "En Fazla 50 Karakter Girilebilir")]
        public string GlutenStatus { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Slug { get; set; } = string.Empty;

        [DisplayName("Çapraz Bulaşma Riski")]
        public bool CrossContaminationRisk { get; set; }



        [DisplayName("Aktif/Pasif")]
        public bool IsActive { get; set; } = true;

        [DisplayName("Birim Fiyat")]
        public decimal? Price { get; set; }

        [DisplayName("Resim")]
        public string? PhotoPath { get; set; }

        [DisplayName("Anahtar Kelimeler")]
        public string? Keywords { get; set; }

        public bool IsHighlighted { get; set; } = false;

        //[DisplayName("Çok Satanlar")]
        //public int TopSeller { get; set; } //CokSatanlar

        //[DisplayName("BunaBakan")]
        //public int Related { get; set; } //BunaBakanlar / buna bakan buna da baktı
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }
        public string? AffiliateUrl { get; set; }

        public bool IsSponsored { get; set; } = false;

        public int SponsorPriority { get; set; } = 0;

        // Hesaplanan Alanlar
        [NotMapped]
        public double AverageRating
        {
            get
            {
                if (Experiences == null || !Experiences.Any())
                    return 0;

                return Experiences.Average(x => x.Rating);
            }
        }


        [NotMapped]
        public int ExperienceCount
        {
            get
            {
                return Experiences?.Count ?? 0;
            }
        }
    }


}
