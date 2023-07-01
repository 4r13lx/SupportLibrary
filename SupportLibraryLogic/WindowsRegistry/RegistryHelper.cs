using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace SupportLibrary.WindowsRegistry
{
    /// <summary>
    /// Helper class for Windows Registry access related tasks.<para/>
    /// For example: Read and Set registry key-values.
    /// </summary>
    public sealed class RegistryHelper : IRegistryHelper
    {
        private const RegistryHive DEFAULT_REG_HIVE = RegistryHive.LocalMachine;
        private const RegistryValueType DEFAULT_VALUE_TYPE = RegistryValueType.String;
        private const string DEFAULT_KEY_PATH = @"SOFTWARE\ApplicationName\";

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
                return this.GetKeyValue(parameters.RegistryHive, parameters.KeyPath, parameters.KeyName, parameters.ValueName);
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
                return this.GetKeyValue(DEFAULT_REG_HIVE, DEFAULT_KEY_PATH, keyName, valueName);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\ApplicationName\'.</param>
        /// <param name="keyName">Registry keyName to retrieve. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to retrieve. For example: 'TestValueString'</param>
        /// <returns>The value associated with the received parameters.</returns>
        public object GetKeyValue(string keyPath, string keyName, string valueName)
        {
            try
            {
                return this.GetKeyValue(RegistryHive.LocalMachine, keyPath, keyName, valueName);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <param name="registryHive">Registry hive to the keyName. For example: 'RegistryHive.LocalMachine'.</param>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\ApplicationName\'.</param>
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
                if (registryKey == null) { throw new RegSubKeyNotFoundException(String.Format("The registry key '{0}' does not exist.", subKeyPath), subKeyPath); }

                object value = registryKey.GetValue(valueName);
                if (value == null) { throw new RegValueNotFoundException(String.Format("The registry value '{0}' does not exist.", valueName), subKeyPath, valueName); }

                return value;
            }
            catch (Exception) { throw; }
        }

        #endregion

        #region GetKeyValue<T> Methods

        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <typeparam name="TResult">T-Type for the return value.</typeparam>
        /// <param name="parameters">RegistryHelper parameters</param>
        /// <returns>The T-Type value associated with the received parameters.</returns>
        public TResult GetKeyValue<TResult>(RegistryHelperParams parameters)
        {
            try
            {
                object obj = this.GetKeyValue(parameters.RegistryHive, parameters.KeyPath, parameters.KeyName, parameters.ValueName);
                return (TResult)Convert.ChangeType(obj, typeof(TResult)); // (TResult)obj;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <typeparam name="TResult">T-Type for the return value.</typeparam>
        /// <param name="keyName">Registry keyName to retrieve. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to retrieve. For example: 'TestValueString'</param>
        /// <returns>The T-Type value associated with the received parameters.</returns>
        public TResult GetKeyValue<TResult>(string keyName, string valueName)
        {
            try
            {
                object obj = this.GetKeyValue(DEFAULT_REG_HIVE, DEFAULT_KEY_PATH, keyName, valueName);
                return (TResult)Convert.ChangeType(obj, typeof(TResult)); // (TResult)obj;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <typeparam name="TResult">T-Type for the return value.</typeparam>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\ApplicationName\'.</param>
        /// <param name="keyName">Registry keyName to retrieve. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to retrieve. For example: 'TestValueString'</param>
        /// <returns>The T-Type value associated with the received parameters.</returns>
        public TResult GetKeyValue<TResult>(string keyPath, string keyName, string valueName)
        {
            try
            {
                object obj = this.GetKeyValue(DEFAULT_REG_HIVE, keyPath, keyName, valueName);
                return (TResult)Convert.ChangeType(obj, typeof(TResult)); // (TResult)obj;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Retrieves a registry key value.
        /// </summary>
        /// <typeparam name="TResult">T-Type for the return value.</typeparam>
        /// <param name="registryHive">Registry hive to the keyName. For example: 'RegistryHive.LocalMachine'.</param>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\ApplicationName\'.</param>
        /// <param name="keyName">Registry keyName to retrieve. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to retrieve. For example: 'TestValueString'</param>
        /// <returns>The T-Type value associated with the received parameters.</returns>
        public TResult GetKeyValue<TResult>(RegistryHive registryHive, string keyPath, string keyName, string valueName)
        {
            try
            {
                object obj = this.GetKeyValue(DEFAULT_REG_HIVE, keyPath, keyName, valueName);
                return (TResult)Convert.ChangeType(obj, typeof(TResult)); // (TResult)obj;
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
                this.SetKeyValue(parameters.RegistryHive, parameters.KeyPath, parameters.KeyName, parameters.ValueType, parameters.ValueName, parameters.ValueData);
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
            this.SetKeyValue(DEFAULT_REG_HIVE, DEFAULT_KEY_PATH, keyName, DEFAULT_VALUE_TYPE, valueName, valueData);
        }

        /// <summary>
        /// Creates a registry key value.
        /// </summary>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\ApplicationName\'.</param>
        /// <param name="keyName">Registry keyName to create. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to set. For example: 'TestValueString'</param>
        /// <param name="valueData">Registry valueData to set. For example: 'test'</param>
        public void SetKeyValue(string keyPath, string keyName, string valueName, object valueData)
        {
            this.SetKeyValue(DEFAULT_REG_HIVE, keyPath, keyName, DEFAULT_VALUE_TYPE, valueName, valueData);
        }

        /// <summary>
        /// Creates a registry key value.
        /// </summary>
        /// <param name="registryHive">Registry hive to the keyName. For example: 'RegistryHive.LocalMachine'.</param>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\ApplicationName\'.</param>
        /// <param name="keyName">Registry keyName to create. For example: 'SupportLibrary'.</param>
        /// <param name="valueName">Registry valueName to set. For example: 'TestValueString'</param>
        /// <param name="valueData">Registry valueData to set. For example: 'test'</param>
        public void SetKeyValue(RegistryHive registryHive, string keyPath, string keyName, string valueName, object valueData)
        {
            this.SetKeyValue(registryHive, keyPath, keyName, DEFAULT_VALUE_TYPE, valueName, valueData);
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
            this.SetKeyValue(DEFAULT_REG_HIVE, DEFAULT_KEY_PATH, keyName, valueType, valueName, valueData);
        }

        /// <summary>
        /// Creates a registry key value.
        /// </summary>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\ApplicationName\'.</param>
        /// <param name="keyName">Registry keyName to create. For example: 'SupportLibrary'.</param>
        /// <param name="valueType">Registry valueType to set. For example: 'RegistryValueType.String'</param>
        /// <param name="valueName">Registry valueName to set. For example: 'TestValueString'</param>
        /// <param name="valueData">Registry valueData to set. For example: 'Test'</param>
        public void SetKeyValue(string keyPath, string keyName, RegistryValueType valueType, string valueName, object valueData)
        {
            this.SetKeyValue(DEFAULT_REG_HIVE, keyPath, keyName, valueType, valueName, valueData);
        }

        /// <summary>
        /// Creates a registry key value.
        /// </summary>
        /// <param name="registryHive">Registry hive to the keyName. For example: 'RegistryHive.LocalMachine'.</param>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\ApplicationName\'.</param>
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
                if (valueType == RegistryValueType.Binary)          { registryValueKind = RegistryValueKind.Binary; }
                else if (valueType == RegistryValueType.Integer)    { registryValueKind = RegistryValueKind.DWord; }
                else if (valueType == RegistryValueType.Long)       { registryValueKind = RegistryValueKind.QWord; }
                else if (valueType == RegistryValueType.String)     { registryValueKind = RegistryValueKind.String; }

                RegistryKey registryKey = regKey.CreateSubKey(keyPath + keyName);
                registryKey.SetValue(valueName, valueData, registryValueKind);
                registryKey.Close();
            }
            catch (Exception) { throw; }
        }

        #endregion

        #region GetKeyValues Methods

        /// <summary>
        /// Retrieves all values within a registry key.
        /// </summary>
        /// <param name="keyName">Registry keyName to retrieve. For example: 'SupportLibrary'.</param>
        /// <returns>The values within the received parameters.</returns>
        public Dictionary<string, object> GetKeyValues(string keyName)
        {
            try
            {
                return this.GetKeyValues(DEFAULT_REG_HIVE, DEFAULT_KEY_PATH, keyName);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Retrieves all values within a registry key.
        /// </summary>
        /// <param name="registryHive">Registry hive to the keyName. For example: 'RegistryHive.LocalMachine'.</param>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\ApplicationName\'.</param>
        /// <param name="keyName">Registry keyName to retrieve. For example: 'SupportLibrary'.</param>
        /// <returns>The values within the received parameters.</returns>
        public Dictionary<string, object> GetKeyValues(RegistryHive registryHive, string keyPath, string keyName)
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

                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (string valueName in registryKey.GetValueNames())
                    result.Add(valueName, registryKey.GetValue(valueName));

                return result;
            }
            catch (Exception) { throw; }
        }

        #endregion

        #region SetKeyValues Methods

        /// <summary>
        /// Creates a registry key values.
        /// </summary>
        /// <param name="keyName">Registry keyName to create. For example: 'SupportLibrary'.</param>
        /// <param name="keyValues">Values to write to registry.</param>
        public void SetKeyValues(string keyName, Dictionary<string, object> keyValues)
        {
            try
            {
                this.SetKeyValues(DEFAULT_REG_HIVE, DEFAULT_KEY_PATH, keyName, keyValues);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Creates a registry key values.
        /// </summary>
        /// <param name="registryHive">Registry hive to the keyName. For example: 'RegistryHive.LocalMachine'.</param>
        /// <param name="keyPath">Registry path to the keyName. For example: 'SOFTWARE\ApplicationName\'.</param>
        /// <param name="keyName">Registry keyName to create. For example: 'SupportLibrary'.</param>
        /// <param name="keyValues">Values to write to registry.</param>
        public void SetKeyValues(RegistryHive registryHive, string keyPath, string keyName, Dictionary<string, object> keyValues)
        {
            try
            {
                if (registryHive != RegistryHive.LocalMachine && registryHive != RegistryHive.CurrentUser)
                    throw new ArgumentException(String.Format("The registry hive '{0}' is not supported.", registryHive), "registryHive");

                RegistryKey registryKeyBase = null;
                if (registryHive == RegistryHive.LocalMachine)  { registryKeyBase = Registry.LocalMachine; }
                if (registryHive == RegistryHive.CurrentUser)   { registryKeyBase = Registry.CurrentUser; }

                RegistryKey registryKey = registryKeyBase.CreateSubKey(keyPath + keyName);

                foreach (KeyValuePair<string, object> keyValue in keyValues)
                {
                    RegistryValueKind registryValueKind = RegistryValueKind.Unknown;
                    if (keyValue.Value is byte[])       { registryValueKind = RegistryValueKind.Binary; }
                    else if (keyValue.Value is int)     { registryValueKind = RegistryValueKind.DWord; }
                    else if (keyValue.Value is long)    { registryValueKind = RegistryValueKind.QWord; }
                    else if (keyValue.Value is string)  { registryValueKind = RegistryValueKind.String; }
                    else throw new ArgumentException($"The type '{ keyValue.Value.GetType() }' for registry value '{ keyValue.Key }' is not supported.", "keyValues");

                    registryKey.SetValue(keyValue.Key, keyValue.Value, registryValueKind);
                }

                registryKey.Close();
            }
            catch (Exception) { throw; }
        }

        #endregion
    }
}
