using EmployeeManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels
{
    public class EmployeeDeleteViewModel: EmployeeCreateViewModel
    {
        public int Id { get; set; }
        public string? ImagPath { get; set;}

    }
}
