using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace XMLDataset
{
	/// <summary>
	/// Summary description for TraversalForm.
	/// </summary>
	public class TraversalForm : System.Windows.Forms.Form
	{
      private System.Windows.Forms.TextBox textBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private string m_strOutput;

		public TraversalForm()
		{
			InitializeComponent();

         try
         {
            // Load the document
            XmlDataDocument doc = loadXML();
            if ( doc == null )
               return;

            // Traverse the document using relational calls
            m_strOutput += 
               "***Retrieving Using Relational Calls***\r\n\r\n";
            retrieveAsData( doc );

            // Traverse the document using DOM calls
            m_strOutput += 
               "\r\n\r\n***Retrieving Using DOM Calls***\r\n\r\n";
            retrieveAsXml( doc );

            // Show the document 
            m_strOutput += 
               "\r\n\r\n***The XMLDataDocument ***\r\n\r\n";
            StringWriter writerString = new StringWriter();
            XmlTextWriter writer = new XmlTextWriter( writerString );
            writer.Formatting = Formatting.Indented;
            doc.WriteTo( writer );
            writer.Flush();
            m_strOutput += writerString.ToString();

            textBox1.Text = m_strOutput;
         }
         catch ( Exception exception )
         {
            MessageBox.Show( exception.Message );
         }
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
         this.textBox1 = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // textBox1
         // 
         this.textBox1.Location = new System.Drawing.Point(24, 27);
         this.textBox1.Multiline = true;
         this.textBox1.Name = "textBox1";
         this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.textBox1.Size = new System.Drawing.Size(351, 345);
         this.textBox1.TabIndex = 0;
         this.textBox1.Text = "";
         // 
         // TraversalForm
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(398, 398);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.textBox1});
         this.Name = "TraversalForm";
         this.Text = "TraversalForm";
         this.ResumeLayout(false);

      }
		#endregion

		/// <summary>
		/// Uses relational calls to traverse the XML document and
      /// display one employee on screen.
		/// </summary>
      /// <param name="doc">The XML document</param>
      private void retrieveAsData( XmlDataDocument doc )
      {
         string strFirstName;
         string strMiddleName;
         string strLastName;
         string strDate;
         string strWage;

         // Use the Select method to retrieve data relationally
         DataTable tblEmp = doc.DataSet.Tables["Employee"];
         DataRelation relation = 
            doc.DataSet.Relations["EmpWageHistory"];
         DataRow[] rows = tblEmp.Select( "EmployeeID = 1" );
        
         for( int i = 0; i < rows.Length; i ++ )
         {
            // Get the Employee information retrieved
            DataRow rowEmp = rows[i];

            strFirstName = rowEmp[1].ToString();
            strMiddleName = rowEmp[2].ToString();
            strLastName = rowEmp[3].ToString();

            m_strOutput += "Name: " + strFirstName;
            m_strOutput += " " + strMiddleName;
            m_strOutput += " " + strLastName + "\r\n";

            // Now get the Wage history information, it is a child
            // of the Employee row.
            DataRow[] rowsWage = rowEmp.GetChildRows( relation );
            for( int j = 0; j < rowsWage.Length; j++ )
            {
               DataRow rowWage = rowsWage[j];
               strDate = rowWage[1].ToString();
               strWage = rowWage[2].ToString();

               m_strOutput += "Wage Chage Date: " + 
                  strDate.Substring( 0, 10);
               m_strOutput += " Amount: " + strWage + "\r\n";
            }
         }
      }

		/// <summary>
		/// Uses XML DOM calls to traverse the XML document and
      /// display one employee on screen.
		/// </summary>
      /// <param name="doc">The XML document</param>
      private void retrieveAsXml( XmlDataDocument doc )
      {
         String strEmployeeID = "1";
         
         String strData = "";
         String strTag = "";
         String strFirstName = "";
         String strMiddleName = "";
         String strLastName = "";
         String strDate = "";
         String strWage = "";

         bool bWroteEmployeeName = false;

         XmlElement root = doc.DocumentElement;

         // Get all employee elements in the document
         XmlNodeList nodeList1 = 
            root.GetElementsByTagName( "Employee" );

         foreach ( XmlNode node1 in nodeList1 )
         {
            if ( node1 is System.Xml.XmlElement )
            {
               // See if this is the employee ID we were looking for
               XmlElement elemEmp = (XmlElement) node1;      
               if ( elemEmp.GetAttribute( "EmployeeID" ) 
                  != strEmployeeID )
                  continue;
         
               XmlNodeList nodeList2 = elemEmp.ChildNodes;
               foreach ( XmlNode node2 in nodeList2 )
               {
                  if ( node2 is System.Xml.XmlElement )
                  {
                     // If we find a <WageChange> element we must
                     // traverse its children, othewise retrieve
                     // the employee name information.
                     XmlElement element2 = (XmlElement) node2;
                     strTag = element2.Name;
                     if ( strTag == "WageChange" )
                     {
                        if ( bWroteEmployeeName == false )
                        {
                           m_strOutput += "Name: " + strFirstName;
                           m_strOutput += " " + strMiddleName;
                           m_strOutput += " " + strLastName + "\r\n";
                           bWroteEmployeeName = true;
                        }

                        XmlElement elemWage = element2;
                        XmlNodeList nodeList3 = element2.ChildNodes;
               
                        foreach ( XmlNode node3 in nodeList3 )
                        {
                           if ( node3 is System.Xml.XmlElement )
                           {
                              XmlElement element3 = (XmlElement) node3;
                              strTag = element3.Name;
                              strData = element3.FirstChild.Value;
               
                              if ( strTag == "Date" )
                                 strDate = strData;
                              else if ( strTag == "Wage" )
                                 strWage = strData;
                           }
                        }

                        m_strOutput += "Wage Change Date: " + 
                           strDate.Substring( 0, 10);
                        m_strOutput += " Amount: " + strWage + "\r\n";
                     }
                     else
                     {
                        strData = element2.FirstChild.Value;
               
                        if ( strTag == "FirstName" )
                           strFirstName = strData;
                        else if ( strTag == "MiddleName" )
                           strMiddleName = strData;
                        else if ( strTag == "LastName" )
                           strLastName = strData;
                     }
                  }
               }
            }
         }
      }

		/// <summary>
		/// Loads two XML documents from disk into a XMLDataDocument and
      /// establishes a realtion between the two.
		/// </summary>
      /// <returns>The XMLDataDocument</returns>
      private XmlDataDocument loadXML()
      {
         // Load the employees XML file
         DataSet dsEmployees = new DataSet( "Employees" );
         dsEmployees.ReadXmlSchema( "employee.xsd" );
         dsEmployees.ReadXml( "employee1.xml" );
         dsEmployees.Tables[0].TableName = "Employee";

         // Load the wage history XML file
         DataSet dsWageHistory = new DataSet( "WageHistory" );
         dsWageHistory.ReadXmlSchema( "wagehistory.xsd" );
         dsWageHistory.ReadXml( "wagehistory.xml" );
         dsWageHistory.Tables[0].TableName = "WageChange";

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

         // Set as nested. If an XML document is written to disk it
         // will now contain <WageChange> elements as children of 
         // <Employee> elements.
         relation.Nested = true;

         // Add the relation
         dsEmployees.Relations.Add( relation );

         // Instantiate the document
         XmlDataDocument doc = new XmlDataDocument( dsEmployees );
         return doc;
      }
   }
}
