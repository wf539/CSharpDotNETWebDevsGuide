using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace WeatherView
{
	/// <summary>
	/// Simple demo form for ListView and TreeView controls
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
      private System.Windows.Forms.ImageList ilSmall;
      private System.Windows.Forms.ListView listView1;
      private System.Windows.Forms.ImageList ilLarge;
      private System.Windows.Forms.ColumnHeader columnHeader1;
      private System.Windows.Forms.ColumnHeader columnHeader2;
      private System.Windows.Forms.ContextMenu cmView;
      private System.Windows.Forms.MenuItem miList;
      private System.Windows.Forms.MenuItem miDetails;
      private System.Windows.Forms.TreeView treeView1;
      private System.Windows.Forms.Splitter splitter1;
      private System.Windows.Forms.MenuItem miSmallIcon;
      private System.Windows.Forms.MenuItem miLargeIcon;
      private System.ComponentModel.IContainer components;

		public Form1()
		{
			InitializeComponent();
         ListViewItem lvi = new ListViewItem
            (new string[] { "Hail", "Possible" } );

         listView1.Items.Add (lvi);

         // Use snow’s ImageIndex (1) for image & selected image
         TreeNode tn = new TreeNode ("Sleet", 1, 1);

         // treeView1.Nodes[1] is the Snow Node.
         // We want to add to *its* node collection.
         treeView1.Nodes[1].Nodes.Add (tn);
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
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
         System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
                                                                                                                 "Sun",
                                                                                                                 "Low"}, 0);
         System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
                                                                                                                 "Snow",
                                                                                                                 "Moderate"}, 1);
         System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
                                                                                                                 "Clouds",
                                                                                                                 "High"}, 2);
         System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
                                                                                                                 "Rain",
                                                                                                                 "Certain"}, 3);
         this.cmView = new System.Windows.Forms.ContextMenu();
         this.miLargeIcon = new System.Windows.Forms.MenuItem();
         this.miSmallIcon = new System.Windows.Forms.MenuItem();
         this.miList = new System.Windows.Forms.MenuItem();
         this.miDetails = new System.Windows.Forms.MenuItem();
         this.ilLarge = new System.Windows.Forms.ImageList(this.components);
         this.listView1 = new System.Windows.Forms.ListView();
         this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
         this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
         this.ilSmall = new System.Windows.Forms.ImageList(this.components);
         this.treeView1 = new System.Windows.Forms.TreeView();
         this.splitter1 = new System.Windows.Forms.Splitter();
         this.SuspendLayout();
         // 
         // cmView
         // 
         this.cmView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                               this.miLargeIcon,
                                                                               this.miSmallIcon,
                                                                               this.miList,
                                                                               this.miDetails});
         this.cmView.Popup += new System.EventHandler(this.cmView_Popup);
         // 
         // miLargeIcon
         // 
         this.miLargeIcon.Index = 0;
         this.miLargeIcon.Text = "Large Icons";
         this.miLargeIcon.Click += new System.EventHandler(this.miLargeIcon_Click);
         // 
         // miSmallIcon
         // 
         this.miSmallIcon.Index = 1;
         this.miSmallIcon.Text = "Small Icons";
         this.miSmallIcon.Click += new System.EventHandler(this.miSmallIcon_Click);
         // 
         // miList
         // 
         this.miList.Index = 2;
         this.miList.Text = "List";
         this.miList.Click += new System.EventHandler(this.miList_Click);
         // 
         // miDetails
         // 
         this.miDetails.Index = 3;
         this.miDetails.Text = "Details";
         this.miDetails.Click += new System.EventHandler(this.miDetails_Click);
         // 
         // ilLarge
         // 
         this.ilLarge.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
         this.ilLarge.ImageSize = new System.Drawing.Size(32, 32);
         this.ilLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilLarge.ImageStream")));
         this.ilLarge.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // listView1
         // 
         this.listView1.AllowDrop = true;
         this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                    this.columnHeader1,
                                                                                    this.columnHeader2});
         this.listView1.ContextMenu = this.cmView;
         this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
         listViewItem1.UseItemStyleForSubItems = false;
         listViewItem2.UseItemStyleForSubItems = false;
         listViewItem3.UseItemStyleForSubItems = false;
         listViewItem4.UseItemStyleForSubItems = false;
         this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
                                                                                  listViewItem1,
                                                                                  listViewItem2,
                                                                                  listViewItem3,
                                                                                  listViewItem4});
         this.listView1.LargeImageList = this.ilLarge;
         this.listView1.Location = new System.Drawing.Point(163, 0);
         this.listView1.Name = "listView1";
         this.listView1.Size = new System.Drawing.Size(213, 296);
         this.listView1.SmallImageList = this.ilSmall;
         this.listView1.TabIndex = 0;
         this.listView1.View = System.Windows.Forms.View.Details;
         this.listView1.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
         this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView1_DragDrop);
         this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView1_DragEnter);
         // 
         // columnHeader1
         // 
         this.columnHeader1.Text = "Outlook";
         this.columnHeader1.Width = 73;
         // 
         // columnHeader2
         // 
         this.columnHeader2.Text = "Probability";
         this.columnHeader2.Width = 84;
         // 
         // ilSmall
         // 
         this.ilSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
         this.ilSmall.ImageSize = new System.Drawing.Size(16, 16);
         this.ilSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSmall.ImageStream")));
         this.ilSmall.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // treeView1
         // 
         this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
         this.treeView1.ImageList = this.ilSmall;
         this.treeView1.Name = "treeView1";
         this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
                                                                              new System.Windows.Forms.TreeNode("Sun", 0, 0),
                                                                              new System.Windows.Forms.TreeNode("Snow", 1, 1),
                                                                              new System.Windows.Forms.TreeNode("Clouds", 2, 2, new System.Windows.Forms.TreeNode[] {
                                                                                                                                                                       new System.Windows.Forms.TreeNode("Rain", 3, 3)})});
         this.treeView1.Size = new System.Drawing.Size(160, 296);
         this.treeView1.TabIndex = 1;
         this.treeView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView1_ItemDrag);
         // 
         // splitter1
         // 
         this.splitter1.Location = new System.Drawing.Point(160, 0);
         this.splitter1.Name = "splitter1";
         this.splitter1.Size = new System.Drawing.Size(3, 296);
         this.splitter1.TabIndex = 2;
         this.splitter1.TabStop = false;
         // 
         // Form1
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(376, 296);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.listView1,
                                                                      this.splitter1,
                                                                      this.treeView1});
         this.Name = "Form1";
         this.Text = "Weather View";
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

      private void listView1_ItemActivate(object sender, System.EventArgs e)
      {
         MessageBox.Show (listView1.SelectedItems[0].Text);
      }

      private void miLargeIcon_Click(object sender, System.EventArgs e)
      {
            listView1.View = View.LargeIcon;
      }

      private void miSmallIcon_Click(object sender, System.EventArgs e)
      {
         listView1.View = View.SmallIcon;
      }

      private void miList_Click(object sender, System.EventArgs e)
      {
         listView1.View = View.List;
      }

      private void miDetails_Click(object sender, System.EventArgs e)
      {
         listView1.View = View.Details;
      }

      private void cmView_Popup(object sender, System.EventArgs e)
      {
         miLargeIcon.Checked = (listView1.View == View.LargeIcon);
         miSmallIcon.Checked = (listView1.View == View.SmallIcon);
         miList.Checked = (listView1.View == View.List);
         miDetails.Checked = (listView1.View == View.Details);
      }

      private void treeView1_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
      {
         // Start drag and drop operation
         treeView1.DoDragDrop (e.Item, DragDropEffects.Move);
      }

      private void listView1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
      {
         // Decide if drop is permitted
         if (e.Data.GetDataPresent (typeof (TreeNode)))
            e.Effect = DragDropEffects.Move;
      }

      private void listView1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
      {
         // Handle drop: retrieve original data (TreeNode) and move to ListView
         if (e.Data.GetDataPresent (typeof (TreeNode)))
         {
            TreeNode tn = (TreeNode) e.Data.GetData (typeof (TreeNode));
            listView1.Items.Add (tn.Text, tn.ImageIndex);
            treeView1.Nodes.Remove (tn);
         }
      }
	}
}
