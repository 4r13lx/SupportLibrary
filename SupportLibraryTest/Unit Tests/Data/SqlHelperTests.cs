using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Data;
using SupportLibrary.Testing;

namespace SupportLibraryTest.Data
{
    /// <summary>
    /// Testing of Data.SQLHelper namespace classes.
    /// Please make sure that resource file 'Resources\SupportLibrary.reg' was previouslly added to windows registry.
    /// Please make sure that resource files 'Resources\*.sql' were previouslly executed on database.
    /// </summary>
    [TestClass]
    public class SqlHelperTests
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "Data.SqlHelper")]
        public void SqlHelper_Constructor()
        {
            // arrange
            string expectedConnString1 = "Data Source=db_servername; database=db_name; user id=db_username; password=db_user_password";
            string expectedConnString2 = "Data Source=SomeServer; database=SomeDatabase; Integrated Security=True";
            string logStoreSp = "sp_SupportLibrary_LogStore";

            // act
            SqlHelper sqlHelper1 = new SqlHelper("SupportLibrary");
            SqlHelper sqlHelper2 = new SqlHelper("SupportLibrary", "SomeServer", "SomeDatabase", true, 60);

            // assert
            Assert.AreEqual("SupportLibrary", sqlHelper1.ApplicationName, "Assert 01");
            Assert.AreEqual(expectedConnString1, sqlHelper1.ConnectionString, "Assert 02");
            Assert.AreEqual(false, sqlHelper1.IntegratedSecurity, "Assert 04");
            Assert.AreEqual(0, sqlHelper1.ExecutionTimeout, "Assert 03");
            Assert.AreEqual(logStoreSp, sqlHelper1.LogStoreSp, "Assert 05");

            Assert.AreEqual("SupportLibrary", sqlHelper2.ApplicationName, "Assert 01");
            Assert.AreEqual(expectedConnString2, sqlHelper2.ConnectionString, "Assert 02");
            Assert.AreEqual(true, sqlHelper2.IntegratedSecurity, "Assert 04");
            Assert.AreEqual(60, sqlHelper2.ExecutionTimeout, "Assert 03");
            Assert.AreEqual(logStoreSp, sqlHelper2.LogStoreSp, "Assert 05");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Data.SqlHelper")]
        public void SqlHelper_ExecuteStored()
        {
            // arrange & act
            int returnValue1 = new SqlHelper("SupportLibrary").ExecuteStored("sp_SupportLibrary_GetData", "");
            int returnValue2 = new SqlHelper("SupportLibrary").ExecuteStored("sp_SupportLibrary_GetData", "", -1);

            // assert
            Assert.AreEqual(1234, returnValue1, "Assert 01");
            Assert.AreEqual(1234, returnValue2, "Assert 02");           
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Data.SqlHelper")]
        public void SqlHelper_ExecuteStoredLog_FullLog()
        {
            // arrange
            DateTime dateTimeStart = DateTime.Now.AddSeconds(-1);

            var tuples = new List<Tuple<string, int, string, string, int>>
            {
                new Tuple<string, int, string, string, int>("MyEntity", 1, "sp_SupportLibrary_GetData", "test1", 1000),
                new Tuple<string, int, string, string, int>("MyEntity", 2, "sp_SupportLibrary_GetData", "test2", 1001)
            };

            // act
            for (int i = 0; i < tuples.Count; i++)
            {
                new SqlHelper("SupportLibrary").ExecuteStoredLog(tuples[i].Item1, tuples[i].Item2, tuples[i].Item3, tuples[i].Item4, tuples[i].Item5);
            }

            DataTable dtActual = new SqlHelper("SupportLibrary").GetDataTable("sp_SupportLibrary_GetLogs", dateTimeStart, tuples.Count);

            // assert
            for (int i = 0; i < tuples.Count; i++)
            {
                // int eventId = Convert.ToInt32(dtActual.Rows[i]["EventID"]);
                string entityName = Convert.ToString(dtActual.Rows[i]["EntityName"]);
                int entityId = Convert.ToInt32(dtActual.Rows[i]["EntityID"]);
                string user = Convert.ToString(dtActual.Rows[i]["User"]);
                DateTime logTime = Convert.ToDateTime(dtActual.Rows[i]["LogTime"]);
                string description = Convert.ToString(dtActual.Rows[i]["Description"]);

                Assert.AreEqual(tuples[i].Item1, entityName, "Assert 01");
                Assert.AreEqual(tuples[i].Item2, entityId, "Assert 02");
                Assert.IsTrue(user.StartsWith(@"DOMAIN\"), "Assert 03");
                Assert.IsTrue(logTime > dateTimeStart, "Assert 04");
                Assert.IsTrue(description.Contains(tuples[i].Item3), "Assert 05");
                Assert.IsTrue(description.Contains(tuples[i].Item4), "Assert 06");
                Assert.IsTrue(description.Contains(tuples[i].Item5.ToString()), "Assert 07");
            }
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Data.SqlHelper")]
        public void SqlHelper_ExecuteStoredLog_LogReturnedValue()
        {
            // arrange
            DateTime dateTimeStart = DateTime.Now.AddSeconds(-1);

            var tuples = new List<Tuple<string, string, string, int>>
            {
                new Tuple<string, string, string, int>("MyEntity", "sp_SupportLibrary_GetData", "test3", 1002),
                new Tuple<string, string, string, int>("MyEntity", "sp_SupportLibrary_GetData", "test4", 1003)
            };

            // act
            for (int i = 0; i < tuples.Count; i++)
            {
                new SqlHelper("SupportLibrary").ExecuteStoredLog(tuples[i].Item1, tuples[i].Item2, tuples[i].Item3, tuples[i].Item4);
            }

            DataTable dtActual = new SqlHelper("SupportLibrary").GetDataTable("sp_SupportLibrary_GetLogs", dateTimeStart, tuples.Count);

            // assert
            for (int i = 0; i < tuples.Count; i++)
            {
                // int eventId = Convert.ToInt32(dtActual.Rows[i]["EventID"]);
                string entityName = Convert.ToString(dtActual.Rows[i]["EntityName"]);
                int entityId = Convert.ToInt32(dtActual.Rows[i]["EntityID"]);
                string user = Convert.ToString(dtActual.Rows[i]["User"]);
                DateTime logTime = Convert.ToDateTime(dtActual.Rows[i]["LogTime"]);
                string description = Convert.ToString(dtActual.Rows[i]["Description"]);

                Assert.AreEqual(tuples[i].Item1, entityName, "Assert 01");
                Assert.AreEqual(1234, entityId, "Assert 02");
                Assert.IsTrue(user.StartsWith(@"DOMAIN\"), "Assert 03");
                Assert.IsTrue(logTime > dateTimeStart, "Assert 04");
                Assert.IsTrue(description.Contains(tuples[i].Item2), "Assert 05");
                Assert.IsTrue(description.Contains(tuples[i].Item3), "Assert 06");
                Assert.IsTrue(description.Contains(tuples[i].Item4.ToString()), "Assert 07");
            }       
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Data.SqlHelper")]
        public void SqlHelper_GetData()
        {
            // arrange
            DataTable dtExpected1 = new DataTable("DataTable1");
            dtExpected1.Columns.Add(new DataColumn("Id", Type.GetType("System.Int32")));
            dtExpected1.Columns.Add(new DataColumn("Column1", Type.GetType("System.String")));
            dtExpected1.Columns.Add(new DataColumn("Column2", Type.GetType("System.String")));
            dtExpected1.Columns.Add(new DataColumn("Column3", Type.GetType("System.String")));
            dtExpected1.Columns.Add(new DataColumn("Column4", Type.GetType("System.String")));
            dtExpected1.Columns.Add(new DataColumn("Column5", Type.GetType("System.String")));
            dtExpected1.Rows.Add(1, "This", "is", "a", "test", "string.");

            DataTable dtExpected2 = new DataTable("DataTable2");
            dtExpected2.Columns.Add(new DataColumn("Id", Type.GetType("System.Int32")));
            dtExpected2.Columns.Add(new DataColumn("Column1", Type.GetType("System.String")));
            dtExpected2.Columns.Add(new DataColumn("Column2", Type.GetType("System.String")));
            dtExpected2.Columns.Add(new DataColumn("Column3", Type.GetType("System.String")));
            dtExpected2.Columns.Add(new DataColumn("Column4", Type.GetType("System.String")));
            dtExpected2.Columns.Add(new DataColumn("Column5", Type.GetType("System.String")));
            dtExpected2.Rows.Add(2, "This", "is", "another", "test", "string.");

            DataSet dsExpected = new DataSet("DataSet");
            dsExpected.Tables.Add(dtExpected1);
            dsExpected.Tables.Add(dtExpected2);

            // act
            DataTable dtActual = new SqlHelper("SupportLibrary").GetDataTable("sp_SupportLibrary_GetData", "");
            DataSet dsActual = new SqlHelper("SupportLibrary").GetDataSet("sp_SupportLibrary_GetData", "");

            // assert
            AssertEx.Data.AreEqual(dtExpected1, dtActual, "Assert 01");
            AssertEx.Data.AreEqual(dsExpected, dsActual, "Assert 02");
        }
    }
}
