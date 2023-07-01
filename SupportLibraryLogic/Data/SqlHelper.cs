using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;

using SupportLibrary.Text;
using SupportLibrary.WindowsRegistry;

namespace SupportLibrary.Data
{
    /// <summary>
    /// Helper class for SQL related tasks.<para/>
    /// For example: exec a SP, retrieve DataTable and DataSet from a SP, Begin and Commit Transactions.
    /// </summary>
    public sealed class SqlHelper : ISqlHelper
    {
        #region Properties

        /// <summary>
        /// Get the application name.
        /// </summary>
        public string ApplicationName { get; private set; }

        /// <summary>
        /// Get the flag to use integrated security.
        /// </summary>
        public bool IntegratedSecurity { get; private set; }

        /// <summary>
        /// Get the wait time in seconds before terminating the attempt to execute an operation. A value of 0 indicates no wait limit.
        /// </summary>
        public int ExecutionTimeout { get; private set; }
        
        /// <summary>
        /// Get the current ConnectionString. Built from received parameters and windows registry for missing parameters.
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Get the current SP used for logging purposes.
        /// </summary>
        public string LogStoreSp { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor. Retrieves the missing data and setups the ConecctionString.
        /// Optional parameters left in null will be obtained from the Windows Registry.
        /// </summary>
        /// <param name="applicationName">Application name.</param>
        /// <param name="serverName">Server name.</param>
        /// <param name="databaseName">Database name.</param>
        /// <param name="integratedSecurity">Flag to use integrated security.</param>
        /// <param name="executionTimeout">Wait time in seconds before terminating the attempt to execute an operation. A value of 0 indicates no wait limit.</param>
        /// <returns>Returns a connection string for a given ApplicationName.</returns>
        public SqlHelper(string applicationName, string serverName = null, string databaseName = null, bool integratedSecurity = false, int executionTimeout = 0)
        {
            try
            {
                this.ApplicationName = applicationName;
                this.IntegratedSecurity = integratedSecurity;
                this.ExecutionTimeout = executionTimeout;

                this.ConnectionString = this.GetConnectionString(serverName, databaseName);
                this.LogStoreSp = new RegistryHelper().GetKeyValue<string>(applicationName, "LogstoreSp");
            }
            catch (Exception) { throw; }
        }

        #endregion

        #region ExecuteStored Methods

        /// <summary>
        /// Executes a stored procedure.
        /// </summary>
        /// <param name="stored">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Object array with the parameters for the stored procedure.</param>
        /// <returns>The returned value from stored procedure.</returns>
        public int ExecuteStored(string stored, params object[] parameters)
        {
            return ExecuteStoredFull(new SqlHelperLogParams(), stored, parameters);
        }

        /// <summary>
        /// Executes a stored procedure and logs the data sended to it. Uses received EntityName, and the SP returned value as EntityId.
        /// </summary>
        /// <param name="entityName">EntityName for logging.</param>
        /// <param name="stored">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Object array with the parameters for the stored procedure.</param>
        /// <returns>The returned value from stored procedure.</returns>
        public int ExecuteStoredLog(string entityName, string stored, params object[] parameters)
        {
            return ExecuteStoredFull(new SqlHelperLogParams(entityName, -1, true), stored, parameters);
        }

        /// <summary>
        /// Executes a stored procedure and logs the data sended to it. Uses received EntityName and EntityId.
        /// </summary>
        /// <param name="entityName">EntityName for logging.</param>
        /// <param name="entityId">EntityId for logging.</param>
        /// <param name="stored">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Object array with the parameters for the stored procedure.</param>
        /// <returns>The returned value from stored procedure.</returns>
        public int ExecuteStoredLog(string entityName, int entityId, string stored, params object[] parameters)
        {
            return ExecuteStoredFull(new SqlHelperLogParams(entityName, entityId, false), stored, parameters);
        }

        /// <summary>
        /// Executes a stored procedure, with the option to log the data sent to it.
        /// </summary>
        /// <param name="sqlLogParams">SqlHelper log parameters.</param>
        /// <param name="stored">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Object array with the parameters for the stored procedure.</param>
        /// <returns>The returned value from stored procedure.</returns>
        private int ExecuteStoredFull(SqlHelperLogParams sqlLogParams, string stored, params object[] parameters)
        {
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;

            if (sqlLogParams == null)   { throw new ArgumentNullException("sqlLogParams", "SqlLogParams is null."); }
            if (stored.IsNullOrEmpty()) { throw new ArgumentNullException("stored", "Stored is null or empty."); }

            try
            {
                sqlConnection = new SqlConnection(this.ConnectionString);
                sqlConnection.Open();

                // transaction
                if (sqlLogParams.LogEnabled) { sqlTransaction = sqlConnection.BeginTransaction(); }

                SqlCommand sqlCommand = BuildSqlCommand(sqlConnection, sqlTransaction, stored, parameters);
                sqlCommand.ExecuteNonQuery();

                // logging
                if (sqlLogParams.LogEnabled) { LogEvent(sqlLogParams, sqlConnection, sqlCommand, parameters); }
                if (sqlTransaction != null) { sqlTransaction.Commit(); }

                return Convert.ToInt32(sqlCommand.Parameters[0].Value);
            }
            catch (Exception) { if (sqlTransaction != null) { sqlTransaction.Rollback(); } throw; }
            finally { if (sqlConnection.State == ConnectionState.Open) { sqlConnection.Dispose(); } }
        }

        #endregion

        #region GetDataTable & GetDataSet Methods

        /// <summary>
        /// Executes a stored procedure, return the retrieved data as DataTable.
        /// </summary>
        /// <param name="stored">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Object array with the parameters for the stored procedure.</param>
        /// <returns>A DataTable with the first ResulSet returned by the stored procedure.</returns>
        public DataTable GetDataTable(string stored, params object[] parameters)
        {
            SqlConnection sqlConnection = null;
            if (stored.IsNullOrEmpty()) { throw new ArgumentNullException("stored", "Stored is null or empty."); }

            try
            {
                sqlConnection = new SqlConnection(this.ConnectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = BuildSqlCommand(sqlConnection, null, stored, parameters);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception) { throw; }
            finally { if (sqlConnection.State == ConnectionState.Open) { sqlConnection.Dispose(); } }
        }

        /// <summary>
        /// Executes a stored procedure, return the retrieved data as DataSet.
        /// </summary>
        /// <param name="stored">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Object array with the parameters for the stored procedure.</param>
        /// <returns>A DataSet with all ResulSets returned by the stored procedure.</returns>
        public DataSet GetDataSet(string stored, params object[] parameters)
        {
            SqlConnection sqlConnection = null;
            if (stored.IsNullOrEmpty()) { throw new ArgumentNullException("stored", "Stored is null or empty."); }

            try
            {
                sqlConnection = new SqlConnection(this.ConnectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = BuildSqlCommand(sqlConnection, null, stored, parameters);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);

                return dataSet;
            }
            catch (Exception) { throw; }
            finally { if (sqlConnection.State == ConnectionState.Open) { sqlConnection.Dispose(); } }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Retrieves the missing data and builds a ConecctionString.
        /// Optional parameters left in null will be obtained from the Windows Registry.
        /// </summary>
        private string GetConnectionString(string serverName = null, string databaseName = null, string user = null, string p = null)
        {
            try
            {
                // get missing values
                serverName = serverName ?? new RegistryHelper().GetKeyValue<string>(this.ApplicationName, "ServerName");
                databaseName = databaseName ?? new RegistryHelper().GetKeyValue<string>(this.ApplicationName, "DatabaseName");
                user = user ?? new RegistryHelper().GetKeyValue<string>(this.ApplicationName, "UserName");
                p = p ?? new RegistryHelper().GetKeyValue<string>(this.ApplicationName, "UserPwd");

                // build
                if (this.IntegratedSecurity)
                    return String.Format("Data Source={0}; database={1}; Integrated Security=True", serverName, databaseName);
                else
                    return String.Format("Data Source={0}; database={1}; user id={2}; password={3}", serverName, databaseName, user, p);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Builds a SqlCommand from the given SqlConnection, stored procedure and parameters, and optionally SqlTransaction.
        /// </summary>
        private SqlCommand BuildSqlCommand(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string stored, params object[] parameters)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.CommandText = stored;
                sqlCommand.Transaction = sqlTransaction ?? null;

                // fill SqlCommand.Parameters property
                SqlCommandBuilder.DeriveParameters(sqlCommand);

                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i] == null)
                    {
                        sqlCommand.Parameters[i + 1].Value = DBNull.Value;
                    }
                    else if (parameters[i] is DataTable)
                    {
                        sqlCommand.Parameters[i + 1].Value = parameters[i];
                        sqlCommand.Parameters[i + 1].TypeName = FixTypeName(sqlCommand.Parameters[i + 1].TypeName);
                    }
                    else
                    {
                        sqlCommand.Parameters[i + 1].Value = parameters[i];
                    }
                }

                return sqlCommand;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Fix 'SqlCommandBuilder.DeriveParameters()' bug in TypeName property
        /// </summary>
        private string FixTypeName(string typeName)
        {
            try
            {
                // Ej. 'Web.dbo.tt_CntrNumbers' -> 'dbo.tt_CntrNumbers'
                int firstIndex = typeName.IndexOf(".");
                if (firstIndex == -1) { return typeName; }

                int secondIndex = typeName.IndexOf(".", firstIndex + 1);
                if (secondIndex == -1) { return typeName; }

                return typeName.Substring(firstIndex + 1);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Log a Event to database.
        /// </summary>
        private void LogEvent(SqlHelperLogParams sqlLogParams, SqlConnection sqlConnection, SqlCommand sqlCommandOri, object[] parameters)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(sqlCommandOri.CommandText + "|");

                SqlParameterCollection sqlParamsColl = sqlCommandOri.Parameters;
                for (int i = 1; i < sqlParamsColl.Count; i++)
                    stringBuilder.Append(Convert.ToString(sqlParamsColl[i]) + "=" + Convert.ToString(parameters[i - 1]) + "|");

                SqlCommand sqlCommandNew = new SqlCommand();
                sqlCommandNew.CommandType = CommandType.StoredProcedure;
                sqlCommandNew.CommandText = this.LogStoreSp;
                sqlCommandNew.Connection = sqlConnection;
                sqlCommandNew.Transaction = sqlCommandOri.Transaction;

                SqlCommandBuilder.DeriveParameters(sqlCommandNew);
                sqlCommandNew.Parameters[1].Value = sqlLogParams.EntityName;
                sqlCommandNew.Parameters[2].Value = (sqlLogParams.UseValueFromDB) ? Convert.ToInt32(sqlCommandOri.Parameters[0].Value) : sqlLogParams.EntityId;
                sqlCommandNew.Parameters[3].Value = WindowsIdentity.GetCurrent().Name;
                sqlCommandNew.Parameters[4].Value = stringBuilder.ToString();

                sqlCommandNew.ExecuteNonQuery();
            }
            catch (Exception) { throw; }
        }

        #endregion
    }
}
