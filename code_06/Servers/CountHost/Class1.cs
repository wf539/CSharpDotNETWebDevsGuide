using System;
using System.Runtime.Remoting;

namespace CountHost
{
	class Class1
	{
		static void Main(string[] args)
		{
			try 
			{   
				RemotingConfiguration.Configure(@"..\..\CountHost.exe.config");
			}
			catch (Exception e)
			{ 
				System.Console.WriteLine("Failed to configure host application: " + e.Message,System.Diagnostics.EventLogEntryType.Error);
			}

			System.Console.WriteLine("Press [Enter] to exit...");
			System.Console.ReadLine();
		}
	}
}
