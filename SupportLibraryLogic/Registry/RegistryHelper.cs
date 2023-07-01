using System;
using Microsoft.Win32;

namespace SupportLibrary.Registry
{
    /// <summary>
    /// Helper class for Registry access related tasks.<para/>
    /// For example: Read and Set registry key-values.
    /// </summary>
    public sealed class RegistryHelper : IRegistryHelper
    {
        private const RegistryHive DEFAULT_REG_HIVE = RegistryHive.LocalMachine;
        private const RegistryValueType DEFAULT_VALUE_TYPE = RegistryValueType.String;
        private const string DEFAULT_KEY_PATH = @"SOFTWARE\MSC Argentina\";

        #region GetKeyValue Methods

        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <param name="parameters">Registry parameters</param>
        /// <returns>The value associated with the received parameters.</returns>
        public object GetKeyValue(RegistryHelperParams parameters)
        {
            try
            {
                return GetKeyValue(parameters.RegistryHive, parameters.KeyPath, parameters.KeyName, parameters.ValueName);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <param name="keyName">Registry keyName to retrieve. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to retrieve. For example: 'TestValueString'</param>
        /// <returns>The value associated with the received parameters.</returns>
        public object GetKeyValue(string keyName, string valueName)
        {
            try
            {
                return GetKeyValue(DEFAULT_REG_HIVE, DEFAULT_KEY_PATH, keyName, valueName);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\MSC Argentina\'.</param>
        /// <param name="keyName">Registry keyName to retrieve. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to retrieve. For example: 'TestValueString'</param>
        /// <returns>The value associated with the received parameters.</returns>
        public object GetKeyValue(string keyPath, string keyName, string valueName)
        {
            try
            {
                return GetKeyValue(RegistryHive.LocalMachine, keyPath, keyName, valueName);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <param name="registryHive">Registry hive to the keyName. For example: 'RegistryHive.LocalMachine'.</param>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\MSC Argentina\'.</param>
        /// <param name="keyName">Registry keyName to retrieve. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to retrieve. For example: 'TestValueString'</param>
        /// <returns>The value associated with the received parameters.</returns>
        public object GetKeyValue(RegistryHive registryHive, string keyPath, string keyName, string valueName)
        {
            try
            {
                if (registryHive != RegistryHive.LocalMachine && registryHive != RegistryHive.CurrentUser)
                    throw new ArgumentException(String.Format("The registry hive '{0}' is not supported.", registryHive), "registryHive");

                RegistryKey registryKeyBase = null;
                if (registryHive == RegistryHive.LocalMachine)  { registryKeyBase = Microsoft.Win32.Registry.LocalMachine; }
                if (registryHive == RegistryHive.CurrentUser)   { registryKeyBase = Microsoft.Win32.Registry.CurrentUser; }

                string subKeyPath = keyPath + keyName;
                RegistryKey registryKey = registryKeyBase.OpenSubKey(subKeyPath);
                if (registryKey == null) { throw new RegSubKeyNotFoundException("The registry key does not exist.", subKeyPath); }

                object value = registryKey.GetValue(valueName);
                if (value == null) { throw new RegValueNotFoundException("The registry value does not exist.", subKeyPath, valueName); }

                return value;
            }
            catch (Exception) { throw; }
        }

        #endregion

        #region GetKeyValue<T> Methods

        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="parameters">RegistryHelper parameters</param>
        /// <returns>The T-Type value associated with the received parameters.</returns>
        public T GetKeyValue<T>(RegistryHelperParams parameters)
        {
            try
            {
                object obj = GetKeyValue(parameters.RegistryHive, parameters.KeyPath, parameters.KeyName, parameters.ValueName);
                return (T)Convert.ChangeType(obj, typeof(T)); // (T)obj;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="keyName">Registry keyName to retrieve. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to retrieve. For example: 'TestValueString'</param>
        /// <returns>The T-Type value associated with the received parameters.</returns>
        public T GetKeyValue<T>(string keyName, string valueName)
        {
            try
            {
                object obj = GetKeyValue(DEFAULT_REG_HIVE, DEFAULT_KEY_PATH, keyName, valueName);
                return (T)Convert.ChangeType(obj, typeof(T)); // (T)obj;
            }
            catch (Exception) { throw; }
        }
        
        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\MSC Argentina\'.</param>
        /// <param name="keyName">Registry keyName to retrieve. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to retrieve. For example: 'TestValueString'</param>
        /// <returns>The T-Type value associated with the received parameters.</returns>
        public T GetKeyValue<T>(string keyPath, string keyName, string valueName)
        {
            try
            {
                object obj = GetKeyValue(DEFAULT_REG_HIVE, keyPath, keyName, valueName);
                return (T)Convert.ChangeType(obj, typeof(T)); // (T)obj;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="registryHive">Registry hive to the keyName. For example: 'RegistryHive.LocalMachine'.</param>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\MSC Argentina\'.</param>
        /// <param name="keyName">Registry keyName to retrieve. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to retrieve. For example: 'TestValueString'</param>
        /// <returns>The T-Type value associated with the received parameters.</returns>
        public T GetKeyValue<T>(RegistryHive registryHive, string keyPath, string keyName, string valueName)
        {
            try
            {
                object obj = GetKeyValue(DEFAULT_REG_HIVE, keyPath, keyName, valueName);
                return (T)Convert.ChangeType(obj, typeof(T)); // (T)obj;
            }
            catch (Exception) { throw; }
        }

        #endregion

        #region SetKeyValue Methods

        /// <summary>
        /// Creates a registry key value.
        /// </summary>
        /// <param name="parameters">RegistryHelper parameters</param>
        public void SetKeyValue(RegistryHelperParams parameters)
        {
            try
            {
                SetKeyValue(parameters.RegistryHive, parameters.KeyPath, parameters.KeyName, parameters.ValueType, parameters.ValueName, parameters.ValueData);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Creates a registry key value.
        /// </summary>
        /// <param name="keyName">Registry keyName to create. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to set. For example: 'TestValueString'</param>
        /// <param name="valueData">Registry valueData to set. For example: 'test'</param>
        public void SetKeyValue(string keyName, string valueName, object valueData)
        {
            SetKeyValue(DEFAULT_REG_HIVE, DEFAULT_KEY_PATH, keyName, DEFAULT_VALUE_TYPE, valueName, valueData);
        }

        /// <summary>
        /// Creates a registry key value.
        /// </summary>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\MSC Argentina\'.</param>
        /// <param name="keyName">Registry keyName to create. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to set. For example: 'TestValueString'</param>
        /// <param name="valueData">Registry valueData to set. For example: 'test'</param>
        public void SetKeyValue(string keyPath, string keyName, string valueName, object valueData)
        {
            SetKeyValue(DEFAULT_REG_HIVE, keyPath, keyName, DEFAULT_VALUE_TYPE, valueName, valueData);
        }

        /// <summary>
        /// Creates a registry key value.
        /// </summary>
        /// <param name="registryHive">Registry hive to the keyName. For example: 'RegistryHive.LocalMachine'.</param>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\MSC Argentina\'.</param>
        /// <param name="keyName">Registry keyName to create. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to set. For example: 'TestValueString'</param>
        /// <param name="valueData">Registry valueData to set. For example: 'test'</param>
        public void SetKeyValue(RegistryHive registryHive, string keyPath, string keyName, string valueName, object valueData)
        {
            SetKeyValue(registryHive, keyPath, keyName, DEFAULT_VALUE_TYPE, valueName, valueData);
        }

        /// <summary>
        /// Creates a registry key value.
        /// </summary>
        /// <param name="keyName">Registry keyName to create. For example: 'SupportLibrary'.</param>
        /// <param name="valueType">Registry valueType to set. For example: 'RegistryValueType.String'</param>
        /// <param name="valueName">Registry valueName to set. For example: 'TestValueString'</param>
        /// <param name="valueData">Registry valueData to set. For example: 'test'</param>
        public void SetKeyValue(string keyName, RegistryValueType valueType, string valueName, object valueData)
        {
            SetKeyValue(DEFAULT_REG_HIVE, DEFAULT_KEY_PATH, keyName, valueType, valueName, valueData);
        }

        /// <summary>
        /// Creates a registry key value.
        /// </summary>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\MSC Argentina\'.</param>
        /// <param name="keyName">Registry keyName to create. For example: 'SupportLibrary'.</param>
        /// <param name="valueType">Registry valueType to set. For example: 'RegistryValueType.String'</param>
        /// <param name="valueName">Registry valueName to set. For example: 'TestValueString'</param>
        /// <param name="valueData">Registry valueData to set. For example: 'Test'</param>
        public void SetKeyValue(string keyPath, string keyName, RegistryValueType valueType, string valueName, object valueData)
        {
            SetKeyValue(DEFAULT_REG_HIVE, keyPath, keyName, valueType, valueName, valueData);
        }

        /// <summary>
        /// Creates a registry key value.
        /// </summary>
        /// <param name="registryHive">Registry hive to the keyName. For example: 'RegistryHive.LocalMachine'.</param>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\MSC Argentina\'.</param>
        /// <param name="keyName">Registry keyName to create. For example: 'SupportLibrary'.</param>
        /// <param name="valueType">Registry valueType to set. For example: 'RegistryValueType.String'</param>
        /// <param name="valueName">Registry valueName to set. For example: 'TestValueString'</param>
        /// <param name="valueData">Registry valueData to set. For example: 'Test'</param>
        public void SetKeyValue(RegistryHive registryHive, string keyPath, string keyName, RegistryValueType valueType, string valueName, object valueData)
        {
            try
            {
                if (registryHive != RegistryHive.LocalMachine && registryHive != RegistryHive.CurrentUser)
                    throw new ArgumentException(String.Format("The registry hive '{0}' is not supported.", registryHive), "registryHive");

                if (valueType != RegistryValueType.Binary && valueType != RegistryValueType.Integer &&
                    valueType != RegistryValueType.Long && valueType != RegistryValueType.String)
                    throw new ArgumentException(String.Format("The registry value type '{0}' is not supported.", valueType), "valueType");

                RegistryKey regKey = null;
                if (registryHive == RegistryHive.LocalMachine)  { regKey = Microsoft.Win32.Registry.LocalMachine; }
                if (registryHive == RegistryHive.CurrentUser)   { regKey = Microsoft.Win32.Registry.CurrentUser; }

                RegistryValueKind registryValueKind = RegistryValueKind.Unknown;
                if (valueType == RegistryValueType.Binary)      { registryValueKind = RegistryValueKind.Binary; }
                if (valueType == RegistryValueType.Integer)     { registryValueKind = RegistryValueKind.DWord; }
                if (valueType == RegistryValueType.Long)        { registryValueKind = RegistryValueKind.QWord; }
                if (valueType == RegistryValueType.String)      { registryValueKind = RegistryValueKind.String; }

                RegistryKey registryKey = regKey.CreateSubKey(keyPath + keyName);
                registryKey.SetValue(valueName, valueData, registryValueKind);
            }
            catch (Exception) { throw; }
        }

        #endregion
    }
}
