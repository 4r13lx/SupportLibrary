using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SupportLibrary.Collections;

namespace SupportLibraryTest.Collections
{
    /// <summary>
    /// Testing of Collections namespace classes.
    /// </summary>
    [TestClass]
    public class CollectionsTest
    {
        [TestMethod, TestPropertyAttribute("Unit Tests", "Collections")]
        public void ArrayEx_Add_Item()
        {
            // arrange
            string[] array = { "1", "2", "3" };
            string[] arrayExpected = { "1", "2", "3", "4" };

            // act
            ArrayEx.Add(ref array, "4");

            // assert
            Assert.AreEqual(arrayExpected.Length, array.Length, "Assert 01");
            CollectionAssert.AreEqual(arrayExpected, array, "Assert 02");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Collections")]
        public void ArrayEx_Add_Secuence()
        {
            // arrange
            string[] array = { "1", "2" };
            string[] arrayExpected = { "1", "2", "3", "4" };

            // act
            ArrayEx.Add(ref array, new string[] { "3", "4" });

            // assert
            Assert.AreEqual(arrayExpected.Length, array.Length, "Assert 01");
            CollectionAssert.AreEqual(arrayExpected, array, "Assert 02");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Collections")]
        public void ArrayEx_Concatenate_Item()
        {
            // arrange
            string[] array = { "1", "2", "3" };
            string[] arrayExpected = { "1", "2", "3", "4" };

            // act
            array = ArrayEx.Concatenate(array, "4");

            // assert
            Assert.AreEqual(arrayExpected.Length, array.Length, "Assert 01");
            CollectionAssert.AreEqual(arrayExpected, array, "Assert 02");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Collections")]
        public void ArrayEx_Concatenate_Secuence()
        {
            // arrange
            string[] array = { "1", "2" };
            string[] arrayExpected = { "1", "2", "3", "4" };

            // act
            array = array.Concatenate(new string[] { "3", "4" });

            // assert
            Assert.AreEqual(arrayExpected.Length, array.Length, "Assert 01");
            CollectionAssert.AreEqual(arrayExpected, array, "Assert 02");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Collections")]
        public void ArrayEx_RemoveAt()
        {
            // arrange
            string[] array = { "1", "2", "3", "4" };
            string[] arrayExpected = { "1", "2", "4" };

            // act
            ArrayEx.RemoveAt(ref array, 2);

            // assert
            Assert.AreEqual(arrayExpected.Length, array.Length, "Assert 01");
            CollectionAssert.AreEqual(arrayExpected, array, "Assert 02");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Collections")]
        public void ArrayEx_RemoveAll()
        {
            // arrange
            string[] array = { "1", "2", "3", "4" };
            string[] arrayExpected = { "1", "2", "4" };

            // act
            int count = ArrayEx.RemoveAll(ref array, a => a == "3");

            // assert
            Assert.AreEqual(1, count, "Assert 01");
            Assert.AreEqual(arrayExpected.Length, array.Length, "Assert 02");
            CollectionAssert.AreEqual(arrayExpected, array, "Assert 03");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Collections")]
        public void Extensions_Array_Concatenate_Item()
        {
            // arrange
            string[] array = { "1", "2", "3" };
            string[] arrayExpected = { "1", "2", "3", "4" };

            // act
            array = array.Concatenate("4");

            // assert
            Assert.AreEqual(arrayExpected.Length, array.Length, "Assert 01");
            CollectionAssert.AreEqual(arrayExpected, array, "Assert 02");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Collections")]
        public void Extensions_Array_Concatenate_Secuence()
        {
            // arrange
            string[] array = { "1", "2" };
            string[] arrayExpected = { "1", "2", "3", "4" };

            // act
            array = array.Concatenate(new string[] { "3", "4" });

            // assert
            Assert.AreEqual(arrayExpected.Length, array.Length, "Assert 01");
            CollectionAssert.AreEqual(arrayExpected, array, "Assert 02");
        }
    }
}
