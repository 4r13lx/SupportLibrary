using System;
using System.Collections.Generic;

namespace SupportLibrary.Collections
{
    /// <summary>
    /// Extension Methods for Array related tasks.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Concatenates a value to the end of the Array, and returns the result as a new Array.
        /// </summary>
        /// <typeparam name="T">T-Type for the Array.</typeparam>
        /// <param name="self">The Array in which to add the value.</param>
        /// <param name="value">Value to add.</param>
        /// <returns>The resulting Array.</returns>
        public static T[] Concatenate<T>(this T[] self, T value)
        {
            try
            {
                return ArrayEx.Concatenate(self, value);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Concatenates a value secuence to the end of the Array, and returns the result as a new Array.
        /// </summary>
        /// <typeparam name="T">T-Type for the Array.</typeparam>
        /// <param name="self">The Array in which to add the secuence.</param>
        /// <param name="value">Value to add.</param>
        /// <returns>The resulting Array.</returns>
        public static T[] Concatenate<T>(this T[] self, IEnumerable<T> value)
        {
            try
            {
                return ArrayEx.Concatenate(self, value);
            }
            catch (Exception) { throw; }
        }
    }
}
