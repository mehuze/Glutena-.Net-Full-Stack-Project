using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glutena.Models.Entities
{
    public class User : BaseEntity
    {//Evet, admin ve kullanıcı ayrımı için
      

        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter girilebilir.")]
        public string UserName { get; set; } = string.Empty;

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email giriniz.")]
        public string Email { get; set; } = string.Empty;

        [DisplayName("Şifre")]
        [Required(ErrorMessage = "Şifre zorunludur.")]
        [StringLength(100)]
        public string PasswordHash { get; set; } = string.Empty;

        [DisplayName("Admin Mi?")]
        public bool IsAdmin { get; set; } = false; // normal kullanıcı veya personel

        [DisplayName("Aktif/Pasif")]
        public bool IsActive { get; set; } = true;

        // İlişkiler
        public ICollection<Testimonial>? Testimonials { get; set; }
        public ICollection<Blog>? Blogs { get; set; }
        public ICollection<Product>? Products { get; set; } // Admin eklediği ürünler

    }
}
