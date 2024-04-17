namespace FinalProject.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public ICollection<Product> Product { get; set; } = null!;
        public string Image { get; set; }
    }
}
