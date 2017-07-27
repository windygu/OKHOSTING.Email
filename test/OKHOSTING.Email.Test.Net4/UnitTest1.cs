using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OKHOSTING.Email.Test.Net4
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			Email.Net4.ClientSetup.Thunderbird.SetupMailAccount("David", "david@okhosting.com", "unknown");
		}
	}
}
