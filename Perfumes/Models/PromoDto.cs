namespace Perfumes.Models
{
    public class PromoDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid ProductId { get; set; }
    }
}
