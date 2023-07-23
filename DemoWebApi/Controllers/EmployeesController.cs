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
                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

                if (entity != null)
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, "Employee with Id" + id.ToString() + "not found");
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

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + "not found to delete");
                    }
                    else
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(System.Net.HttpStatusCode.OK);
                    }
                }
            }
            catch (System.Exception ex)
            {

                return Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
            }

        }

        public HttpResponseMessage Put(int id, [FromBody] Employee employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + "not found to update");
                    }
                    else
                    {
                        entity.FirstName = employee.FirstName;
                        entity.LastName = employee.LastName;
                        entity.Salary = employee.Salary;
                        entity.Gender = employee.Gender;
                        entities.SaveChanges();
                        return Request.CreateResponse(System.Net.HttpStatusCode.OK, entity);
                    }

                }
            }
            catch (System.Exception ex)
            {

                return Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
