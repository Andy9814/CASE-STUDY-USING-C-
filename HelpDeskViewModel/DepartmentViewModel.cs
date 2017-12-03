using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//added
using HelpdeskDAL;

//added
using System.Reflection;


namespace HelpdeskViewModels
{
    public class DepartmentViewModel
    {
        private DepartmentModel _model;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Timer { get; set; }

        //constructor
        public DepartmentViewModel()
        {
            _model = new DepartmentModel();
        }

        public List<DepartmentViewModel> GetAll()
        {
            List<DepartmentViewModel> allVms = new List<DepartmentViewModel>();
            try
            {
                List<Department> allDepartments = _model.GetAll();
                foreach (Department dept in allDepartments)
                {
                    DepartmentViewModel deptVm = new DepartmentViewModel();

                    deptVm.Id = dept.Id;
                    deptVm.Name = dept.DepartmentName;
                    deptVm.Timer = Convert.ToBase64String(dept.Timer);
                    allVms.Add(deptVm);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allVms;
        }

    }
}
