using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public double OldPrice { get; set; }       
        public double Rating { get; set; }
        public double SKU { get; set; }
        public bool isStocked { get; set; }
        public bool isDeleted { get; set; }
        public string Description { get; set; }
		public string Features { get; set; }
		public string Material { get; set; }
        public string ClaimedSize { get; set; }
		public string RecommendedUse { get; set; }
		public string Manufacturer { get; set; }
        public string PosterImage { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
