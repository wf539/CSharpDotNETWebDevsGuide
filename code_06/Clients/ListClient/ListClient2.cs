using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;

namespace ListClient
{
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ListBox listBox1;
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			InitializeComponent();
		
			RemotingConfiguration.Configure(@"..\..\ListClient.cfg");
		}

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

		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(8, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(200, 24);
			this.button1.TabIndex = 0;
			this.button1.Text = "Get Country List";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// listBox1
			// 
			this.listBox1.Location = new System.Drawing.Point(8, 48);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(200, 108);
			this.listBox1.TabIndex = 1;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(216, 165);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listBox1,
																		  this.button1});
			this.Name = "Form1";
			this.Text = "Remoting Example";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{

		}
		
		private void button1_Click(object sender, System.EventArgs e)
		{
			ListServer.CompanyLists cLst = new ListServer.CompanyLists();
			listBox1.DataSource = cLst.getCountryList();
		}
	}
}
