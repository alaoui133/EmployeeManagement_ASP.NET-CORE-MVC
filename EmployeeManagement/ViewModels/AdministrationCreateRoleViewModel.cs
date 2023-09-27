using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels
{
    public class AdministrationCreateRoleViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        [Display(Name ="Enter the Role Name")]
        
        
        public string RoleName { get; set; }    

    }
}
