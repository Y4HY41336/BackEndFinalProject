using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModel
{
    public class BrandUpdateViewModel
    {
        [Required]
        public string BrandName { get; set; }
        public IFormFile? Image { get; set; }
    }
}
