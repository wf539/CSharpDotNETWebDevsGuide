using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;

namespace ListHost
{
	public class Service1 : System.ServiceProcess.ServiceBase
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Service1()
		{
			// This call is required by the Windows.Forms Component Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitComponent call
		}

		static void Main()
		{
			System.ServiceProcess.ServiceBase[] ServicesToRun;
	
			ServicesToRun = new System.ServiceProcess.ServiceBase[] { new Service1() };

			System.ServiceProcess.ServiceBase.Run(ServicesToRun);
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			this.ServiceName = "ListService";
		}
		#endregion

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) 
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Set things in motion so your service can do its work.
		/// </summary>
		protected override void OnStart(string[] args)
		{
			// TODO: Add code here to start your service.
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
 
		/// <summary>
		/// Stop this service.
		/// </summary>
		protected override void OnStop()
		{
			// TODO: Add code here to perform any tear-down necessary to stop your service.
		}
	}
}
