namespace simpleMail
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Web.Mail;

	/// <summary>
	///		Summary description for WebUserControl1.
	/// </summary>
	public abstract class WebUserControl1 : System.Web.UI.UserControl
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
	

		/// <summary>
		public WebUserControl1()
		{
			this.Init += new System.EventHandler(Page_Init);
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
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
