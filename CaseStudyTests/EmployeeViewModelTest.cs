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
        [TestMethod]
        public void EmployeeViewModelUpdateTwiceShouldReturnStaleInt()
        {
            EmployeeViewModel vm1 = new EmployeeViewModel();
            EmployeeViewModel vm2 = new EmployeeViewModel();
            vm1.Lastname = "Span";
            vm2.Lastname = "Span";
            vm1.getByLastName();
            vm2.getByLastName();
            vm1.Phoneno = "(555)555-8358";
            vm2.Phoneno = "(555)555-9779";
            if (vm1.Update() == 1)
            {
                Assert.IsTrue(vm2.Update() == -2);
            }
            else
                Assert.Fail();
        }

    }
}
