using Microsoft.EntityFrameworkCore;
namespace EmployeeManagement.Models.Repositories
{
    public class SqlEmployeeRepository : ICompanyRepository<Employee>
    {
        private readonly AppDbContext _context;

        public SqlEmployeeRepository(AppDbContext context)
        {
            this._context = context;

        }
        public Employee Add(Employee entity)
        {
            _context.Employees.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Employee Delete(int id)
        {
            var employee = _context.Employees.SingleOrDefault(emp=> emp.Id == id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return employee;
        }

        public Employee Get(int id)
        {
            var employee = _context.Employees.SingleOrDefault(emp => emp.Id == id);
            return employee;
        }

        public IEnumerable<Employee> GetAll()
        {
          return  _context.Employees;
        }

        public Employee Update(Employee EntityChanges)
        {
            var employee = _context.Employees.Attach(EntityChanges);
            employee.State = EntityState.Modified;
            _context.SaveChanges();

            return EntityChanges;
        }
    }
}
