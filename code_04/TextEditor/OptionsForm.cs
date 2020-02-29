using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace TextEditor
{
	public class OptionsForm : TextEditor.DialogForm
	{
      private TextBox hostControl;
      
      private System.Windows.Forms.TabControl tabControl1;
      private System.Windows.Forms.TabPage tabPage1;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.CheckBox chkWordWrap;
      private System.Windows.Forms.CheckBox chkApplyAll;
      private System.Windows.Forms.NumericUpDown nudFontSize;
		private System.ComponentModel.IContainer components = null;

      public bool ShouldApplyAll
      {
         get {return chkApplyAll.Checked;}
      }

      public OptionsForm (TextBox hostControl)
      {
         InitializeComponent();

         // Save hostControl parameter to class field
         this.hostControl = hostControl;

         chkWordWrap.Checked = hostControl.WordWrap;
         nudFontSize.Value = (decimal) hostControl.Font.Size;
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

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         this.chkWordWrap = new System.Windows.Forms.CheckBox();
         this.tabControl1 = new System.Windows.Forms.TabControl();
         this.tabPage1 = new System.Windows.Forms.TabPage();
         this.label1 = new System.Windows.Forms.Label();
         this.nudFontSize = new System.Windows.Forms.NumericUpDown();
         this.chkApplyAll = new System.Windows.Forms.CheckBox();
         this.tabControl1.SuspendLayout();
         this.tabPage1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).BeginInit();
         this.SuspendLayout();
         // 
         // btnOK
         // 
         this.btnOK.Location = new System.Drawing.Point(101, 213);
         this.btnOK.TabIndex = 1;
         this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
         // 
         // btnCancel
         // 
         this.btnCancel.Location = new System.Drawing.Point(184, 213);
         this.btnCancel.TabIndex = 2;
         // 
         // chkWordWrap
         // 
         this.chkWordWrap.Location = new System.Drawing.Point(12, 16);
         this.chkWordWrap.Name = "chkWordWrap";
         this.chkWordWrap.TabIndex = 0;
         this.chkWordWrap.Text = "Word Wrap";
         // 
         // tabControl1
         // 
         this.tabControl1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right);
         this.tabControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                  this.tabPage1});
         this.tabControl1.Location = new System.Drawing.Point(8, 6);
         this.tabControl1.Name = "tabControl1";
         this.tabControl1.SelectedIndex = 0;
         this.tabControl1.Size = new System.Drawing.Size(251, 199);
         this.tabControl1.TabIndex = 0;
         // 
         // tabPage1
         // 
         this.tabPage1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                               this.label1,
                                                                               this.nudFontSize,
                                                                               this.chkApplyAll,
                                                                               this.chkWordWrap});
         this.tabPage1.Location = new System.Drawing.Point(4, 22);
         this.tabPage1.Name = "tabPage1";
         this.tabPage1.Size = new System.Drawing.Size(243, 173);
         this.tabPage1.TabIndex = 0;
         this.tabPage1.Text = "Editor";
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(12, 48);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(52, 13);
         this.label1.TabIndex = 1;
         this.label1.Text = "&Font Size";
         // 
         // nudFontSize
         // 
         this.nudFontSize.Location = new System.Drawing.Point(12, 64);
         this.nudFontSize.Name = "nudFontSize";
         this.nudFontSize.Size = new System.Drawing.Size(56, 20);
         this.nudFontSize.TabIndex = 2;
         // 
         // chkApplyAll
         // 
         this.chkApplyAll.Location = new System.Drawing.Point(12, 136);
         this.chkApplyAll.Name = "chkApplyAll";
         this.chkApplyAll.Size = new System.Drawing.Size(144, 24);
         this.chkApplyAll.TabIndex = 3;
         this.chkApplyAll.Text = "Apply to All Windows";
         // 
         // OptionsForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(266, 242);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.btnCancel,
                                                                      this.btnOK,
                                                                      this.tabControl1});
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
         this.Name = "OptionsForm";
         this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
         this.tabControl1.ResumeLayout(false);
         this.tabPage1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

      private void btnOK_Click(object sender, System.EventArgs e)
      {
         hostControl.WordWrap = chkWordWrap.Checked ;
         hostControl.Font = new Font (hostControl.Font.Name, (float) nudFontSize.Value);
      }
	}
}

