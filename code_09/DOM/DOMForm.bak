using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Xml;

namespace DOM
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
      private System.Windows.Forms.TextBox m_FirstName;
      private System.Windows.Forms.TextBox m_LastName;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.DateTimePicker dateTimePicker1;
      private System.Windows.Forms.GroupBox groupBox3;
      private System.Windows.Forms.TextBox m_Wage;
      private System.Windows.Forms.TextBox m_MiddleName;
      private System.Windows.Forms.RadioButton radioSalaried;
      private System.Windows.Forms.RadioButton radioHourly;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox m_SSN;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.GroupBox groupBox4;
      private System.Windows.Forms.Button buttonNew;
      private System.Windows.Forms.Button btnUpdate;
      private System.Windows.Forms.Button btnDelete;
      private System.Windows.Forms.Label label7;
      private System.Windows.Forms.Button btnReset;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

      private XmlDocument m_xmlDocument;
      private System.Windows.Forms.ComboBox m_comboEmp;
      private int m_NextID = 1;
      private int m_curEmployeeID = 0;
      private System.Windows.Forms.CheckBox checkboxActive;

      private string TagEmployees  = "Employees";
      private string TagEmployee   = "Employee";
      private string TagEmployeeID = "EmployeeID";
      private string TagFirstName  = "FirstName";
      private string TagMiddleName = "MiddleName";
      private string TagLastName   = "LastName";
      private string TagSalaried   = "Salaried";
      private string TagWage       = "Wage";
      private string TagActive     = "Active";
      private string TagSSN        = "SSN";
      private string TagStartDate  = "StartDate";

      private string XMLFileName = "employees.xml";
      

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         intializeForm();
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
         this.checkboxActive = new System.Windows.Forms.CheckBox();
         this.radioHourly = new System.Windows.Forms.RadioButton();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.groupBox3 = new System.Windows.Forms.GroupBox();
         this.groupBox4 = new System.Windows.Forms.GroupBox();
         this.m_MiddleName = new System.Windows.Forms.TextBox();
         this.m_SSN = new System.Windows.Forms.TextBox();
         this.m_Wage = new System.Windows.Forms.TextBox();
         this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
         this.radioSalaried = new System.Windows.Forms.RadioButton();
         this.m_FirstName = new System.Windows.Forms.TextBox();
         this.label3 = new System.Windows.Forms.Label();
         this.m_LastName = new System.Windows.Forms.TextBox();
         this.buttonNew = new System.Windows.Forms.Button();
         this.label4 = new System.Windows.Forms.Label();
         this.label5 = new System.Windows.Forms.Label();
         this.label6 = new System.Windows.Forms.Label();
         this.label7 = new System.Windows.Forms.Label();
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.m_comboEmp = new System.Windows.Forms.ComboBox();
         this.btnDelete = new System.Windows.Forms.Button();
         this.btnReset = new System.Windows.Forms.Button();
         this.btnUpdate = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // checkboxActive
         // 
         this.checkboxActive.Checked = true;
         this.checkboxActive.CheckState = System.Windows.Forms.CheckState.Checked;
         this.checkboxActive.Location = new System.Drawing.Point(328, 184);
         this.checkboxActive.Name = "checkboxActive";
         this.checkboxActive.Size = new System.Drawing.Size(64, 24);
         this.checkboxActive.TabIndex = 9;
         this.checkboxActive.Text = "Active";
         // 
         // radioHourly
         // 
         this.radioHourly.Location = new System.Drawing.Point(400, 96);
         this.radioHourly.Name = "radioHourly";
         this.radioHourly.Size = new System.Drawing.Size(64, 24);
         this.radioHourly.TabIndex = 7;
         this.radioHourly.Text = "Hourly";
         // 
         // groupBox1
         // 
         this.groupBox1.Location = new System.Drawing.Point(48, 81);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(252, 191);
         this.groupBox1.TabIndex = 9;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Demographics";
         // 
         // groupBox2
         // 
         this.groupBox2.Location = new System.Drawing.Point(312, 80);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(232, 80);
         this.groupBox2.TabIndex = 4;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "Wage Information";
         // 
         // groupBox3
         // 
         this.groupBox3.Location = new System.Drawing.Point(312, 168);
         this.groupBox3.Name = "groupBox3";
         this.groupBox3.Size = new System.Drawing.Size(232, 105);
         this.groupBox3.TabIndex = 8;
         this.groupBox3.TabStop = false;
         this.groupBox3.Text = "Status Information";
         // 
         // groupBox4
         // 
         this.groupBox4.Location = new System.Drawing.Point(28, 58);
         this.groupBox4.Name = "groupBox4";
         this.groupBox4.Size = new System.Drawing.Size(536, 232);
         this.groupBox4.TabIndex = 10;
         this.groupBox4.TabStop = false;
         this.groupBox4.Text = "Employee Information";
         // 
         // m_MiddleName
         // 
         this.m_MiddleName.Location = new System.Drawing.Point(138, 144);
         this.m_MiddleName.Name = "m_MiddleName";
         this.m_MiddleName.Size = new System.Drawing.Size(136, 20);
         this.m_MiddleName.TabIndex = 3;
         this.m_MiddleName.Text = "";
         // 
         // m_SSN
         // 
         this.m_SSN.Location = new System.Drawing.Point(138, 220);
         this.m_SSN.Name = "m_SSN";
         this.m_SSN.Size = new System.Drawing.Size(136, 20);
         this.m_SSN.TabIndex = 5;
         this.m_SSN.Text = "";
         // 
         // m_Wage
         // 
         this.m_Wage.Location = new System.Drawing.Point(368, 128);
         this.m_Wage.Name = "m_Wage";
         this.m_Wage.TabIndex = 8;
         this.m_Wage.Text = "";
         // 
         // dateTimePicker1
         // 
         this.dateTimePicker1.CustomFormat = "";
         this.dateTimePicker1.Location = new System.Drawing.Point(328, 232);
         this.dateTimePicker1.Name = "dateTimePicker1";
         this.dateTimePicker1.TabIndex = 10;
         // 
         // radioSalaried
         // 
         this.radioSalaried.Checked = true;
         this.radioSalaried.Location = new System.Drawing.Point(328, 96);
         this.radioSalaried.Name = "radioSalaried";
         this.radioSalaried.Size = new System.Drawing.Size(64, 24);
         this.radioSalaried.TabIndex = 6;
         this.radioSalaried.TabStop = true;
         this.radioSalaried.Text = "Salaried";
         // 
         // m_FirstName
         // 
         this.m_FirstName.Location = new System.Drawing.Point(138, 106);
         this.m_FirstName.Name = "m_FirstName";
         this.m_FirstName.Size = new System.Drawing.Size(136, 20);
         this.m_FirstName.TabIndex = 2;
         this.m_FirstName.Text = "";
         // 
         // label3
         // 
         this.label3.Location = new System.Drawing.Point(51, 144);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(80, 23);
         this.label3.TabIndex = 2;
         this.label3.Text = "Middle Name:";
         this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // m_LastName
         // 
         this.m_LastName.Location = new System.Drawing.Point(138, 182);
         this.m_LastName.Name = "m_LastName";
         this.m_LastName.Size = new System.Drawing.Size(136, 20);
         this.m_LastName.TabIndex = 4;
         this.m_LastName.Text = "";
         // 
         // buttonNew
         // 
         this.buttonNew.Location = new System.Drawing.Point(164, 313);
         this.buttonNew.Name = "buttonNew";
         this.buttonNew.TabIndex = 11;
         this.buttonNew.Text = "New";
         this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
         // 
         // label4
         // 
         this.label4.Location = new System.Drawing.Point(328, 128);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(40, 23);
         this.label4.TabIndex = 2;
         this.label4.Text = "Wage:";
         // 
         // label5
         // 
         this.label5.Location = new System.Drawing.Point(67, 220);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(64, 23);
         this.label5.TabIndex = 2;
         this.label5.Text = "SSN:";
         this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // label6
         // 
         this.label6.Location = new System.Drawing.Point(320, 216);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(64, 23);
         this.label6.TabIndex = 6;
         this.label6.Text = "Start Date:";
         this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // label7
         // 
         this.label7.Location = new System.Drawing.Point(152, 20);
         this.label7.Name = "label7";
         this.label7.Size = new System.Drawing.Size(61, 23);
         this.label7.TabIndex = 14;
         this.label7.Text = "Employee:";
         this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(67, 106);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(64, 23);
         this.label1.TabIndex = 2;
         this.label1.Text = "First Name:";
         this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // label2
         // 
         this.label2.Location = new System.Drawing.Point(67, 182);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(64, 23);
         this.label2.TabIndex = 2;
         this.label2.Text = "Last Name:";
         this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
         // 
         // m_comboEmp
         // 
         this.m_comboEmp.DropDownWidth = 121;
         this.m_comboEmp.Location = new System.Drawing.Point(218, 20);
         this.m_comboEmp.Name = "m_comboEmp";
         this.m_comboEmp.Size = new System.Drawing.Size(222, 21);
         this.m_comboEmp.TabIndex = 1;
         this.m_comboEmp.SelectedIndexChanged += new System.EventHandler(this.SelectedEmployeeIndexChanged);
         // 
         // btnDelete
         // 
         this.btnDelete.Location = new System.Drawing.Point(354, 313);
         this.btnDelete.Name = "btnDelete";
         this.btnDelete.TabIndex = 13;
         this.btnDelete.Text = "Delete";
         this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
         // 
         // btnReset
         // 
         this.btnReset.Location = new System.Drawing.Point(490, 5);
         this.btnReset.Name = "btnReset";
         this.btnReset.Size = new System.Drawing.Size(89, 39);
         this.btnReset.TabIndex = 15;
         this.btnReset.Text = "Empty Employee List";
         this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
         // 
         // btnUpdate
         // 
         this.btnUpdate.Location = new System.Drawing.Point(259, 314);
         this.btnUpdate.Name = "btnUpdate";
         this.btnUpdate.TabIndex = 12;
         this.btnUpdate.Text = "Save";
         this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
         // 
         // Form1
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(592, 357);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.btnReset,
                                                                      this.label7,
                                                                      this.btnDelete,
                                                                      this.btnUpdate,
                                                                      this.buttonNew,
                                                                      this.m_SSN,
                                                                      this.label5,
                                                                      this.dateTimePicker1,
                                                                      this.label6,
                                                                      this.checkboxActive,
                                                                      this.m_MiddleName,
                                                                      this.label3,
                                                                      this.m_LastName,
                                                                      this.label2,
                                                                      this.label1,
                                                                      this.m_FirstName,
                                                                      this.m_comboEmp,
                                                                      this.radioHourly,
                                                                      this.label4,
                                                                      this.m_Wage,
                                                                      this.radioSalaried,
                                                                      this.groupBox2,
                                                                      this.groupBox3,
                                                                      this.groupBox1,
                                                                      this.groupBox4});
         this.Name = "Form1";
         this.Text = "DOM Sample";
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

      /// <summary>
      /// Initializes the form. If we have an employee XML file already
      /// saved, load it. Otherwise, create an empty one.        
      /// </summary>
      private void intializeForm()
      {
         bool bLoaded = false;
         
         try
         {
            // Load an existing employee file if there is one
            m_xmlDocument = new XmlDocument();
            m_xmlDocument.Load( XMLFileName );
            bLoaded = true;   
         }
         catch
         {
            // Ignore, we couldn't load the xml document from disk
         }

         try
         {
            // If we found an employee file, load the form, otherwise
            // create and empty XML document.
            if ( bLoaded == true )
               fillComboBox();
            else
               createEmptyXMLDocument();
         }
         catch ( Exception exception )
         {
            MessageBox.Show( exception.Message );   
         }
      }


      /// <summary>
      /// Handler for the "Empty Employee List" button. Starts with a 
      /// fresh XML document.
      /// </summary>
      /// <param name="sender">Event sender, this form.</param>
      /// <param name="e">Event arguments - unused</param>
      private void btnReset_Click(object sender, System.EventArgs e)
      {
         createEmptyXMLDocument();
         m_xmlDocument.Save( XMLFileName );
         m_NextID = 1;
         m_curEmployeeID = 0;
         m_comboEmp.Items.Clear();
         m_comboEmp.Text = "";
         buttonNew_Click( this, null );
      }


      /// <summary>
      /// Handler for the "New" button. Clears form for creating a new
      /// employee.
      /// </summary>
      /// <param name="sender">Event sender, this form.</param>
      /// <param name="e">Event arguments - unused</param>
      private void buttonNew_Click(object sender, System.EventArgs e)
      {
         m_FirstName.Text = "";
         m_MiddleName.Text = "";
         m_LastName.Text = "";
         m_Wage.Text = "";
         m_SSN.Text = "";
         radioSalaried.Checked = true;   
         radioSalaried.Checked = true;   
         dateTimePicker1.Value = DateTime.Today;
         m_comboEmp.SelectedIndex = -1;
         m_curEmployeeID = 0;
         m_FirstName.Select();
      }

      /// <summary>
      /// Handler for the "Save" button. We either are adding a new
      /// employee or updating one.
      /// </summary>
      /// <param name="sender">Event sender, this form.</param>
      /// <param name="e">Event arguments - unused</param>
      private void btnUpdate_Click(object sender, System.EventArgs e)
      {
         // Is this a new or an update, check current employee ID
         bool bAddNew = (m_curEmployeeID == 0) ? true : false;

         try
         {
            // Gather the data from the screen
            string strFirstName = m_FirstName.Text;
            string strMiddleName = m_MiddleName.Text;
            string strLastName = m_LastName.Text;
            string strWage = m_Wage.Text;
            string strSSN = m_SSN.Text;
            string strSalaried = 
               radioSalaried.Checked ? "true" : "false";   
            string strActive = 
               radioSalaried.Checked ? "true" : "false";   
            string strDate = 
               dateTimePicker1.Value.ToString("yyy-MM-dd");

            // Validate the data
            if ( validateNotEmpty( strFirstName, "First name", 
               m_FirstName ) == false )
               return;
               
            if ( validateNotEmpty( strMiddleName, "Middle name", 
               m_MiddleName ) == false )
               return;
               
            if ( validateNotEmpty( strLastName, "Last name", 
               m_LastName ) == false )
               return;
               
            if ( validateNotEmpty( strSSN, "SSN", m_SSN ) == false )
               return;
            if ( validateNotEmpty( strWage, "Wage", m_Wage ) == false )
               return;
            if ( validateDouble( strWage, "Wage", m_Wage ) == false )
               return;

            if ( bAddNew == true )
            {
               // Add employee to the document and to the combo box 
               // and then set the next available ID.
               addEmployee( m_xmlDocument, m_NextID, strFirstName, 
                  strMiddleName, strLastName, strSalaried, strWage,
                  strActive, strSSN, strDate );
                  
               addEmployeeToCombo( m_NextID, strFirstName, 
                  strMiddleName, strLastName );
                  
               m_NextID++;
            }
            else
            {
               // Update the current employee
               updateEmployee( m_xmlDocument, m_curEmployeeID, 
                  strFirstName, strMiddleName, strLastName, 
                  strSalaried, strWage, strActive, strSSN, strDate );
            }
            
            // Commit changes to disk file.
            m_xmlDocument.Save( XMLFileName );
         }
         catch ( Exception exception )
         {
            MessageBox.Show( exception.Message );
         }
      }

      /// <summary>
      /// Handler for the "Delete" button. Find the employee and delete
      /// them from the XML document and update the form.
      /// </summary>
      /// <param name="sender">Event sender, this form.</param>
      /// <param name="e">Event arguments - unused</param>
      private void btnDelete_Click(object sender, System.EventArgs e)
      {
         // Nobody selected
         if ( m_curEmployeeID == 0 )
            return;

         try
         {
            Employee emp = (Employee) m_comboEmp.SelectedItem;
            int nIndex = m_comboEmp.SelectedIndex;

            deleteEmployee( m_xmlDocument, emp.EmployeeID );
            m_xmlDocument.Save( XMLFileName );

            // Update the form
            m_comboEmp.Items.Remove( emp );
            if ( m_comboEmp.Items.Count == 0 )
            {
               buttonNew_Click( this, null );
               m_comboEmp.Text = "";
            }
            else
            {
               nIndex--;
               if ( nIndex < 0 )
                  nIndex = 0;
               m_comboEmp.SelectedIndex = nIndex;
            }
         }
         catch ( Exception exception )
         {
            MessageBox.Show( exception.Message );
         }
      } 


      /// <summary>
      /// Handler for the a new employee selected on the in the combo 
      /// box.
      /// </summary>
      /// <param name="sender">Event sender, this form.</param>
      /// <param name="e">Event arguments - unused</param>
      private void SelectedEmployeeIndexChanged(object sender, 
         System.EventArgs e)
      {
         // Sanity check
         if ( m_comboEmp.SelectedIndex < 0 )
            return;

         // Load the employee information from the XML document and
         // display on the form.
         Employee employee = (Employee) m_comboEmp.SelectedItem;
         loadEmployee( employee.EmployeeID.ToString() );
         m_FirstName.Select();
      }


      /// <summary>
      /// Helper function which validates that a required field is not
      /// blank. If it is, an error is displayed.
      /// </summary>
      /// <param name="strValue">The string to check</param>
      /// <param name="strFieldName">Name of field</param>
      /// <param name="control">Control on form</param>
      private bool validateNotEmpty( string strValue, 
         string strFieldName, Control control )
      {
         if ( strValue.Length < 1 )
         {
            MessageBox.Show( strFieldName + " cannot be blank." );
            control.Select();
            return false;
         }
         return true;
      }

      /// <summary>
      /// Helper function which validates that a numeric field
      /// contains valid characters.
      /// </summary>
      /// <param name="strValue">The string to check</param>
      /// <param name="strFieldName">Name of field</param>
      /// <param name="control">Control on form</param>
      private bool validateDouble( string strValue, 
         string strFieldName, Control control )
      {
         bool bOK = false;
         try
         {
            double dValue = Convert.ToDouble( strValue );
            bOK = true;
         }
         catch
         {
            MessageBox.Show( strFieldName + " must be numeric." );
            control.Select();
         }
         return bOK;
      }


      /// <summary>
      /// Reads through the XML document and fills the employee      
      /// combobox with a list of all the employees in the XML
      /// document.
      /// </summary>
      /// <param name="strValue">The string to check</param>
      /// <param name="strFieldName">Name of field</param>
      /// <param name="control">Control on form</param>
      private void fillComboBox()
      {
         int nMaxID = 0;
         
         XmlElement root = m_xmlDocument.DocumentElement;

         // Get all employee elements in the document
         XmlNodeList nodeList = 
            root.GetElementsByTagName( TagEmployee );

         for ( int i = 0; i < nodeList.Count; i++ )
         {
            XmlNode nodeEmployee = nodeList.Item( i );
            if ( nodeEmployee is System.Xml.XmlElement )
            {
               // We have an employee element, parse it to build and
               // Employee object
               XmlElement element = (XmlElement) nodeEmployee;      
               Employee emp = buildEmployeeFromXml( element );
               
               // Keep track of the max ID used, so we can set the
               // next avaialable ID
               int nID = Convert.ToInt32( emp.EmployeeID );
               if ( nID > nMaxID )
                  nMaxID = nID;

               // Add the employee object to the combo box
               m_comboEmp.Items.Add( emp );
            }
         }

         // Select the first employee if we added any.
         if ( m_comboEmp.Items.Count > 0 )
            m_comboEmp.SelectedIndex = 0;
            
         // Set the next available ID
         m_NextID = nMaxID + 1;         
      }
      
      
      /// <summary>
      /// Creates an empty XML document.
      /// </summary>
      private void createEmptyXMLDocument()
      {
         // Create a new DOM based XML document
         m_xmlDocument = new XmlDocument();

         // Add the XML declaration
         XmlDeclaration dec = 
            m_xmlDocument.CreateXmlDeclaration("1.0", "", "yes");
         m_xmlDocument.PrependChild ( dec );

         // Add the root element
         XmlElement nodeElem = 
            m_xmlDocument.CreateElement( TagEmployees );
         m_xmlDocument.AppendChild( nodeElem );
      }

      /// <summary>
      /// Adds an employee to the DOM document        
      /// </summary>
      /// <param name="doc">The XML document the employee is added 
      /// to</param>
      /// <param name="nEmployeeID">Employee ID</param>
      /// <param name="strFirstName">Employee first name</param>
      /// <param name="strMiddleName">Employee middle initial</param>
      /// <param name="strLastName">Employee last name</param>
      /// <param name="strSalaried">Is a salaried employee?</param>
      /// <param name="strWage">Employee Wage</param>
      /// <param name="strActive">Active Employee?</param>
      /// <param name="strSSN">SSN</param>
      /// <param name="strStartDate">Start Date</param>
      private void addEmployee( XmlDocument doc, int nEmployeeID,
			string strFirstName, string strMiddleName, string strLastName,
         string strSalaried, string strWage, string strActive, 
         string strSSN, string strStartDate )
		{
         // Create a new employee element.  Append it as a child of the
         // root element.
         XmlElement nodeParent = doc.DocumentElement;
         XmlElement elemEmployee = doc.CreateElement( TagEmployee );
         elemEmployee.SetAttribute( TagEmployeeID, 
            nEmployeeID.ToString() );
         nodeParent.AppendChild( elemEmployee );
         
         // Add the child elements that make up the employee element
         addTextElement( doc, elemEmployee, TagFirstName, 
            strFirstName );

         addTextElement( doc, elemEmployee, TagMiddleName, 
            strMiddleName );
         
         addTextElement( doc, elemEmployee, TagLastName, strLastName );
         addTextElement( doc, elemEmployee, TagSalaried, strSalaried );  
         addTextElement( doc, elemEmployee, TagWage, strWage );
         addTextElement( doc, elemEmployee, TagActive, strActive ); 
         addTextElement( doc, elemEmployee, TagSSN, strSSN );
         addTextElement( doc, elemEmployee, TagStartDate, 
            strStartDate );
      }

      /// <summary>
      /// Adds a text element to the DOM document        
      /// </summary>
      /// <param name="doc">The XML document the employee is added
      /// to</param>
      /// <param name="nodeParent">The parent of new element</param>
      /// <param name="strTag">Element tag</param>
      /// <param name="strValue">Element text value</param>
      /// <returns>The new element</returns>
      private XmlElement addTextElement( XmlDocument doc, 
         XmlElement nodeParent, string strTag, string strValue )
		{
         // Create a new element with tag passed in
			XmlElement nodeElem = doc.CreateElement( strTag );

         // Create a text node using value passed in
			XmlText nodeText = doc.CreateTextNode( strValue );
         
         // Add the element as a child of parent passed in
			nodeParent.AppendChild( nodeElem );

         // Add the text node as a child of the new element
			nodeElem.AppendChild( nodeText );

         return nodeElem;
		}

      /// <summary>
      /// Updates an employee to the DOM document        
      /// </summary>
      /// <param name="doc">The XML document</param>
      /// <param name="nEmployeeID">Employee ID</param>
      /// <param name="strFirstName">Employee first name</param>
      /// <param name="strMiddleName">Employee middle initial</param>
      /// <param name="strLastName">Employee last name</param>
      /// <param name="strSalaried">Is a salaried employee?</param>
      /// <param name="strWage">Employee Wage</param>
      /// <param name="strActive">Active Employee?</param>
      /// <param name="strSSN">SSN</param>
      /// <param name="strStartDate">Start Date</param>
      private void updateEmployee( XmlDocument doc, int nEmployeeID,
			string strFirstName, string strMiddleName, string strLastName,
         string strSalaried, string strWage, string strActive, 
         string strSSN, string strStartDate )
		{
         // Find the employee
         XmlElement empElement = findEmployee( m_xmlDocument, 
            nEmployeeID.ToString() );
         if ( empElement == null )
            return;

         // Get a list of all the child nodes of the employee
         XmlNodeList nodeList = empElement.ChildNodes;
               
         // For each element, get the element tag. Based on the tag,
         // set the text data that will be added.
         for ( int i = 0; i < nodeList.Count; i++ )
         {
            XmlNode node = nodeList.Item( i );
            
            string strType = node.GetType().ToString();
            if ( node is System.Xml.XmlElement )  // sanity check
            {
               XmlElement element = (XmlElement) node;
               string strTag = element.Name;
               string strData = "";
               
               if ( strTag == TagFirstName )
                  strData = strFirstName;
               else if ( strTag == TagMiddleName )
                     strData = strMiddleName;
               else if ( strTag == TagLastName )
                     strData = strLastName;
               else if ( strTag == TagWage )
                     strData = strWage;
               else if ( strTag == TagSSN )
                     strData = strSSN;
               else if ( strTag == TagSalaried )
                     strData = strSalaried;
               else if ( strTag == TagActive )
                     strData = strActive;
               else if ( strTag == TagStartDate )
                     strData = strStartDate;
               else
                  continue; 

               // Create a new text node with the appropriate data and
               // replace the current text node, effectively updating.
			      XmlText nodeText = doc.CreateTextNode( strData );
               element.ReplaceChild( nodeText, element.FirstChild );
            }
         }
      }


      /// <summary>
      /// Deletes an employee from the DOM document        
      /// </summary>
      /// <param name="doc">The XML document</param>
      /// <param name="strEmployeeID">Employee ID</param>
      private void deleteEmployee( XmlDocument doc, 
         string strEmployeeID )
      {
         // Find the employee in the XML file
         XmlElement element = findEmployee( m_xmlDocument, 
            strEmployeeID ); 

         // Not found, do nothing
         if ( element == null )
            return;

         // Remove from the XML document               
         XmlElement root = m_xmlDocument.DocumentElement;
         root.RemoveChild( element );
      }
      
      
      /// <summary>
      /// Adds an employee object to the employee combo box
      /// </summary>
      /// <param name="nEmployeeID">Employee ID</param>
      /// <param name="strFirstName">Employee first name</param>
      /// <param name="strMiddleName">Employee middle initial</param>
      /// <param name="strLastName">Employee last name</param>
      private void addEmployeeToCombo( int nEmployeeID, 
         string strFirstName, string strMiddleName, string strLastName)
      {
         // Create a new employee object
         Employee emp = new Employee();
         emp.FirstName = strFirstName;
         emp.MiddleName = strMiddleName;
         emp.LastName = strLastName;
         emp.EmployeeID = nEmployeeID.ToString();
         
         // Add to the combo box and then select it
         m_comboEmp.Items.Add( emp );
         m_comboEmp.SelectedItem = emp;
      }


      /// <summary>
      /// Finds an employee in the XML document and then updates the
      /// form with the data for the employee
      /// </summary>
      /// <param name="strEmployeeID">Employee ID</param>
      private void loadEmployee( string strEmployeeID )
      {
         // Find the employee in the XML document, if not found do
         // nothing.
         XmlElement empElement = findEmployee( m_xmlDocument, 
            strEmployeeID );
         if ( empElement == null )
            return;

         // Build and employee object from the XML and update the form
         // data.
         Employee emp = buildEmployeeFromXml( empElement );
         m_FirstName.Text = emp.FirstName;
         m_LastName.Text = emp.LastName;
         m_MiddleName.Text = emp.MiddleName;
         m_Wage.Text = emp.Wage;
         m_SSN.Text = emp.SSN;
         radioSalaried.Checked = (emp.Salaried=="true") ? true : false;
         checkboxActive.Checked = (emp.Active=="true") ? true : false;
         dateTimePicker1.Value = Convert.ToDateTime( emp.StartDate );

         // Save the employees current ID, so we know we are editing
         // rather than in new mode.
         m_curEmployeeID = Convert.ToInt32( strEmployeeID );
      }


      /// <summary>
      /// Finds an employee in the XML document.
      /// </summary>
      /// <param name="doc">The XML document</param>
      /// <param name="strEmployeeID">Employee ID</param>
      private XmlElement findEmployee( XmlDocument doc, 
         string strEmployeeID )
      {
         XmlElement nodeFound = null;
         XmlElement root = doc.DocumentElement;

         // Get all employee elements in a document
         XmlNodeList nodeList = 
            root.GetElementsByTagName( TagEmployee );

         foreach ( XmlNode nodeEmployee in nodeList )
         {
            if ( nodeEmployee is System.Xml.XmlElement)
            {
               // Get the EmployeeID attribute.  If it matches the one
               // we are looking for save the node for later removal.
               XmlElement elemEmployee = (XmlElement) nodeEmployee;
               String strIDFound = 
                  elemEmployee.GetAttribute( "EmployeeID" );
               
               if ( strIDFound != null && strIDFound == strEmployeeID )
               {
                  nodeFound = elemEmployee;
                  break;
               }
            }
         }
      
         return nodeFound;
      }

      /// <summary>
      /// Builds an employee object from an Employee node in the
      /// XML document.   
      /// </summary>
      /// <param name="empElement">The employee element</param>
      private Employee buildEmployeeFromXml( XmlElement empElement )
      {
         // Create th new employee object and add the Employee ID
         Employee emp = new Employee();
         emp.EmployeeID = empElement.GetAttribute( "EmployeeID" );
         
         XmlNodeList nodeList = empElement.ChildNodes;
               
         // For each element, get the element tag. Based on the tag,
         // set the appropriate Employee object data.
         for ( int i = 0; i < nodeList.Count; i++ )
         {
            XmlNode node = nodeList.Item( i );
            
            string strType = node.GetType().ToString();
            if ( node is System.Xml.XmlElement )  // sanity check
            {
               XmlElement element = (XmlElement) node;
               string strTag = element.Name;
               string strData = element.FirstChild.Value;
               
               if ( strTag == TagFirstName )
                  emp.FirstName = strData;
               else if ( strTag == TagMiddleName )
                  emp.MiddleName = strData;
               else if ( strTag == TagLastName )
                  emp.LastName = strData;
               else if ( strTag == TagWage )
                  emp.Wage = strData;
               else if ( strTag == TagSSN )
                  emp.SSN = strData;
               else if ( strTag == TagSalaried )
                  emp.Salaried = strData;
               else if ( strTag == TagActive )
                  emp.Active = strData;
               else if ( strTag == TagStartDate )
                  emp.StartDate = strData;
            }
         }
      
         return emp;
      }
   }

   /// <summary>
   /// Represents a single employee
   /// </summary>
   class Employee
   {
      private string m_EmployeeID;
      private string m_strFirstName;
      private string m_strLastName;
      private string m_strMiddleName;
      private string m_strWage;
      private string m_strSSN;
      private string m_strSalary;
      private string m_strActive;
      private string m_strStartDate;
   
      public string EmployeeID
      {
         get { return m_EmployeeID; }
         set { m_EmployeeID = value; }
      }
   
      // FirstName property
      public string FirstName
      {
         get { return m_strFirstName; }
         set { m_strFirstName = value; }
      }

      // MiddleName property
      public string MiddleName
      {
         get { return m_strMiddleName; }
         set { m_strMiddleName = value; }
      }

      // LastName property
      public string LastName
      {
         get { return m_strLastName; }
         set { m_strLastName = value; }
      }

      // LastName property
      public string Wage
      {
         get { return m_strWage; }
         set { m_strWage = value; }
      }

      // LastName property
      public string SSN
      {
         get { return m_strSSN; }
         set { m_strSSN = value; }
      }

      // LastName property
      public string Salaried
      {
         get { return m_strSalary; }
         set { m_strSalary = value; }
      }

      // LastName property
      public string Active
      {
         get { return m_strActive; }
         set { m_strActive = value; }
      }

      // LastName property
      public string StartDate
      {
         get { return m_strStartDate; }
         set { m_strStartDate = value; }
      }

      override public string ToString()
      {
         return LastName + ", " + FirstName + " " + MiddleName;
      }
   }
}