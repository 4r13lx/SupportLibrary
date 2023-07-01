using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Reflection;
using System.ComponentModel;

using SupportLibrary.Text;

namespace SupportLibrary.Data
{
    /// <summary>
    /// Helper class for Data related tasks.<para/>
    /// For example: DataRow, DataTable and DataSet related operations, etc.
    /// </summary>
    public static class DataHelper
    {
        /// <summary>
        /// Converts object to T-Type. If object is DBNull then returns defaultValue.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="obj">Object instance to convert.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Object converted to T-Type.</returns>
        public static T ReplaceDbNull<T>(Object obj, T defaultValue)
        {
            try
            {
                if (obj.GetType() == DBNull.Value.GetType())
                    return defaultValue;
                else
                    return (T)obj; // ERROR return (T)Convert.ChangeType(obj, typeof(T));
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Converts object to T-Type. If object is DBNull then throws the received Exception.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="obj">Object to convert.</param>
        /// <param name="exception">Exception to throw.</param>
        /// <returns>Object converted to T-Type.</returns>
        public static T ThrowIfDbNull<T>(Object obj, Exception exception)
        {
            try
            {
                if (exception == null) { throw new ArgumentNullException("exception", "Exception is null."); }

                if (obj.GetType() == DBNull.Value.GetType())
                    throw exception;
                else
                    return (T)obj;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Converts object to T-Type. If object is DBNull then throws an ApplicationException with the given message.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="obj">Object to convert.</param>
        /// <param name="errMsg">A message that describes the error.</param>
        /// <returns>Object converted to T-Type.</returns>
        public static T ThrowIfDbNull<T>(Object obj, String errMsg)
        {
            try
            {
                if (errMsg == null) { throw new ArgumentNullException("errMsg", "ErrorMessage is null."); }
                ApplicationException applicationException = new ApplicationException(errMsg);

                return ThrowIfDbNull<T>(obj, applicationException);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Converts object to T-Type. If object is DBNull then throws a Exception of the specified Type.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="obj">Object to convert.</param>
        /// <param name="exType">Type of the Exception to throw.</param>
        /// <param name="errMsg">A message that describes the error.</param>
        /// <returns>Object converted to T-Type.</returns>
        public static T ThrowIfDbNull<T>(Object obj, Type exType, string errMsg)
        {
            try
            {
                Exception exception = null;

                if (exType != typeof(Exception) && exType.IsSubclassOf(typeof(Exception)) == false)
                    throw new ArgumentException("ExType must be System.Exception or a Type derived from it.", "exType");
                if (errMsg == null)
                    throw new ArgumentNullException("errMsg", "Parameter is null.");

                if (exType == typeof(ArgumentNullException))
                    exception = (Exception)Activator.CreateInstance(exType, "", errMsg);
                else
                    exception = (Exception)Activator.CreateInstance(exType, errMsg);

                return ThrowIfDbNull<T>(obj, exception);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Converts an entity collection to DataTable. The DataTable name is set to T-Type name.
        /// </summary>
        /// <typeparam name="T">T-Type of the collection.</typeparam>
        /// <param name="list">Entity collection to convert. For example: 'List&lt;SomeEntity&gt;'.</param>
        /// <param name="propNames">Properties names to map into the DataTable.</param>
        /// <returns>A DataTable with the data from input list.</returns>
        public static DataTable ToDataTable<T>(IList<T> list, params string[] propNames)
        {
            try
            {
                return ToDataTable<T>(typeof(T).Name, list, propNames);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Converts an entity collection to DataTable.
        /// </summary>
        /// <typeparam name="T">T-Type of the collection.</typeparam>
        /// <param name="tableName">Name for the resulting DataTable.</param>
        /// <param name="list">Entity collection to convert. For example: 'List&lt;SomeEntity&gt;'.</param>
        /// <param name="propNames">Properties names to map into the DataTable.</param>
        /// <returns>A DataTable with the data from input list.</returns>
        public static DataTable ToDataTable<T>(string tableName, IList<T> list, params string[] propNames)
        {
            try
            {
                if (list == null)                       { throw new ArgumentNullException("list", "List is null."); }
                if (propNames.ToList().Contains(null))  { throw new ArgumentNullException("propNames", "PropNames contains a null string."); }
                if (propNames.ToList().Contains(""))    { throw new ArgumentException("PropNames contains an empty string.", "propNames"); }

                DataTable dataTable = new DataTable(tableName);

                PropertyDescriptorCollection sourceProps = TypeDescriptor.GetProperties(typeof(T));
                List<PropertyDescriptor> destinationProps = new List<PropertyDescriptor>();

                // lista ordenada de las propiedades a mapear
                foreach (string propName in propNames)
                {
                    PropertyDescriptor propertyDescriptor = sourceProps.Find(propName, true);

                    // validaciones
                    if (propertyDescriptor == null)
                        throw new ArgumentException(String.Format("The property '{0}' don't exists in '{1}'.", propName, typeof(T)), "propNames");
                    if (propertyDescriptor.PropertyType.IsClass && propertyDescriptor.PropertyType != typeof(String)) // !propertyDescriptor.PropertyType.IsValueType
                        throw new ArgumentException(String.Format("The property '{0}' is invalid, only value-types are allowed.", propName), "propNames");

                    destinationProps.Add(propertyDescriptor);
                }

                // armar la estructura de tabla
                foreach (PropertyDescriptor propertyDescriptor in destinationProps)
                {
                    dataTable.Columns.Add(propertyDescriptor.Name, Nullable.GetUnderlyingType(propertyDescriptor.PropertyType) ?? propertyDescriptor.PropertyType);
                }

                // copiar los datos
                foreach (T item in list)
                {
                    DataRow dataRow = dataTable.NewRow();

                    foreach (PropertyDescriptor propertyDescriptor in destinationProps)
                        dataRow[propertyDescriptor.Name] = propertyDescriptor.GetValue(item) ?? DBNull.Value;

                    dataTable.Rows.Add(dataRow);
                }

                return dataTable;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Converts a primitive-values collection to DataTable. The DataTable name is set to T-Type name.
        /// </summary>
        /// <typeparam name="T">T-Type of the collection.</typeparam>
        /// <param name="list">Primitive-values collection to convert. For example: 'List&lt;int&gt;'.</param>
        /// <param name="columnName">Name for the column on resulting DataTable.</param>
        /// <returns>A DataTable with the values from input list.</returns>
        public static DataTable ToDataTable<T>(IList<T> list, string columnName)
        {
            try
            {
                return ToDataTable<T>(typeof(T).Name, list, columnName);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Converts a primitive-values collection to DataTable.
        /// </summary>
        /// <typeparam name="T">T-Type of the collection.</typeparam>
        /// <param name="tableName">Name for the resulting DataTable.</param>
        /// <param name="list">Primitive-values collection to convert. For example: 'List&lt;int&gt;'.</param>
        /// <param name="columnName">Name for the column on resulting DataTable.</param>
        /// <returns>A DataTable with the values from input list.</returns>
        public static DataTable ToDataTable<T>(string tableName, IList<T> list, string columnName)
        {
            try
            {
                if (list == null)               { throw new ArgumentNullException("list", "List is null."); }
                if (columnName.IsNullOrEmpty()) { throw new ArgumentNullException("columnName", "ColumnName is null or empty."); }

                DataTable dataTable = new DataTable(tableName);

                // armar la estructura de tabla
                dataTable.Columns.Add(columnName, Nullable.GetUnderlyingType(typeof(T).UnderlyingSystemType) ?? typeof(T).UnderlyingSystemType);

                // copiar los datos
                foreach (T item in list)
                {
                    DataRow dataRow = dataTable.NewRow();

                    if (item != null)
                        dataRow[columnName] = item;
                    else
                        dataRow[columnName] = DBNull.Value;

                    dataTable.Rows.Add(dataRow);
                }

                return dataTable;
            }
            catch (Exception) { throw; }
        }
    }
}
