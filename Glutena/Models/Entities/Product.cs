using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glutena.Models.Entities
{
    public class Product : BaseEntity
    {//Evet, ürünleri göstermek için

        public ICollection<ProductCategory> ProductCategories { get; set; }
    = new List<ProductCategory>();

        [DisplayName("Kategori")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }


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



        [DisplayName("Çapraz Bulaşma Riski")]
        public bool CrossContaminationRisk { get; set; }



        [DisplayName("Aktif/Pasif")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Fiyat zorunludur.")]
        [Range(0, 100000, ErrorMessage = "Fiyat 0'dan büyük olmalıdır.")]
        [DisplayName("Birim Fiyat")]
        public decimal Price { get; set; }


        private int _Kdv {  get; set; }
        public int Kdv
        { get { return _Kdv; } 
            set { _Kdv =Math.Abs( value); }
        }
        [DisplayName("Puan")]
        [Range(0, 5, ErrorMessage = "Puan 0-5 arasında olmalıdır.")]
        public decimal Rating { get; set; } = 0;

        [DisplayName("Resim")]
        public string? PhotoPath { get; set; }

        [DisplayName("Anahtar Kelimeler")]
        public string? Keywords { get; set; }

        public int HighLighted { get; set; } //öne cıkanlar = like

        //[DisplayName("Çok Satanlar")]
        //public int TopSeller { get; set; } //CokSatanlar

        [DisplayName("BunaBakan")]
        public int Related { get; set; } //BunaBakanlar / buna bakan buna da baktı
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }
        public string? AffiliateUrl { get; set; }

        public bool IsSponsored { get; set; } = false;

        public int SponsorPriority { get; set; } = 0;


    }
}
