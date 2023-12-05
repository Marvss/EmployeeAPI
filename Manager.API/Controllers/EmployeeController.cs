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
        public IActionResult CreateEmployee([FromBody] EmployeePostModel employeePostModel)
        {
            try
            {
                // Validar el modelo antes de crear el empleado
               employeeService.ValidateEmployeeData(employeePostModel);

                // Mapea los datos al modelo interno
                var employee = new Employee
                {
                    Name = employeePostModel.Name,
                    DPI = employeePostModel.DPI,
                    DateOfBirth = employeePostModel.DateOfBirth,
                    Gender = employeePostModel.Gender,
                    HireDate = employeePostModel.HireDate,
                    Age = employeePostModel.Age,
                    Address = employeePostModel.Address,
                    NIT = employeePostModel.NIT,
                    DepartmentCode = employeePostModel.DepartmentCode,
                    // EmployeeId no se especifica aquí, ya que es asignado por la base de datos
                    // DepartmentDescription se asignará en la lógica de servicio o repositorio
                };

                // Lógica para guardar el nuevo empleado en la base de datos
                employeeService.AddEmployee(employee);

                // Devuelve una respuesta exitosa
                return Ok(employee);
            }
            catch (ArgumentException ex)
            {
                // Captura las excepciones de validación y devuelve una respuesta 400 Bad Request con el mensaje de error
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] EmployeePostModel employeePostModel)
        {
            var existingEmployee = employeeService.GetEmployeeById(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }
            try
            {
                // Validar el modelo antes de crear el empleado
                employeeService.ValidateEmployeeData(employeePostModel);

                // Mapea los datos al modelo interno
                var employee = new Employee
                {
                    Name = employeePostModel.Name,
                    DPI = employeePostModel.DPI,
                    DateOfBirth = employeePostModel.DateOfBirth,
                    Gender = employeePostModel.Gender,
                    HireDate = employeePostModel.HireDate,
                    Age = employeePostModel.Age,
                    Address = employeePostModel.Address,
                    NIT = employeePostModel.NIT,
                    DepartmentCode = employeePostModel.DepartmentCode,
                    // EmployeeId no se especifica aquí, ya que es asignado por la base de datos
                    // DepartmentDescription se asignará en la lógica de servicio o repositorio
                };

                // Lógica para guardar el nuevo empleado en la base de datos
                employeeService.AddEmployee(employee);

                // Devuelve una respuesta exitosa
                return Ok(employee);
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
