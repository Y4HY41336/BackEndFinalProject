using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModel
{
    public class CategoryViewModel
    {
        [Required]
        public string CategoryName { get; set; }
        public IFormFile Image { get; set; }
    }
}
