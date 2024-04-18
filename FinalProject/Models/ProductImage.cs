namespace FinalProject.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Image { get; set; } = "DefaultImage";
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
