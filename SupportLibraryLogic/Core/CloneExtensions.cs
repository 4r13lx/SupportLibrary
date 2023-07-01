using System;
using System.Collections.Generic;
using System.Linq;

namespace SupportLibrary.Core
{
    /// <summary>
    /// Extension Methods for Cloning related tasks.<para/>
    /// </summary>
    public static class CloneExtensions
    {
        /// <summary>
        /// Makes a copy of this instance.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="source">Object to clone.</param>
        /// <param name="deepCopy">Flag to make a shallow or deep copy of the object.</param>
        /// <returns>A copy of this instance.</returns>
        public static T Clone<T>(this T source, bool deepCopy = false) where T : class, ICloneableExtended
        {
            if (source == null) { throw new ArgumentNullException(nameof(source), $"{ nameof(source) } is null."); }

            return (deepCopy) ? (T)source.DeepClone() : (T)source.ShallowClone();
        }

        /// <summary>
        /// Makes a copy of this collection.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <param name="collection">Collection to clone.</param>
        /// /// <param name="deepCopy">Flag  to make a shallow or deep copy of the collection.</param>
        /// <returns>A copy of this collection.</returns>
        public static IEnumerable<T> Clone<T>(this IEnumerable<T> collection, bool deepCopy = false) where T : ICloneableExtended
        {
            if (collection == null) { throw new ArgumentNullException(nameof(collection), $"{ nameof(collection) } is null."); }

            return (deepCopy) ? collection.Select(a => (T)a.DeepClone()) : collection.Select(a => (T)a.ShallowClone());
        }
    }
}
