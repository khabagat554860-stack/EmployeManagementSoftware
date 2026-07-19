using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeManagementSoftware
{
    public class Employee
    {
        // From AddEmployee form
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }

        // From Salary1 form
        public decimal BasicSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal Deductions { get; set; }

        // Automatic calculations
        public decimal GrossPay => BasicSalary + Allowances;
        public decimal NetSalary => GrossPay - Deductions;
    }
}
