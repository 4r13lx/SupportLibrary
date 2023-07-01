using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace SupportLibrary.ActiveDirectory
{
    /// <summary>
    /// Helper class for Active Directory related tasks.<para/>
    /// For example: Read email address from an Active Directory user.
    /// </summary>
    public sealed class ActiveDirectoryHelper : IActiveDirectoryHelper
    {
        [DllImport("advapi32.dll")]
        private static extern int RevertToSelf();

        private const string defaultPath = "LDAP://domain.name.com";

        #region Public Properties

        /// <summary>
        /// Gets current path at which to bind on the Active Directory.
        /// </summary>
        public string ActiveDirectoryPath { get; private set; }

        /// <summary>
        /// Gets current RevertImpersonation setting.
        /// </summary>
        public bool RevertImpersonation { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor. Binds this instance to the node in Active Directory Domain Services located at specified path.
        /// </summary>
        /// <param name="path">The path at which to bind on the Active Directory. If no set, "LDAP://domain.name.com" will be used.</param>
        public ActiveDirectoryHelper(string path = defaultPath) : this(path, false)
        {
        }

        /// <summary>
        /// Constructor. Binds this instance to the node in Active Directory Domain Services located at defaultPath.
        /// </summary>
        /// <param name="revertImpersonation">Flag to revert impersonation while accessing the Active Directory.</param>
        public ActiveDirectoryHelper(bool revertImpersonation) : this(defaultPath, revertImpersonation)
        {
        }
        
        /// <summary>
        /// Constructor. Binds this instance to the node in Active Directory Domain Services located at the specified path.
        /// </summary>
        /// <param name="path">The path at which to bind on the Active Directory.</param>
        /// <param name="revertImpersonation">Flag to revert impersonation while accessing the Active Directory.</param>
        public ActiveDirectoryHelper(string path, bool revertImpersonation)
        {
            this.ActiveDirectoryPath = path;
            this.RevertImpersonation = revertImpersonation;
        }

        #endregion

        #region Public FindDirectoryEntry() methods

        /// <summary>
        /// Searchs for a DirectoryEntry that match the given AccountName.
        /// </summary>
        /// <param name="accountName">SAM AccountName to find on Active Directory. For example: 'username' or 'DOMAIN\username'</param>
        /// <returns>The DirectoryEntry found on Active Directory.</returns>
        public DirectoryEntry FindDirectoryEntry(string accountName)
        {
            WindowsIdentity currentUser = null;
            accountName = accountName.ToLower().Replace("DOMAIN\\", "");

            try
            {
                // obtener el usuario con el que esta corriendo el proceso (en ASP.net: Impersonate = true)
                currentUser = WindowsIdentity.GetCurrent();

                if (this.RevertImpersonation && currentUser.ImpersonationLevel == TokenImpersonationLevel.Impersonation)
                {
                    // P/Invoke: revierte la impersonalización desde este punto del código
                    // desde aquí se ejecuta con la identidad base (en ASP.net: Impersonate = false)
                    RevertToSelf();
                }

                // pointer to directory in the server
                DirectoryEntry directoryEntry = new DirectoryEntry(this.ActiveDirectoryPath);

                // seeker & search filter
                DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry);
                directorySearcher.Filter = "(&(objectCategory=person)(objectClass=user)(samaccountname=" + accountName + "))";

                // search on Active Directory
                SearchResultCollection searchResultCollection = directorySearcher.FindAll();
                if (searchResultCollection.Count == 0) { throw new ActiveDirectoryObjectNotFoundException(String.Format("AccountName '{0}' not found.", accountName)); }

                // return
                return searchResultCollection[0].GetDirectoryEntry();
            }
            catch (COMException ex) { throw new DirectoryServicesCOMException(ex.Message); }
            catch (Exception) { throw; }
            finally
            {
                // volver a ejecutar bajo la identidad original del usuario (en ASP.net: Impersonate = true)
                if (this.RevertImpersonation && currentUser.ImpersonationLevel == TokenImpersonationLevel.None) { currentUser.Impersonate(); }
            }
        }

        #endregion

        #region Public GetProperty() methods

        /// <summary>
        /// Searchs for a DirectoryEntry that match the given AccountName, and then retrieves the given PropertyName from it.
        /// </summary>
        /// <param name="accountName">SAM AccountName to find on Active Directory. For example: 'username' or 'DOMAIN\username'</param>
        /// <param name="propertyName">PropertyName to retrieve. For example: 'mail' or 'userPrincipalName'.</param>
        /// <returns>The value found on the given property.</returns>
        public object GetProperty(string accountName, string propertyName)
        {
            try
            {
                // search on Active Directory
                DirectoryEntry directoryEntry = FindDirectoryEntry(accountName);
                if (!directoryEntry.Properties.Contains(propertyName)) { throw new ActiveDirectoryObjectNotFoundException(String.Format("PropertyName '{0}' not found.", accountName)); }

                // return
                return directoryEntry.Properties[propertyName].Value;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Searchs for a DirectoryEntry that match the given AccountName, and then retrieves the given PropertyName from it.
        /// </summary>
        /// <param name="accountName">SAM AccountName to find on Active Directory. For example: 'username' or 'DOMAIN\username'</param>
        /// <param name="property">Well known property to retrieve. For example: 'Email' or 'PrincipalName'.</param>
        /// <returns>The value found on the given property.</returns>
        public object GetProperty(string accountName, DirectoryEntryProperty property)
        {
            try
            {
                return GetProperty(accountName, property.ToPropertyName());
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Searchs for a DirectoryEntry that match the given AccountName, and then retrieves the given PropertyName from it.
        /// </summary>
        /// <typeparam name="TResult">T-Type for the return value.</typeparam>
        /// <param name="accountName">SAM AccountName to find on Active Directory. For example: 'username' or 'DOMAIN\username'</param>
        /// <param name="propertyName">PropertyName to retrieve. For example: 'mail' or 'userPrincipalName'.</param>
        /// <returns>The value found on the given property.</returns>
        public TResult GetProperty<TResult>(string accountName, string propertyName)
        {
            try
            {
                return (TResult)GetProperty(accountName, propertyName);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Searchs for a DirectoryEntry that match the given AccountName, and then retrieves the given PropertyName from it.
        /// </summary>
        /// <typeparam name="TResult">T-Type for the return value.</typeparam>
        /// <param name="accountName">SAM AccountName to find on Active Directory. For example: 'username' or 'DOMAIN\username'</param>
        /// <param name="property">Well known property to retrieve. For example: 'Email' or 'PrincipalName'.</param>
        /// <returns>The value found on the given property.</returns>
        public TResult GetProperty<TResult>(string accountName, DirectoryEntryProperty property)
        {
            try
            {
                return (TResult)GetProperty(accountName, property.ToPropertyName());
            }
            catch (Exception) { throw; }
        }

        #endregion
    }
}
