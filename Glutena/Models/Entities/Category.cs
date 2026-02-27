using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glutena.Models.Entities
{
    public class Category : BaseEntity
    {//Evet, ürünleri kategorilere ayırmak için
        public ICollection<ProductCategory> ProductCategories { get; set; }
    = new List<ProductCategory>();

        [DisplayName("KATEGORİ ADI")]
        [Required(ErrorMessage = "Kategori Adı Zorunlu Alan")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter girilebilir")]
        public string CategoryName { get; set; } = string.Empty;

        [DisplayName("Üst Kategori")]
        public int? ParentId { get; set; }
        public Category? Parent { get; set; }
        public ICollection<Category>? Children { get; set; }

        [DisplayName("Aktif/Pasif")]
        public bool Active { get; set; } = true;

        // İlişki: Bir kategoriye birden fazla ürün olabilir
        
    }
}
