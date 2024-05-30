using ClinicSoftware.Context;
using ClinicSoftware.Models;
using ClinicSoftware.Repositories.Interfaces;

namespace ClinicSoftware.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        //Get all Employees
        public IEnumerable<Employee> Employees => _context.Employees ?? throw new InvalidOperationException("Não foi encontrado nenhum funcionário.");


        //Add new Employee
        public async Task AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Objeto funcionário não pode ser nulo.");
            }
        
            try
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar adicionar o funcionário.", ex);
            }
        }

        //Edit Employee
        public async Task EditEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Objeto funcionário não pode ser nulo.");
            }

            var existingEmployee = await _context.Employees.FindAsync(employee.EmployeeId) ?? throw new InvalidOperationException("Não foi localizado o funcionário.");

            _context.Entry(existingEmployee).CurrentValues.SetValues(employee);

            await _context.SaveChangesAsync();
        }

        //Delete a Employee
        public async Task DeleteEmployee(int employeeId)
        {
            var existingEmployee = await _context.Employees.FindAsync(employeeId) ?? throw new InvalidOperationException("Não foi localizado o funcionário.");

            _context.Employees.Remove(existingEmployee);

            await _context.SaveChangesAsync();
        }

        //Get Employee by Id
        public async Task<Employee> GetEmployeeId(int employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId) ?? throw new InvalidOperationException("Não foi localizado o funcionário.");

            return employee;
        }
    }
}
