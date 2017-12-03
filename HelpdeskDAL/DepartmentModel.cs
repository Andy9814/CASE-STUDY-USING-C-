using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace HelpdeskDAL
{
    public class DepartmentModel
    {
        // create a repository
        IRepository<Department> repo;
        public DepartmentModel()
        {
            repo = new HelpdeskRepository<Department>();
        }

        // create getall department method
        public List<Department> GetAll()
        {
            List<Department> allDepartments = new List<Department>();

            try
            {
                allDepartments = repo.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allDepartments;
        }

    }
}
