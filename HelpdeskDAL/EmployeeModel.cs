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
     
            IRepository<Employee> repo;
            public EmployeeModel()
            {
                repo = new HelpdeskRepository<Employee>();
            }

            public Employee GetByEmial(string name) {
            List<Employee> selectedEmployee = null;

            try
            {
                HelpdeskContext ctx = new HelpdeskContext();

                selectedEmployee = repo.GetByExpression(Emp => Emp.Email == name);
            }
            catch (Exception ex)
            {
                Console.Write("problem in " + GetType().Name + MethodBase.GetCurrentMethod().Name + "  " + ex.Message);
                throw ex;

            }
            return selectedEmployee.FirstOrDefault();


        }



        public Employee GetById(int id)
        {
            List<Employee> selectedEmployees = null;
            try
            {
                HelpdeskContext ctx = new HelpdeskContext();
                selectedEmployees = repo.GetByExpression(Emp => Emp.Id == id);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;

            }
            return selectedEmployees.FirstOrDefault();
        }


        public List<Employee> GetAll()
        {
            List<Employee> allEmployees = new List<Employee>();
            try
            {
                HelpdeskContext ctx = new HelpdeskContext();
                allEmployees = repo.GetAll();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;

            }
            return allEmployees;
        }






        public int Add(Employee newEmployee)
        {
            try
            {
                //HelpdeskContext ctx = new HelpdeskContext();

                //ctx.Employees.Add(newEmployee);
                //ctx.SaveChanges();
                repo.Add(newEmployee);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;

            }
            return newEmployee.Id;
        }





        public /*int*/ UpdateStatus Update(Employee updatedEmployee)
        {
            //int employeesUpdated = -1;
            UpdateStatus opStatus = UpdateStatus.Failed;


            try
            {
                /* HelpdeskContext cty = new HelpdeskContext();
                 Employee currentEmployee = cty.Employees.FirstOrDefault(emp => emp.Id == updatedEmployee.Id);
                cty.Entry(currentEmployee).CurrentValues.SetValues(updatedEmployee);
                employeesUpdated = cty.SaveChanges(); */
                opStatus = repo.Update(updatedEmployee);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return opStatus;
        }





        public int delete(int id)
        {
            int EmployeesDeleted = -1;
            try
            {
                //HelpdeskContext ctx = new HelpdeskContext();
                //Employee currentStudent = ctx.Employees.FirstOrDefault(Emp => Emp.Id == id);
                //ctx.Employees.Remove(currentStudent);


                EmployeesDeleted = repo.Delete(id);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;

            }
            return EmployeesDeleted;
        }


        public Employee getByLastName(string name)
        {
            List<Employee> currentEmployee = null;
            try
            {
              
               // HelpdeskContext ctx = new HelpdeskContext();

                currentEmployee = repo.GetByExpression(emp => emp.LastName == name);


            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return currentEmployee.FirstOrDefault();

        }

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
