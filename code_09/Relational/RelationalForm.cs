using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace XMLDataset
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class DataSetForm : System.Windows.Forms.Form
	{
      private System.Windows.Forms.Button button1;
      private System.Windows.Forms.DataGrid m_gridEmployees;
      private System.Windows.Forms.Button button2;
      private System.Windows.Forms.DataGrid m_gridRaises;
      private System.Windows.Forms.DataGrid m_gridDataTypes;
      private System.Windows.Forms.Button btnTraverse;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DataSetForm()
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
         this.m_gridEmployees = new System.Windows.Forms.DataGrid();
         this.m_gridDataTypes = new System.Windows.Forms.DataGrid();
         this.m_gridRaises = new System.Windows.Forms.DataGrid();
         this.button1 = new System.Windows.Forms.Button();
         this.button2 = new System.Windows.Forms.Button();
         this.btnTraverse = new System.Windows.Forms.Button();
         ((System.ComponentModel.ISupportInitialize)(this.m_gridEmployees)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_gridDataTypes)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_gridRaises)).BeginInit();
         this.SuspendLayout();
         // 
         // m_gridEmployees
         // 
         this.m_gridEmployees.CaptionText = "DataSet From XML";
         this.m_gridEmployees.DataMember = "";
         this.m_gridEmployees.Location = new System.Drawing.Point(16, 24);
         this.m_gridEmployees.Name = "m_gridEmployees";
         this.m_gridEmployees.Size = new System.Drawing.Size(568, 143);
         this.m_gridEmployees.TabIndex = 2;
         // 
         // m_gridDataTypes
         // 
         this.m_gridDataTypes.CaptionText = "Table Column Data Types";
         this.m_gridDataTypes.DataMember = "";
         this.m_gridDataTypes.Location = new System.Drawing.Point(19, 293);
         this.m_gridDataTypes.Name = "m_gridDataTypes";
         this.m_gridDataTypes.Size = new System.Drawing.Size(568, 173);
         this.m_gridDataTypes.TabIndex = 3;
         // 
         // m_gridRaises
         // 
         this.m_gridRaises.CaptionText = "Pay Raise Information";
         this.m_gridRaises.DataMember = "";
         this.m_gridRaises.Location = new System.Drawing.Point(16, 182);
         this.m_gridRaises.Name = "m_gridRaises";
         this.m_gridRaises.Size = new System.Drawing.Size(568, 95);
         this.m_gridRaises.TabIndex = 3;
         // 
         // button1
         // 
         this.button1.Location = new System.Drawing.Point(600, 24);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(88, 48);
         this.button1.TabIndex = 0;
         this.button1.Text = "Load XML Without Schema";
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // button2
         // 
         this.button2.Location = new System.Drawing.Point(600, 96);
         this.button2.Name = "button2";
         this.button2.Size = new System.Drawing.Size(88, 48);
         this.button2.TabIndex = 2;
         this.button2.Text = "Load XML With Schema";
         this.button2.Click += new System.EventHandler(this.button2_Click);
         // 
         // btnTraverse
         // 
         this.btnTraverse.Location = new System.Drawing.Point(600, 168);
         this.btnTraverse.Name = "btnTraverse";
         this.btnTraverse.Size = new System.Drawing.Size(88, 48);
         this.btnTraverse.TabIndex = 2;
         this.btnTraverse.Text = "Traverse XML";
         this.btnTraverse.Click += new System.EventHandler(this.btnTraverse_Click);
         // 
         // DataSetForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(696, 522);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.btnTraverse,
                                                                      this.m_gridDataTypes,
                                                                      this.m_gridRaises,
                                                                      this.m_gridEmployees,
                                                                      this.button2,
                                                                      this.button1});
         this.Name = "DataSetForm";
         this.Text = "Relational Data XML Sample";
         ((System.ComponentModel.ISupportInitialize)(this.m_gridEmployees)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_gridDataTypes)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_gridRaises)).EndInit();
         this.ResumeLayout(false);

      }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new DataSetForm());
		}

      /// <summary>
      /// Called when Load XML Without Schema button is pressed
      /// </summary>
      private void button1_Click(object sender, System.EventArgs e)
      {
         Cursor currentCursor = Cursor.Current;

         try
         {
            Cursor.Current = Cursors.WaitCursor;

            // Create two new datasets and load them from XML files
            // on disk
            DataSet dsEmployees = new DataSet( "Employees" );
            DataSet dsWageHistory = new DataSet( "WageHistory" );
            loadAndDisplayDatasets( dsEmployees, dsWageHistory );
         }
         catch ( Exception exception )
         {
            MessageBox.Show( exception.Message );
         }
         finally 
         {
            Cursor.Current = currentCursor;
         }
      }


      /// <summary>
      /// Called when Load XML With Schema button is pressed
      /// </summary>
      private void button2_Click(object sender, System.EventArgs e)
      {
         Cursor currentCursor = Cursor.Current;

         try
         {
            Cursor.Current = Cursors.WaitCursor;

            // Create two new datasets and load them from XML files
            // on disk using XML schemas
            DataSet dsEmployees = new DataSet( "Employees" );
            DataSet dsWageHistory = new DataSet( "WageHistory" );

            dsEmployees.ReadXmlSchema( "employee.xsd" );
            dsWageHistory.ReadXmlSchema( "wagehistory.xsd" );
            loadAndDisplayDatasets( dsEmployees, dsWageHistory );
         }
         catch ( Exception exception )
         {
            MessageBox.Show( exception.Message );
         }
         finally 
         {
            Cursor.Current = currentCursor;
         }
      }

      /// <summary>
      /// Loads XML files from disk into a dataset and displays the
      /// data contained in them on screen        
      /// </summary>
      /// <param name="dsEmployees">The employees dataset</param>
      /// <param name="dsWageHistory">The wage history dataset</param>
      private void loadAndDisplayDatasets( DataSet dsEmployees, 
         DataSet dsWageHistory )
      {
         // Load the dataset from XML files on disk
         dsEmployees.ReadXml( "employee1.xml" );
         dsWageHistory.ReadXml( "wagehistory.xml" );

         // Copy the WageChange table into the Employees dataset
         dsEmployees.Tables.Add( 
            dsWageHistory.Tables["WageChange"].Copy() );

         DataTable tblEmp = dsEmployees.Tables["Employee"];
         DataTable tblWageHistory = dsEmployees.Tables["WageChange"];

         // Create a relation between the two tables based on employee
         // ID
         DataRelation relation = new DataRelation(
            "EmpWageHistory", 
            new DataColumn[] {tblEmp.Columns["EmployeeID"]},
            new DataColumn[] {tblWageHistory.Columns["EmployeeID"]},
            false);
         
         dsEmployees.Relations.Add( relation );

         // Bind the dataset to the grid, so we can see it
         m_gridEmployees.SetDataBinding( dsEmployees, "Employee" );
         m_gridRaises.SetDataBinding( dsEmployees, "Employee.EmpWageHistory" );

         // Save the schema to disk
         dsEmployees.WriteXmlSchema( "employees2.xsd" );
 
         // Create a third dataset to hold the names of the columns
         // and their datatypes so we can display this information in
         // the third grid to see the effect of using a schema when
         // reading in the xml files.
         DataSet dsDataTypes = new DataSet( "DataTypes" );
         DataTable tblDataTypes = new DataTable( "DataTypes" );
         DataColumn colName = new DataColumn( "Column Name" );
         DataColumn colType = new DataColumn( "Data Type" );

         tblDataTypes.Columns.Add( colName );
         tblDataTypes.Columns.Add( colType );
         dsDataTypes.Tables.Add( tblDataTypes );

         foreach ( DataTable table in dsEmployees.Tables )
         {
            string strTableName = table.TableName;

            foreach ( DataColumn column in table.Columns )
            {
               string strName = strTableName + "." + column.ColumnName;
               string strDataType = column.DataType.ToString();

               DataRow row = tblDataTypes.NewRow();
               row["Column Name"] = strName;
               row["Data Type"] = strDataType;
               tblDataTypes.Rows.Add( row );
            }
         }
         
         // Bind the column and data type information to the grid
         m_gridDataTypes.PreferredColumnWidth = 200;
         m_gridDataTypes.SetDataBinding( dsDataTypes, "DataTypes" );
      }

      /// <summary>
      /// Called when Traverse XML button is pressed. Shows new form.
      /// </summary>
      private void btnTraverse_Click(object sender, System.EventArgs e)
      {
         Cursor currentCursor = Cursor.Current;

         try
         {
          Cursor.Current = Cursors.WaitCursor;
          Form form = new TraversalForm();
          form.ShowDialog();
         }
         catch ( Exception exception )
         {
            MessageBox.Show( exception.Message );
         }
         finally 
         {
            Cursor.Current = currentCursor;
         }
      }

   }
}
