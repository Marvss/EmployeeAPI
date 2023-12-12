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
        public IActionResult InsertEmployee([FromBody]CreateEmployee createemployee)
        {
            try
            {
                // Mapea los datos al modelo interno
                var employee = new Employee
                {
                    Name = createemployee.Name,
                    DPI = createemployee.DPI,
                    DateOfBirth = createemployee.DateOfBirth,
                    Gender = createemployee.Gender,
                    HireDate = createemployee.HireDate,
                    Age = createemployee.Age,
                    Address = createemployee.Address,
                    NIT = createemployee.NIT,
                    DepartmentCode = createemployee.DepartmentCode,
                    // EmployeeId no se especifica aquí, ya que es asignado por la base de datos
                    // DepartmentDescription se asignará en la lógica de servicio o repositorio
                };

                // Lógica para crear el empleado a través del servicio
                employeeService.InsertEmployee(employee);

                // Devuelve una respuesta exitosa
                return Ok(employee);
            }
            catch (ArgumentException ex)
            {
                // Captura las excepciones de validación y devuelve una respuesta 400 Bad Request con el mensaje de error
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut]
        public IActionResult UpdateEmployee(int employeeId, [FromBody] CreateEmployee createemployee)
        {
            try
            {
                // Verificar si el empleado con el ID proporcionado existe
                var existingEmployee = employeeService.GetEmployeeById(employeeId);

                if (existingEmployee == null)
                {
                    // Si el empleado no existe, devolver un error 404 Not Found
                    return NotFound($"No se encontró un empleado con ID {employeeId}");
                }

                // Mapear los datos actualizados al modelo existente
                existingEmployee.Name = createemployee.Name;
                existingEmployee.DPI = createemployee.DPI;
                existingEmployee.DateOfBirth = createemployee.DateOfBirth;
                existingEmployee.Gender = createemployee.Gender;
                existingEmployee.HireDate = createemployee.HireDate;
                existingEmployee.Age = createemployee.Age;
                existingEmployee.Address = createemployee.Address;
                existingEmployee.NIT = createemployee.NIT;
                existingEmployee.DepartmentCode = createemployee.DepartmentCode;

                // Lógica para actualizar el empleado a través del servicio
                employeeService.UpdateEmployee(existingEmployee);

                // Devuelve una respuesta exitosa
                return Ok(existingEmployee);
            }
            catch (ArgumentException ex)
            {
                // Captura las excepciones de validación y devuelve una respuesta 400 Bad Request con el mensaje de error
                return BadRequest(new { error = ex.Message });
            }
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
