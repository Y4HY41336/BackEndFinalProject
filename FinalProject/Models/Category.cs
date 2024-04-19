namespace FinalProject.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string Image { get; set; }
        public bool isDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }


        public ICollection<Product> Product { get; set; } = null!;
    }
}
