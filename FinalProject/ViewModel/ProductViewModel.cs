using FinalProject.Models;
using Microsoft.Build.Framework;

namespace FinalProject.ViewModel
{
    public class ProductViewModel
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public double Price { get; set; }
        public double OldPrice { get; set; } = 0;
        [Required]
        public double Rating { get; set; }
        [Required]
        public double SKU { get; set; }
        public bool isStocked { get; init; } = true;
        public bool isDeleted { get; init; } = false;

        public string Description { get; set; } = "TestDescription";

        public string Features { get; set; } = "TestFeatures";

        public string Material { get; set; } = "TestMaterial";

        public string ClaimedSize { get; set; } = "46";

        public string RecommendedUse { get; set; } = "TestRecommendedUse";

        public string Manufacturer { get; set; } = "TestManufacturer";
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public IFormFile PosterImage { get; set; }
        //public List<IFormFile> Images { get; set; }
    }
}
