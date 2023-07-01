using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Data;
using SupportLibrary.Testing;
using SupportLibraryTest.Entities;

namespace SupportLibraryTest.Data
{
    /// <summary>
    /// Testing of Data.DataHelper namespace classes.
    /// </summary>
    [TestClass]
    public class DataHelperTests
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "Data.DataHelper")]
        public void DataHelper_ReplaceDbNull()
        {
            // arrange
            DataTable dataTable = new DataTable("Table");
            dataTable.Columns.Add("StringColumn", Type.GetType("System.String"));
            dataTable.Columns.Add("IntColumn", Type.GetType("System.Int32"));
            dataTable.Columns.Add("LongColumn", Type.GetType("System.Int64"));
            dataTable.Columns.Add("FloatColumn", Type.GetType("System.Single"));
            dataTable.Columns.Add("DoubleColumn", Type.GetType("System.Double"));
            dataTable.Columns.Add("DecimalColumn", Type.GetType("System.Decimal"));
            dataTable.Columns.Add("DateColumn", Type.GetType("System.DateTime"));

            DataRow drWithValues = dataTable.Rows.Add("Test", 10, 10L, 10.3F, 10.3D, 10.3M, new DateTime(2016, 12, 31));
            DataRow drWithNulls = dataTable.Rows.Add(null, null, null, null, null, null, null);

            // act
            int valueInt = DataHelper.ReplaceDbNull<int>(drWithValues["IntColumn"], -1);
            long valueLong = DataHelper.ReplaceDbNull<long>(drWithValues["LongColumn"], -1L);
            float valueFloat = DataHelper.ReplaceDbNull<float>(drWithValues["FloatColumn"], -1F);
            double valueDouble = DataHelper.ReplaceDbNull<double>(drWithValues["DoubleColumn"], -1.0D);
            decimal valueDecimal = DataHelper.ReplaceDbNull<decimal>(drWithValues["DecimalColumn"], -1.0M);
            string valueString = DataHelper.ReplaceDbNull<string>(drWithValues["StringColumn"], "");
            DateTime valueDate = DataHelper.ReplaceDbNull<DateTime>(drWithValues["DateColumn"], DateTime.MinValue);
            int? valueNullableInt = DataHelper.ReplaceDbNull<int?>(drWithValues["IntColumn"], null);
            DateTime? ValueNullableDate = DataHelper.ReplaceDbNull<DateTime?>(drWithValues["DateColumn"], null);

            int defaultInt = DataHelper.ReplaceDbNull<int>(drWithNulls["IntColumn"], -1);
            long defaultLong = DataHelper.ReplaceDbNull<long>(drWithNulls["LongColumn"], -1L);
            float defaultFloat = DataHelper.ReplaceDbNull<float>(drWithNulls["FloatColumn"], -1F);
            double defaultDouble = DataHelper.ReplaceDbNull<double>(drWithNulls["DoubleColumn"], -1.0D);
            decimal defaultDecimal = DataHelper.ReplaceDbNull<decimal>(drWithNulls["DecimalColumn"], -1.0M);
            string defaultStringEmpty = DataHelper.ReplaceDbNull<string>(drWithNulls["StringColumn"], "");
            string defaultStringNull = DataHelper.ReplaceDbNull<string>(drWithNulls["StringColumn"], null);
            DateTime defaultDate = DataHelper.ReplaceDbNull<DateTime>(drWithNulls["DateColumn"], DateTime.MinValue);
            int? defaultNullableInt = DataHelper.ReplaceDbNull<int?>(drWithNulls["IntColumn"], null);
            DateTime? defaultNullableDate = DataHelper.ReplaceDbNull<DateTime?>(drWithNulls["DateColumn"], null);

            // assert
            Assert.AreEqual(10, valueInt, "Assert 01");
            Assert.AreEqual(10L, valueLong, "Assert 02");
            Assert.AreEqual(10.3F, valueFloat, "Assert 03");
            Assert.AreEqual(10.3D, valueDouble, "Assert 04");
            Assert.AreEqual(10.3M, valueDecimal, "Assert 05");
            Assert.AreEqual("Test", valueString, "Assert 06");
            Assert.AreEqual(new DateTime(2016, 12, 31), valueDate, "Assert 07");
            Assert.AreEqual(10, valueNullableInt, "Assert 08");
            Assert.AreEqual(new DateTime(2016, 12, 31), ValueNullableDate, "Assert 09");

            Assert.AreEqual(-1, defaultInt, "Assert 10");
            Assert.AreEqual(-1L, defaultLong, "Assert 11");
            Assert.AreEqual(-1F, defaultFloat, "Assert 12");
            Assert.AreEqual(-1.0D, defaultDouble, "Assert 13");
            Assert.AreEqual(-1.0M, defaultDecimal, "Assert 14");
            Assert.AreEqual("", defaultStringEmpty, "Assert 15");
            Assert.AreEqual(null, defaultStringNull, "Assert 16");
            Assert.AreEqual(DateTime.MinValue, defaultDate, "Assert 17");
            Assert.AreEqual(null, defaultNullableDate, "Assert 18");
            Assert.AreEqual(null, defaultNullableDate, "Assert 19");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Data.DataHelper")]
        public void DataHelper_ThrowIfDbNull()
        {
            // arrange
            DataTable dataTable = new DataTable("Table");
            dataTable.Columns.Add("StringColumn", Type.GetType("System.String"));
            dataTable.Columns.Add("IntColumn", Type.GetType("System.Int32"));
            dataTable.Columns.Add("LongColumn", Type.GetType("System.Int64"));
            dataTable.Columns.Add("FloatColumn", Type.GetType("System.Single"));
            dataTable.Columns.Add("DoubleColumn", Type.GetType("System.Double"));
            dataTable.Columns.Add("DecimalColumn", Type.GetType("System.Decimal"));
            dataTable.Columns.Add("DateColumn", Type.GetType("System.DateTime"));

            DataRow drWithValues = dataTable.Rows.Add("Test", 10, 10L, 10.3F, 10.3D, 10.3M, new DateTime(2016, 12, 31));

            // act
            string stringValue = DataHelper.ThrowIfDbNull<string>(drWithValues["StringColumn"], "El valor es DBNull.");
            int intValue = DataHelper.ThrowIfDbNull<int>(drWithValues["IntColumn"], "El valor es DBNull.");
            long longValue = DataHelper.ThrowIfDbNull<long>(drWithValues["LongColumn"], "El valor es DBNull.");
            float floatValue = DataHelper.ThrowIfDbNull<float>(drWithValues["FloatColumn"], "El valor es DBNull.");
            double doubleValue = DataHelper.ThrowIfDbNull<double>(drWithValues["DoubleColumn"], "El valor es DBNull.");
            decimal decimalValue = DataHelper.ThrowIfDbNull<decimal>(drWithValues["DecimalColumn"], "El valor es DBNull.");
            DateTime dateValue = DataHelper.ThrowIfDbNull<DateTime>(drWithValues["DateColumn"], "El valor es DBNull.");

            // assert
            Assert.AreEqual("Test", stringValue, "Assert 01");
            Assert.AreEqual(10, intValue, "Assert 02");
            Assert.AreEqual(10L, longValue, "Assert 03");
            Assert.AreEqual(10.3F, floatValue, "Assert 04");
            Assert.AreEqual(10.3D, doubleValue, "Assert 05");
            Assert.AreEqual(10.3M, decimalValue, "Assert 06");
            Assert.AreEqual(new DateTime(2016, 12, 31), dateValue, "Assert 07");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Data.DataHelper")]
        public void DataHelper_ThrowIfDbNull_ThrowUsing_Exception()
        {
            // arrange
            DataTable dataTable = new DataTable("Table");
            dataTable.Columns.Add("StringColumn", Type.GetType("System.String"));
            
            DataRow drWithNulls = dataTable.Rows.Add(DBNull.Value);

            List<Exception> exceptions = new List<Exception>();
            exceptions.Add(new Exception("Mensaje de error 1."));
            exceptions.Add(new ArgumentException("Mensaje de error 2."));
            exceptions.Add(new ArgumentNullException("column1", "Mensaje de error 3."));
            exceptions.Add(new DivideByZeroException("Mensaje de error 4."));
            exceptions.Add(new NullReferenceException("Mensaje de error 5."));
            exceptions.Add(new InvalidCastException(""));

            foreach (Exception exception in exceptions)
            {
                try
                {
                    // act
                    DataHelper.ThrowIfDbNull<string>(drWithNulls["StringColumn"], exception);

                    // assert
                    Assert.Fail("DataHelper.ThrowIfDbNull() parameters were not properly validated.");
                }
                catch (Exception ex)
                {
                    // assert
                    Assert.AreEqual(exception.GetType(), ex.GetType(), "Assert 01");
                    Assert.AreEqual(exception.Message, ex.Message, "Assert 02");
                }
            }
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Data.DataHelper")]
        public void DataHelper_ThrowIfDbNull_ThrowUsing_ErrorMessage()
        {
            // arrange
            DataTable dataTable = new DataTable("Table");
            dataTable.Columns.Add("StringColumn", Type.GetType("System.String"));
            
            DataRow drWithNulls = dataTable.Rows.Add(DBNull.Value);

            List<string> errorMessages = new List<string>();
            errorMessages.Add("Error número 1.");
            errorMessages.Add("Error número 2.");
            errorMessages.Add("");

            foreach (string errorMessage in errorMessages)
            {
                try
                {
                    // act
                    DataHelper.ThrowIfDbNull<string>(drWithNulls["StringColumn"], errorMessage);

                    // assert
                    Assert.Fail("DataRow.ThrowIfDbNull() parameters were not properly validated.");
                }
                catch (Exception ex)
                {
                    // assert
                    Assert.AreEqual(typeof(ApplicationException), ex.GetType(), "Assert 01");
                    Assert.AreEqual(errorMessage, ex.Message, "Assert 02");
                }
            }
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Data.DataHelper")]
        public void DataHelper_ThrowIfDbNull_ThrowUsing_ExceptionType()
        {
            // arrange
            DataTable dataTable = new DataTable("Table");
            dataTable.Columns.Add("StringColumn", Type.GetType("System.String"));
            
            DataRow drWithNulls = dataTable.Rows.Add(DBNull.Value);

            List<Tuple<Type, String>> tuples = new List<Tuple<Type, String>>();
            tuples.Add(Tuple.Create<Type, String>(typeof(Exception), "Mensaje de error 1."));
            tuples.Add(Tuple.Create<Type, String>(typeof(ArgumentException), "Mensaje de error 2."));
            tuples.Add(Tuple.Create<Type, String>(typeof(DivideByZeroException), "Mensaje de error 3."));
            tuples.Add(Tuple.Create<Type, String>(typeof(NullReferenceException), "Mensaje de error 4."));
            tuples.Add(Tuple.Create<Type, String>(typeof(InvalidCastException), ""));

            foreach (Tuple<Type, String> tuple in tuples)
            {
                try
                {
                    // act
                    DataHelper.ThrowIfDbNull<string>(drWithNulls["StringColumn"], tuple.Item1, tuple.Item2);

                    // assert
                    Assert.Fail("DataRow.ThrowIfDbNull() parameters were not properly validated.");
                }
                catch (Exception ex)
                {
                    // assert
                    Assert.AreEqual(tuple.Item1, ex.GetType(), "Assert 01");
                    Assert.AreEqual(tuple.Item2, ex.Message, "Assert 02");
                }
            }
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Data.DataHelper")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DataHelper_ThrowIfDbNull_ThrowUsing_Exception_FailsOnNullArgument()
        {
            // arrange
            DataTable dataTable = new DataTable("Table");
            dataTable.Columns.Add("StringColumn", Type.GetType("System.String"));
            
            DataRow drWithNulls = dataTable.Rows.Add(DBNull.Value);

            // act
            DataHelper.ThrowIfDbNull<string>(drWithNulls["StringColumn"], (Exception)null);     // exception can't be null
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Data.DataHelper")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DataHelper_ThrowIfDbNull_ThrowUsing_ErrorMessage_FailsOnNullArgument()
        {
            // arrange
            DataTable dataTable = new DataTable("Table");
            dataTable.Columns.Add("StringColumn", Type.GetType("System.String"));
            
            DataRow drWithNulls = dataTable.Rows.Add(DBNull.Value);

            // act
            DataHelper.ThrowIfDbNull<string>(drWithNulls["StringColumn"], (String)null);        // error message can't be null
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Data.DataHelper")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DataHelper_ThrowIfDbNull_ThrowUsing_ExceptionType_FailsOnNullArgument()
        {
            // arrange
            DataTable dataTable = new DataTable("Table");
            dataTable.Columns.Add("StringColumn", Type.GetType("System.String"));
            
            DataRow drWithNulls = dataTable.Rows.Add(DBNull.Value);

            // act
            DataHelper.ThrowIfDbNull<string>(drWithNulls["StringColumn"], typeof(DivideByZeroException), null); // error message can't be null
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Data.DataHelper")]
        public void DataHelper_ToDataTable_EntitiesCollection()
        {
            // arrange
            List<Person> lstEntities = new List<Person>()
            {
                new Person() { Id = 1, FirstName = "Pablo", LastName = "Gutierrez", Age = 20, Tall = 1.75F },
                new Person() { Id = 2, FirstName = "Maria", LastName = "Lopez", Age = 30, Tall = 1.65F },
                new Person() { Id = 3, FirstName = "Carlos", LastName = "Gonzalez", Age = 40, Tall = 1.70F }
            };

            DataTable dtExpected = new DataTable("Table");
            dtExpected.Columns.Add("FirstName", Type.GetType("System.String"));
            dtExpected.Columns.Add("LastName", Type.GetType("System.String"));
            dtExpected.Columns.Add("Age", Type.GetType("System.Byte"));
            dtExpected.Rows.Add("Pablo", "Gutierrez", 20);
            dtExpected.Rows.Add("Maria", "Lopez", 30);
            dtExpected.Rows.Add("Carlos", "Gonzalez", 40);

            // act
            DataTable dtActual = DataHelper.ToDataTable<Person>("TestEntity", lstEntities, "FirstName", "LastName", "Age");

            // assert
            AssertEx.Data.AreEqual(dtExpected, dtActual, "Assert 01");
            Assert.AreEqual("TestEntity", dtActual.TableName, "Assert 02");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Data.DataHelper")]
        public void DataHelper_ToDataTable_EntitiesCollection_FailsOnInvalidPropertyName()
        {
            // arrange
            List<Person> lstEntities = new List<Person>()
            {
                new Person() { Id = 1, FirstName = "Pablo", LastName = "Gutierrez", Age = 20, Tall = 1.75F },
                new Person() { Id = 2, FirstName = "Maria", LastName = "Lopez", Age = 30, Tall = 1.65F },
                new Person() { Id = 3, FirstName = "Carlos", LastName = "Gonzalez", Age = 40, Tall = 1.70F }
            };

            Action action1 = () => DataHelper.ToDataTable<Person>(lstEntities, "FirstName", "InvalidPropertyName", "Age");
            Action action2 = () => DataHelper.ToDataTable<Person>(lstEntities, "FirstName", null, "Age");

            // act & assert
            ArgumentException exception1 = AssertEx.Exceptions.Throws<ArgumentException>(action1, "Assert 01");
            Assert.AreEqual("propNames", exception1.ParamName, "Assert 02");

            ArgumentNullException exception2 = AssertEx.Exceptions.Throws<ArgumentNullException>(action2, "Assert 03");
            Assert.AreEqual("propNames", exception2.ParamName, "Assert 04");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Data.DataHelper")]
        public void DataHelper_ToDataTable_ValueTypeCollection()
        {
            // arrange
            List<int> lstInteger = new List<int>() { 2, 5, 9, 7, 2, 3 };
            List<string> lstString = "This is a test string. This is another test string.".Split(' ').ToList();

            DataTable dtExpectedInteger = new DataTable("Int32");
            dtExpectedInteger.Columns.Add("IntColumn", Type.GetType("System.Int32"));
            dtExpectedInteger.Rows.Add(2);
            dtExpectedInteger.Rows.Add(5);
            dtExpectedInteger.Rows.Add(9);
            dtExpectedInteger.Rows.Add(7);
            dtExpectedInteger.Rows.Add(2);
            dtExpectedInteger.Rows.Add(3);

            DataTable dtExpectedString = new DataTable("String");
            dtExpectedString.Columns.Add("StringColumn", Type.GetType("System.String"));
            dtExpectedString.Rows.Add("This");
            dtExpectedString.Rows.Add("is");
            dtExpectedString.Rows.Add("a");
            dtExpectedString.Rows.Add("test");
            dtExpectedString.Rows.Add("string.");
            dtExpectedString.Rows.Add("This");
            dtExpectedString.Rows.Add("is");
            dtExpectedString.Rows.Add("another");
            dtExpectedString.Rows.Add("test");
            dtExpectedString.Rows.Add("string.");
    
            // act
            DataTable dtActualInteger = DataHelper.ToDataTable<int>("TableInt", lstInteger, "IntColumn");
            DataTable dtActualString = DataHelper.ToDataTable<string>(lstString, "StringColumn");

            // assert
            AssertEx.Data.AreEqual(dtExpectedInteger, dtActualInteger, "Assert 01");
            Assert.AreEqual("TableInt", dtActualInteger.TableName, "Assert 02");

            AssertEx.Data.AreEqual(dtExpectedString, dtActualString, "Assert 03");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Data.DataHelper")]
        public void DataHelper_ToDataTable_ValueTypeCollection_FailsOnNullArgument()
        {
            // arrange
            List<int> lstInteger = new List<int>() { 2, 5, 9, 7, 2, 3 };

            Action action1 = () => DataHelper.ToDataTable<int>(null, "IntColumn");
            Action action2 = () => DataHelper.ToDataTable<int>(lstInteger, "");
            Action action3 = () => DataHelper.ToDataTable<int>(lstInteger, (string)null);

            // act & assert
            ArgumentNullException exception1 = AssertEx.Exceptions.Throws<ArgumentNullException>(action1, "Assert 01");
            Assert.AreEqual("list", exception1.ParamName, "Assert 02");

            ArgumentException exception2 = AssertEx.Exceptions.Throws<ArgumentNullException>(action2, "Assert 03");
            Assert.AreEqual("columnName", exception2.ParamName, "Assert 04");

            ArgumentNullException exception3 = AssertEx.Exceptions.Throws<ArgumentNullException>(action3, "Assert 05");
            Assert.AreEqual("columnName", exception3.ParamName, "Assert 06");
        }
    }
}
