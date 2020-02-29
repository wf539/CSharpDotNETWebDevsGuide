using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace XSL
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
      private System.Windows.Forms.TextBox textBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.Button btnShowHTML;
      private System.Windows.Forms.Button btnRun;
      private string m_strOutput;

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
         this.btnRun = new System.Windows.Forms.Button();
         this.textBox1 = new System.Windows.Forms.TextBox();
         this.btnShowHTML = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // btnRun
         // 
         this.btnRun.Location = new System.Drawing.Point(152, 355);
         this.btnRun.Name = "btnRun";
         this.btnRun.Size = new System.Drawing.Size(75, 40);
         this.btnRun.TabIndex = 1;
         this.btnRun.Text = "Run XSL Sample";
         this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
         // 
         // textBox1
         // 
         this.textBox1.Location = new System.Drawing.Point(16, 24);
         this.textBox1.Multiline = true;
         this.textBox1.Name = "textBox1";
         this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.textBox1.Size = new System.Drawing.Size(464, 312);
         this.textBox1.TabIndex = 0;
         this.textBox1.Text = "";
         // 
         // btnShowHTML
         // 
         this.btnShowHTML.Enabled = false;
         this.btnShowHTML.Location = new System.Drawing.Point(278, 355);
         this.btnShowHTML.Name = "btnShowHTML";
         this.btnShowHTML.Size = new System.Drawing.Size(75, 40);
         this.btnShowHTML.TabIndex = 1;
         this.btnShowHTML.Text = "Show HTML Report";
         this.btnShowHTML.Click += new System.EventHandler(this.btnShowHTML_Click_1);
         // 
         // Form1
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(504, 405);
         this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                      this.btnShowHTML,
                                                                      this.btnRun,
                                                                      this.textBox1});
         this.Name = "Form1";
         this.Text = "XSL Sample";
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
      /// Called when Run XSL Sample button is pressed
      /// </summary>
      private void btnRun_Click(object sender, System.EventArgs e)
      {
         Cursor currentCursor = Cursor.Current;

         try
         {
            Cursor.Current = Cursors.WaitCursor;

            // Do XSL transformations
            doXMLToXMLTransform();
            doXMLToHTMLTranformation();

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
      /// Transforms XML to XML using XSL
      /// </summary>
      private void doXMLToXMLTransform()
      {
         // Show the original document on screen
         m_strOutput = "*** personnel.xml - Original XML ***\r\n\r\n";
         showXMLDocument( "personnel.xml" );

         // Load the new document, apply an XSL tranformation to
         // it and save the new document to disk
         XPathDocument docXPath = 
            new XPathDocument( "personnel.xml" );

         XslTransform xslTransform = new XslTransform();
         XmlTextWriter writer = 
            new XmlTextWriter( "salaried.xml", null );
         
         xslTransform.Load( "salariedpersonnel.xsl" );
         xslTransform.Transform( docXPath, null, writer );
         writer.Close();

         m_strOutput += 
            "*** salaried.xml - Transformed XML ***\r\n\r\n";
         
         // Show the transformed document
         showXMLDocument( "salaried.xml" );
      }


      /// <summary>
      /// Transforms XML to HTML using XSL
      /// </summary>
      private void doXMLToHTMLTranformation()
      {
         // Load the XML document, apply an XSL transformation to it,
         // resulting in HTML which is written to disk.
         XPathDocument docXPath = new XPathDocument( "salaried.xml" );
         XslTransform xslTransform = new XslTransform();
         XmlTextWriter writer = 
            new XmlTextWriter( "salariedreport.html", null );
         
         xslTransform.Load( "salariedreport.xsl" );
         xslTransform.Transform( docXPath, null, writer );
         writer.Close();
         btnShowHTML.Enabled = true;
      }

      
      /// <summary>
      /// Loads an XML document from a file on disk and converts to a 
      /// string formatted with indentation for easier reading.  Then
      /// displays the xml file on screen.
      /// </summary>
      /// <param name="strXMLFileName">
      ///   The name of the XML file to load from disk
      /// </param>
      private void showXMLDocument( string strXMLFileName )
      {
         XmlDocument docDOM = new XmlDocument();
         docDOM.Load( strXMLFileName );

         StringWriter writerString = new StringWriter();
         XmlTextWriter writer2 = new XmlTextWriter( writerString );
         writer2.Formatting = Formatting.Indented;
         docDOM.WriteTo( writer2 );
         writer2.Flush();
         m_strOutput += writerString.ToString() + "\r\n\r\n";
      }

      /// <summary>
      /// Called when Show HTML Report button is pressed. Lauches
      /// Internet Explorer with the report that was created.
      /// </summary>
      private void btnShowHTML_Click_1(object sender, System.EventArgs e)
      {
         System.Diagnostics.Process.Start("salariedreport.html");
      }

	}
}
