using System;
using System.ComponentModel;

namespace SupportLibrary.ActiveDirectory
{
    /// <summary>
    /// Identifies a well known DirectoryEntry.Property value. For example: 'Email' or 'PrincipalName'.
    /// </summary>
    [DefaultValue(None)]
    public enum DirectoryEntryProperty
    {
        /// <summary>
        /// Means an unknown property.
        /// </summary>
        None = 0,

        /// <summary>
        /// Means the complete name property. For example: 'N Surname IT'.
        /// </summary>
        CompleteName = 1,

        /// <summary>
        /// Means the first name property. For example: 'Name'.
        /// </summary>
        FirstName = 2,

        /// <summary>
        /// Means the last name property. For example: 'Surname'.
        /// </summary>
        LastName = 3,

        /// <summary>
        /// Means the name property. For example: 'N Surname IT'.
        /// </summary>
        Name = 4,

        /// <summary>
        /// Means the display name property. For example: 'N Surname IT'.
        /// </summary>
        DisplayName = 5,

        /// <summary>
        /// Means the description property. For example: 'Surname, FirstName'.
        /// </summary>
        Description = 6,

        /// <summary>
        /// Means the account name property. For example: 'useraccount'.
        /// </summary>
        AccountName = 7,

        /// <summary>
        /// Means the principal name property. For example: 'useraccount@domain.name.com'.
        /// </summary>
        PrincipalName = 8,

        /// <summary>
        /// Means the telephone number property. For example: '11-1000-2000'.
        /// </summary>
        TelephoneNumber = 9,

        /// <summary>
        /// Means the extension number property. For example: '2000'.
        /// </summary>
        ExtensionNumber = 10,

        /// <summary>
        /// Means the email property. For example: 'xxxx.xxxx@domain.name.com'.
        /// </summary>
        Email = 11,

        /// <summary>
        /// Means the email nickname property. For example: 'xxxx.xxxx'.
        /// </summary>
        EmailNickname = 12,

        /// <summary>
        /// Means the home directory path property. For example: '\\SERVERNAME\XXXXX'.
        /// </summary>
        HomeDirectory = 13,

        /// <summary>
        /// Means the home directory drive letter property. For example: 'L:'.
        /// </summary>
        HomeDrive = 14,

        /// <summary>
        /// Means the department 1 property. For example: 'MIS/IT'.
        /// </summary>
        Department1 = 15,

        /// <summary>
        /// Means the department 2 property. For example: 'Information Technology'.
        /// </summary>
        Department2 = 16,

        /// <summary>
        /// Means the department 3 property. For example: 'IT Development'.
        /// </summary>
        Department3 = 17,

        /// <summary>
        /// means the title within the department. For example: 'Analyst'.
        /// </summary>
        Title = 18
    }
}
