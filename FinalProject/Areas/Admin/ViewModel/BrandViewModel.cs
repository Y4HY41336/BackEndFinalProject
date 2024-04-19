using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModel
{
    public class BrandViewModel
    {
        [Required]
        public string BrandName { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
