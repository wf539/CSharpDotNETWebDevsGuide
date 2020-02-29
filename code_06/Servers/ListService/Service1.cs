using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Runtime.Remoting;

namespace ListService
{
	public class Service1 : System.ServiceProcess.ServiceBase
	{
		
		private System.ComponentModel.Container components = null;

		public Service1()
		{
			InitializeComponent();
		}

		static void Main()
		{
			System.ServiceProcess.ServiceBase[] ServicesToRun;
			ServicesToRun = new System.ServiceProcess.ServiceBase[] { new Service1() };
			System.ServiceProcess.ServiceBase.Run(ServicesToRun);
		}

		private void InitializeComponent()
		{
			this.ServiceName = "ListService";
		}

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

		protected override void OnStart(string[] args)
		{
			// TODO: Add code here to start your service.
			EventLog myLog = new EventLog();
			myLog.Source = "ListService";

			try 
			{   
				RemotingConfiguration.Configure("ListService.exe.config");
				myLog.WriteEntry("Configuration from ListService.exe.cfg successful");
			}
			catch (Exception e)
			{ 
				myLog.WriteEntry("Failed to configure host application: " + e.Message,System.Diagnostics.EventLogEntryType.Error);
			}
		}
 
		protected override void OnStop()
		{}
	}
}
