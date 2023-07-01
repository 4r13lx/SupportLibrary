using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

using SupportLibrary.Testing;

namespace SupportLibraryTest
{
    /// <summary>
    /// Testing of Testing namespace classes.
    /// </summary>
    [TestClass]
    public class TestingTests
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "Testing")]
        public void TestHelper_AssertThrows_ValidData()
        {
            // arrange
            Action action1 = () => { throw new Exception("A regular exception."); };
            Action action2 = () => { throw new ApplicationException("A application exception."); };
            Action action3 = () => { throw new ArgumentException("A argument exception."); };
            Action action4 = () => { throw new DivideByZeroException("A division by zero exception."); };
            Action action5 = () => { int a = 10, b = 0, c = a / b; };   // DivideByZeroException

            // act
            Exception exception1 = TestHelper.AssertThrows<Exception>(action1);
            ApplicationException exception2 = TestHelper.AssertThrows<ApplicationException>(action2);
            ArgumentException exception3 = TestHelper.AssertThrows<ArgumentException>(action3);
            DivideByZeroException exception4 = TestHelper.AssertThrows<DivideByZeroException>(action4);
            DivideByZeroException exception5 = TestHelper.AssertThrows<DivideByZeroException>(action5);

            // assert
            Assert.AreEqual("A regular exception.", exception1.Message, "Assert 01");
            Assert.AreEqual("A application exception.", exception2.Message, "Assert 02");
            Assert.AreEqual("A argument exception.", exception3.Message, "Assert 03");
            Assert.AreEqual("A division by zero exception.", exception4.Message, "Assert 04");
            Assert.AreEqual("Attempted to divide by zero.", exception5.Message, "Assert 05"); // exception default message
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Testing")]
        public void TestHelper_AssertThrows_InvalidData_NoException()
        {
            // arrange
            List<Action> lstAction = new List<Action>()
            {
                () => { return; },
                () => { int a = 10, b = 10, c = a / b; },
                () => Substitute.For<IServiceProvider>().ToString(),
            };

            foreach (Action action in lstAction)
            {
                try
                {
                    // act
                    TestHelper.AssertThrows<Exception>(action);

                    // assert
                    Assert.Fail("TestHelper.AssertThrows() lack of exception was not properly validated.");
                }
                catch (AssertFailedExceptionEx ex)  // expected exception
                {
                    Assert.AreEqual(AssertFailedExceptionCause.NoExceptionThrown, ex.AssertFailedExceptionCause);
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.Message);
                }
            }
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Testing")]
        public void TestHelper_AssertThrows_InvalidData_DiferentException()
        {
            // arrange
            List<Action> lstAction = new List<Action>()
            {
                () => { int a = 10, b = 0, c = a / b; },                    // DivideByZeroException
                () => Substitute.For<System.Text.StringBuilder>().Clear()   // TypeLoadException
            };

            foreach (Action action in lstAction)
            {
                try
                {
                    // act
                    TestHelper.AssertThrows<Exception>(action);

                    // assert
                    Assert.Fail("TestHelper.AssertThrows() lack of exception was not properly validated.");
                }
                catch (AssertFailedExceptionEx ex)  // expected exception
                {
                    Assert.AreEqual(AssertFailedExceptionCause.DiferentExceptionThrown, ex.AssertFailedExceptionCause);
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.Message);
                }
            }
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Testing")]
        public void TestHelper_Data_AreEqual_DataRows_ValidData()
        {
            // arrange
            DataTable dataTable1 = new DataTable("Table1");
            dataTable1.Columns.Add("FirstName", Type.GetType("System.String"));
            dataTable1.Columns.Add("LastName", Type.GetType("System.String"));
            dataTable1.Columns.Add("Age", Type.GetType("System.Byte"));

            DataTable dataTable2 = new DataTable("Table2");
            dataTable2.Columns.Add("FirstName", Type.GetType("System.String"));
            dataTable2.Columns.Add("LastName", Type.GetType("System.String"));
            dataTable2.Columns.Add("Age", Type.GetType("System.Byte"));

            DataRow dataRow1 = dataTable1.Rows.Add("Pablo", "Gutierrez", 20);   // table 1
            DataRow dataRow2 = dataTable1.Rows.Add("Pablo", "Gutierrez", 20);   // table 1
            DataRow dataRow3 = dataTable2.Rows.Add("Pablo", "Gutierrez", 20);   // table 2
            DataRow dataRow4 = dataTable2.Rows.Add("Maria", "Lopez", 30);       // table 2

            // act & assert
            TestHelper.Data.AreEqual(dataRow1, dataRow2, "Assert 01");
            TestHelper.Data.AreEqual(dataRow2, dataRow3, "Assert 02");
            TestHelper.Data.AreEqual(dataRow3, dataRow1, "Assert 03");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Testing")]
        public void TestHelper_Data_AreEqual_DataTables_ValidData()
        {
            // arrange
            DataTable dataTable1 = new DataTable("Table1");
            dataTable1.Columns.Add("FirstName", Type.GetType("System.String"));
            dataTable1.Columns.Add("LastName", Type.GetType("System.String"));
            dataTable1.Columns.Add("Age", Type.GetType("System.Byte"));
            dataTable1.Rows.Add("Pablo", "Gutierrez", 20);
            dataTable1.Rows.Add("Maria", "Lopez", 30);

            DataTable dataTable2 = new DataTable("Table2");
            dataTable2.Columns.Add("FirstName", Type.GetType("System.String"));
            dataTable2.Columns.Add("LastName", Type.GetType("System.String"));
            dataTable2.Columns.Add("Age", Type.GetType("System.Byte"));
            dataTable2.Rows.Add("Pablo", "Gutierrez", 20);
            dataTable2.Rows.Add("Maria", "Lopez", 30);

            // act & assert
            TestHelper.Data.AreEqual(dataTable1, dataTable2, "Assert 01");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Testing")]
        public void TestHelper_Data_AreEqual_DataSets_ValidData()
        {
            // arrange
            DataSet dataSet1 = new DataSet("DataSet1");
            DataTable dataTable1 = dataSet1.Tables.Add("Table1");
            dataTable1.Columns.Add("FirstName", Type.GetType("System.String"));
            dataTable1.Columns.Add("LastName", Type.GetType("System.String"));
            dataTable1.Columns.Add("Age", Type.GetType("System.Byte"));
            dataTable1.Rows.Add("Pablo", "Gutierrez", 20);
            dataTable1.Rows.Add("Maria", "Lopez", 30);

            DataSet dataSet2 = new DataSet("DataSet2");
            DataTable dataTable2 = dataSet2.Tables.Add("Table2");
            dataTable2.Columns.Add("FirstName", Type.GetType("System.String"));
            dataTable2.Columns.Add("LastName", Type.GetType("System.String"));
            dataTable2.Columns.Add("Age", Type.GetType("System.Byte"));
            dataTable2.Rows.Add("Pablo", "Gutierrez", 20);
            dataTable2.Rows.Add("Maria", "Lopez", 30);

            // act & assert
            TestHelper.Data.AreEqual(dataSet1, dataSet2, "Assert 01");
        }
    }
}
