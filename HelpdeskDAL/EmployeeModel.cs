using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data.Entity.Infrastructure;
namespace HelpdeskDAL
    
{
    public class EmployeeModel
    {
     // create the repo
            IRepository<Employee> repo;
            public EmployeeModel()
            {
                repo = new HelpdeskRepository<Employee>();
            }


        // create the getbyEmail method 
        public Employee GetByEmail(string email)
        {
            List<Employee> selectedEmployee = null;

            try
            {
                
                selectedEmployee = repo.GetByExpression(emp => emp.Email == email);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedEmployee.FirstOrDefault();
        }


        // create the getbyid method 
        public Employee GetById(int id)
        {
           
            List<Employee> selectedEmployeeId = null;
            try
            {
                selectedEmployeeId = repo.GetByExpression(emp => emp.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedEmployeeId.FirstOrDefault();
        }


        // create  Getall method 
        public List<Employee> GetAll()
        {
            List<Employee> allEmployees = new List<Employee>();

            try
            {
                
                allEmployees = repo.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allEmployees;
        }



        // create the add method 
        public int Add(Employee newEmployee)
        {
            try
            {
                /* HelpdeskContext cty = new HelpdeskContext();
                 cty.Employees.Add(newEmployee);
                 cty.SaveChanges(); */
                repo.Add(newEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return newEmployee.Id;
        }


        //create updaetestatus
        public /*int*/ UpdateStatus Update(Employee updatedEmployee)
        {
            
            UpdateStatus opStatus = UpdateStatus.Failed;


            try
            {
               
                opStatus = repo.Update(updatedEmployee);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return opStatus;
        }




        //create delete
        public int Delete(int id)
        {
            int employeesDeleted = -1;

            try
            {
               
                employeesDeleted = repo.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return employeesDeleted;
        }


        //create teh getbylastname
        public Employee GetByLastname(string name)
        {
           
            List<Employee> selectEmployee = null;

            try
            {
                selectEmployee = repo.GetByExpression(emp => emp.LastName == name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectEmployee.FirstOrDefault();
        }


        // since we dont need the UpdateForConcurrency we gonna delete


        //public UpdateStatus UpdateForConcurrency(Employee updatedEmployee)
        //{
        //    UpdateStatus opStatus = UpdateStatus.Failed;
        //    try
        //    {
        //        HelpdeskContext dbContext = new HelpdeskContext();
        //        Employee currentEmployee = dbContext.Employees.FirstOrDefault(emp => emp.Id == updatedEmployee.Id);
        //        dbContext.Entry(currentEmployee).OriginalValues["Timer"] = updatedEmployee.Timer;
        //        dbContext.Entry(currentEmployee).CurrentValues.SetValues(updatedEmployee);
        //        if (dbContext.SaveChanges() == 1)
        //            opStatus = UpdateStatus.Ok;
        //    }
        //    catch (DbUpdateConcurrencyException dbx)
        //    {
        //        opStatus = UpdateStatus.Stale;
        //        Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + dbx.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
        //        throw ex;
        //    }
        //    return opStatus;
        //}




    }
}
