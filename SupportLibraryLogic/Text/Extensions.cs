using System;
using System.Linq;

using SupportLibrary.RegularExpressions;

namespace SupportLibrary.Text
{
    /// <summary>
    /// Extension Methods for String related tasks.<para/>
    /// For example: new Split overload or Truncate operations.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Indicates whether the specified string is null or an System.String.Empty string.
        /// </summary>
        /// <param name="value">The string to validate.</param>
        /// <returns>True if the value is null or an empty string (""); otherwise, false.</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            try
            {
                return String.IsNullOrEmpty(value);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Indicates whether the specified string is not null or an System.String.Empty string.
        /// </summary>
        /// <param name="value">The string to validate.</param>
        /// <returns>False if the value is null or an empty string (""); otherwise, true.</returns>
        public static bool IsNotNullOrEmpty(this string value)
        {
            try
            {
                return !String.IsNullOrEmpty(value);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Indicates whether the given text is all numbers.
        /// </summary>
        /// <param name="value">The string to validate.</param>
        /// <returns>True if the value is all numbers.</returns>
        public static bool IsNumeric(this string value)
        {
            try
            {
                return RegexHelper.Miscellaneous.IsNumeric(value);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Retrieves a substring from this instance. The substring starts at a specified character position and has up to the specified lenght.
        /// Resulting out-of-range character positions will be cutted off.
        /// </summary>
        /// <param name="value">The string to substring.</param>
        /// <param name="startIndex">The zero-bazed starting character position of a substring in this instance.</param>
        /// <param name="length">The numbers of characters in the substring.</param>
        /// <returns>A substring of this instance, starting from startIndex, up to the specified characters length.</returns>
        public static string SafeSubstring(this string value, int startIndex, int length)
        {
            if (value == null) { throw new ArgumentNullException(nameof(value), $"{ nameof(value) } is null."); }

            return new string((value ?? string.Empty).Skip(startIndex).Take(length).ToArray());
        }

        /// <summary>
        /// Returns a string array that contains the substrings in this instance that are delimited by elements of a specified Unicode character secuence.<para/>
        /// The option StringSplitOptions.RemoveEmptyEntries is used.
        /// </summary>
        /// <param name="value">The string to split.</param>
        /// <param name="separator">A secuence of Unicode characters that delimit the substring in this instance.</param>
        /// <returns>An array whose elements contain the substrings from this instance that are delimited by one or more strings in separator.</returns>
        public static string[] Split(this string value, params string[] separator)
        {
            try
            {
                return value.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Retrieves a substring from this instance. The substring starts and ends at the specified characters positions.
        /// </summary>
        /// <param name="value">The string to substring.</param>
        /// <param name="startIndex">The zero-bazed starting character position of a substring in this instance.</param>
        /// <param name="endIndex">The zero-bazed ending character position of a substring in this instance.</param>
        /// <returns>A substring of this instance, starting from startIndex, up to endIndex.</returns>
        public static string SubstringByRange(this string value, int startIndex, int endIndex)
        {
            try
            {
                if (startIndex > endIndex)      { throw new ArgumentOutOfRangeException(nameof(startIndex), $"{ nameof(startIndex) } must be lower or equal than { nameof(endIndex) }."); }
                if (startIndex < 0)             { throw new ArgumentOutOfRangeException(nameof(startIndex), $"{ nameof(startIndex) } must be greater or equal than 0."); }
                if (endIndex >= value.Length)   { throw new ArgumentOutOfRangeException(nameof(endIndex), $"{ nameof(endIndex) } must be lower than string length."); }

                return value.Substring(startIndex, endIndex - startIndex + 1);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Returns a string that contains a substring from this instance, starting from the beginning, up to the expecified length.
        /// </summary>
        /// <param name="value">The string to truncate.</param>
        /// <param name="length">Maximum number of characters in the returned string.</param>
        /// <returns>A string that is equivalent to this string, starting from the beginning, up to the expecified length.</returns>
        public static string Truncate(this string value, int length)
        {
            try
            {
                if (value == null) { throw new ArgumentNullException(nameof(value), $"{ nameof(value) } is null."); }

                return value.Substring(0, System.Math.Min(value.Length, length));
            }
            catch (Exception) { throw; }
        }
    }
}
