using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.Remoting;

namespace VersionClient
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnGetVersion;
		private VersionServer.Test objRemote;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			RemotingConfiguration.Configure(@"..\..\VersionClient.exe.config");
			objRemote = new VersionServer.Test();

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnGetVersion = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnGetVersion
			// 
			this.btnGetVersion.Location = new System.Drawing.Point(24, 8);
			this.btnGetVersion.Name = "btnGetVersion";
			this.btnGetVersion.Size = new System.Drawing.Size(160, 24);
			this.btnGetVersion.TabIndex = 0;
			this.btnGetVersion.Text = "Get Version";
			this.btnGetVersion.Click += new System.EventHandler(this.btnGetVersion_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(248, 157);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnGetVersion});
			this.Name = "Form1";
			this.Text = "Version Client";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void btnGetVersion_Click(object sender, System.EventArgs e) {
			MessageBox.Show(objRemote.getVersion());
			
		}

		private void Form1_Load(object sender, System.EventArgs e) {

		}
	}
}
