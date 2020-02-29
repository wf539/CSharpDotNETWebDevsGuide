using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;

namespace mb
{
	/// <summary>
	/// Summary description for deleteBoard.
	/// </summary>
	public class deleteBoard : System.Web.UI.Page
	{
		public deleteBoard()
		{
			Page.Init += new System.EventHandler(Page_Init);
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			//(1) read the querystring get value of "board"
			//(2) call delete Method
			//(3) transfer back to boardlist.aspx
			
			int boardID = int.Parse(Request.QueryString["board"]);

			mb.components.mbdataaccess delBoard = new mb.components.mbdataaccess();
			delBoard.delGroup(boardID);
			
			Server.Transfer("boardlist.aspx");
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
