using System;
using System.ComponentModel;
using System.Reflection;

namespace SupportLibrary.Core
{
    /// <summary>
    /// Extension Methods for Enumeration related tasks.<para/>
    /// For example: get the integer value for an enumeration instance.
    /// </summary>
    public static class EnumerationExtensions
    {
        /// <summary>
        /// Get the integer value for the current enum instance.
        /// </summary>
        /// <param name="value">Enum instance from which retrieve its value</param>
        /// <returns>An integer value</returns>
        public static int GetValue(this Enum value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Get the description value for the current enum instance.
        /// </summary>
        /// <param name="value">Enum instance from which retrieve its value</param>
        /// <returns>An string value</returns>
        public static string GetDescription(this Enum value)
        {
            try
            {
                Type type = value.GetType();

                FieldInfo fieldInfo = type.GetField(value.ToString());
                DescriptionAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

                if (attrs.Length == 0)
                    throw new ArgumentException($"The Enum value '{ value.ToString() }' don't have any Description.", nameof(value));

                return attrs[0].Description;
            }
            catch (Exception) { throw; }
        }
    }
}
