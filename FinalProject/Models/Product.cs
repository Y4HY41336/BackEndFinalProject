using Microsoft.Build.Framework;
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
        public bool isDeleted { get; init; }
        public string Description { get; set; }
		public string Features { get; set; }
		public string Material { get; set; }
        public string ClaimedSize { get; set; }
		public string RecommendedUse { get; set; }
		public string Manufacturer { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
