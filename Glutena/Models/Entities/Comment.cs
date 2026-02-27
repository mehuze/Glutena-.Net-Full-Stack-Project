using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glutena.Models.Entities
{
    public class Comment : BaseEntity
    {
        

        [Required]
        [DisplayName("Kullanıcı")]
        public int UserId { get; set; }

        [Required]
        [DisplayName("Ürün")]
        public int ProductId { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 10)]
        [DisplayName("Yorum")]
        public string Review { get; set; } = string.Empty;

        [Range(1, 5)]
        [DisplayName("Puan")]
        public int Rating { get; set; }


        [DisplayName("Onaylandı mı?")]
        public bool IsApproved { get; set; } = false;

        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}