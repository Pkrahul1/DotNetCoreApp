using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIEmpty.Models
{
    public class EmployeeManagement:IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public EmployeeManagement()
        {
            _employeeList = new List<Employee>()
            {
                new Employee(){Id=1,Name="rahul",Email="r@gmail.com",Dept="SE"},
                new Employee(){Id=2,Name="David",Email="d@gmail.com",Dept="TA"},
                new Employee(){Id=3,Name="Vikas",Email="V@gmail.com",Dept="SSE"}
            };
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == id);
        }
    }
}
