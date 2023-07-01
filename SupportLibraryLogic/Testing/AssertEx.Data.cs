using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SupportLibrary.Testing
{
    /// <summary>
    /// Helper class for software Testing over Data objects related tasks.<para/>
    /// For example: Assert.AreEqual operations over Data objects for Unit Testing.
    /// </summary>
    public sealed class Data
    {
        ICustomDataRowComparer<DataRow> dataRowComparer;

        /// <summary>
        /// Constructor. Makes use of DataRowComparer from .NET framework.
        /// </summary>
        public Data()
        {
            this.dataRowComparer = new DataRowComparerWrapper<DataRow>();
        }

        /// <summary>
        /// Constructor. Makes use of received CustomDataRowComparer.
        /// </summary>
        /// <param name="dataRowComparer">ICustomDataRowComparer to use for comparitions purposes.</param>
        public Data(ICustomDataRowComparer<DataRow> dataRowComparer)
        {
            this.dataRowComparer = dataRowComparer;
        }

        /// <summary>
        /// Verifies that two specified DataRows are equal. The assertion fails if the objects are not equal.
        /// </summary>
        /// <param name="expected">The first DataRow to compare. This is the DataRow the unit test expects.</param>
        /// <param name="actual">The second DataRow to compare. This is the DataRow the unit test produced.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public void AreEqual(DataRow expected, DataRow actual, string message = "")
        {
            try
            {
                bool result = this.dataRowComparer.Equals(expected, actual);
                Assert.AreEqual(true, result, message + " ." + dataRowComparer.GetLastCompareErrorMessage());
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Verifies that two specified DataTables are equal. The assertion fails if the objects are not equal.
        /// </summary>
        /// <param name="expected">The first DataTable to compare. This is the DataRow the unit test expects.</param>
        /// <param name="actual">The second DataTable to compare. This is the DataRow the unit test produced.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public void AreEqual(DataTable expected, DataTable actual, string message = "")
        {
            try
            {
                Assert.AreEqual(expected.Rows.Count, actual.Rows.Count, String.Format("Row count between expected and actual DataTables is diferent. {0}", message));

                for (int i = 0; i < expected.Rows.Count; i++)
                {
                    bool result = this.dataRowComparer.Equals(expected.Rows[i], actual.Rows[i]);
                    Assert.IsTrue(result, String.Format("Row number '{0}' is diferent between expected and actual DataTables. Detailed error information: {1}. {2}", i, dataRowComparer.GetLastCompareErrorMessage(), message));
                }
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Verifies that two specified DataSets are equal. The assertion fails if the objects are not equal.
        /// </summary>
        /// <param name="expected">The first DataSet to compare. This is the DataRow the unit test expects.</param>
        /// <param name="actual">The second DataSet to compare. This is the DataRow the unit test produced.</param>
        /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
        public void AreEqual(DataSet expected, DataSet actual, string message = "")
        {
            try
            {
                Assert.AreEqual(expected.Tables.Count, actual.Tables.Count, String.Format("Table count between expected and actual DataSets is diferent. {0}", message));

                for (int i = 0; i < expected.Tables.Count; i++)
                {
                    Assert.AreEqual(expected.Tables[i].Rows.Count, actual.Tables[i].Rows.Count, String.Format("Table {0}, row count between expected and actual DataTables is diferent. {1}", i, message));

                    for (int j = 0; j < expected.Tables[i].Rows.Count; j++)
                    {
                        bool result = this.dataRowComparer.Equals(expected.Tables[i].Rows[j], actual.Tables[i].Rows[j]);
                        Assert.IsTrue(result, String.Format("Table {0}, row number '{1}' is diferent between expected and actual DataTables. Detailed error information: {2}. {3}", i, j, dataRowComparer.GetLastCompareErrorMessage(), message));
                    }

                }
            }
            catch (Exception) { throw; }
        }
    }
}
