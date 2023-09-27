using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels
{
    public class UpdateRoleViewModel
    {
        public string Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [Display(Name="Enter New Role Name")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; }

    }

}
