using EmployeeDataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public HttpResponseMessage Get(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var entity = entities.Employees.FirstOrDefault(e=>e.ID == id);

                if (entity != null)
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, "Employee with Id" +  id.ToString() + "not found");
                }
            }
        }

        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(System.Net.HttpStatusCode.Created, employee);
                    message.Headers.Location = new System.Uri(Request.RequestUri + employee.ID.ToString());
                    return message;
                }
            }
            catch (System.Exception ex)
            {

                return Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
