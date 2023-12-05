using Manager.DAL;
using Manager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Service
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return employeeRepository.GetEmployeeById(employeeId);
        }

        public List<Employee> GetAllEmployeesWithDepartments()
        {
            return employeeRepository.GetAllEmployeesWithDepartments();
        }

        public void AddEmployee(Employee employee)
        {
            ValidateEmployeeData(employeePostModel);
            employeeRepository.InsertEmployee(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            ValidateEmployeeData(employee);
            if (GetEmployeeById(employee.EmployeeId) != null)
            {
                employeeRepository.UpdateEmployee(employee);
            }
            else
            {
                throw new InvalidOperationException("Employee not found.");
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            employeeRepository.DeleteEmployee(employeeId);
        }

        public void ValidateEmployeeData(EmployeePostModel employee)
        {
            if (string.IsNullOrWhiteSpace(employee.Name))
            {
                throw new ArgumentException("El nombre del empleado no puede estar vacío o nulo.", nameof(employee.Name));
            }

            if (employee.DateOfBirth > DateTime.Now || employee.DateOfBirth.Year < 1900)
            {
                throw new ArgumentException("La fecha de nacimiento del empleado no es válida.", nameof(employee.DateOfBirth));
            }

            if (employee.Gender != 'M' && employee.Gender != 'F')
            {
                throw new ArgumentException("El género del empleado debe ser 'M' o 'F'.", nameof(employee.Gender));
            }

            if (employee.HireDate > DateTime.Now)
            {
                throw new ArgumentException("La fecha de ingreso del empleado no puede ser en el futuro.", nameof(employee.HireDate));
            }

            if (string.IsNullOrWhiteSpace(employee.Address))
            {
                throw new ArgumentException("La dirección del empleado no puede estar vacía o nula.", nameof(employee.Address));
            }

            if (employee.DepartmentCode <= 0)
            {
                throw new ArgumentException("El código del departamento debe ser mayor que cero.", nameof(employee.DepartmentCode));
            }
        }

    }
}
