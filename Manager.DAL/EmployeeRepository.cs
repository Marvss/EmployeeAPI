using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Manager.Domain.Models;

namespace Manager.DAL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string connectionString;
        public EmployeeRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_ObtenerEmpleadoPorID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idEmpleado", employeeId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapEmployeeFromReader(reader);
                        }
                        return null;
                    }
                }
            }
        }

        public List<Employee> GetAllEmployeesWithDepartments()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_ObtenerEmpleadosConDepartamento", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(MapEmployeeFromReader(reader));
                        }
                    }
                }
            }
            return employees;
        }

        public void InsertEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_InsertarEmpleado", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SetEmployeeParameters(command, employee);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_ActualizarEmpleado", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idEmpleado", employee.EmployeeId);

                    SetEmployeeParameters(command, employee);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_EliminarEmpleado", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idEmpleado", employeeId);
                    command.ExecuteNonQuery();
                }
            }
        }

        private Employee MapEmployeeFromReader(SqlDataReader reader)
        {
            return new Employee
            {
                EmployeeId = (int)reader["idEmpleado"],
                Name = (string)reader["nombre"],
                DPI = (string)reader["DPI"],
                DateOfBirth = (DateTime)reader["fechaNacimiento"],
                Gender = Convert.ToChar(reader["sexo"]),
                HireDate = (DateTime)reader["fechaIngreso"],
                Age = (int)reader["edad"],
                Address = (string)reader["direccion"],
                NIT = (string)reader["NIT"],
                DepartmentCode = (int)reader["codigoDepartamento"],
            };
        }

        private void SetEmployeeParameters(SqlCommand command, Employee employee)
        {
            command.Parameters.AddWithValue("@nombre", employee.Name);
            command.Parameters.AddWithValue("@DPI", employee.DPI);
            command.Parameters.AddWithValue("@fechaNacimiento", employee.DateOfBirth);
            command.Parameters.AddWithValue("@sexo", employee.Gender);
            command.Parameters.AddWithValue("@fechaIngreso", employee.HireDate);
            command.Parameters.AddWithValue("@direccion", employee.Address);
            command.Parameters.AddWithValue("@NIT", employee.NIT);
            command.Parameters.AddWithValue("@codigoDepartamento", employee.DepartmentCode);
        }
    }
}
