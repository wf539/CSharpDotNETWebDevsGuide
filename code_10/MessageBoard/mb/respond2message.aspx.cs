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

namespace mb
{
	/// <summary>
	/// Summary description for respond2message.
	/// </summary>
	public class respond2message : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox name;
		protected System.Web.UI.WebControls.TextBox email;
		protected System.Web.UI.WebControls.TextBox responseMessage;
		protected System.Web.UI.WebControls.RegularExpressionValidator rexEmail;
		protected System.Web.UI.WebControls.Label ClientScriptHandle;
		protected System.Web.UI.WebControls.Button sendResponse;
		protected System.Web.UI.HtmlControls.HtmlInputHidden msgID;
		protected System.Web.UI.WebControls.TextBox subject;
		protected mb.components.mbdataaccess dbMethod;
		private string type;
		private int messageID;

		public respond2message()
		{
			Page.Init += new System.EventHandler(Page_Init);
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			dbMethod = new mb.components.mbdataaccess();
			
			if( Request.QueryString["msgID"] != null )
			{
				messageID = int.Parse(Request.QueryString["msgID"]);
				Response.Write("msgID = " + messageID);
				int groupID = (int)Session["groupID"];
				type = "response";
			}
			else
			{
				type = "message";
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
			this.sendResponse.Click += new System.EventHandler(this.sendResponse_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void sendResponse_Click(object sender, System.EventArgs e)
		{
			if(type =="message")
			{
				int groupID = (int)Session["groupID"];
				dbMethod.addMessage( name.Text, responseMessage.Text, subject.Text, email.Text, groupID);
			}
			else
			{
				dbMethod.addResponse( messageID, name.Text, email.Text,responseMessage.Text);
			}
			//reload client frames to reflect changes
			ClientScriptHandle.Text ="<script language='javascript'>window.opener.document.forms[0].ClientPost.value='noDelete';window.opener.document.forms[0].submit();window.close();</script>";
		}
	}
}
