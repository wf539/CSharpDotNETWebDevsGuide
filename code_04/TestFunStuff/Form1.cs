using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace TestFunStuff
{
	/// <summary>
	/// Test form for ScrollingText custom control
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
      private FunStuff.ScrollingText scrollingText1;
      private FunStuff.WashedButton washedButton1;
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			InitializeComponent();
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
         this.washedButton1 = new FunStuff.WashedButton();
         this.scrollingText1 = new FunStuff.ScrollingText();
         this.SuspendLayout();
         // 
         // washedButton1
         // 
         this.washedButton1.Anchor = System.Windows.Forms.AnchorStyles.Top;
         this.washedButton1.Location = new System.Drawing.Point(168, 120);
         this.washedButton1.Name = "washedButton1";
         this.washedButton1.Size = new System.Drawing.Size(120, 40);
         this.washedButton1.TabIndex = 1;
         this.washedButton1.Text = "Washed Button";
         // 
         // scrollingText1
         // 
         this.scrollingText1.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
         this.scrollingText1.Location = new System.Drawing.Point(8, 16);
         this.scrollingText1.Name = "scrollingText1";
         this.scrollingText1.Size = new System.Drawing.Size(456, 64);
         this.scrollingText1.TabIndex = 0;
         this.scrollingText1.Text = "This is a Custom Control";
         // 
         // Form1
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(472, 200);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.washedButton1,
                                                                      this.scrollingText1});
         this.Name = "Form1";
         this.Text = "Form1";
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
	}
}
