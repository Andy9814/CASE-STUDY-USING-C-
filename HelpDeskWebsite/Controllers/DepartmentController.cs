using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//added
using HelpdeskViewModels;

namespace HelpdeskWebsite.Controllers
{
    public class DepartmentController : ApiController
    {
        [Route("api/department")]
        public IHttpActionResult GetAll()
        {
            try
            {
                DepartmentViewModel dept = new DepartmentViewModel();
                List<DepartmentViewModel> allDepartments = dept.GetAll();
                return Ok(allDepartments);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }
    }
}
