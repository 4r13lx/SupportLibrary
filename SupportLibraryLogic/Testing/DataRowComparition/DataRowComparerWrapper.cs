using System;
using System.Collections.Generic;
using System.Data;

namespace SupportLibrary.Testing
{
    /// <summary>
    /// Wrapper to adapt System.Data.DataRowComparer to ICustomDataRowComparer.
    /// </summary>
    /// <typeparam name="TRow">T-Type for the row comparition</typeparam>
    public class DataRowComparerWrapper<TRow> : ICustomDataRowComparer<TRow> where TRow : DataRow
    {
        /// <summary>
        /// Gets default DataRowComparer from .NET framework.
        /// </summary>
        protected IEqualityComparer<TRow> dataRowComparer = DataRowComparer<DataRow>.Default;

        /// <summary>
        /// Compares two DataRow.
        /// </summary>
        /// <param name="x">DataRow to compare</param>
        /// <param name="y">DataRow to compare</param>
        /// <returns>True if are equals</returns>
        public bool Equals(TRow x, TRow y)
        {
            return dataRowComparer.Equals(x, y);
        }

        /// <summary>
        /// Calculates a DataRow hash.
        /// </summary>
        /// <param name="dataRow">DataRow from which to calculate hash</param>
        /// <returns>Integer with the calculated hash</returns>
        public int GetHashCode(TRow dataRow)
        {
            return dataRowComparer.GetHashCode(dataRow);
        }

        /// <summary>
        /// Gets last comparition error.
        /// </summary>
        /// <returns>String with the error message</returns>
        public string GetLastCompareErrorMessage()
        {
            return String.Empty;
        }
    }
}
