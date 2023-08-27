using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
               new Employee()
               {
                   Id = 1,
                   Name = "Ibrahim Alaoui",
                   Departement = Departement.IT,
                   Email = "alaoui_ibrahim@gmail.live",
                   Image = "/images/u1.png"
               },
                new Employee()
                {
                    Id = 2,
                    Name = "youness alami",
                    Departement = Departement.Networking,
                    Email = "alami.youness@gmail.com",
                    Image = "/images/u1.png"
                },
                 new Employee()
                 {
                     Id = 3,
                     Name = "rayan ouazzani",
                     Departement = Departement.Networking,
                     Email = "rayan.ouazzani@gmail.com",
                     Image = "/images/u1.png"
                 }
               );
        }
    }
}
