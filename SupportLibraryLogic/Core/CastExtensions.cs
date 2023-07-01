using System;
using System.Collections.Generic;

namespace SupportLibrary.Core
{
    /// <summary>
    /// Extension Methods for Casting related tasks.<para/>
    /// For example: Cast.To(source, dest) or source.CastTo&lt;dest&gt;().
    /// </summary>
    public static class CastExtensions
    {
        /// <summary>
        /// Cast this instance to TResult.
        /// </summary>
        /// <typeparam name="TResult">T-Type for the return value.</typeparam>
        /// <param name="source">Object to cast into another type.</param>
        /// <param name="recursive">Flag to cast composite types too.</param>
        /// <returns>A T-Type instance with values from source entity.</returns>
        public static TResult CastTo<TResult>(this object source, bool recursive = true) where TResult : new()
        {
            try
            {
                return Cast.To<TResult>(source, new TResult(), recursive);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Cast this instance to TResult.
        /// </summary>
        /// <typeparam name="TResult">T-Type for the return value.</typeparam>
        /// <param name="source">Object to cast into another type.</param>
        /// <param name="dest">A T-Type instance for the return value.</param>
        /// <param name="recursive">Flag to cast composite types too.</param>
        /// <returns>A T-Type instance with values from source entity.</returns>
        public static TResult CastTo<TResult>(this object source, TResult dest, bool recursive = true) where TResult : new()
        {
            try
            {
                return Cast.To<TResult>(source, dest, recursive);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Cast this instance to a list of TResult.
        /// </summary>
        /// <typeparam name="T">T-Type for the source value.</typeparam>
        /// <typeparam name="TResult">T-Type for the return value.</typeparam>
        /// <param name="source">Object to cast into another type.</param>
        /// <param name="recursive">Flag to cast composite types too.</param>
        /// <returns>A T-Type instance with values from source entity.</returns>
        public static List<TResult> CastCollectionTo<T, TResult>(this IList<T> source, bool recursive = true) where TResult : new()
        {
            try
            {
                return Cast.CollectionTo<T, TResult>(source, new TResult(), recursive);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Cast this instance to a list of TResult.
        /// </summary>
        /// <typeparam name="T">T-Type for the return value.</typeparam>
        /// <typeparam name="TResult">T-Type for the return value.</typeparam>
        /// <param name="source">Object to cast into another type.</param>
        /// <param name="dest">A T-Type instance for the return value.</param>
        /// <param name="recursive">Flag to cast composite types too.</param>
        /// <returns>A T-Type instance with values from source entity.</returns>
        public static List<TResult> CastCollectionTo<T, TResult>(this IList<T> source, TResult dest, bool recursive = true) where TResult : new()
        {
            try
            {
                return Cast.CollectionTo<T, TResult>(source, dest, recursive);
            }
            catch (Exception) { throw; }
        }
    }
}
