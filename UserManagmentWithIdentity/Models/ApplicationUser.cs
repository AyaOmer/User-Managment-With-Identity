using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace UserManagmentWithIdentity.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public byte[]? ProfilePicture { get; set; }
    }
}
