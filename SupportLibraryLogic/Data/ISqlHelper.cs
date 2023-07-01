using System;
using System.Data;

namespace SupportLibrary.Data
{
    /// <summary>
    /// Interface for SQL related tasks.<para/>
    /// For example: exec a SP, retrieve DataTable and DataSet from a SP, Begin and Commit Transactions.
    /// </summary>
    public interface ISqlHelper
    {
        /// <summary>
        /// Get the application name.
        /// </summary>
        string ApplicationName { get; }

        /// <summary>
        /// Get the flag to use integrated security.
        /// </summary>
        bool IntegratedSecurity { get; }

        /// <summary>
        /// Get the wait time in seconds before terminating the attempt to execute an operation. A value of 0 indicates no wait limit.
        /// </summary>
        int ExecutionTimeout { get; }

        /// <summary>
        /// Get the current ConnectionString. Built from received parameters and windows registry for missing parameters.
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Get the current SP used for logging purposes.
        /// </summary>
        string LogStoreSp { get; }

        /// <summary>
        /// Executes a stored procedure.
        /// </summary>
        /// <param name="stored">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Object array with the parameters for the stored procedure.</param>
        /// <returns>The returned value from stored procedure.</returns>
        int ExecuteStored(string stored, params object[] parameters);

        /// <summary>
        /// Executes a stored procedure and logs the data sended to it. Uses received EntityName, and the SP returned value as EntityId.
        /// </summary>
        /// <param name="entityName">EntityName for logging.</param>
        /// <param name="stored">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Object array with the parameters for the stored procedure.</param>
        /// <returns>The returned value from stored procedure.</returns>
        int ExecuteStoredLog(string entityName, string stored, params object[] parameters);

        /// <summary>
        /// Executes a stored procedure and logs the data sended to it. Uses received EntityName and EntityId.
        /// </summary>
        /// <param name="entityName">EntityName for logging.</param>
        /// <param name="entityId">EntityId for logging.</param>
        /// <param name="stored">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Object array with the parameters for the stored procedure.</param>
        /// <returns>The returned value from stored procedure.</returns>
        int ExecuteStoredLog(string entityName, int entityId, string stored, params object[] parameters);

        /// <summary>
        /// Executes a stored procedure, return the retrieved data as DataTable.
        /// </summary>
        /// <param name="stored">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Object array with the parameters for the stored procedure.</param>
        /// <returns>A DataTable with the first ResulSet returned by the stored procedure.</returns>
        DataTable GetDataTable(string stored, params object[] parameters);

        /// <summary>
        /// Executes a stored procedure, return the retrieved data as DataSet.
        /// </summary>
        /// <param name="stored">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Object array with the parameters for the stored procedure.</param>
        /// <returns>A DataSet with all ResulSets returned by the stored procedure.</returns>
        DataSet GetDataSet(string stored, params object[] parameters);
    }
}
