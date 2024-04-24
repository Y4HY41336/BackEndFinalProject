namespace FinalProject.ViewModel
{
    public class BlogPageViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }

        public DateTime CreatedDate { get; set; }

        public IFormFile PosterImage { get; set; } = null!;
    }
}
