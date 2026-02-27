using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glutena.Models.Entities
{
    public class Blog : BaseEntity
    {//Evet, içerik paylaşımı için
       

        [DisplayName("Başlık")]
        [Required(ErrorMessage ="Başlık Zorunlu Alan")]
        [StringLength(50,ErrorMessage ="En Fazla 50 Karakter Girilebilir")]
        public string BlogName { get; set; } = string.Empty;

        [DisplayName("İçerik")]
        [Required(ErrorMessage = "İçerik Zorunlu Alan")]
        [StringLength(150, ErrorMessage = "En Fazla 150 Karakter Girilebilir")]
        public string Content { get; set; } = string.Empty;


        [DisplayName("Oluşturulma Tarihi")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        [DisplayName("Aktif/Pasif")]
        public bool IsActive { get; set; }

        [DisplayName("Resim Seçiniz")]
        public string? PhotoPath { get; set; }
    }
}
