using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Messaging;
using System.Xml.Serialization;
using MSMQGraphics;

namespace DrawingSender
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Button btnColor;

		private System.Drawing.Color currentColor;
		private System.Drawing.Pen currentPen;
		private int startx;
		private int starty;
		private int endx;
		private int endy;
		private bool lineInProgress = false;
		private MSMQGraphics.Drawing thisDrawing = new MSMQGraphics.Drawing();
		private System.Messaging.MessageQueue drawingMQ;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			currentColor = Color.Black;
			currentPen = new Pen(currentColor);

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
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.btnColor = new System.Windows.Forms.Button();
			this.drawingMQ = new System.Messaging.MessageQueue();
			this.btnSend = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// btnColor
			// 
			this.btnColor.Location = new System.Drawing.Point(144, 232);
			this.btnColor.Name = "btnColor";
			this.btnColor.Size = new System.Drawing.Size(136, 24);
			this.btnColor.TabIndex = 2;
			this.btnColor.Text = "Change Color";
			this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
			// 
			// drawingMQ
			// 
			this.drawingMQ.Path = "synergy\\drawings";
			this.drawingMQ.SynchronizingObject = this;
			// 
			// btnSend
			// 
			this.btnSend.Location = new System.Drawing.Point(296, 232);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(128, 24);
			this.btnSend.TabIndex = 1;
			this.btnSend.Text = "Send";
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(8, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(416, 216);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
			this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
			this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
			this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(432, 261);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnColor,
																		  this.btnSend,
																		  this.pictureBox1});
			this.Name = "Form1";
			this.Text = "Drawing Sender";
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
		private void btnSend_Click(object sender, System.EventArgs e)
		{

			System.Messaging.Message m = new System.Messaging.Message();
			m.Body = thisDrawing;

			try
			{
				drawingMQ.Send(m);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " " + ex.InnerException.Message);
			}
		}

		private void btnColor_Click(object sender, System.EventArgs e)
		{
			colorDialog1.ShowDialog();
			currentColor = colorDialog1.Color;
			btnColor.BackColor = currentColor;
			currentPen = new System.Drawing.Pen(currentColor);
		}

		private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			startx = e.X;
			starty = e.Y;
			lineInProgress = true;
		}

		private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (lineInProgress)
			{
				endx = e.X;
				endy = e.Y;
				pictureBox1.Invalidate();
			}
		}

		private void pictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (lineInProgress)
			{
				lineInProgress = false;
				Graphics g = pictureBox1.CreateGraphics();
				g.DrawLine(currentPen,startx,starty,e.X,e.Y);
				Line l = new Line(ColorTranslator.ToWin32(currentColor),startx,starty,e.X,e.Y);
				thisDrawing.add(l);
			}
		}

		private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			thisDrawing.draw(e.Graphics);
			if (lineInProgress)
			{
				e.Graphics.DrawLine(currentPen,startx,starty,endx,endy);
			}
		}
	}
}
