using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace UserManagmentWithIdentity.ViewModels
{
    public class RoleFormViewModel
    {
        [System.ComponentModel.DataAnnotations.Required, StringLength(256)]
        public string Name { get; set; }
    }
}
