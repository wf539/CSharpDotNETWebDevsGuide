using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;

namespace ListHost
{

	public class CompanyListHost
	{
		
		static void Main(string[] args)
		{
	
			HttpServerChannel myChannel = new HttpServerChannel (8080);
			ChannelServices.RegisterChannel(myChannel);

			RemotingConfiguration.RegisterWellKnownServiceType(typeof(ListServer.CompanyLists), "CompanyLists", WellKnownObjectMode.Singleton);

			System.Console.WriteLine("Press [Enter] to exit...");
			System.Console.ReadLine();
		}
	}
}
