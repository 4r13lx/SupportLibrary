using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Registry;

namespace SupportLibraryTest
{
    /// <summary>
    /// Testing of Registry namespace classes.<para/>
    /// This tests uses the registry path 'HKLM\SOFTWARE\WoW6432Node\MSC Argentina\SupportLibrary for testing purpose.
    /// </summary>
    [TestClass]
    public class RegistryTests
    {
        private const RegistryHive DEFAULT_REG_HIVE = RegistryHive.LocalMachine;
        private const string DEFAULT_KEY_PATH = @"SOFTWARE\MSC Argentina\";
        private const string DEFAULT_KEY_NAME = @"SupportLibrary\Test";

        [TestMethod, TestPropertyAttribute("Unit Tests", "Registry")]
        public void RegistryHelper_SetGetKeyValue_ValidData()
        {
            // act
            new RegistryHelper().SetKeyValue(DEFAULT_KEY_NAME, RegistryValueType.Binary, "TestValueBinary", BitConverter.GetBytes(1234));
            new RegistryHelper().SetKeyValue(DEFAULT_KEY_NAME, RegistryValueType.Integer, "TestValueInteger", 1234);
            new RegistryHelper().SetKeyValue(DEFAULT_KEY_NAME, RegistryValueType.Long, "TestValueLong", 1234);
            new RegistryHelper().SetKeyValue(DEFAULT_KEY_NAME, RegistryValueType.String, "TestValueString", "Test");

            byte[] binaryData = (byte[])new RegistryHelper().GetKeyValue(DEFAULT_KEY_NAME, "TestValueBinary");
            int integerData = (int)new RegistryHelper().GetKeyValue(DEFAULT_KEY_NAME, "TestValueInteger");
            long longData = (long)new RegistryHelper().GetKeyValue(DEFAULT_KEY_NAME, "TestValueLong");
            string stringData = (string)new RegistryHelper().GetKeyValue(DEFAULT_KEY_NAME, "TestValueString");

            // assert
            Assert.IsTrue(BitConverter.ToInt32(binaryData, 0) == 1234, "Assert 01");
            Assert.AreEqual(1234, integerData, "Assert 02");
            Assert.AreEqual(1234, longData, "Assert 03");
            Assert.AreEqual("Test", stringData, "Assert 04");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Registry")]
        public void RegistryHelper_SetGetKeyValue_T_ValidData()
        {
            // act
            new RegistryHelper().SetKeyValue(DEFAULT_KEY_NAME, RegistryValueType.Binary, "TestValueBinary", BitConverter.GetBytes(1234));
            new RegistryHelper().SetKeyValue(DEFAULT_KEY_NAME, RegistryValueType.Integer, "TestValueInteger", 1234);
            new RegistryHelper().SetKeyValue(DEFAULT_KEY_NAME, RegistryValueType.Long, "TestValueLong", 1234);
            new RegistryHelper().SetKeyValue(DEFAULT_KEY_NAME, RegistryValueType.String, "TestValueString", "Test");

            byte[] binaryData = new RegistryHelper().GetKeyValue<byte[]>(DEFAULT_KEY_NAME, "TestValueBinary");
            int integerData = new RegistryHelper().GetKeyValue<int>(DEFAULT_KEY_NAME, "TestValueInteger");
            long longData = new RegistryHelper().GetKeyValue<long>(DEFAULT_KEY_NAME, "TestValueLong");
            string stringData = new RegistryHelper().GetKeyValue<string>(DEFAULT_KEY_NAME, "TestValueString");

            // assert
            Assert.IsTrue(BitConverter.ToInt32(binaryData, 0) == 1234, "Assert 01");
            Assert.AreEqual(1234, integerData, "Assert 02");
            Assert.AreEqual(1234, longData, "Assert 03");
            Assert.AreEqual("Test", stringData, "Assert 04");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Registry")]
        public void RegistryHelper_SetGetKeyValue_InvalidData_ValueName()
        {
            try
            {
                // act
                new RegistryHelper().SetKeyValue(DEFAULT_KEY_NAME, "TestValueString", "Test");
                new RegistryHelper().GetKeyValue<string>(DEFAULT_KEY_NAME, "invalidValue");

                // assert
                Assert.Fail("RegistryHelper.GetKeyValue() parameter was not properly validated.");
            }
            catch (RegValueNotFoundException ex) { Assert.AreEqual("invalidValue", ex.Value); }
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Registry")]
        public void RegistryHelper_SetKeyValue_InvalidData_Params()
        {
            // arrange
            List<RegistryHelperParams> lstParams = new List<RegistryHelperParams>();
            lstParams.Add(new RegistryHelperParams() { RegistryHive = RegistryHive.Unknown, KeyPath = DEFAULT_KEY_PATH, KeyName = DEFAULT_KEY_NAME, ValueType = RegistryValueType.String, ValueName = "TestValueString", ValueData = "Test" });
            lstParams.Add(new RegistryHelperParams() { RegistryHive = DEFAULT_REG_HIVE, KeyPath = DEFAULT_KEY_PATH, KeyName = DEFAULT_KEY_NAME, ValueType = RegistryValueType.Unknown, ValueName = "TestValueString", ValueData = "Test" });
            lstParams.Add(new RegistryHelperParams() { RegistryHive = DEFAULT_REG_HIVE, KeyPath = DEFAULT_KEY_PATH, KeyName = DEFAULT_KEY_NAME, ValueType = RegistryValueType.Binary, ValueName = "TestValueBinary", ValueData = "Test" });
            lstParams.Add(new RegistryHelperParams() { RegistryHive = DEFAULT_REG_HIVE, KeyPath = DEFAULT_KEY_PATH, KeyName = DEFAULT_KEY_NAME, ValueType = RegistryValueType.Integer, ValueName = "TestValueInteger", ValueData = "Test" });
            lstParams.Add(new RegistryHelperParams() { RegistryHive = DEFAULT_REG_HIVE, KeyPath = DEFAULT_KEY_PATH, KeyName = DEFAULT_KEY_NAME, ValueType = RegistryValueType.Long, ValueName = "TestValueLong", ValueData = "Test" });
            lstParams.Add(new RegistryHelperParams() { RegistryHive = DEFAULT_REG_HIVE, KeyPath = DEFAULT_KEY_PATH, KeyName = DEFAULT_KEY_NAME, ValueType = RegistryValueType.String, ValueName = "TestValueLong", ValueData = null });
                
            foreach (RegistryHelperParams parameters in lstParams)
            {
                try
                {
                    // act
                    new RegistryHelper().SetKeyValue(parameters);

                    // assert
                    Assert.Fail("RegistryHelper.SetKeyValue() parameters were not properly validated.");
                }
                catch (ArgumentException) { /* expected exception */ }
                catch (Exception ex) { Assert.Fail(ex.Message); }
            }
        }
    }
}
