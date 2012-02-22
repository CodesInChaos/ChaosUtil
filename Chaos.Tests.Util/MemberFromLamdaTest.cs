using Chaos.Util.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ChaosUtilTests
{


	/// <summary>
	///This is a test class for MemberFromLamdaTest and is intended
	///to contain all MemberFromLamdaTest Unit Tests
	///</summary>
	[TestClass()]
	public class MemberFromLamdaTest
	{
		class Test
		{
			public int Field;
			public int Property { get; set; }
		}

		[TestMethod()]
		public void PropertyInfoTest()
		{
			//MemberFromLamda.PropertyInfo(() => new Test().Field);
		}


		[TestMethod]
		public void MemberInfoTest()
		{
			return;
			Expression exp = null; // TODO: Initialize to an appropriate value
			MemberInfo expected = null; // TODO: Initialize to an appropriate value
			MemberInfo actual;
			actual = MemberFromLamda.MemberInfo(exp);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}
	}
}
