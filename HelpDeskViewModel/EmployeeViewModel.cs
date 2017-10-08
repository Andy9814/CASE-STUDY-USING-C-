using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpdeskDAL;
using System.Reflection;


namespace HelpDeskViewModel
{
    public class EmployeeViewModel
    {
        public EmployeeModel _model = new EmployeeModel();
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phoneno { get; set; }
        public string Timer { get; set; }
        public int DepartmentID { get; set; }
        public int ID { get; set; }
        public string staffPicture64 { get; set; }





        public EmployeeViewModel()
        {

        }

        public void getByLastName()
        {
            try
            {
                Employee emp = _model.getByLastName(Lastname);
                Title = emp.Title;
                Firstname = emp.FirstName;
                Lastname = emp.LastName;
                Email = emp.Email;
                Phoneno = emp.PhoneNo;
                ID = emp.Id;
                DepartmentID = emp.DepartmentId;
                if (emp.StaffPicture != null)
                {
                    staffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                }
                Timer = Convert.ToBase64String(emp.Timer);
            }
            catch (Exception ex)
            {
                Lastname = "Not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;

            }

        }
}
}
