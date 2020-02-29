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
	/// Summary description for Form2.
	/// </summary>
	public class Form2 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private MSMQGraphics.Drawing thisDrawing;
		private System.Messaging.MessageQueue drawingMQ;
		private System.ComponentModel.IContainer components;

		public Form2()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			thisDrawing = new MSMQGraphics.Drawing();
			checkForDrawing();
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.drawingMQ = new System.Messaging.MessageQueue();
			this.SuspendLayout();
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
			// drawingMQ
			// 
			this.drawingMQ.Path = "synergy\\drawings";
			this.drawingMQ.SynchronizingObject = this;
			this.drawingMQ.ReceiveCompleted += new System.Messaging.ReceiveCompletedEventHandler(this.drawingMQ_ReceiveCompleted);
			// 
			// Form2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(440, 229);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pictureBox1});
			this.Name = "Form2";
			this.Text = "Drawing Receiver";
			this.Load += new System.EventHandler(this.Form2_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form2());
		}

		private void Form2_Load(object sender, System.EventArgs e)
		{

		}

		private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			thisDrawing.draw(e.Graphics);
		}

		private void checkForDrawing()
		{
			drawingMQ.BeginReceive(new TimeSpan(0, 0, 3));
		}

		private void drawingMQ_ReceiveCompleted(object sender, System.Messaging.ReceiveCompletedEventArgs e)
		{
			
			System.Messaging.Message m;
			MSMQGraphics.Drawing d;
			
			try
			{
				if (e.Message != null)
				{
					m = e.Message;
					m.Formatter = new XmlMessageFormatter(new Type[]{typeof(MSMQGraphics.Drawing ),typeof(MSMQGraphics.Line )});
					d = (MSMQGraphics.Drawing )m.Body;
					thisDrawing = d;
					pictureBox1.Invalidate();
				}
			}
			catch
			{
			}

			drawingMQ.BeginReceive(new TimeSpan(0, 0, 3));
		}

	}
}
