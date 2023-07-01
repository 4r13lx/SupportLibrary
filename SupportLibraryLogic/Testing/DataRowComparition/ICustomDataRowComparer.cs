using System.Data;

namespace SupportLibrary.Testing
{
    /// <summary>
    /// Interface for DataRow comparition.<para/>
    /// </summary>
    /// <typeparam name="TRow">T-Type for the row comparition</typeparam>
    public interface ICustomDataRowComparer<TRow> where TRow : DataRow
    {
        /// <summary>
        /// Compares two DataRow.
        /// </summary>
        /// <param name="x">DataRow to compare</param>
        /// <param name="y">DataRow to compare</param>
        /// <returns>True if are equals</returns>
        bool Equals(TRow x, TRow y);

        /// <summary>
        /// Calculates a DataRow hash.
        /// </summary>
        /// <param name="dataRow">DataRow from which to calculate hash</param>
        /// <returns>Integer with the calculated hash</returns>
        int GetHashCode(TRow dataRow);

        /// <summary>
        /// Gets last comparition error.
        /// </summary>
        /// <returns>String with the error message</returns>
        string GetLastCompareErrorMessage();
    }
}
