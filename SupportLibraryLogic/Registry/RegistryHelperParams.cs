using System;

namespace SupportLibrary.Registry
{
    /// <summary>
    /// Parameters for RegistryHelper class methods.
    /// </summary>
    public sealed class RegistryHelperParams
    {
        /// <summary>
        /// Registry hive to the keyName. For example: 'RegistryHive.LocalMachine'.
        /// </summary>
        public RegistryHive RegistryHive { get; set; }

        /// <summary>
        /// Registry path to the keyName. For example: 'SOFTWARE\MSC Argentina\'.
        /// </summary>
        public string KeyPath { get; set; }

        /// <summary>
        /// Registry keyName to retrieve. For example: 'SupportLibrary'.
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// Registry valueType to set. For example: 'RegistryValueType.String'.
        /// </summary>
        public RegistryValueType ValueType { get; set; }

        /// <summary>
        /// Registry valueName to retrieve. For example: 'TestValueString'.
        /// </summary>
        public string ValueName { get; set; }

        /// <summary>
        /// Registry valueData to set. For example: 'Test'.
        /// </summary>
        public object ValueData { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public RegistryHelperParams()
        {
            Initialize();
        }

        private void Initialize()
        {
            this.RegistryHive = RegistryHive.Unknown;
            this.KeyPath = "";
            this.KeyName = "";
            this.ValueType = RegistryValueType.Unknown;
            this.ValueName = "";
            this.ValueData = "";
        }
    }
}
