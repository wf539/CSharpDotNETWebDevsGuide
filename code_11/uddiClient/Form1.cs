using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace uddiClient {
	/// <summary>
	///   Sample Web client illustrating using UDDI.
	/// </summary>
	/// <remarks>
	///   Author: Adrian Turtschi; aturtschi@hotmail.com; Sept 2001
	/// </remarks>
	public class Form1 : System.Windows.Forms.Form {
    private System.Windows.Forms.TextBox airportTemperature;
    private System.Windows.Forms.TextBox enterAirportCode;
    private System.Windows.Forms.Button getTemperature;
    private System.Windows.Forms.Label label1;
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
      this.airportTemperature = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.enterAirportCode = new System.Windows.Forms.TextBox();
      this.getTemperature = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // airportTemperature
      // 
      this.airportTemperature.Location = new System.Drawing.Point(136, 48);
      this.airportTemperature.Multiline = true;
      this.airportTemperature.Name = "airportTemperature";
      this.airportTemperature.Size = new System.Drawing.Size(232, 56);
      this.airportTemperature.TabIndex = 2;
      this.airportTemperature.Text = "";
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(8, 48);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(120, 24);
      this.label1.TabIndex = 3;
      this.label1.Text = "Temperature at airport:";
      // 
      // enterAirportCode
      // 
      this.enterAirportCode.Location = new System.Drawing.Point(136, 8);
      this.enterAirportCode.Name = "enterAirportCode";
      this.enterAirportCode.Size = new System.Drawing.Size(144, 20);
      this.enterAirportCode.TabIndex = 1;
      this.enterAirportCode.Text = "Enter Airport Code (ICAO)!";
      // 
      // getTemperature
      // 
      this.getTemperature.Location = new System.Drawing.Point(8, 8);
      this.getTemperature.Name = "getTemperature";
      this.getTemperature.Size = new System.Drawing.Size(104, 24);
      this.getTemperature.TabIndex = 0;
      this.getTemperature.Text = "Get Temperature!";
      this.getTemperature.Click += new System.EventHandler(this.getTemperature_Click);
      // 
      // Form1
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(376, 117);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.label1,
                                                                  this.airportTemperature,
                                                                  this.enterAirportCode,
                                                                  this.getTemperature});
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);

    }
		#endregion

		[STAThread]
		static void Main() {
			Application.Run(new Form1());
		}

    private void getTemperature_Click(
      object sender, System.EventArgs e) {
      try {
        com.capescience.www.AirportWeather airportWeather =
          new com.capescience.www.AirportWeather();
        airportTemperature.Text = 
          airportWeather.getTemperature(enterAirportCode.Text);
      } catch(Exception ex) {
        // error handling goes here...
      }
    }
	}
}
