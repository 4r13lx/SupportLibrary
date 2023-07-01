using System;

namespace SupportLibrary.Data
{
    /// <summary>
    /// Parameters for SqlHelper class methods.
    /// </summary>
    public sealed class SqlHelperLogParams
    {
        /// <summary>
        /// Flag to set logging On/Off.
        /// </summary>
        public bool LogEnabled { get; private set; }

        /// <summary>
        /// Entity name.
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Entity id.
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Flag to replace EntityId with the returned value from stored procedure.
        /// </summary>
        public bool UseValueFromDB { get; private set; }

        /// <summary>
        /// Constructor. Sets logging Off.
        /// </summary>
        public SqlHelperLogParams()
        {
            this.LogEnabled = false;
            this.EntityName = "";
            this.EntityId = -1;
            this.UseValueFromDB = false;
        }

        /// <summary>
        /// Constructor. Sets logging On.
        /// </summary>
        /// <param name="entityName">Entity name.</param>
        /// <param name="entityId">Entity id.</param>
        /// <param name="useDbEntityId">Flag to replace EntityId with the returned from the Stored.</param>
        public SqlHelperLogParams(string entityName, int entityId, bool useDbEntityId)
        {
            this.LogEnabled = true;
            this.EntityName = entityName;
            this.EntityId = entityId;
            this.UseValueFromDB = useDbEntityId;
        }
    }
}
