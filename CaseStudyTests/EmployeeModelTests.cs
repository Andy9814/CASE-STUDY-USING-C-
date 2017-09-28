using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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

        [TestMethod]
        public void EmployeeModelGetAllShouldReturnList()
        {
            EmployeeModel model = new EmployeeModel();
            List<Employee> allEmployees = model.GetAll();
            Assert.IsTrue(allEmployees.Count > 0);
        }

        [TestMethod]
        public void EmployeeModelAddShouldReturnNewId()
        {
            EmployeeModel model = new EmployeeModel();
            Employee newEmployee = new Employee();
            newEmployee.Title = "Mr.";
            newEmployee.FirstName = "Test";
            newEmployee.LastName = "Student";
            newEmployee.Email = "ts@abc.com";
            newEmployee.PhoneNo = "(555)555-5551";
            newEmployee.DepartmentId = 100;
            int newId = model.Add(newEmployee);
            Assert.IsTrue(newId > 0);

        }

        [TestMethod]
        public void EmployeeModelGetByIdShouldReturnList()
        {
            EmployeeModel model = new EmployeeModel();

            Employee someEmployee = model.GetByEmial("ts@abc.com");
            Employee anotherEmployee = model.GetById(someEmployee.Id);

            Assert.IsNotNull(anotherEmployee);
        }


        [TestMethod]
        public void EmployeeModelUpdateShouldReturnNewId()
        {
            EmployeeModel model = new EmployeeModel();
            Employee updateEmployee = model.GetByEmial("ts@abc.com");
            updateEmployee.Email = "ts@xyz.com";

            int EmployeeUpdated = model.Update(updateEmployee);
            Assert.IsTrue(EmployeeUpdated > 0);

        }


        public void EmployeeModelDeleteShouldReturnNewId()
        {
            EmployeeModel model = new EmployeeModel();
            Employee deleteEmployee = model.GetByEmial("ts@xyz.com");

            int EmployeeDeleted = model.delete(deleteEmployee.Id);
            Assert.IsTrue(EmployeeDeleted == 1);

        }




    }


}
