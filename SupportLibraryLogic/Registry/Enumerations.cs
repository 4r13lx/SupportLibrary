using System;
using System.ComponentModel;

namespace SupportLibrary.Registry
{
    /// <summary>
    /// Registry hive to the keyName. For example: 'RegistryHive.LocalMachine'.
    /// </summary>
    [DefaultValue(LocalMachine)]
    public enum RegistryHive
    {
        /// <summary>
        /// Means an unknown or unsupported registry hive.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The registry hive under: 'Computer\HKEY_LOCAL_MACHINE\'.
        /// </summary>
        LocalMachine = 1,

        /// <summary>
        /// The registry hive under: 'Computer\HKEY_CURRENT_USER\'
        /// </summary>
        CurrentUser = 2
    }

    /// <summary>
    /// Registry valueType to set. For example: 'RegistryValueType.String'.
    /// </summary>
    [DefaultValue(String)]
    public enum RegistryValueType
    {
        /// <summary>
        /// Means an unknown or unsupported registry value type.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Means a string registry value type.
        /// </summary>
        String = 1,

        /// <summary>
        /// Means a binary registry value type.
        /// </summary>
        Binary = 2,

        /// <summary>
        /// Means a integer 'DWORD (32-bit)' registry value type.
        /// </summary>
        Integer = 3,

        /// <summary>
        /// Means a long 'QWORD (64-bit)' registry value type.
        /// </summary>
        Long = 4
    }
}
