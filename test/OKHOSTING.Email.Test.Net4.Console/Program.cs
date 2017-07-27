using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKHOSTING.Email.Test.Net4.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			Email.Net4.ClientSetup.Thunderbird.SetupMailAccount("David", "david@okhosting.com", "unknown");
		}
	}
}
