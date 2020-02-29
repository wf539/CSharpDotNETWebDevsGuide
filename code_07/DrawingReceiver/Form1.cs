using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Messaging;
using System.Xml.Serialization;
using MSMQGraphics;

namespace DrawingReceiver
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private MSMQGraphics.Drawing thisDrawing;
		private System.Messaging.MessageQueue drawingMQ;
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			thisDrawing = new MSMQGraphics.Drawing();
			timer1.Enabled = true;
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
			this.components = new System.ComponentModel.Container();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.drawingMQ = new System.Messaging.MessageQueue();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Interval = 4000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// drawingMQ
			// 
			this.drawingMQ.Path = "synergy\\drawings";
			this.drawingMQ.SynchronizingObject = this;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(8, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(416, 216);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(440, 229);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pictureBox1});
			this.Name = "Form1";
			this.Text = "Drawing Receiver";
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

		private void Form1_Load(object sender, System.EventArgs e)
		{

		}

		private void checkForDrawing()
		{
			System.Messaging.Message m;
			MSMQGraphics.Drawing d;

			try
			{
				m = drawingMQ.Receive(new TimeSpan(0, 0, 1));

				m.Formatter = new XmlMessageFormatter(new Type[]{typeof(MSMQGraphics.Drawing ),typeof(MSMQGraphics.Line )});
			
				d = (MSMQGraphics.Drawing )m.Body;

				thisDrawing = d;

				pictureBox1.Invalidate();
			}

			catch 
			{
				// We don’t want to display a message after every 5 second poll if no messages are availiable
			}
			
		}

		private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			thisDrawing.draw(e.Graphics);
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			checkForDrawing();
		}
	}
}
