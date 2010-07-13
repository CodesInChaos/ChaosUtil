using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChaosUtil.TreeDocuments;

namespace ChaosUtilTests
{
	/// <summary>
	/// Summary description for TreeDocTest
	/// </summary>
	[TestClass]
	public class TreeDocTest
	{
		public TreeDocTest()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[TestMethod]
		public void ValueConstructionAndSerialization()
		{
			Assert.AreEqual("key:value", TreeDoc.CreateLeaf("key", "value").SaveAsElement());
			Assert.AreEqual("value", TreeDoc.CreateLeaf("value").SaveAsElement());
			Assert.AreEqual("key()", TreeDoc.CreateNull("key").SaveAsElement());
			Assert.AreEqual("()", TreeDoc.CreateNull().SaveAsElement());
		}

		[TestMethod]
		public void ListConstructionAndSerialization()
		{
			Assert.AreEqual("(1,2,3)", TreeDoc.CreateList("", "1", "2", "3").SaveAsElement());
			Assert.AreEqual("(1,2,3)", TreeDoc.CreateListRange(new string[] { "1", "2", "3" }).SaveAsElement());
			Assert.AreEqual("(1,2,3)", TreeDoc.CreateListRange(new int[] { 1, 2, 3 }).SaveAsElement());
			Assert.AreEqual("A(1,2,3)", TreeDoc.CreateList("A", 1, "2", 3.0).SaveAsElement());
		}

		[TestMethod]
		public void QuotingAndSerialization()
		{
			Assert.AreEqual("AZaz09+-._", TreeDoc.CreateLeaf("AZaz09+-._").SaveAsElement());
			Assert.AreEqual("\"a b\"", TreeDoc.CreateLeaf("a b").SaveAsElement());
			Assert.AreEqual("\"" + @"\0\r\n\t\\" + "\\\"\"", TreeDoc.CreateLeaf("\0\r\n\t\\\"").SaveAsElement());
		}

		[TestMethod]
		public void ExpandedSerialization()
		{
			var td1=TreeDoc.CreateList("A", "C1", "C2");
			var td2 = TreeDoc.CreateList("C3", "C31", "C32");
			td1.Add(td2);
			td2.ForceExpand = true;
			string s = td1.SaveAsElement();
		}

		[TestMethod]
		public void SimpleParse()
		{
			string s = " a ( b:c, d, e ( ) ) ";
			TreeDoc td=TreeDoc.Parse(s);
			string s2 = td.SaveAsList();
			Assert.AreEqual("a(b:c,d,e())", s2);
			TreeDoc.Parse("\"a\"");
		}
	}
}
