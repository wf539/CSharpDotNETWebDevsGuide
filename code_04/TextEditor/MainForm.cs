using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace TextEditor
{
	/// <summary>
	/// Main MDI Parent Form
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
      private System.Windows.Forms.MainMenu mainMenu1;
      private System.Windows.Forms.MenuItem miFile;
      private System.Windows.Forms.MenuItem miNew;
      private System.Windows.Forms.MenuItem miExit;
      private System.Windows.Forms.MenuItem miWindow;
      private System.Windows.Forms.MdiClient mdiClient1;
      private System.Windows.Forms.MenuItem miTileVertical;
      private System.Windows.Forms.MenuItem miTileHorizontal;
      private System.Windows.Forms.MenuItem miCascade;
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
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
         this.miTileVertical = new System.Windows.Forms.MenuItem();
         this.miTileHorizontal = new System.Windows.Forms.MenuItem();
         this.miWindow = new System.Windows.Forms.MenuItem();
         this.miCascade = new System.Windows.Forms.MenuItem();
         this.mdiClient1 = new System.Windows.Forms.MdiClient();
         this.miNew = new System.Windows.Forms.MenuItem();
         this.miExit = new System.Windows.Forms.MenuItem();
         this.miFile = new System.Windows.Forms.MenuItem();
         this.mainMenu1 = new System.Windows.Forms.MainMenu();
         this.SuspendLayout();
         // 
         // miTileVertical
         // 
         this.miTileVertical.Index = 0;
         this.miTileVertical.Text = "Tile Vertical";
         this.miTileVertical.Click += new System.EventHandler(this.miTileVertical_Click);
         // 
         // miTileHorizontal
         // 
         this.miTileHorizontal.Index = 1;
         this.miTileHorizontal.Text = "Tile Horizontal";
         this.miTileHorizontal.Click += new System.EventHandler(this.miTileHorizontal_Click);
         // 
         // miWindow
         // 
         this.miWindow.Index = 1;
         this.miWindow.MdiList = true;
         this.miWindow.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                 this.miTileVertical,
                                                                                 this.miTileHorizontal,
                                                                                 this.miCascade});
         this.miWindow.MergeOrder = 20;
         this.miWindow.Text = "&Window";
         // 
         // miCascade
         // 
         this.miCascade.Index = 2;
         this.miCascade.Text = "Cascade";
         this.miCascade.Click += new System.EventHandler(this.miCascade_Click);
         // 
         // mdiClient1
         // 
         this.mdiClient1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.mdiClient1.Name = "mdiClient1";
         this.mdiClient1.TabIndex = 0;
         // 
         // miNew
         // 
         this.miNew.Index = 0;
         this.miNew.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
         this.miNew.Text = "&New";
         this.miNew.Click += new System.EventHandler(this.miNew_Click);
         // 
         // miExit
         // 
         this.miExit.Index = 1;
         this.miExit.Text = "E&xit";
         this.miExit.Click += new System.EventHandler(this.miExit_Click);
         // 
         // miFile
         // 
         this.miFile.Index = 0;
         this.miFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                               this.miNew,
                                                                               this.miExit});
         this.miFile.Text = "&File";
         // 
         // mainMenu1
         // 
         this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                  this.miFile,
                                                                                  this.miWindow});
         // 
         // MainForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(448, 351);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.mdiClient1});
         this.IsMdiContainer = true;
         this.Menu = this.mainMenu1;
         this.Name = "MainForm";
         this.Text = "Simple Editor";
         this.ResumeLayout(false);

      }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

      private void miExit_Click(object sender, System.EventArgs e)
      {
         Close();
      }

      private void miNew_Click(object sender, System.EventArgs e)
      {
         EditForm ef = new EditForm();
         ef.MdiParent = this;             // this makes ef an MDI child form
         ef.Show();
      }

      private void miTileVertical_Click(object sender, System.EventArgs e)
      {
         LayoutMdi (MdiLayout.TileVertical);
      }

      private void miTileHorizontal_Click(object sender, System.EventArgs e)
      {
         LayoutMdi (MdiLayout.TileHorizontal);
      }

      private void miCascade_Click(object sender, System.EventArgs e)
      {
         LayoutMdi (MdiLayout.Cascade);
      }
	}
}
