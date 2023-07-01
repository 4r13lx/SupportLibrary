using System;
using System.DirectoryServices;

namespace SupportLibrary.ActiveDirectory
{
    /// <summary>
    /// Interface for Active Directory related tasks.<para/>
    /// For example: Read and Save both Text and Binary files, or clean-up old files from a directory.
    /// </summary>
    public interface IActiveDirectoryHelper
    {
        /// <summary>
        /// Gets current path at which to bind on the Active Directory.
        /// </summary>
        string ActiveDirectoryPath { get; }

        /// <summary>
        /// Gets current RevertImpersonation setting.
        /// </summary>
        bool RevertImpersonation { get; }

        /// <summary>
        /// Searchs for a DirectoryEntry that match the given AccountName.
        /// </summary>
        /// <param name="accountName">SAM AccountName to find on Active Directory. For example: 'username' or 'DOMAIN\username'</param>
        /// <returns>The DirectoryEntry found on Active Directory.</returns>
        DirectoryEntry FindDirectoryEntry(string accountName);

        /// <summary>
        /// Searchs for a DirectoryEntry that match the given AccountName, and then retrieves the given PropertyName from it.
        /// </summary>
        /// <param name="accountName">SAM AccountName to find on Active Directory. For example: 'username' or 'DOMAIN\username'</param>
        /// <param name="property">Well known property to retrieve. For example: 'Email' or 'PrincipalName'.</param>
        /// <returns>The value found on the given property.</returns>
        object GetProperty(string accountName, DirectoryEntryProperty property);

        /// <summary>
        /// Searchs for a DirectoryEntry that match the given AccountName, and then retrieves the given PropertyName from it.
        /// </summary>
        /// <param name="accountName">SAM AccountName to find on Active Directory. For example: 'username' or 'DOMAIN\username'</param>
        /// <param name="propertyName">PropertyName to retrieve. For example: 'mail' or 'userPrincipalName'.</param>
        /// <returns>The value found on the given property.</returns>
        object GetProperty(string accountName, string propertyName);

        /// <summary>
        /// Searchs for a DirectoryEntry that match the given AccountName, and then retrieves the given PropertyName from it.
        /// </summary>
        /// <typeparam name="TResult">T-Type for the return value.</typeparam>
        /// <param name="accountName">SAM AccountName to find on Active Directory. For example: 'username' or 'DOMAIN\username'</param>
        /// <param name="property">Well known property to retrieve. For example: 'Email' or 'PrincipalName'.</param>
        /// <returns>The value found on the given property.</returns>
        TResult GetProperty<TResult>(string accountName, DirectoryEntryProperty property);

        /// <summary>
        /// Searchs for a DirectoryEntry that match the given AccountName, and then retrieves the given PropertyName from it.
        /// </summary>
        /// <typeparam name="TResult">T-Type for the return value.</typeparam>
        /// <param name="accountName">SAM AccountName to find on Active Directory. For example: 'username' or 'DOMAIN\username'</param>
        /// <param name="propertyName">PropertyName to retrieve. For example: 'mail' or 'userPrincipalName'.</param>
        /// <returns>The value found on the given property.</returns>
        TResult GetProperty<TResult>(string accountName, string propertyName);
    }
}
