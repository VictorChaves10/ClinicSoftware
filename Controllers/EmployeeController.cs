using ClinicSoftware.Models;
using ClinicSoftware.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSoftware.Controllers
{
    public class EmployeeController : Controller
    {
       private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            var employees = _employeeRepository.Employees;

            return View(employees);
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var employee = await _employeeRepository.GetEmployeeId(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {

            if (ModelState.IsValid)
            {
                await _employeeRepository.AddEmployee(employee);

                return RedirectToAction("Index", "Home");
            }

            return View(employee);
        }


        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int id)

        {
            var employee = await _employeeRepository.GetEmployeeId(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }


        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee)
        {

            if (ModelState.IsValid)
            {        
                await _employeeRepository.EditEmployee(employee);
                return RedirectToAction("Details", "Employee", new { id = employee.EmployeeId });
            }

            return View(employee);
        }


        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int Id)
        {

            var employee = await _employeeRepository.GetEmployeeId(Id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }


        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeId(employeeId);

            if (employee == null)
            {
                return NotFound();
            }
            await _employeeRepository.DeleteEmployee(employeeId);

            return RedirectToAction(nameof(Index));
        }

    }
}
