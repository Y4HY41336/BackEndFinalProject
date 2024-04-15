using FinalProject.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModel
{
    public class ProductDetailViewModel
    {
		public string Title { get; set; } = null!;
		public double Price { get; set; }
		[Range(0, 5, ErrorMessage = "Value must be between 0 and 5")]
		public double Rating { get; set; }
		public double SKU { get; set; }

		public int BrandId { get; set; }
		public string BrandName { get; set; } = null!;

		public int CategoryId { get; set; }
		public string CategoryName { get; set; } = null!;

		public string Description { get; set; } = null!;
		public string Features { get; set; } = null!;
		public string Material { get; set; } = null!;
		public string ClaimedSize { get; set; } = null!;
		public string RecommendedUse { get; set; } = null!;
		public string Manufacturer { get; set; } = null!;

	}
}
