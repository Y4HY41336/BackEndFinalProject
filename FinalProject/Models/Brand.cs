namespace FinalProject.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string BrandName { get; set; } = null!;
        public ICollection<Product> Product { get; set; } = null!;
        public string Image { get; set; }
    }
}
