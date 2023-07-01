using System;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

using SupportLibrary.Web;
using SupportLibrary.Testing;

namespace SupportLibraryTest.Web
{
    /// <summary>
    /// Testing of Web namespace classes.
    /// </summary>
    [TestClass]
    public class SessionHelperTests
    {
        string keyName = "KeyName";
        string keyValue = "Test value";
        string resourceName = "Resource";

        [TestMethod, TestPropertyAttribute("Unit Tests", "Web")]
        public void SessionHelper_Retrieve()
        {
            // arrange
            HttpSessionStateBase session = Substitute.For<HttpSessionStateBase>();
            session[keyName].Returns(keyValue);

            // act
            session.Add(keyName, keyValue);

            ISessionHelper sessionHelper = new SessionHelper(session);
            string value = sessionHelper.Retrieve<string>(keyName);

            // assert
            Assert.AreEqual(keyValue, value, "Assert 01");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Web")]
        public void SessionHelper_Retrieve_FailsOnInvalidArgs()
        {
            // arrange
            HttpSessionStateBase session = Substitute.For<HttpSessionStateBase>();

            Action action1 = () => new SessionHelper(session).Retrieve<string>(null);                               // 1st param null
            Action action2 = () => new SessionHelper(session).RetrieveWithMessage<string>(null, "error message");   // 1st param null
            Action action3 = () => new SessionHelper(session).RetrieveWithMessage<string>(keyName, null);           // 2nd param null
            Action action4 = () => new SessionHelper(session).RetrieveWithRecourceKey<string>(null, resourceName);  // 1st param null
            Action action5 = () => new SessionHelper(session).RetrieveWithRecourceKey<string>(keyName, null);       // 2nd param null

            // act & assert
            ArgumentNullException ex1 = AssertEx.Exceptions.Throws<ArgumentNullException>(action1);
            ArgumentNullException ex2 = AssertEx.Exceptions.Throws<ArgumentNullException>(action2);
            ArgumentNullException ex3 = AssertEx.Exceptions.Throws<ArgumentNullException>(action3);
            ArgumentNullException ex4 = AssertEx.Exceptions.Throws<ArgumentNullException>(action4);
            ArgumentNullException ex5 = AssertEx.Exceptions.Throws<ArgumentNullException>(action5);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Web")]
        public void SessionHelper_TryRetrieve()
        {
            // arrange
            HttpSessionStateBase session = Substitute.For<HttpSessionStateBase>();
            session[keyName].Returns(keyValue);
            string value1 = "", value2 = "";

            // act
            session.Add(keyName, keyValue);

            ISessionHelper sessionHelper = new SessionHelper(session);
            bool result1 = sessionHelper.TryRetrieve<string>(keyName, ref value1);
            bool result2 = sessionHelper.TryRetrieve<string>("invalid", ref value2);

            // assert
            Assert.AreEqual(result1, true, "Assert 01");
            Assert.AreEqual(value1, keyValue, "Assert 02");
            Assert.AreEqual(result2, false, "Assert 03");
            Assert.AreEqual(value2, null, "Assert 04");
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Web")]
        public void SessionHelper_TryRetrieve_FailsOnInvalidArgs()
        {
            // arrange
            HttpSessionStateBase session = Substitute.For<HttpSessionStateBase>();

            string value1 = "";
            Action action1 = () => new SessionHelper(session).TryRetrieve<string>(null, ref value1);    // 1st param null

            // act & assert
            ArgumentNullException ex1 = AssertEx.Exceptions.Throws<ArgumentNullException>(action1);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Web")]
        public void Extensions_Retrieve_FailsOnInvalidArgs()
        {
            // arrange
            HttpRequest request = new HttpRequest("", "http://tempuri.org", "");
            HttpResponse response = new HttpResponse(null);
            HttpContext context = new HttpContext(request, response);

            Action action1 = () => context.RetrieveFromSession<string>(null);                               // 1st param null
            Action action2 = () => context.RetrieveFromSessionWithMessage<string>(null, "error message");   // 1st param null
            Action action3 = () => context.RetrieveFromSessionWithMessage<string>(keyName, null);           // 2nd param null
            Action action4 = () => context.RetrieveFromSessionWithRecourceKey<string>(null, resourceName);  // 1st param null
            Action action5 = () => context.RetrieveFromSessionWithRecourceKey<string>(keyName, null);       // 2nd param null

            // act & assert
            ArgumentNullException ex1 = AssertEx.Exceptions.Throws<ArgumentNullException>(action1);
            ArgumentNullException ex2 = AssertEx.Exceptions.Throws<ArgumentNullException>(action2);
            ArgumentNullException ex3 = AssertEx.Exceptions.Throws<ArgumentNullException>(action3);
            ArgumentNullException ex4 = AssertEx.Exceptions.Throws<ArgumentNullException>(action4);
            ArgumentNullException ex5 = AssertEx.Exceptions.Throws<ArgumentNullException>(action5);
        }

        [TestMethod, TestPropertyAttribute("Unit Tests", "Web")]
        public void Extensions_TryRetrieve_FailsOnInvalidArgs()
        {
            // arrange
            HttpRequest request = new HttpRequest("", "http://tempuri.org", "");
            HttpResponse response = new HttpResponse(null);
            HttpContext context = new HttpContext(request, response);

            string value1 = "";
            Action action1 = () => context.TryRetrieveFromSession<string>(null, ref value1);    // 1st param null

            // act & assert
            ArgumentNullException ex1 = AssertEx.Exceptions.Throws<ArgumentNullException>(action1);
        }
    }
}
