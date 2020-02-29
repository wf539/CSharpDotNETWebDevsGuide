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
using System.Web.Mail;

namespace simpleMail
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtFrom;
		protected System.Web.UI.WebControls.TextBox txtTo;
		protected System.Web.UI.WebControls.TextBox txtCC;
		protected System.Web.UI.WebControls.TextBox txtSubject;
		protected System.Web.UI.WebControls.Button btnEmail;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTxtfrom;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTxtfrom;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTxtto;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTxtto;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTxtcc;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTxtcc;
		protected System.Web.UI.WebControls.TextBox txtMessage;
	
		public WebForm1()
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
			this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnEmail_Click(object sender, System.EventArgs e)
		{
			MailMessage mail = new MailMessage();
			mail.From = txtFrom.Text;
			mail.To = txtTo.Text;
			mail.Cc = txtCC.Text;
			mail.Subject = txtSubject.Text;
			mail.Body = txtMessage.Text;
			mail.BodyFormat = MailFormat.Text;
			SmtpMail.Send(mail);


				}
	}
}
