using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public double Price { get; set; }
        public double OldPrice { get; set; }
        [Range(0, 5, ErrorMessage = "Value must be between 0 and 5")]
        public double Rating { get; set; }
        public double SKU { get; set; }
		public bool isStocked { get; init; }
        public string Description { get; set; } = null!;
		public string Features { get; set; } = null!;
		public string Material { get; set; } = null!;
		public string ClaimedSize { get; set; } = null!;
		public string RecommendedUse { get; set; } = null!;
		public string Manufacturer { get; set; } = null!;

		public int BrandId { get; set; }
        public Brand Brand { get; set; } = null!;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

    }
}
