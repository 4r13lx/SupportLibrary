using System;
using System.Data;
using System.Text;

namespace SupportLibrary.Testing
{
    /// <summary>
    /// Custom implementation of ICustomDataRowComparer.
    /// </summary>
    /// <typeparam name="TRow">T-Type for the row comparition</typeparam>
    public class CustomDataRowComparer<TRow> : ICustomDataRowComparer<TRow> where TRow : DataRow
    {
        private string lastCompareErrorMessage;

        /// <summary>
        /// Compares two DataRow.
        /// </summary>
        /// <param name="x">DataRow to compare</param>
        /// <param name="y">DataRow to compare</param>
        /// <returns>True if are equals</returns>
        public bool Equals(TRow x, TRow y)
        {
            StringBuilder errors = new StringBuilder();

            foreach (DataColumn column in x.Table.Columns)
                if (!y.Table.Columns.Contains(column.ColumnName))
                    errors.AppendLine(String.Format("Column {0} in first table is not found in second table.", column.ColumnName));

            foreach (DataColumn column in y.Table.Columns)
                if (!x.Table.Columns.Contains(column.ColumnName))
                    errors.AppendLine(String.Format("Column {0} in second table is not found in first table.", column.ColumnName));

            foreach (DataColumn column in x.Table.Columns)
                if (GetValueHashCode(x[column.ColumnName])!= GetValueHashCode(y[column.ColumnName]))
                    errors.AppendLine(String.Format("Values in column {0} are not equal, first table ({1}), second table({2}).", column.ColumnName, x[column.ColumnName].ToString(), y[column.ColumnName].ToString()));

            this.lastCompareErrorMessage = errors.ToString();
            return !(errors.Length > 0);
        }

        /// <summary>
        /// Calculates a DataRow hash.
        /// </summary>
        /// <param name="dataRow">DataRow from which to calculate hash</param>
        /// <returns>Integer with the calculated hash</returns>
        public int GetHashCode(TRow dataRow)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates a DataRow value hash.
        /// </summary>
        /// <param name="value">Object from which to calculate hash</param>
        /// <returns>Integer with the calculated hash</returns>
        public int GetValueHashCode(Object value)
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Gets last comparition error.
        /// </summary>
        /// <returns>String with the error message</returns>
        public string GetLastCompareErrorMessage()
        {
            return this.lastCompareErrorMessage;
        }
    }
}
