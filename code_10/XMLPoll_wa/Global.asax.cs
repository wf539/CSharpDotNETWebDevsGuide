using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Xml;
using System.IO;

namespace XMLPoll_wa 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(Object sender, EventArgs e)
		{
		/*	//read xmlfile store in application level variable
			XmlDocument xpoll = new XmlDocument();
			xpoll.Load( Server.MapPath("poll.xml") );
			Application["ProgramersPoll"] = xpoll.OuterXml;*/
		}
 
		protected void Application_End(Object sender, EventArgs e)
		{
			/*//write application variable back to the xmlfile
			StreamWriter sr = File.CreateText( Server.MapPath("poll.xml") );
			sr.Write( Application["ProgramersPoll"] );
			sr.Close();*/
		}

		protected void Session_Start(Object sender, EventArgs e)
		{
			/*//read xmlfile store in application level variable
			XmlDocument xpoll = new XmlDocument();
			xpoll.Load( Server.MapPath("poll.xml") );
			Session["ProgramersPoll"] = xpoll.OuterXml;*/
		}

		protected void Session_End(Object sender, EventArgs e)
		{
			/*//write application variable back to the xmlfile
			StreamWriter sr = File.CreateText( Server.MapPath("poll.xml") );
			sr.Write( Session["ProgramersPoll"] );
			sr.Close();*/
		}		

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}
	}
}

