using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels
{
    public class UpdateUsersRoleViewModel
    {
        public string UserId { get; set; }
     
        public string UserName { get; set; }
       public bool IsSelected { get; set; }

    }

}
