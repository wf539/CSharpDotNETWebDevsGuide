using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace OrdersDataSet
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.ComboBox comboBox1;

		private string sConn = "Provider=Microsoft.Jet.OLEDB.4.0;" +
								"Data Source=F:\\Books\\C#.NET\\CDROM\\OrdersDataSet\\Data\\Northwind.mdb;" +
								"Persist Security Info=False";
		private CDalOleDb db;
		private DataSet ds;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cmdUpdate;
		
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
			Cursor.Current = Cursors.WaitCursor;
			db = new CDalOleDb(sConn);
			popCboCustomers();
			Cursor.Current = Cursors.Default;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				// Just in case they forgot
				SaveRecords();
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
			this.label1 = new System.Windows.Forms.Label();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.cmdUpdate = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(64, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(184, 24);
			this.label1.TabIndex = 6;
			this.label1.Text = "Customer ID:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dataGrid1
			// 
			this.dataGrid1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.dataGrid1.DataMember = "";
			this.dataGrid1.Location = new System.Drawing.Point(0, 40);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(392, 232);
			this.dataGrid1.TabIndex = 1;
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownWidth = 264;
			this.comboBox1.Location = new System.Drawing.Point(256, 8);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(128, 21);
			this.comboBox1.TabIndex = 2;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// cmdUpdate
			// 
			this.cmdUpdate.Location = new System.Drawing.Point(8, 8);
			this.cmdUpdate.Name = "cmdUpdate";
			this.cmdUpdate.Size = new System.Drawing.Size(56, 23);
			this.cmdUpdate.TabIndex = 7;
			this.cmdUpdate.Text = "Update";
			this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(392, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.cmdUpdate,
																		  this.label1,
																		  this.comboBox1,
																		  this.dataGrid1});
			this.Name = "Form1";
			this.Text = "Orders";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
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

		

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string sCustID = comboBox1.SelectedItem.ToString();
			Cursor.Current = Cursors.WaitCursor;
			label1.Text = GetCustomerName(sCustID);
			popGrdOrders(sCustID);
			Cursor.Current = Cursors.Default;
		}

		private void popGrdOrders(string sCustID)
		{
			if (ds != null)
			{
				ds.Clear();
			}
			ds = db.GetOrders(sCustID);
			dataGrid1.DataSource = ds;
		}

		private void Accept_Click(object sender, System.EventArgs e)
		{
			ds.AcceptChanges();
		}

		private void Reject_Click(object sender, System.EventArgs e)
		{
			ds.RejectChanges();
		}

		private void Save_Click(object sender, System.EventArgs e)
		{
			SaveRecords();
		}

		private void popCboCustomers()
		{
			OleDbDataReader dr;

			dr = db.GetCustomers();

			comboBox1.Items.Clear();
			comboBox1.BeginUpdate();
			while (dr.Read()) 
			{
				comboBox1.Items.Add(dr.GetString(0));
			}
			comboBox1.EndUpdate();
			
			// always call Close when done reading, this frees up the 
			// connection to service other requests.
			dr.Close();
			
		}

		private string GetCustomerName(string sCustID)
		{
			return db.GetCustomerName(sCustID);
		}

		private void SaveRecords()
		{
			db.SaveRecords("Orders");
			//this.popGrdOrders(comboBox1.SelectedItem.ToString());
		}

		private void cmdUpdate_Click(object sender, System.EventArgs e)
		{
			SaveRecords();
		}

			
	}
}
