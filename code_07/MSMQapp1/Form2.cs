using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Messaging;

namespace MSMQapp1
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.Button btnReceive;
		private System.Messaging.MessageQueue MyMQ;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button btnSend2;
		private int iCount = 0;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			MyMQ.DefaultPropertiesToSend.Label = "Default Label";
			MyMQ.DefaultPropertiesToSend.Priority = MessagePriority.Highest ;

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
			this.btnSend2 = new System.Windows.Forms.Button();
			this.MyMQ = new System.Messaging.MessageQueue();
			this.btnSend = new System.Windows.Forms.Button();
			this.btnReceive = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnSend2
			// 
			this.btnSend2.Location = new System.Drawing.Point(8, 56);
			this.btnSend2.Name = "btnSend2";
			this.btnSend2.Size = new System.Drawing.Size(168, 24);
			this.btnSend2.TabIndex = 2;
			this.btnSend2.Text = "Send with Custom Message";
			this.btnSend2.Click += new System.EventHandler(this.btnSend2_Click);
			// 
			// MyMQ
			// 
			this.MyMQ.Formatter = new System.Messaging.BinaryMessageFormatter(System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full, System.Runtime.Serialization.Formatters.FormatterTypeStyle.TypesAlways);
			this.MyMQ.Path = "synergy\\Private$\\alpha";
			this.MyMQ.SynchronizingObject = this;
			// 
			// btnSend
			// 
			this.btnSend.Location = new System.Drawing.Point(8, 8);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(104, 24);
			this.btnSend.TabIndex = 0;
			this.btnSend.Text = "Send";
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// btnReceive
			// 
			this.btnReceive.Location = new System.Drawing.Point(128, 8);
			this.btnReceive.Name = "btnReceive";
			this.btnReceive.Size = new System.Drawing.Size(96, 24);
			this.btnReceive.TabIndex = 1;
			this.btnReceive.Text = "Receive";
			this.btnReceive.Click += new System.EventHandler(this.btnReceive_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(424, 173);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnSend2,
																		  this.btnReceive,
																		  this.btnSend});
			this.Name = "Form1";
			this.Text = "Form1";
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

		private void btnSend_Click(object sender, System.EventArgs e)
		{

			iCount++;

			try
			{
				MyMQ.Send("message contents " + iCount.ToString());
			}

			catch(Exception ex)
			{
				MessageBox.Show(this,ex.Message);
			}
		}

		private void btnReceive_Click(object sender, System.EventArgs e)
		{
		
			System.Messaging.Message m;
			String str;

			try
			{
				m = MyMQ.Receive(new TimeSpan(0, 0, 3));
				str = (String)m.Body;
			}
			catch
			{
				str = "No messages were receieved";
			}
            
			MessageBox.Show(this,str);
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
		}

		private void btnSend2_Click(object sender, System.EventArgs e)
		{
			System.Messaging.Message m = new System.Messaging.Message();
			//m.Formatter = new System.Messaging.BinaryMessageFormatter();
			m.Body = "Content";
			MyMQ.Send(m);
		}

	}

	public class Teacher
	{
		public String name;
		public Student[] students;
		public int salary;
	}

	public class Student
	{
		public String name;
		public TimeSpan timeinclass;
	}
}