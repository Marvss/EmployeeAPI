using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Domain.Models
{
    public class CreateEmployee
    {
        public string Name { get; set; }
        public string DPI { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }
        public DateTime HireDate { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string NIT { get; set; }
        public int DepartmentCode { get; set; }
    }
}
