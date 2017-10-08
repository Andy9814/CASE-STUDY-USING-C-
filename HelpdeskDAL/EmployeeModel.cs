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


        }

        public Employee GetById(int id)
        {
            Employee selectedEmployees = null;
            try
            {
                HelpdeskContext ctx = new HelpdeskContext();
                selectedEmployees = ctx.Employees.FirstOrDefault(Emp => Emp.Id == id);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;

            }
            return selectedEmployees;
        }


        public List<Employee> GetAll()
        {
            List<Employee> allEmployees = new List<Employee>();
            try
            {
                HelpdeskContext ctx = new HelpdeskContext();
                allEmployees = ctx.Employees.ToList();

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
                HelpdeskContext ctx = new HelpdeskContext();

                ctx.Employees.Add(newEmployee);
                ctx.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;

            }
            return newEmployee.Id;
        }





        public int Update(Employee updateEmployee)
        {
            int EmployeesUpdated = -1;
            try
            {
                HelpdeskContext ctx = new HelpdeskContext();
                Employee currentEmployee = ctx.Employees.FirstOrDefault(stu => stu.Id == updateEmployee.Id);
                ctx.Entry(currentEmployee).CurrentValues.SetValues(updateEmployee);

                EmployeesUpdated = ctx.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;

            }
            return EmployeesUpdated;

        }



        public int delete(int id)
        {
            int EmployeesDeleted = -1;
            try
            {
                HelpdeskContext ctx = new HelpdeskContext();
                Employee currentStudent = ctx.Employees.FirstOrDefault(Emp => Emp.Id == id);
                ctx.Employees.Remove(currentStudent);


                EmployeesDeleted = ctx.SaveChanges();

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
            Employee currentEmployee = null;
            try
            {
              
                HelpdeskContext ctx = new HelpdeskContext();

                currentEmployee = ctx.Employees.FirstOrDefault(emp => emp.LastName == name);


            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return currentEmployee;

        }






    }
}
