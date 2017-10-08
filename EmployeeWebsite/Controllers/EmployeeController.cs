using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelpDeskViewModels;

namespace EmployeeWebsite.Controllers
{
    public class EmployeeController : ApiController
    {
        [Route ("api/employee/{name}")]
        public IHttpActionResult Get(string name)
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                emp.Lastname = name;
                emp.getByLastName();
                return Ok(emp);
            }
            catch(Exception ex)
            {
                return BadRequest("retrieve failed - " + ex.Message);
            }
        }
    }
}
