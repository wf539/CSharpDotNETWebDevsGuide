using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TextEditor
{
	/// <summary>
	/// A reusable dialog form class
	/// </summary>
	public class DialogForm : System.Windows.Forms.Form
	{
      protected System.Windows.Forms.Button btnOK;
      protected System.Windows.Forms.Button btnCancel;
		private System.ComponentModel.Container components = null;

		public DialogForm()
		{
			InitializeComponent();
         this.Icon = null;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
         this.btnOK = new System.Windows.Forms.Button();
         this.btnCancel = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // btnOK
         // 
         this.btnOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.btnOK.Location = new System.Drawing.Point(101, 213);
         this.btnOK.Name = "btnOK";
         this.btnOK.TabIndex = 0;
         this.btnOK.Text = "OK";
         // 
         // btnCancel
         // 
         this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
         this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.btnCancel.Location = new System.Drawing.Point(184, 213);
         this.btnCancel.Name = "btnCancel";
         this.btnCancel.TabIndex = 1;
         this.btnCancel.Text = "Cancel";
         // 
         // DialogForm
         // 
         this.AcceptButton = this.btnOK;
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.CancelButton = this.btnCancel;
         this.ClientSize = new System.Drawing.Size(266, 242);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.btnCancel,
                                                                      this.btnOK});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "DialogForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "DialogForm";
         this.ResumeLayout(false);

      }
		#endregion
	}
}
