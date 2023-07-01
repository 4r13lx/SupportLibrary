using System;

namespace SupportLibrary.ActiveDirectory
{
    /// <summary>
    /// Extension Methods for Active Directory related tasks.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts this instance to a valid DirectoryEntry.Properties value name.<para/>
        /// </summary>
        /// <param name="value">Object instance to convert.</param>
        /// <returns>A valid DirectoryEntry.Properties value name.</returns>
        public static string ToPropertyName(this DirectoryEntryProperty value)
        {
            try
            {
                switch (value)
                {
                    case DirectoryEntryProperty.CompleteName:   return "cn";
                    case DirectoryEntryProperty.FirstName:      return "givenName";
                    case DirectoryEntryProperty.LastName:       return "sn";
                    case DirectoryEntryProperty.Name:           return "name";
                    case DirectoryEntryProperty.DisplayName:    return "displayName";
                    case DirectoryEntryProperty.Description:    return "description";
                    case DirectoryEntryProperty.AccountName:    return "sAMAccountName";
                    case DirectoryEntryProperty.PrincipalName:  return "userPrincipalName";
                    case DirectoryEntryProperty.TelephoneNumber: return "telephoneNumber";
                    case DirectoryEntryProperty.ExtensionNumber: return "ipPhone";
                    case DirectoryEntryProperty.Email:          return "mail";
                    case DirectoryEntryProperty.EmailNickname:  return "mailNickname";
                    case DirectoryEntryProperty.HomeDirectory:  return "homeDirectory";
                    case DirectoryEntryProperty.HomeDrive:      return "homeDrive";
                    case DirectoryEntryProperty.Department1:    return "department";
                    case DirectoryEntryProperty.Department2:    return "extensionAttribute6";
                    case DirectoryEntryProperty.Department3:    return "extensionAttribute7";
                    case DirectoryEntryProperty.Title:          return "extensionAttribute8";
                    default: throw new InvalidCastException(String.Format("Convertion failed from enum DirectoryEntryProperty.{0} to a valid DirectoryEntry.Properties value name.", value));
                }
            }
            catch (Exception) { throw; }
        }
    }
}
