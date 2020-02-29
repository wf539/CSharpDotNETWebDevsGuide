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
	/// Summary description for createMod.
	/// </summary>
	public class createMod : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.TextBox txtEmail;
		protected System.Web.UI.WebControls.TextBox txtPassword;
	
		public createMod()
		{
			Page.Init += new System.EventHandler(Page_Init);
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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

			string addEmail = txtEmail.Text;
			string addPassword =  txtPassword.Text;
			mb.components.mbdataaccess newMod = new mb.components.mbdataaccess();
       
			Session["moderatorID"] =  newMod.addMod(addEmail,addPassword);

			Server.Transfer("createBoard.aspx");
		}
	}
}
