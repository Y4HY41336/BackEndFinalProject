using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModel
{
    public class ProductUpdateViewModel
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public double Price { get; set; }
        [Required]
        public double OldPrice { get; set; } = 0;
        [Range(0, 5, ErrorMessage = "Value must be between 0 and 5")]
        public double Rating { get; set; }
        [Required]
        public double SKU { get; set; }
        [Required]
        public bool isStocked { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Features { get; set; }
        [Required]
        public string Material { get; set; }
        [Required]
        public string ClaimedSize { get; set; }
        [Required]
        public string RecommendedUse { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public IFormFile? PosterImage { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
