namespace Perfumes.Models
{
    public class ProductPromoDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public int IsInStock { get; set; }
        public int PromoPrice { get; set; }
        public string? PromoName { get; set; }
    }
}
