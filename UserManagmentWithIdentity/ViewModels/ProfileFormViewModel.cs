using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace UserManagmentWithIdentity.ViewModels
{
    public class ProfileFormViewModel
    {
        public string Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "inCorreCT Name", MinimumLength = 2)]

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "inCorreCT userName")]

        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
