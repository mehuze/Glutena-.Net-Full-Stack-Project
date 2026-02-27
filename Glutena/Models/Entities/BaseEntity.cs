using System.ComponentModel.DataAnnotations;

namespace Glutena.Models.Entities
{
    public abstract class BaseEntity
    {//BaseEntity tek başına tablo olmayacak

        // Sadece diğer entity’lere temel olacak
        //Tüm entity’lerin ortak alanlarını taşıyan soyut temel sınıftır.(Veri yapısı standardı)
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; }
    }
}