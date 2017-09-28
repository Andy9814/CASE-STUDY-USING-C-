using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HelpdeskDAL ;
namespace CaseStudyTests
{
    [TestClass]
    public class EmployeeModelTests
    {
        [TestMethod]
        public void EmployeeModelGetEmailShouldReturnEmail()
        {
            EmployeeModel model = new EmployeeModel();
            Employee SomeEmployee = model.GetByEmial("bs@abc.com");
                Assert.IsNotNull(SomeEmployee);
        }

        [TestMethod]
        public void EmployeeModelGetEmailShouldNotReturnEmail()
        {
            EmployeeModel model = new EmployeeModel();
            Employee someEmployee = model.GetByEmial("nripdeep@");
                Assert.IsNull(someEmployee);
        }
    }

    
}
