using FinalProject.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModel
{
    public class HeaderViewModel
    {
        public List<BasketItem> ?BasketItem { get; set; }
        public double? TotalPrice { get; set; } = 0;
    }
}
