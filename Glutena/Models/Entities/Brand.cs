using System.ComponentModel.DataAnnotations;

namespace Glutena.Models.Entities
{
    public class Brand : BaseEntity
    {
       

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public List<Product>? Products { get; set; }
    }
}