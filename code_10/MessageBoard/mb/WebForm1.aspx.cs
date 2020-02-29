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

namespace mb
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button login;
	
		public WebForm1()
		{
			Page.Init += new System.EventHandler(Page_Init);
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			/*mb.components.mbdataaccess temp = new mb.components.mbdataaccess();
			string xstr = temp.getAllGroups().GetXml();*/
			XmlDocument xtemp = new XmlDocument();
			//xtemp.LoadXml( xstr );
			xtemp.Load( Server.MapPath("admin_group_list.xslt") );
			Response.Write( xtemp.OuterXml );
			Response.End();
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
			this.login.Click += new System.EventHandler(this.login_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void login_Click(object sender, System.EventArgs e)
		{

		}
	}
}
