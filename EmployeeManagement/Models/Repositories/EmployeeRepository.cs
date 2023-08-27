using EmployeeManagement.ViewModels;

namespace EmployeeManagement.Models.Repositories
{
    public class EmployeeRepository : ICompanyRepository<Employee>
    {
        public static List<Employee> employees;
        public EmployeeRepository()
        {
            employees = new List<Employee>();
            employees.Add(new Employee() { Id=1,Name="Alaoui", Image="/Images/u1.png",Email="alaoui144@gmail.com",Departement= Departement.IT});
            employees.Add(new Employee() { Id=2,Name="Alami", Image = "/Images/u1.png", Email ="alami@gmail.com",Departement= Departement.Networking});
            employees.Add(new Employee() { Id=3,Name="Razi", Image = "/Images/u1.png", Email ="razi@gmail.com",Departement=Departement.IT });
            employees.Add(new Employee() { Id=4,Name="Charradi", Image = "/Images/u1.png", Email ="charradi@gmail.com",Departement=Departement.HR });
            employees.Add(new Employee() { Id=5,Name="Ouzzani", Image = "/Images/u1.png", Email ="ouzzani@gmail.com",Departement=Departement.Networking });
            employees.Add(new Employee() { Id=6,Name="Lemrani", Image = "/Images/u1.png", Email ="lemrani144@gmail.com",Departement=Departement.DTT });
        }

        public Employee Add(Employee employee)
        {
            employee.Id= employees.Max(emp => emp.Id)+1;
            employee.Image = "/Images/u1.png";
            employees.Add(employee);

            return employee;
        }

     

        public Employee Get(int id)
        {
            return employees.SingleOrDefault(emp => emp.Id == id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return employees;
        }

        public Employee Update(Employee EntityChanges)
        {
            var employee = employees.Find(emp => emp.Id == EntityChanges.Id);
            if (employee != null)
            {
                employee.Name = EntityChanges.Name;
                employee.Image = EntityChanges.Image;
                employee.Email = EntityChanges.Email;
                employee.Departement = EntityChanges.Departement;

            }
            return employee;
        }

        public Employee Delete(int id)
        {
          var employee =  employees.Find(emp => emp.Id == id);
            if (employee != null)
            {
                employees.Remove(employee);
            }
            return employee;
        }
    }
}
