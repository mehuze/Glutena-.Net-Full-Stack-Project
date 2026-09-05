namespace Glutena.Models.Entities
{
    public class BlacklistWord
    {
        public int Id { get; set; }
        public string Word { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
