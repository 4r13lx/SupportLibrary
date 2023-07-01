using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SupportLibrary.Testing
{
    /// <summary>
    /// Helper class for software Testing over comparitions related tasks.<para/>
    /// For example: Assert.IsEmpty operations for Unit Testing.
    /// </summary>
    public sealed class Text
    {
        internal Text() { }

        #region IsEmpty

        /// <summary>
        /// Asserts that a string is empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        public void IsEmpty(string value)
        {
            IsEmpty(value, "", null);
        }

        /// <summary>
        /// Asserts that a string is empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public void IsEmpty(string value, string message)
        {
            IsEmpty(value, message, null);
        }

        /// <summary>
        /// Asserts that a string is empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public void IsEmpty(string value, string message, params object[] parameters)
        {
            Assert.IsTrue(value.Length == 0, message, parameters);
        }

        #endregion

        #region IsNotEmpty

        /// <summary>
        /// Asserts that a string is not empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        public void IsNotEmpty(string value)
        {
            IsNotEmpty(value, "", null);
        }

        /// <summary>
        /// Asserts that a string is not empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public void IsNotEmpty(string value, string message)
        {
            IsNotEmpty(value, message, null);
        }

        /// <summary>
        /// Asserts that a string is not empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public void IsNotEmpty(string value, string message, params object[] parameters)
        {
            Assert.IsFalse(value.Length == 0, message, parameters);
        }

        #endregion

        #region AreEqualIgnoringCase

        /// <summary>
        /// Asserts that two strings are equal, without regard to case. 
        /// </summary>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        public void AreEqualIgnoringCase(string expected, string actual)
        {
            AreEqualIgnoringCase(expected, actual, "", null);
        }

        /// <summary>
        /// Asserts that two strings are equal, without regard to case. 
        /// </summary>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public void AreEqualIgnoringCase(string expected, string actual, string message)
        {
            AreEqualIgnoringCase(expected, actual, message, null);
        }

        /// <summary>
        /// Asserts that two strings are equal, without regard to case. 
        /// </summary>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public static void AreEqualIgnoringCase(string expected, string actual, string message, params object[] parameters)
        {
            Assert.IsTrue(String.Compare(expected, actual, StringComparison.CurrentCultureIgnoreCase) == 0, message, parameters);
        }

        #endregion
    }
}
