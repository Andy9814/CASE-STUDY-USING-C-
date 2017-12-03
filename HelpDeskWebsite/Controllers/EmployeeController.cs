﻿ using System;
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
                EmployeeViewModel emp = new EmployeeViewModel();
                emp.Lastname = name;
                emp.getByLastName();
                return Ok(emp);

            }
            catch(Exception ex)
            {
                return BadRequest("Retrieve failded - " + ex.Message);
            }
        }
        [Route("api/employees")]
        public IHttpActionResult Put(EmployeeViewModel emp)
        {
            try
            {
                int retVal = emp.Update();
                switch (retVal)
                {
                    case 1:
                        return Ok("Employee " + emp.Lastname + " updated");
                    case -1:
                        return Ok("Employee " + emp.Lastname + " not updated");
                    case -2:
                        return Ok("Data is stale for " + emp.Lastname + ", Employee not updated!");
                    default:
                        return Ok("Employee " + emp.Lastname + " not updated");
                }

            }
            catch (Exception ex)
            {
                return BadRequest("Update failed - " + ex.Message);
            }
        }
        [Route("api/employees")]
        public IHttpActionResult GetAll()
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                List<EmployeeViewModel> allEmployees = emp.GetAll();
                return Ok(allEmployees);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }

        [Route("api/employees")]
        public IHttpActionResult Post(EmployeeViewModel emp)
        {
            try
            {
                emp.Add();
                if (emp.Id > 0)
                {
                    return Ok("Employee " + emp.Lastname + " added!");
                }
                else
                {
                    return Ok("Employee " + emp.Lastname + " not added!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Creation failed - Contact Tech Support");
            }
        }
        
        //delte function 
        [Route("api/employees/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                emp.Id = id;

                if (emp.Delete() == 1)
                {
                    return Ok("Employee deleted!");
                }
                else
                {
                    return Ok("Employee not deleted!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Delete failed - Contact Tech Support");
            }
        }
    }
}
