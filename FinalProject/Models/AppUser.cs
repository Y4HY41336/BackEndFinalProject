using Microsoft.AspNetCore.Identity;

namespace FinalProject.Models
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; } = null!;
        public bool IsActive { get; set; }

        public string ProfilePhoto { get; set; } = "default.jpg";
    }
}
