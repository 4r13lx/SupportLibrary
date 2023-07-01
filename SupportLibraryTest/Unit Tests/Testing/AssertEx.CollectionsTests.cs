using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Testing;

namespace SupportLibraryTest.Testing
{
    /// <summary>
    /// Testing of Testing namespace classes.
    /// </summary>
    [TestClass]
    public class AssertEx_CollectionsTests
    {
        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        public void AssertEx_Collections_IsEmpty()
        {
            AssertEx.Collections.IsEmpty(new int[0], "Assert 01");
            AssertEx.Collections.IsEmpty(new ArrayList(), "Assert 02");
            AssertEx.Collections.IsEmpty(new Hashtable(), "Assert 03");
        }

        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        [ExpectedException(typeof(AssertFailedException))]
        public void AssertEx_Collections_IsEmpty_FailsOnNonEmptyArray()
        {
            AssertEx.Collections.IsEmpty(new int[] { 1, 2, 3 }, "Assert 01");
        }

        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        public void AssertEx_Collections_IsNotEmpty()
        {
            // arrange
            int[] array = new int[] { 1, 2, 3 };
            ArrayList list = new ArrayList(array);
            Hashtable hash = new Hashtable();
            hash.Add("array", array);

            // act & assert
            AssertEx.Collections.IsNotEmpty(array, "Assert 01");
            AssertEx.Collections.IsNotEmpty(list, "Assert 02");
            AssertEx.Collections.IsNotEmpty(hash, "Assert 03");
        }

        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        [ExpectedException(typeof(AssertFailedException))]
        public void AssertEx_Collections_IsNotEmpty_FailsOnEmptyArray()
        {
            AssertEx.Collections.IsNotEmpty(new int[0], "Assert 01");
        }

        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        [ExpectedException(typeof(AssertFailedException))]
        public void AssertEx_Collections_IsNotEmpty_FailsOnEmptyArrayList()
        {
            AssertEx.Collections.IsNotEmpty(new ArrayList());
        }

        [TestMethod(), TestPropertyAttribute("Unit Tests", "Testing")]
        [ExpectedException(typeof(AssertFailedException))]
        public void AssertEx_Collections_IsNotEmpty_FailsOnEmptyHashTable()
        {
            AssertEx.Collections.IsNotEmpty(new Hashtable());
        }
    }
}
