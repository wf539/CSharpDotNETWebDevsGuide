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
	/// Summary description for boardlist.
	/// </summary>
	public class boardlist : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox moderatorLogin;
		protected System.Web.UI.WebControls.TextBox moderatorPassword;
	
		protected System.Web.UI.WebControls.RequiredFieldValidator reqLogin;
		protected System.Web.UI.WebControls.RequiredFieldValidator reqPassword;
		protected System.Web.UI.WebControls.RegularExpressionValidator rexLogin;
			
		protected System.Web.UI.WebControls.CheckBox adminCheckBox;
		protected System.Web.UI.WebControls.Xml xList;
		protected System.Web.UI.WebControls.Button login;
		protected XmlDocument listdoc;
		protected System.Web.UI.WebControls.Label adminLabel;
		private int moderatorID;
		protected System.Web.UI.WebControls.Label scriptHandle;
		protected mb.components.mbdataaccess grouplist;
		protected System.Web.UI.WebControls.Label CreateNew;
		protected bool IsAdmin;

		public boardlist()
		{
			Page.Init += new System.EventHandler(Page_Init);
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			this.IsAdmin = (bool)Session["IsAdmin"];
			this.moderatorID = (int)Session["moderatorID"];
			listdoc = new XmlDocument();

			grouplist = new mb.components.mbdataaccess();
			listdoc.LoadXml( grouplist.getAllGroups().GetXml() );
			initialize_MessageList();			
		}

		private void initialize_MessageList()
		{
			string xsltSource;

			if( this.IsAdmin )
			{
				xsltSource = "admin_group_list.xslt";
			}
			else
			{
				xsltSource = "user_group_list.xslt";
			}

			xList.Document = listdoc;
			xList.TransformSource = xsltSource ;
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
			if( adminLabel.Text =="administrator: logged in.")
			{
				//page is on forced refresh. do nothing
				return;
			}

			if( adminCheckBox.Checked )
			{
				if((moderatorLogin.Text =="sa@site.admin") && (moderatorPassword.Text=="password"))
				{
					//login admin
					this.IsAdmin = true;
					Session["IsAdmin"] = true;
					//re-initalize list
					initialize_MessageList();
					adminLabel.Text ="administrator: logged in.";
					
				}
				else
				{
					adminLabel.Text = "<span style='color:red'>* invalid admin login *</span>";
				}
			}
			else
			{
				adminLabel.Text = "";
				string moderatorID = grouplist.loginDbConn( moderatorLogin.Text, moderatorPassword.Text );
				if( moderatorID !="error")
				{
					Session["moderatorID"] = int.Parse(moderatorID);
					adminLabel.Text = "<span style='color:red'>moderator logged in</span>";
					CreateNew.Text = "<span style='width:20px'>&nbsp;</span><a style='color:white' href='createBoard.aspx?mod=" + moderatorID + "' target='main'>create new group</a>";
				}
			}
		}

		
	}
}
