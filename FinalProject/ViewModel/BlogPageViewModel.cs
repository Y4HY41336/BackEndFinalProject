
using FinalProject.Models;

namespace FinalProject.ViewModel
{
    public class BlogPageViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string FamousWord { get; set; }
        public string AuthorComment { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public IFormFile PosterImage { get; set; } = null!;
        public List<IFormFile> Images { get; set; } = null!;

        public List<Blog> Blogs { get; set; }
    }
}
