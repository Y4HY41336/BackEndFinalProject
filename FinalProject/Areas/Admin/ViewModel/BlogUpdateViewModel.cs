using FinalProject.Migrations;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModel
{
    public class BlogUpdateViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string FamousWord { get; set; }
        [Required]
        public string AuthorComment { get; set; }
        [Required]
        public bool isDeleted { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public IFormFile? PosterImage { get; set; } = null!;
        public IFormFile? Image { get; set; } = null!;
    }
}
