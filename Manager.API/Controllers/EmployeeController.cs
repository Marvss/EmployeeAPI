using Manager.Domain.Models;
using Manager.Service;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employee = employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [HttpGet]
        public ActionResult<List<Employee>> GetAllEmployees()
        {
            var employees = employeeService.GetAllEmployeesWithDepartments();
            return employees;
        }

        [HttpPost]
        public IActionResult InsertEmployee(CreateEmployee employee)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(CreateEmployee employee)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var existingEmployee = employeeService.GetEmployeeById(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            employeeService.DeleteEmployee(id);
            return Ok();
        }
    }
}
