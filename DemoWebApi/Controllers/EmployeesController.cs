using EmployeeDataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DemoWebApi.Controllers
{
    public class EmployeesController : ApiController
    {
        public IEnumerable<Employee> GetEmployees()
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                return entities.Employees.ToList();
            }
        }

        public Employee Get(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                return entities.Employees.FirstOrDefault(e=>e.ID == id);
            }
        }
    }
}
