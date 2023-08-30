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
        [MinLength(8 ,ErrorMessage = " Password must be with a minimum length of 8 ")]
        public string Password { get; set; }

        [Display(Name ="Remember Me")]
        public Boolean Remember { get; set; }

    }
}
