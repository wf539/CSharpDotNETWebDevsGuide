using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;

namespace CountClient
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnInc;
		private System.Windows.Forms.Button btnDec;
		private System.Windows.Forms.TextBox txtValue;
		private System.ComponentModel.IContainer components;

		private CountServer.Count objCount;
		private System.Windows.Forms.Button btnRenew;
		private System.Windows.Forms.Button btnSponsor;

		private ClientSponsor mSponsor;
		private ILease mLease;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			RemotingConfiguration.Configure(@"..\..\CountClient.exe.config");

			objCount = new CountServer.Count();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.btnSponsor = new System.Windows.Forms.Button();
			this.btnRenew = new System.Windows.Forms.Button();
			this.btnDec = new System.Windows.Forms.Button();
			this.txtValue = new System.Windows.Forms.TextBox();
			this.btnInc = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnSponsor
			// 
			this.btnSponsor.Location = new System.Drawing.Point(112, 80);
			this.btnSponsor.Name = "btnSponsor";
			this.btnSponsor.Size = new System.Drawing.Size(88, 32);
			this.btnSponsor.TabIndex = 5;
			this.btnSponsor.Text = "Add Sponsor";
			this.btnSponsor.Click += new System.EventHandler(this.btnSponsor_Click);
			// 
			// btnRenew
			// 
			this.btnRenew.Location = new System.Drawing.Point(16, 80);
			this.btnRenew.Name = "btnRenew";
			this.btnRenew.Size = new System.Drawing.Size(88, 32);
			this.btnRenew.TabIndex = 4;
			this.btnRenew.Text = "Renew Lease";
			this.btnRenew.Click += new System.EventHandler(this.btnRenew_Click);
			// 
			// btnDec
			// 
			this.btnDec.Location = new System.Drawing.Point(112, 8);
			this.btnDec.Name = "btnDec";
			this.btnDec.Size = new System.Drawing.Size(88, 24);
			this.btnDec.TabIndex = 1;
			this.btnDec.Text = "Dec";
			this.btnDec.Click += new System.EventHandler(this.btnDec_Click);
			// 
			// txtValue
			// 
			this.txtValue.Location = new System.Drawing.Point(216, 8);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(56, 20);
			this.txtValue.TabIndex = 2;
			this.txtValue.Text = "";
			// 
			// btnInc
			// 
			this.btnInc.Location = new System.Drawing.Point(16, 8);
			this.btnInc.Name = "btnInc";
			this.btnInc.Size = new System.Drawing.Size(88, 24);
			this.btnInc.TabIndex = 0;
			this.btnInc.Text = "Inc";
			this.btnInc.Click += new System.EventHandler(this.btnInc_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(400, 125);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnSponsor,
																		  this.btnRenew,
																		  this.txtValue,
																		  this.btnDec,
																		  this.btnInc});
			this.Name = "Form1";
			this.Text = "Leasing and Sponsorship example";
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

		private void btnInc_Click(object sender, System.EventArgs e)
		{
			txtValue.Text = objCount.inc().ToString();
		}

		private void btnDec_Click(object sender, System.EventArgs e)
		{
			txtValue.Text = objCount.dec().ToString();
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{

		}

		private void btnRenew_Click(object sender, System.EventArgs e)
		{
			mLease = (ILease)RemotingServices.GetLifetimeService(objCount);

			try
			{
				mLease.Renew(System.TimeSpan.FromSeconds(10));
				MessageBox.Show(this,"Lease renewed for 10 seconds");
			}
			catch
			{
				MessageBox.Show(this,"Lease has expired");
			}
		}

		private void btnSponsor_Click(object sender, System.EventArgs e)
		{

			mLease = (ILease)RemotingServices.GetLifetimeService(objCount);

			mSponsor = new ClientSponsor();
			mSponsor.RenewalTime = TimeSpan.FromSeconds(15);

			try
			{
				mLease.Register(mSponsor);
			}
			catch
			{
				MessageBox.Show(this,"Lease has expired");
			}


			MessageBox.Show("Sponsor registered with object");
		}
	}
}
