using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SupportLibrary.Testing
{
    /// <summary>
    /// Helper class for software Testing over Conditions related tasks.<para/>
    /// For example: Assert.Greater operations for Unit Testing.
    /// </summary>
    public sealed class Condition
    {
        internal Condition() { }

        #region Greater

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be greater.</param>
        /// <param name="arg2">The second value, expected to be less.</param>
        public void Greater<T>(T arg1, T arg2) where T : IComparable
        {
            Greater(arg1, arg2, "{0} is less than or equal to {1}.", arg2, arg1);
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be greater.</param>
        /// <param name="arg2">The second value, expected to be less.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public void Greater<T>(T arg1, T arg2, string message) where T : IComparable
        {
            if ((arg1 as IComparable).CompareTo(arg2) <= 0) { Assert.Fail(message); }
        }

        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be greater.</param>
        /// <param name="arg2">The second value, expected to be less.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public void Greater<T>(T arg1, T arg2, string message, params object[] parameters) where T : IComparable
        {
            if ((arg1 as IComparable).CompareTo(arg2) <= 0) { Assert.Fail(message, parameters); }
        }

        #endregion

        #region GreaterOrEqual

        /// <summary>
        /// Verifies that the first value is greater than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be greater.</param>
        /// <param name="arg2">The second value, expected to be less.</param>
        public void GreaterOrEqual<T>(T arg1, T arg2) where T : IComparable
        {
            GreaterOrEqual(arg1, arg2, "{0} is less than {1}.", arg2, arg1);
        }

        /// <summary>
        /// Verifies that the first value is greater than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be greater.</param>
        /// <param name="arg2">The second value, expected to be less.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public void GreaterOrEqual<T>(T arg1, T arg2, string message) where T : IComparable
        {
            if ((arg1 as IComparable).CompareTo(arg2) < 0) { Assert.Fail(message); }
        }

        /// <summary>
        /// Verifies that the first value is greater than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be greater.</param>
        /// <param name="arg2">The second value, expected to be less.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public void GreaterOrEqual<T>(T arg1, T arg2, string message, params object[] parameters) where T : IComparable
        {
            if ((arg1 as IComparable).CompareTo(arg2) < 0) { Assert.Fail(message, parameters); }
        }

        #endregion

        #region Less

        /// <summary>
        /// Verifies that the first value is less than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be less.</param>
        /// <param name="arg2">The second value, expected to be greater.</param>
        public void Less<T>(T arg1, T arg2) where T : IComparable
        {
            Less(arg1, arg2, "{0} is greater than or equal to {1}.", arg2, arg1);
        }

        /// <summary>
        /// Verifies that the first value is less than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be less.</param>
        /// <param name="arg2">The second value, expected to be greater.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public void Less<T>(T arg1, T arg2, string message) where T : IComparable
        {
            if (((IComparable)arg1).CompareTo(arg2) >= 0) { Assert.Fail(message); }
        }

        /// <summary>
        /// Verifies that the first value is less than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be less.</param>
        /// <param name="arg2">The second value, expected to be greater.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public void Less<T>(T arg1, T arg2, string message, params object[] parameters) where T : IComparable
        {
            if (((IComparable)arg1).CompareTo(arg2) >= 0) { Assert.Fail(message, parameters); }
        }

        #endregion

        #region LessOrEqual

        /// <summary>
        /// Verifies that the first value is less than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be less.</param>
        /// <param name="arg2">The second value, expected to be greater.</param>
        public void LessOrEqual<T>(T arg1, T arg2) where T : IComparable
        {
            LessOrEqual(arg1, arg2, "{0} is greater than {1}.", arg2, arg1);
        }

        /// <summary>
        /// Verifies that the first value is less than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be less.</param>
        /// <param name="arg2">The second value, expected to be greater.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public void LessOrEqual<T>(T arg1, T arg2, string message) where T : IComparable
        {
            if (((IComparable)arg1).CompareTo(arg2) > 0) { Assert.Fail(message); }
        }

        /// <summary>
        /// Verifies that the first value is less than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be less.</param>
        /// <param name="arg2">The second value, expected to be greater.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public void LessOrEqual<T>(T arg1, T arg2, string message, params object[] parameters) where T : IComparable
        {
            if (((IComparable)arg1).CompareTo(arg2) > 0) { Assert.Fail(message, parameters); }
        }

        #endregion
    }
}
