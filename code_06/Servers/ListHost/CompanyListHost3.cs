using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Diagnostics;

namespace ListHost
{
	/// <summary>
	/// The application ListClient uses this host
	/// </summary>
	/// <remarks>
	/// <H4>Channels</H4>
	/// HttpServerChannel
	/// Port (8080)
	/// <BR></BR>
	/// <H4>Remote Objects</H4>
	/// <B>Type:</B> ListServer.CompanyLists
	/// <B>URI:</B> CompanyLists
	/// <B>Creation Type:</B> Singleton
	/// <B>Test URL:</B><A HREF="http:\\localhost:8080\CompanyLists?wsdl">click to test</A>
	/// </remarks>

	public class CompanyListHost
	{
		/// <summary>
		/// Registers the remoting objects
		/// </summary>
		static void Main(string[] args)
		{
			EventLog myLog = new EventLog();
			myLog.Source = "ListHost";
			bool failed = false;

			try 
			{   
				RemotingConfiguration.Configure(@"..\..\ListHost.exe.config");

				myLog.WriteEntry("Configuration from ListHost.exe.cfg successful");
			}
			catch (Exception e)
			{ 
				myLog.WriteEntry("Failed to configure host application: " + e.Message,System.Diagnostics.EventLogEntryType.Error);
				failed = true;
			}

			if (failed)
			{
				System.Console.WriteLine("Errors at startup - see Event Log.");
			}

			System.Console.WriteLine("Press [Enter] to exit...");
			System.Console.ReadLine();
		}
	}
}
