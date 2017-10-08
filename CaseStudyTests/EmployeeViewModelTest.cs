using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpDeskViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CaseStudyTests
{

    [TestClass]
   public class EmployeeViewModelTest
    {
        [TestMethod]
        public void getByLastName()
        {

            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Lastname = "Span";
            vm.getByLastName();
            Assert.IsTrue(vm.Firstname.Length > 0);




        }

    }
}
