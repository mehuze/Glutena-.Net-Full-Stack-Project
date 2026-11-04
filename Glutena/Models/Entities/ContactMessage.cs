using System.ComponentModel.DataAnnotations;

namespace Glutena.Models.Entities
{
    public class ContactMessage : BaseEntity
    {
        [Required(ErrorMessage = "Adınızı yazın.")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta adresinizi yazın.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi yazın.")]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Subject { get; set; } = "Genel soru";

        [Required(ErrorMessage = "Mesajınızı yazın.")]
        [MaxLength(2000)]
        public string Message { get; set; } = string.Empty;

        // Admin mesajı okudu mu?
        public bool IsRead { get; set; } = false;
    }
}