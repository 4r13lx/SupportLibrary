using System;
using System.DirectoryServices;

namespace SupportLibrary.ActiveDirectory
{
    /// <summary>
    /// Helper class for an easy access to DirectoryEntry.Properties.<para/>
    /// For example: Read an user email address.
    /// </summary>
    public static class DirectoryEntryHelper
    {
        /// <summary>
        /// Retrieves the certain Property value from the received DirectoryEntry.
        /// </summary>
        /// <param name="directoryEntry">A DirectoryEntry.</param>
        /// <param name="property">Well known property to retrieve. For example: 'Email' or 'PrincipalName'.</param>
        /// <returns>The value found on the given property.</returns>
        public static object GetProperty(DirectoryEntry directoryEntry, DirectoryEntryProperty property)
        {
            try
            {
                return directoryEntry.Properties[property.ToPropertyName()].Value;
            }
            catch (Exception) { throw; }
        }
        
        /// <summary>
        /// Retrieves the certain Property value from the received DirectoryEntry.
        /// </summary>
        /// <typeparam name="TResult">T-Type for the return value.</typeparam>
        /// <param name="directoryEntry">A DirectoryEntry.</param>
        /// <param name="property">Well known property to retrieve. For example: 'Email' or 'PrincipalName'.</param>
        /// <returns>The value found on the given property.</returns>
        public static TResult GetProperty<TResult>(DirectoryEntry directoryEntry, DirectoryEntryProperty property)
        {
            try
            {
                return (TResult)directoryEntry.Properties[property.ToPropertyName()].Value;
            }
            catch (Exception) { throw; }
        }
    }
}
