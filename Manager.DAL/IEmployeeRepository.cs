using Manager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.DAL
{
    public interface IEmployeeRepository
    {
        Employee GetEmployeeById(int employeeId);
        List<Employee> GetAllEmployeesWithDepartments();
        void InsertEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int employeeId);
    }
}
