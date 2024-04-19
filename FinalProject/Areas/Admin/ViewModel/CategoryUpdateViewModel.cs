using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModel
{
    public class CategoryUpdateViewModel
    {
        [Required]
        public string CategoryName { get; set; }
        public IFormFile? Image { get; set; }
    }
}
