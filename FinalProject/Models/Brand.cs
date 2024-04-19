namespace FinalProject.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Image { get; set; }
        public bool isDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public ICollection<Product> Product { get; set; } = null!;
    }
}
