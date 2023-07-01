using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SupportLibrary.Testing
{
    /// <summary>
    /// Helper class for software Testing over Collections related tasks.<para/>
    /// For example: Assert.AreEqual operations for Unit Testing.
    /// </summary>
    public sealed class Collections
    {
        internal Collections() { }

        #region IsEmpty(ICollection collection)

        /// <summary>
        /// Assert that an array, list or other collection is empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        public void IsEmpty(ICollection collection)
        {
            IsEmpty(collection, "", null);
        }

        /// <summary>
        /// Assert that an array, list or other collection is empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public void IsEmpty(ICollection collection, string message)
        {
            IsEmpty(collection, message, null);
        }

        /// <summary>
        /// Assert that an array, list or other collection is empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public void IsEmpty(ICollection collection, string message, params object[] parameters)
        {
            Assert.IsTrue(collection.Count == 0, message, parameters);
        }

        #endregion

        #region IsNotEmpty(ICollection collection)

        /// <summary>
        /// Assert that an array, list or other collection is not empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        public void IsNotEmpty(ICollection collection)
        {
            IsNotEmpty(collection, "", null);
        }

        /// <summary>
        /// Assert that an array, list or other collection is not empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public void IsNotEmpty(ICollection collection, string message)
        {
            IsNotEmpty(collection, message, null);
        }

        /// <summary>
        /// Assert that an array, list or other collection is not empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public void IsNotEmpty(ICollection collection, string message, params object[] parameters)
        {
            Assert.IsFalse(collection.Count == 0, message, parameters);
        }

        #endregion
    }
}
