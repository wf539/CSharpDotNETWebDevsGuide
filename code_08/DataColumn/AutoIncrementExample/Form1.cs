using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace AutoIncrementExample
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.DataGrid dataGrid1;
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
			this.button1 = new System.Windows.Forms.Button();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(216, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(160, 32);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.Location = new System.Drawing.Point(176, 72);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(328, 176);
			this.dataGrid1.TabIndex = 1;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(552, 301);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.dataGrid1,
																		  this.button1});
			this.Name = "Form1";
			this.Text = "Form1";
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

		private void button1_Click(object sender, System.EventArgs e)
		{
			DataTable tmpTable;

			tmpTable = AddAutoIncrementColumn();
			dataGrid1.DataSource = tmpTable;
		}

		private DataTable AddAutoIncrementColumn()
		{
			DataColumn myColumn = new DataColumn();
			DataColumn myData = new DataColumn();

			myColumn.DataType = System.Type.GetType("System.Int32");
			myColumn.ColumnName = "PK_ID";
			myColumn.AutoIncrement = true;
			myColumn.ReadOnly = true;

			myData.DataType = System.Type.GetType("System.String");
			myData.ColumnName = "strData";

			// Add the column to a new DataTable.
			DataTable myTable = new DataTable("MyTable");
			myTable.Columns.Add(myColumn);
			myTable.Columns.Add(myData);

			return myTable;
		}

	}
}
