using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupportLibrary.Testing;

namespace SupportLibraryTest.Testing
{
    /// <summary>
    /// Testing of Testing namespace classes.
    /// </summary>
    [TestClass]
    public class AssertEx_DataTests
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "Testing")]
        public void AssertEx_Data_AreEqual_DataRows()
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
            AssertEx.Data.AreEqual(dataRow1, dataRow2, "Assert 01");
            AssertEx.Data.AreEqual(dataRow2, dataRow3, "Assert 02");
            AssertEx.Data.AreEqual(dataRow3, dataRow1, "Assert 03");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Testing")]
        public void AssertEx_Data_AreEqual_DataRows_ErrorMEssage()
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

            // act & assert
            /// <summary>
            /// Asserts for Data related operations.
            /// </summary>
            SupportLibrary.Testing.Data AssertExData = new SupportLibrary.Testing.Data(new CustomDataRowComparer<DataRow>());
            AssertExData.AreEqual(dataRow1, dataRow2, "Assert 01");
        }


        [TestMethod, TestPropertyAttribute("Unit Tests", "Testing")]
        public void AssertEx_Data_AreEqual_DataTables()
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
            AssertEx.Data.AreEqual(dataTable1, dataTable2, "Assert 01");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Testing")]
        public void AssertEx_Data_AreEqual_DataSets()
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
            AssertEx.Data.AreEqual(dataSet1, dataSet2, "Assert 01");
        }
    }
}
