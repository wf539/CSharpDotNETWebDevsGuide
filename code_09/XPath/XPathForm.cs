using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace XPath
{
   /// <summary>
   /// Summary description for Form1.
   /// </summary>
   public class Form1 : System.Windows.Forms.Form
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;
      private System.Windows.Forms.TextBox textBox1;
      private System.Windows.Forms.Button button1;

      private string m_strOutput;

      public Form1()
      {
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
         this.textBox1 = new System.Windows.Forms.TextBox();
         this.button1 = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // textBox1
         // 
         this.textBox1.Location = new System.Drawing.Point(16, 16);
         this.textBox1.Multiline = true;
         this.textBox1.Name = "textBox1";
         this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.textBox1.Size = new System.Drawing.Size(384, 272);
         this.textBox1.TabIndex = 2;
         this.textBox1.Text = "";
         // 
         // button1
         // 
         this.button1.Location = new System.Drawing.Point(168, 312);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(80, 40);
         this.button1.TabIndex = 1;
         this.button1.Text = "Run XPath Queries";
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // Form1
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(416, 357);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.textBox1,
                                                                      this.button1});
         this.Name = "Form1";
         this.Text = "XPath Sample";
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
      /// Called when Run XPath Queries is pressed
      /// </summary>
      private void button1_Click(object sender, System.EventArgs e)
      {
         Cursor currentCursor = Cursor.Current;

         try
         {
            Cursor.Current = Cursors.WaitCursor;

            // Do XPath queries on both XPath Documents and 
            // DOM based XML documents
            doXPathDocumentQueries();
            doXmlDocumentQueries();

            // Show results on screen
            textBox1.Text = m_strOutput;
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
      /// Do XPath queries against a read-only XPathDocument
      /// </summary>
      private void doXPathDocumentQueries()
      {
         m_strOutput = 
            "*** Beginning XPathDocument Queries ***\r\n\r\n";

         // Load the XML document into a read-only XPathDocument
         // and instantiate a navigator for queries.
         XPathDocument doc = new XPathDocument( "personnel.xml" );
         XPathNavigator navigator = doc.CreateNavigator();

         m_strOutput += "*** Show All Wages ***\r\n\r\n";
        
         // Find all Employee/Wage elements in the document and display
         // the wage information on screen
         XPathNodeIterator iterator =  
            navigator.Select( "descendant::Employee/Wage" );

         while ( iterator.MoveNext() )
         {
            m_strOutput += iterator.Current.Name + ": ";
            m_strOutput += iterator.Current.Value + "\r\n";
         }

         m_strOutput += 
            "\r\n\r\n*** Show All Employees in Seattle ***\r\n\r\n";

         // Find all employees in the Seattle office and display
         // their names on screen
         iterator = 
            navigator.Select( "//Employee[Location/Zip='98103']" );

         while ( iterator.MoveNext() )
         {
            XPathNavigator nav2 = iterator.Current;
            nav2.MoveToFirstChild();
            m_strOutput += nav2.Value;                // First name
            nav2.MoveToNext();
            m_strOutput += ". " + nav2.Value;         // Middle init
            nav2.MoveToNext();
            m_strOutput += " " + nav2.Value + "\r\n"; // Last name
         }

         m_strOutput += 
            "\r\n\r\n*** Salaried Employee Average Wage ***\r\n\r\n";

         // Calculate the average salary for all salaried employees
         // in the company and display on screen
         Int32 nAverage = 
            (Int32)(Double)navigator.Evaluate( 
               "sum(//Employee[Salaried='true']/Wage) div count(//Employee[Salaried='true'])" );

         m_strOutput += "Average Salary: $" + nAverage.ToString();
      }

      /// <summary>
      /// Do an XPath queries against a DOM based XML document and then
      /// modify the document.
      /// </summary>
      private void doXmlDocumentQueries()
      {
         m_strOutput += 
            "\r\n\r\n*** Beginning XML Document Query ***\r\n\r\n";

         // Load the XML document into a DOM based XML document
         XmlDocument doc = new XmlDocument();
         doc.Load( "personnel.xml" );

         // Get a list of the Active element nodes for each employee 
         // in Portland
         XmlNodeList nodeList = 
            doc.SelectNodes( "//Employee[Location/Zip='97206']/Active"  );

         foreach ( XmlNode node in nodeList )
         {
            // Mark each Portland employee as inactive
            node.InnerText = "false";
         }

         // Display the modified document on screen
         StringWriter writerString = new StringWriter();
         XmlTextWriter writer = new XmlTextWriter( writerString );
         writer.Formatting = Formatting.Indented;
         doc.WriteTo( writer );
         writer.Flush();
         m_strOutput += writerString.ToString();
      }
   }
}
