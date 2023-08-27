using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels
{
    public class AccountLoginViewModel
    {
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAdress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remember Me")]
        public Boolean Remember { get; set; }

    }
}
