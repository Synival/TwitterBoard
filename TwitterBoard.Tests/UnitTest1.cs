using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Net;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web.UI;
using System.Web;

namespace TwitterBoard.Tests {
   [TestClass]
   public class UnitTest1 {
      [TestMethod]
      public void TestMethod_TweetCheckNull () {
         Assert.AreEqual (TwitterBoard._Default.PostTweet (null),
            "Error: No message");
      }
      [TestMethod]
      public void TestMethod_TweetCheckTooLong () {
         Assert.AreEqual (TwitterBoard._Default.PostTweet (new String ('x', 240)),
            "Error: Message too long (240 / 140)");
      }
      [TestMethod]
      public void TestMethod_TweetCheckNoHttpContext () {
         Assert.AreEqual (TwitterBoard._Default.PostTweet ("There is no HttpContext.Current"),
            "Error: No HttpContext.Current");
      }

      private TestContext testContextInstance;
      public TestContext TestContext {
         get { return testContextInstance; }
         set { testContextInstance = value; }
      }

      [TestMethod]
      [HostType("ASP.NET")]
      [AspNetDevelopmentServerHost("$(SolutionDir)\\TwitterBoard", "/")]
      [UrlToTest("http://localhost:62264/Default.aspx")]
      public void TestMethod_TweetUrlToTest () {
         _Default page = (_Default) testContextInstance.RequestedPage;
         Assert.AreEqual (
            "Home Page",
            page.Title);
         Assert.AreEqual (
            false,
            HttpContext.Current.User.Identity.IsAuthenticated);
         Assert.AreEqual (
            "Error: User is not authenticated",
            _Default.PostTweet ("Oh, hi here"));
      }
   }
}