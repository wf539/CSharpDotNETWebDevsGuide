using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace soapExamplesClient
{
  ///<summary>
  /// Sample Web client for soapExamples Web Service.
  ///</summary>
  ///<remarks>
  ///  Author: Adrian Turtschi; aturtschi@hotmail.com; Sept 2001
  ///</remarks>
  public class Form1 : System.Windows.Forms.Form {
    private System.Windows.Forms.Button callEcho;
    private System.Windows.Forms.TextBox enterText;
    private System.Windows.Forms.Label soapReturnLabel;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox soapReturnEcho;
    private System.Windows.Forms.Button callGetCounterInfo;
    private System.Windows.Forms.TextBox categoryName;
    private System.Windows.Forms.TextBox counterName;
    private System.Windows.Forms.TextBox instanceName;
    private System.Windows.Forms.Label soapReturnLabel2;
    private System.Windows.Forms.TextBox soapReturnGetCounterInfo;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if (components != null) {
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
      this.soapReturnLabel2 = new System.Windows.Forms.Label();
      this.callEcho = new System.Windows.Forms.Button();
      this.instanceName = new System.Windows.Forms.TextBox();
      this.soapReturnEcho = new System.Windows.Forms.TextBox();
      this.counterName = new System.Windows.Forms.TextBox();
      this.callGetCounterInfo = new System.Windows.Forms.Button();
      this.enterText = new System.Windows.Forms.TextBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.soapReturnLabel = new System.Windows.Forms.Label();
      this.categoryName = new System.Windows.Forms.TextBox();
      this.soapReturnGetCounterInfo = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // soapReturnLabel2
      // 
      this.soapReturnLabel2.Location = new System.Drawing.Point(24, 216);
      this.soapReturnLabel2.Name = "soapReturnLabel2";
      this.soapReturnLabel2.Size = new System.Drawing.Size(72, 16);
      this.soapReturnLabel2.TabIndex = 9;
      this.soapReturnLabel2.Text = "Return value:";
      // 
      // callEcho
      // 
      this.callEcho.Location = new System.Drawing.Point(8, 8);
      this.callEcho.Name = "callEcho";
      this.callEcho.Size = new System.Drawing.Size(96, 24);
      this.callEcho.TabIndex = 0;
      this.callEcho.Text = "Call echo";
      this.callEcho.Click += new System.EventHandler(this.callEcho_Click);
      // 
      // instanceName
      // 
      this.instanceName.Location = new System.Drawing.Point(128, 176);
      this.instanceName.Name = "instanceName";
      this.instanceName.Size = new System.Drawing.Size(136, 20);
      this.instanceName.TabIndex = 8;
      this.instanceName.Text = "Enter Instance Name!";
      // 
      // soapReturnEcho
      // 
      this.soapReturnEcho.Enabled = false;
      this.soapReturnEcho.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.soapReturnEcho.ForeColor = System.Drawing.SystemColors.WindowText;
      this.soapReturnEcho.Location = new System.Drawing.Point(128, 56);
      this.soapReturnEcho.Name = "soapReturnEcho";
      this.soapReturnEcho.Size = new System.Drawing.Size(136, 20);
      this.soapReturnEcho.TabIndex = 2;
      this.soapReturnEcho.Text = "";
      // 
      // counterName
      // 
      this.counterName.Location = new System.Drawing.Point(128, 144);
      this.counterName.Name = "counterName";
      this.counterName.Size = new System.Drawing.Size(136, 20);
      this.counterName.TabIndex = 7;
      this.counterName.Text = "Enter Counter Name!";
      // 
      // callGetCounterInfo
      // 
      this.callGetCounterInfo.Location = new System.Drawing.Point(8, 112);
      this.callGetCounterInfo.Name = "callGetCounterInfo";
      this.callGetCounterInfo.Size = new System.Drawing.Size(96, 32);
      this.callGetCounterInfo.TabIndex = 5;
      this.callGetCounterInfo.Text = "Call getCounterInfo";
      this.callGetCounterInfo.Click += new System.EventHandler(this.callGetCounterInfo_Click);
      // 
      // enterText
      // 
      this.enterText.Location = new System.Drawing.Point(128, 8);
      this.enterText.Name = "enterText";
      this.enterText.Size = new System.Drawing.Size(136, 20);
      this.enterText.TabIndex = 1;
      this.enterText.Text = "Enter text!";
      // 
      // groupBox1
      // 
      this.groupBox1.Location = new System.Drawing.Point(0, 88);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(264, 8);
      this.groupBox1.TabIndex = 4;
      this.groupBox1.TabStop = false;
      // 
      // soapReturnLabel
      // 
      this.soapReturnLabel.Location = new System.Drawing.Point(24, 56);
      this.soapReturnLabel.Name = "soapReturnLabel";
      this.soapReturnLabel.Size = new System.Drawing.Size(72, 16);
      this.soapReturnLabel.TabIndex = 3;
      this.soapReturnLabel.Text = "Return value:";
      // 
      // categoryName
      // 
      this.categoryName.Location = new System.Drawing.Point(128, 112);
      this.categoryName.Name = "categoryName";
      this.categoryName.Size = new System.Drawing.Size(136, 20);
      this.categoryName.TabIndex = 6;
      this.categoryName.Text = "Enter Category Name!";
      // 
      // soapReturnGetCounterInfo
      // 
      this.soapReturnGetCounterInfo.Enabled = false;
      this.soapReturnGetCounterInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.soapReturnGetCounterInfo.Location = new System.Drawing.Point(128, 216);
      this.soapReturnGetCounterInfo.Name = "soapReturnGetCounterInfo";
      this.soapReturnGetCounterInfo.Size = new System.Drawing.Size(136, 20);
      this.soapReturnGetCounterInfo.TabIndex = 10;
      this.soapReturnGetCounterInfo.Text = "";
      // 
      // Form1
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(272, 245);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.soapReturnGetCounterInfo,
                                                                  this.soapReturnLabel2,
                                                                  this.instanceName,
                                                                  this.counterName,
                                                                  this.categoryName,
                                                                  this.callGetCounterInfo,
                                                                  this.groupBox1,
                                                                  this.soapReturnLabel,
                                                                  this.soapReturnEcho,
                                                                  this.enterText,
                                                                  this.callEcho});
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);

    }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.Run(new Form1());
		}

    private void callEcho_Click(object sender, System.EventArgs e) {
      try {
      localhost.simpleService myWebSvc = 
        new localhost.simpleService();

        this.soapReturnEcho.Text = 
          myWebSvc.echo(this.enterText.Text);
      } catch (Exception ex) {
        // error handling goes here...
      }
    }

    private void callGetCounterInfo_Click(
      object sender, System.EventArgs e) {
      try {
        localhost.simpleService myWebSvc =
          new localhost.simpleService();
 
        this.soapReturnGetCounterInfo.Text =
          myWebSvc.getCounterInfo(
          this.categoryName.Text,
          this.counterName.Text,
          this.instanceName.Text).RawValue.ToString();
      } catch (Exception ex) {
        // error handling goes here...
      }
    }
	}
}
