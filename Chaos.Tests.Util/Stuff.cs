using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chaos.Util;

namespace ChaosUtilTests
{
	[TestClass]
	public class Stuff
	{
		[TestMethod]
		public void BytesToHex()
		{
			Assert.AreEqual("0000", new byte[2].ToHex());
			Assert.AreEqual("1234", new byte[] { 0x12, 0x34 }.ToHex());
			Assert.AreEqual("ABCD", new byte[] { 0xAB, 0xCD }.ToHex());
			Assert.AreEqual("0123456789ABCDEF", new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF }.ToHex());
			Assert.AreEqual("123456789ABCDEF0", new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0 }.ToHex());
		}
	}
}
