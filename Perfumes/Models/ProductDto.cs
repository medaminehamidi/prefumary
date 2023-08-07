namespace Perfumes.Models
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public int IsInStock { get; set; }
    }
}
