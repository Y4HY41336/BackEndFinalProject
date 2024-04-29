using FinalProject.Helpers.Enums;
using FinalProject.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModel
{
    public class UserViewModel
    {
        public AppUser User { get; set; }
        public IList<string> Roles { get; set; } = null!;
    }
}
