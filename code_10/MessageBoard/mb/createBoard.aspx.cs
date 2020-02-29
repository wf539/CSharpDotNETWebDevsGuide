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
	/// Summary description for createBoard.
	/// </summary>
	public class createBoard : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtTitle;
		protected System.Web.UI.WebControls.TextBox txtGroup;
		protected System.Web.UI.WebControls.Label clientScriptHandle;
		protected System.Web.UI.WebControls.Button Button1;
	
		public createBoard()
		{
			Page.Init += new System.EventHandler(Page_Init);
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if( Request.QueryString["mod"] != null )
			{
				Session["moderatorID"] = int.Parse(Request.QueryString["mod"]);
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			string addTitle = txtTitle.Text;
			string addGroup = txtGroup.Text;			
			int addMod = (int)Session["moderatorID"];
			
			
			mb.components.mbdataaccess newGroup = new mb.components.mbdataaccess();

			newGroup.addGroup(addTitle,addGroup,addMod);

			
			//clientScriptHandle.Text = "<script language='javascript'>parent.contents.window.location='boardlist.aspx'</script>";
			clientScriptHandle.Text = "<script language='javascript'>parent.contents.document.forms[0].submit();</script>";

		}
	}
}
