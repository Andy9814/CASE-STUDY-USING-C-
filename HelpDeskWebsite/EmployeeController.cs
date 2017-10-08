 using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelpDeskViewModel;

namespace HelpDeskWebsite
{
    public class EmployeeController : ApiController
    {
        [Route("api/employee/{name}")]

        public IHttpActionResult Get(string name)
        {
            try
            {
                EmployeeViewModel vm = new EmployeeViewModel();
                vm.Lastname = name;
                vm.getByLastName();
                return Ok(vm);

            }
            catch(Exception ex)
            {
                return BadRequest("Retrieve failded - " + ex.Message);
            }
        }

    }
}
