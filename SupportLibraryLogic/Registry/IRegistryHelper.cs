using System;

namespace SupportLibrary.Registry
{
    /// <summary>
    /// Helper class for Registry access related tasks.<para/>
    /// For example: Read and Set registry key-values.
    /// </summary>
    public interface IRegistryHelper
    {
        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <param name="parameters">Registry parameters</param>
        /// <returns>The value associated with the received parameters.</returns>
        object GetKeyValue(RegistryHelperParams parameters);

        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <param name="keyName">Registry keyName to retrieve. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to retrieve. For example: 'TestValueString'</param>
        /// <returns>The value associated with the received parameters.</returns>
        object GetKeyValue(string keyName, string valueName);

        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\MSC Argentina\'.</param>
        /// <param name="keyName">Registry keyName to retrieve. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to retrieve. For example: 'TestValueString'</param>
        /// <returns>The value associated with the received parameters.</returns>
        object GetKeyValue(string keyPath, string keyName, string valueName);

        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <param name="registryHive">Registry hive to the keyName. For example: 'RegistryHive.LocalMachine'.</param>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\MSC Argentina\'.</param>
        /// <param name="keyName">Registry keyName to retrieve. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to retrieve. For example: 'TestValueString'</param>
        /// <returns>The value associated with the received parameters.</returns>
        object GetKeyValue(RegistryHive registryHive, string keyPath, string keyName, string valueName);

        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="parameters">RegistryHelper parameters</param>
        /// <returns>The T-Type value associated with the received parameters.</returns>
        T GetKeyValue<T>(RegistryHelperParams parameters);

        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="keyName">Registry keyName to retrieve. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to retrieve. For example: 'TestValueString'</param>
        /// <returns>The T-Type value associated with the received parameters.</returns>
        T GetKeyValue<T>(string keyName, string valueName);

        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\MSC Argentina\'.</param>
        /// <param name="keyName">Registry keyName to retrieve. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to retrieve. For example: 'TestValueString'</param>
        /// <returns>The T-Type value associated with the received parameters.</returns>
        T GetKeyValue<T>(string keyPath, string keyName, string valueName);

        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="registryHive">Registry hive to the keyName. For example: 'RegistryHive.LocalMachine'.</param>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\MSC Argentina\'.</param>
        /// <param name="keyName">Registry keyName to retrieve. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to retrieve. For example: 'TestValueString'</param>
        /// <returns>The T-Type value associated with the received parameters.</returns>
        T GetKeyValue<T>(RegistryHive registryHive, string keyPath, string keyName, string valueName);

        /// <summary>
        /// Creates a registry key value.
        /// </summary>
        /// <param name="parameters">RegistryHelper parameters</param>
        void SetKeyValue(RegistryHelperParams parameters);

        /// <summary>
        /// Creates a registry key value.
        /// </summary>
        /// <param name="keyName">Registry keyName to create. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to set. For example: 'TestValueString'</param>
        /// <param name="valueData">Registry valueData to set. For example: 'test'</param>
        void SetKeyValue(string keyName, string valueName, object valueData);

        /// <summary>
        /// Creates a registry key value.
        /// </summary>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\MSC Argentina\'.</param>
        /// <param name="keyName">Registry keyName to create. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to set. For example: 'TestValueString'</param>
        /// <param name="valueData">Registry valueData to set. For example: 'test'</param>
        void SetKeyValue(string keyPath, string keyName, string valueName, object valueData);

        /// <summary>
        /// Creates a registry key value.
        /// </summary>
        /// <param name="registryHive">Registry hive to the keyName. For example: 'RegistryHive.LocalMachine'.</param>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\MSC Argentina\'.</param>
        /// <param name="keyName">Registry keyName to create. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to set. For example: 'TestValueString'</param>
        /// <param name="valueData">Registry valueData to set. For example: 'test'</param>
        void SetKeyValue(RegistryHive registryHive, string keyPath, string keyName, string valueName, object valueData);

        /// <summary>
        /// Creates a registry key value.
        /// </summary>
        /// <param name="keyName">Registry keyName to create. For example: 'SupportLibrary'.</param>
        /// <param name="valueType">Registry valueType to set. For example: 'RegistryValueType.String'</param>
        /// <param name="valueName">Registry valueName to set. For example: 'TestValueString'</param>
        /// <param name="valueData">Registry valueData to set. For example: 'test'</param>
        void SetKeyValue(string keyName, RegistryValueType valueType, string valueName, object valueData);

        /// <summary>
        /// Creates a registry key value.
        /// </summary>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\MSC Argentina\'.</param>
        /// <param name="keyName">Registry keyName to create. For example: 'SupportLibrary'.</param>
        /// <param name="valueType">Registry valueType to set. For example: 'RegistryValueType.String'</param>
        /// <param name="valueName">Registry valueName to set. For example: 'TestValueString'</param>
        /// <param name="valueData">Registry valueData to set. For example: 'Test'</param>
        void SetKeyValue(string keyPath, string keyName, RegistryValueType valueType, string valueName, object valueData);

        /// <summary>
        /// Creates a registry key value.
        /// </summary>
        /// <param name="registryHive">Registry hive to the keyName. For example: 'RegistryHive.LocalMachine'.</param>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\MSC Argentina\'.</param>
        /// <param name="keyName">Registry keyName to create. For example: 'SupportLibrary'.</param>
        /// <param name="valueType">Registry valueType to set. For example: 'RegistryValueType.String'</param>
        /// <param name="valueName">Registry valueName to set. For example: 'TestValueString'</param>
        /// <param name="valueData">Registry valueData to set. For example: 'Test'</param>
        void SetKeyValue(RegistryHive registryHive, string keyPath, string keyName, RegistryValueType valueType, string valueName, object valueData);
    }
}
