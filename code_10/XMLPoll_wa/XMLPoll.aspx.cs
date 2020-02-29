using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.IO;

namespace XMLPoll_wa
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.RadioButton focus_web;
		protected System.Web.UI.WebControls.RadioButton focus_mobile;
		protected System.Web.UI.WebControls.RadioButton focus_Desktop;
	
		protected System.Web.UI.WebControls.RadioButton IDE_VSNET;
		protected System.Web.UI.WebControls.RadioButton IDE_XMLSpy;
		protected System.Web.UI.WebControls.RadioButton IDE_other;
		
		protected System.Web.UI.WebControls.Button vote;
		protected System.Web.UI.WebControls.Xml stats;

		protected XmlDocument doc;
		
		public WebForm1()
		{
			Page.Init += new System.EventHandler(Page_Init);
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			doc = new XmlDocument();
			showPollUI();
		}

		private void showPollUI()
		{
			// Put user code to initialize the page here
			doc.Load( Server.MapPath("poll.xml")  );
			stats.Document = doc;
			stats.TransformSource = "stats.xslt";
		}

		private void updateFile( string filePath, XmlDocument xdoc )
		{
			StreamWriter sr = File.CreateText( Server.MapPath( filePath ) );
			try
			{
				sr.Write( xdoc.OuterXml );
				sr.Close();
			}
			catch(Exception e)
			{
				Response.Write( "An Error occured:<br>" + e.ToString() );
				Response.End();
			}
		}

		private void Page_Init(object sender, EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
		}

		

		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.vote.Click += new System.EventHandler(this.vote_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void updateXPoll( string focus, string IDE)
		{
			XmlDocument xpoll = new XmlDocument();
			xpoll = doc;

			XmlElement data = xpoll.CreateElement("data");
			
			XmlElement response1 = xpoll.CreateElement("response");
				XmlAttribute questionID = xpoll.CreateAttribute("question-id");
				XmlAttribute selection = xpoll.CreateAttribute("selection");
				questionID.Value="1";
				selection.Value = focus;
				response1.SetAttributeNode(questionID);
				response1.SetAttributeNode(selection);

			XmlElement response2 = xpoll.CreateElement("response");
				questionID = xpoll.CreateAttribute("question-id");
				selection = xpoll.CreateAttribute("selection");
				questionID.Value="2";
				selection.Value = IDE;
				response2.SetAttributeNode(questionID);
				response2.SetAttributeNode(selection);

			data.AppendChild( response1 );
			data.AppendChild( response2 );

			xpoll.SelectSingleNode("//results").AppendChild(data );

			//update Session variable
			updateFile( "poll.xml", xpoll );
			showPollUI();
		}

		private void vote_Click(object sender, System.EventArgs e)
		{
			string focus;
			string IDE;

			//get Developer focus
			if(focus_Desktop.Checked)
			{
				focus = "Desktop";
			}
			else if(focus_web.Checked)
			{
				focus = "Web";
			}
			else if(focus_mobile.Checked)
			{
				focus = "Mobile";
			}
			else
			{
				focus = "not given";
			}
			
			//get developer tool
			if(IDE_VSNET.Checked)
			{
				IDE = "VSNET";
			}
			else if(IDE_XMLSpy.Checked)
			{
				IDE = "XMLSpy";
			} 
			else if(IDE_other.Checked)
			{
				IDE = "other";
			}
			else
			{
				IDE = "not given";
			}
			
			updateXPoll( focus, IDE );
		}
	}
}
