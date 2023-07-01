using System;
using System.Collections.Generic;
using System.Linq;

namespace SupportLibrary.Collections
{
    /// <summary>
    /// Helper class for Array related tasks.<para/>
    /// For example: Add a new value into an Array.
    /// </summary>
    public static class ArrayEx
    {
        /// <summary>
        /// Adds a value to the end of the Array.
        /// </summary>
        /// <typeparam name="T">T-Type for the source Array.</typeparam>
        /// <param name="source">The Array in which to add the value.</param>
        /// <param name="value">Value to add.</param>
        public static void Add<T>(ref T[] source, T value)
        {
            source = source.Concat(new T[] { value }).ToArray();
        }

        /// <summary>
        /// Adds a value secuence to the end of the Array.
        /// </summary>
        /// <typeparam name="T">T-Type for the source Array.</typeparam>
        /// <param name="source">The Array in which to add the secuence.</param>
        /// <param name="value">Value secuence to add.</param>
        public static void Add<T>(ref T[] source, IEnumerable<T> value)
        {
            source = source.Concat(value).ToArray();
        }

        /// <summary>
        /// Concatenates a value to the end of the Array, and returns the result as a new Array.
        /// </summary>
        /// <typeparam name="T">T-Type for the source Array.</typeparam>
        /// <param name="source">The Array in which to add the value.</param>
        /// <param name="value">Item value to add.</param>
        /// <returns>The resulting Array.</returns>
        public static T[] Concatenate<T>(T[] source, T value)
        {
            return source.Concat(new T[] { value }).ToArray();
        }

        /// <summary>
        /// Concatenates a value to the end of the Array, and returns the result as a new Array.
        /// </summary>
        /// <typeparam name="T">T-Type for the source Array.</typeparam>
        /// <param name="source">The Array in which to add the secuence.</param>
        /// <param name="value">Value secuence to add.</param>
        /// <returns>The resulting Array.</returns>
        public static T[] Concatenate<T>(T[] source, IEnumerable<T> value)
        {
            return source.Concat(value).ToArray();
        }

        /// <summary>
        /// Removes the element at the specified index of the Array&lt;T&gt;.
        /// </summary>
        /// <typeparam name="T">T-Type for the source Array.</typeparam>
        /// <param name="source">The Array from which to remove the element.</param>
        /// <param name="index">The zero-bazed index of the element to remove.</param>
        public static void RemoveAt<T>(ref T[] source, int index)
        {
            // validations
            if (index < 0 || source == null | source.Length == 0) { return; }

            // starting from index position +1, move everything one position to the left
            if (source.Length > index + 1)
                for (int i = index + 1; i < source.Length; i++)
                    source[i - 1] = source[i];

            // remove last element
            Array.Resize<T>(ref source, source.Length - 1);
        }

        /// <summary>
        /// Removes all the elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <typeparam name="T">T-Type for the source Array.</typeparam>
        /// <param name="source">The Array from which to remove the elements.</param>
        /// <param name="match">The Predicate&lt;in T&gt; delegate that defines the conditions of the elements to remove.</param>
        /// <returns>The number of elements removed from the List&lt;T&gt;.</returns>
        public static int RemoveAll<T>(ref T[] source, Predicate<T> match)
        {
            // validations
            if (null == match || null == source || 0 == source.Length) return 0;

            int length = source.Length;
            int destinationIndex = 0;
            T[] destinationArray = new T[length];

            for (int i = 0; i < source.Length; i++)
            {
                if (!match(source[i]))
                {
                    destinationArray[destinationIndex] = source[i];
                    destinationIndex++;
                }
            }

            if (destinationIndex != length)
            {
                Array.Resize<T>(ref destinationArray, destinationIndex);
                source = destinationArray;
            }

            return length - destinationIndex;
        }
    }
}
