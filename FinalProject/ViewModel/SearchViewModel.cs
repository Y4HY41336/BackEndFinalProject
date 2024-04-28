using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModel
{
    public class SearchViewModel
    {
        [Required]
        public string SearchTerm { get; set; }
    }
}
