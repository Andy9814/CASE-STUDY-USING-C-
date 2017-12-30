using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HelpdeskDAL ;
namespace CaseStudyTests 
{
    [TestClass]
    // creating a class
    public class EmployeeModelTests
    {
        // creating a EmployeeModelGetEmailShouldReturnEmail
        [TestMethod]
        public void EmployeeModelGetEmailShouldReturnEmail()
        {
            EmployeeModel model = new EmployeeModel();
            Employee SomeEmployee = model.GetByEmail("bs@abc.com");
                Assert.IsNotNull(SomeEmployee);
        }

        // creating a EmployeeModelGetEmailShouldNotReturnEmail
        [TestMethod]
        public void EmployeeModelGetEmailShouldNotReturnEmail()
        {
            EmployeeModel model = new EmployeeModel();
            Employee someEmployee = model.GetByEmail("nripdeep@");
                Assert.IsNull(someEmployee);
        }

        // creating a EmployeeModelGetAllShouldReturnList
        [TestMethod]
        public void EmployeeModelGetAllShouldReturnList()
        {
            EmployeeModel model = new EmployeeModel();
            List<Employee> allEmployees = model.GetAll();
            Assert.IsTrue(allEmployees.Count > 0);
        }

        // creating a EmployeeModelAddShouldReturnNewId
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
        // creating a EmployeeModelGetByIdShouldReturnList
        [TestMethod]
        public void EmployeeModelGetByIdShouldReturnList()
        {
            EmployeeModel model = new EmployeeModel();

            Employee someEmployee = model.GetByEmail("ts@abc.com");
            Employee anotherEmployee = model.GetById(someEmployee.Id);

            Assert.IsNotNull(anotherEmployee);
        }

        // creating a EmployeeModelUpdateShouldReturnNewId

        //[TestMethod]
        //public void EmployeeModelUpdateShouldReturnNewId()
        //{
        //    EmployeeModel model = new EmployeeModel();
        //    Employee updateEmployee = model.GetByEmial("ts@abc.com");
        //    updateEmployee.Email = "ts@xyz.com";

        //    int EmployeeUpdated = model.Update(updateEmployee);
        //    Assert.IsTrue(EmployeeUpdated > 0);

        //}

        // creating a EmployeeModelDeleteShouldReturnNewId

        public void EmployeeModelDeleteShouldReturnNewId()
        {
            EmployeeModel model = new EmployeeModel();
            Employee deleteEmployee = model.GetByEmail("ts@xyz.com");

            int EmployeeDeleted = model.Delete(deleteEmployee.Id);
            Assert.IsTrue(EmployeeDeleted == 1);

        }


        // creating a EmployeeModelUpdateTwiceShouldReturnStaleStatus

        [TestMethod]
        public void EmployeeModelUpdateTwiceShouldReturnStaleStatus()
        {
            //    EmployeeModel model = new EmployeeModel();
            //    Employee updateEmployee1 = model.getByLastName("Span");
            //    Employee updateEmployee2 = model.getByLastName("Span");
            //    updateEmployee1.PhoneNo = "(555)555-6666";
            //    if (model.Update(updateEmployee1) == UpdateStatus.Ok)
            //    {
            //        updateEmployee2.PhoneNo = "(555)555-7777";
            //        Assert.IsTrue(model.Update(updateEmployee2) == UpdateStatus.Stale);
            //    }
            //    else
            //        Assert.Fail();
            //}

            EmployeeModel model1 = new EmployeeModel();
            EmployeeModel model2 = new EmployeeModel();

            Employee updateEmployee1 = model1.GetByLastname("Span");
            Employee updateEmployee2 = model2.GetByLastname("Span");
            updateEmployee1.PhoneNo = "(555)555-6556";
            if (model1.Update(updateEmployee1) == UpdateStatus.Ok)
            {
                updateEmployee2.PhoneNo = "(555)555-7777";
                Assert.IsTrue(model2.Update(updateEmployee2) == UpdateStatus.Stale);
            }
            else
                Assert.Fail();
        }

        [TestMethod]
        public void LoadPicsShouldReturnTrue()
        {
            DALUtil util = new DALUtil();
            Assert.IsTrue(util.AddEmployeePicsToDb ());
        }




    }


}
