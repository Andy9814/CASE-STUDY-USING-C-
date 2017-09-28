using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace HelpdeskDAL
{
    public class EmployeeModel
    {
        public Employee GetByEmial(string name) {
            Employee selectedEmployee = null;

            try
            {
                HelpdeskContext ctx = new HelpdeskContext();

                selectedEmployee = ctx.Employees.FirstOrDefault(Emp => Emp.Email == name);
            }
            catch (Exception ex)
            {
                Console.Write("problem in " + GetType().Name + MethodBase.GetCurrentMethod().Name + "  " + ex.Message);
                    throw ex;

            }
            return selectedEmployee;


        } }
}
