using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TextEditor
{
	public class EditForm : System.Windows.Forms.Form
	{
      private System.Windows.Forms.TextBox txtEdit;
      private System.Windows.Forms.MainMenu mainMenu1;
      private System.Windows.Forms.MenuItem miView;
      private System.Windows.Forms.MenuItem miOptions;
		private System.ComponentModel.Container components = null;

		public EditForm()
		{
			InitializeComponent();
		}

      public TextBox EditControl
      {
         get {return txtEdit;}
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
         this.txtEdit = new System.Windows.Forms.TextBox();
         this.miOptions = new System.Windows.Forms.MenuItem();
         this.mainMenu1 = new System.Windows.Forms.MainMenu();
         this.miView = new System.Windows.Forms.MenuItem();
         this.SuspendLayout();
         // 
         // txtEdit
         // 
         this.txtEdit.AcceptsReturn = true;
         this.txtEdit.AcceptsTab = true;
         this.txtEdit.AutoSize = false;
         this.txtEdit.Dock = System.Windows.Forms.DockStyle.Fill;
         this.txtEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
         this.txtEdit.Multiline = true;
         this.txtEdit.Name = "txtEdit";
         this.txtEdit.Size = new System.Drawing.Size(292, 276);
         this.txtEdit.TabIndex = 0;
         this.txtEdit.Text = "";
         // 
         // miOptions
         // 
         this.miOptions.Index = 0;
         this.miOptions.Text = "&Options";
         this.miOptions.Click += new System.EventHandler(this.miOptions_Click);
         // 
         // mainMenu1
         // 
         this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                  this.miView});
         // 
         // miView
         // 
         this.miView.Index = 0;
         this.miView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                               this.miOptions});
         this.miView.Text = "&View";
         // 
         // EditForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(292, 276);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.txtEdit});
         this.Menu = this.mainMenu1;
         this.Name = "EditForm";
         this.Text = "Untitled";
         this.ResumeLayout(false);

      }
		#endregion

      private void miOptions_Click(object sender, System.EventArgs e)
      {
         OptionsForm of = new OptionsForm (txtEdit);
         if (of.ShowDialog() == DialogResult.OK && of.ShouldApplyAll)
            foreach (Form child in MdiParent.MdiChildren)
            {
               TextBox childEdit = ((EditForm) child).EditControl;
               childEdit.WordWrap = txtEdit.WordWrap;
               childEdit.Font = txtEdit.Font;
            }

         of.Dispose();	// modal forms don’t dispose automatically!
      }
	}
}
