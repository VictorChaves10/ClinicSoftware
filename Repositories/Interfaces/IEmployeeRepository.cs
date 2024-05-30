using ClinicSoftware.Models;

namespace ClinicSoftware.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task AddEmployee(Employee employee);
        Task EditEmployee(Employee employee);
        Task DeleteEmployee(int employeeId);
        Task<Employee> GetEmployeeId(int employeeId);
        IEnumerable<Employee> Employees { get; }
    }
}
