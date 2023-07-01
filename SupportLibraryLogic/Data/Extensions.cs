using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.ComponentModel;

namespace SupportLibrary.Data
{
    /// <summary>
    /// Extension Methods for Data related tasks.<para/>
    /// For example: DataRow, DataTable and DataSet related operations.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts columnName to T-Type. If the value in columnName is DBNull then returns defaultValue.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="dataRow">Object instance to convert.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>ColumnName converted to T-Type.</returns>
        public static T ReplaceDbNull<T>(this DataRow dataRow, string columnName, T defaultValue)
        {
            try
            {
                return DataHelper.ReplaceDbNull<T>(dataRow[columnName], defaultValue);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Converts columnName to T-Type. If the value in columnName is DBNull then throws the received Exception.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="dataRow">Object instance to convert.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="ex">Exception to throw.</param>
        /// <returns>ColumnName converted to T-Type.</returns>
        public static T ThrowIfDbNull<T>(this DataRow dataRow, string columnName, Exception ex)
        {
            try
            {
                return DataHelper.ThrowIfDbNull<T>(dataRow[columnName], ex);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Converts columnName to T-Type. If the value in columnName is DBNull then throws an ApplicationException with the given message.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="dataRow">Object instance to convert.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="errMsg">A message that describes the error.</param>
        /// <returns>ColumnName converted to T-Type.</returns>
        public static T ThrowIfDbNull<T>(this DataRow dataRow, string columnName, string errMsg)
        {
            try
            {
                return DataHelper.ThrowIfDbNull<T>(dataRow[columnName], errMsg);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Converts columnName to T-Type. If the value in columnName is DBNull then throws a Exception of the specified Type.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="dataRow">Object instance to convert.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="exType">Type of the Exception to throw.</param>
        /// <param name="errMsg">A message that describes the error.</param>
        /// <returns>ColumnName converted to T-Type.</returns>
        public static T ThrowIfDbNull<T>(this DataRow dataRow, string columnName, Type exType, string errMsg)
        {
            try
            {
                return DataHelper.ThrowIfDbNull<T>(dataRow[columnName], exType, errMsg);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Converts this collection to DataTable. The DataTable name is set to T-Type name.
        /// </summary>
        /// <typeparam name="T">T-Type of the collection.</typeparam>
        /// <param name="list">Entity collection to convert. For example: 'List&lt;SomeEntity&gt;'.</param>
        /// <param name="propNames">Properties names to map into the DataTable.</param>
        /// <returns>A DataTable with the data from this list.</returns>
        public static DataTable ToDataTable<T>(this IList<T> list, params string[] propNames)
        {
            try
            {
                return DataHelper.ToDataTable<T>(list, propNames);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Converts this collection to DataTable.
        /// </summary>
        /// <typeparam name="T">T-Type of the collection.</typeparam>
        /// <param name="tableName">Name for the resulting DataTable.</param>
        /// <param name="list">Entity collection to convert. For example: 'List&lt;SomeEntity&gt;'.</param>
        /// <param name="propNames">Properties names to map into the DataTable.</param>
        /// <returns>A DataTable with the data from this list.</returns>
        public static DataTable ToDataTable<T>(this IList<T> list, string tableName, params string[] propNames)
        {
            try
            {
                return DataHelper.ToDataTable<T>(tableName, list, propNames);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Converts this primitive-values collection to DataTable. The DataTable name is set to T-Type name.
        /// </summary>
        /// <typeparam name="T">T-Type of the collection.</typeparam>
        /// <param name="list">Primitive-values collection to convert. For example: 'List&lt;int&gt;'.</param>
        /// <param name="columnName">Name for the column on resulting DataTable.</param>
        /// <returns>A DataTable with the values from this list.</returns>
        public static DataTable ToDataTable<T>(this IList<T> list, string columnName)
        {
            try
            {
                return DataHelper.ToDataTable<T>(list, columnName);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Converts this primitive-values collection to DataTable.
        /// </summary>
        /// <typeparam name="T">T-Type of the collection.</typeparam>
        /// <param name="tableName">Name for the resulting DataTable.</param>
        /// <param name="list">Primitive-values collection to convert. For example: 'List&lt;int&gt;'.</param>
        /// <param name="columnName">Name for the column on resulting DataTable.</param>
        /// <returns>A DataTable with the values from this list.</returns>
        public static DataTable ToDataTable<T>(this IList<T> list, string tableName, string columnName)
        {
            try
            {
                return DataHelper.ToDataTable<T>(tableName, list, columnName);
            }
            catch (Exception) { throw; }
        }
    }
}
