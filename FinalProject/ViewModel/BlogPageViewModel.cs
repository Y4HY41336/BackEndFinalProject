using FinalProject.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModel
{
    public class BlogPageViewModel
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

        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public IFormFile PosterImage { get; set; } = null!;
        public IFormFile Images { get; set; } = null!;

        public List<Blog> Blogs { get; set; }
    }
}
