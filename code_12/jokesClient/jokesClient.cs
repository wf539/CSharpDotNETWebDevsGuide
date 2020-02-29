using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Web.Services.Protocols;
using System.Xml;

namespace jokesClient
{
	/// <summary>
	/// Summary description for f_jokeClient.
	/// </summary>
	public class f_jokeClient : System.Windows.Forms.Form
	{
    // placeholders for web service objects
    private userAdmin.userAdmin userAdminObj ;        
    private jokes.jokes jokesObj;        
    // remembe if objects have been created
    private bool userAdminObjCreated = false;
    private bool jokesObjCreated = false;

    // remember user name and password, and moderator status
    private string userName = "";
    private string password = "";
    private bool isModerator = false;
    // hold jokes
    private jokes.xmlJokesReturn[] myJokes;
    private int jokesReturned = 0;
    private int currentJoke = 0;
    // are we looking at moderated jokes or not?
    private bool moderatedJokes = false;

    private System.Windows.Forms.GroupBox gb_userMenu;
    private System.Windows.Forms.Button b_userAddJoke;
    private System.Windows.Forms.GroupBox gb_moderatorMenu;
    private System.Windows.Forms.Button b_moderatorMakeModerator;
    private System.Windows.Forms.Button b_moderatorGetUnmoderated;
    private System.Windows.Forms.NumericUpDown nud_moderatorHowMany;
    private System.Windows.Forms.GroupBox gb_jokes;
    private System.Windows.Forms.TextBox tb_jokesJoke;
    private System.Windows.Forms.Button b_jokesPrev;
    private System.Windows.Forms.Button b_jokesNext;
    private System.Windows.Forms.Label l_jokesNumber;
    private System.Windows.Forms.Label l_jokesRating;
    private System.Windows.Forms.Button b_userGetJokes;
    private System.Windows.Forms.NumericUpDown nud_userHowMany;
    private System.Windows.Forms.Button b_jokesAddRating;
    private System.Windows.Forms.NumericUpDown nud_jokesRating;
    private System.Windows.Forms.Button b_jokesAddModerated;
    private System.Windows.Forms.Button b_jokesRemove;
    private System.Windows.Forms.Button b_logonRegisterNow;
    private System.Windows.Forms.Button b_logonUserLogOn;
    private System.Windows.Forms.TextBox tb_logonPassword;
    private System.Windows.Forms.Label l_logonUserName;
    private System.Windows.Forms.TextBox tb_logonUserName;
    private System.Windows.Forms.GroupBox gb_userInfo;
    private System.Windows.Forms.Label l_logonPassword;
    private System.Windows.Forms.Button b_logonModeratorLogOn;
    private System.Windows.Forms.Label l_splash;
    private System.Windows.Forms.TextBox 
      tb_moderatorNewModeratorUserName;
    private System.Windows.Forms.Label l_statusMessage;
    private System.Windows.Forms.TextBox tb_userJoke;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public f_jokeClient() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
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
		private void InitializeComponent() {
      this.l_jokesNumber = new System.Windows.Forms.Label();
      this.l_splash = new System.Windows.Forms.Label();
      this.tb_moderatorNewModeratorUserName = new System.Windows.Forms.TextBox();
      this.tb_logonPassword = new System.Windows.Forms.TextBox();
      this.b_moderatorMakeModerator = new System.Windows.Forms.Button();
      this.b_jokesRemove = new System.Windows.Forms.Button();
      this.b_userAddJoke = new System.Windows.Forms.Button();
      this.tb_userJoke = new System.Windows.Forms.TextBox();
      this.nud_moderatorHowMany = new System.Windows.Forms.NumericUpDown();
      this.b_jokesAddModerated = new System.Windows.Forms.Button();
      this.b_logonModeratorLogOn = new System.Windows.Forms.Button();
      this.gb_userMenu = new System.Windows.Forms.GroupBox();
      this.b_userGetJokes = new System.Windows.Forms.Button();
      this.nud_userHowMany = new System.Windows.Forms.NumericUpDown();
      this.b_jokesAddRating = new System.Windows.Forms.Button();
      this.l_logonPassword = new System.Windows.Forms.Label();
      this.b_logonRegisterNow = new System.Windows.Forms.Button();
      this.b_logonUserLogOn = new System.Windows.Forms.Button();
      this.gb_jokes = new System.Windows.Forms.GroupBox();
      this.nud_jokesRating = new System.Windows.Forms.NumericUpDown();
      this.l_jokesRating = new System.Windows.Forms.Label();
      this.b_jokesNext = new System.Windows.Forms.Button();
      this.b_jokesPrev = new System.Windows.Forms.Button();
      this.tb_jokesJoke = new System.Windows.Forms.TextBox();
      this.l_statusMessage = new System.Windows.Forms.Label();
      this.gb_userInfo = new System.Windows.Forms.GroupBox();
      this.tb_logonUserName = new System.Windows.Forms.TextBox();
      this.l_logonUserName = new System.Windows.Forms.Label();
      this.b_moderatorGetUnmoderated = new System.Windows.Forms.Button();
      this.gb_moderatorMenu = new System.Windows.Forms.GroupBox();
      ((System.ComponentModel.ISupportInitialize)(this.nud_moderatorHowMany)).BeginInit();
      this.gb_userMenu.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nud_userHowMany)).BeginInit();
      this.gb_jokes.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nud_jokesRating)).BeginInit();
      this.gb_userInfo.SuspendLayout();
      this.gb_moderatorMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // l_jokesNumber
      // 
      this.l_jokesNumber.Location = new System.Drawing.Point(64, 136);
      this.l_jokesNumber.Name = "l_jokesNumber";
      this.l_jokesNumber.Size = new System.Drawing.Size(80, 16);
      this.l_jokesNumber.TabIndex = 3;
      this.l_jokesNumber.Text = "(no jokes)";
      // 
      // l_splash
      // 
      this.l_splash.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic), System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.l_splash.Location = new System.Drawing.Point(8, 16);
      this.l_splash.Name = "l_splash";
      this.l_splash.Size = new System.Drawing.Size(232, 88);
      this.l_splash.TabIndex = 9;
      this.l_splash.Text = "Jokes Web Service Client";
      // 
      // tb_moderatorNewModeratorUserName
      // 
      this.tb_moderatorNewModeratorUserName.Location = new System.Drawing.Point(136, 72);
      this.tb_moderatorNewModeratorUserName.Name = "tb_moderatorNewModeratorUserName";
      this.tb_moderatorNewModeratorUserName.Size = new System.Drawing.Size(176, 20);
      this.tb_moderatorNewModeratorUserName.TabIndex = 1;
      this.tb_moderatorNewModeratorUserName.Text = "Enter user name of new moderator";
      // 
      // tb_logonPassword
      // 
      this.tb_logonPassword.Location = new System.Drawing.Point(208, 56);
      this.tb_logonPassword.Name = "tb_logonPassword";
      this.tb_logonPassword.PasswordChar = '*';
      this.tb_logonPassword.Size = new System.Drawing.Size(176, 20);
      this.tb_logonPassword.TabIndex = 1;
      this.tb_logonPassword.Text = "";
      // 
      // b_moderatorMakeModerator
      // 
      this.b_moderatorMakeModerator.Location = new System.Drawing.Point(8, 64);
      this.b_moderatorMakeModerator.Name = "b_moderatorMakeModerator";
      this.b_moderatorMakeModerator.Size = new System.Drawing.Size(112, 24);
      this.b_moderatorMakeModerator.TabIndex = 0;
      this.b_moderatorMakeModerator.Text = "Set As Moderator";
      this.b_moderatorMakeModerator.Click += new System.EventHandler(this.b_moderatorMakeModerator_Click);
      // 
      // b_jokesRemove
      // 
      this.b_jokesRemove.Location = new System.Drawing.Point(464, 48);
      this.b_jokesRemove.Name = "b_jokesRemove";
      this.b_jokesRemove.Size = new System.Drawing.Size(112, 24);
      this.b_jokesRemove.TabIndex = 8;
      this.b_jokesRemove.Text = "Remove Joke";
      this.b_jokesRemove.Click += new System.EventHandler(this.b_jokesRemove_Click);
      // 
      // b_userAddJoke
      // 
      this.b_userAddJoke.Location = new System.Drawing.Point(8, 56);
      this.b_userAddJoke.Name = "b_userAddJoke";
      this.b_userAddJoke.Size = new System.Drawing.Size(112, 24);
      this.b_userAddJoke.TabIndex = 7;
      this.b_userAddJoke.Text = "Add New Joke";
      this.b_userAddJoke.Click += new System.EventHandler(this.b_userAddJoke_Click);
      // 
      // tb_userJoke
      // 
      this.tb_userJoke.Location = new System.Drawing.Point(136, 56);
      this.tb_userJoke.Multiline = true;
      this.tb_userJoke.Name = "tb_userJoke";
      this.tb_userJoke.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.tb_userJoke.Size = new System.Drawing.Size(296, 104);
      this.tb_userJoke.TabIndex = 6;
      this.tb_userJoke.Text = "";
      // 
      // nud_moderatorHowMany
      // 
      this.nud_moderatorHowMany.Location = new System.Drawing.Point(144, 24);
      this.nud_moderatorHowMany.Maximum = new System.Decimal(new int[] {
                                                                         10,
                                                                         0,
                                                                         0,
                                                                         0});
      this.nud_moderatorHowMany.Minimum = new System.Decimal(new int[] {
                                                                         1,
                                                                         0,
                                                                         0,
                                                                         0});
      this.nud_moderatorHowMany.Name = "nud_moderatorHowMany";
      this.nud_moderatorHowMany.Size = new System.Drawing.Size(40, 20);
      this.nud_moderatorHowMany.TabIndex = 7;
      this.nud_moderatorHowMany.Value = new System.Decimal(new int[] {
                                                                       1,
                                                                       0,
                                                                       0,
                                                                       0});
      // 
      // b_jokesAddModerated
      // 
      this.b_jokesAddModerated.Location = new System.Drawing.Point(464, 16);
      this.b_jokesAddModerated.Name = "b_jokesAddModerated";
      this.b_jokesAddModerated.Size = new System.Drawing.Size(112, 24);
      this.b_jokesAddModerated.TabIndex = 7;
      this.b_jokesAddModerated.Text = "Accept Joke";
      this.b_jokesAddModerated.Click += new System.EventHandler(this.b_jokesAddModerated_Click);
      // 
      // b_logonModeratorLogOn
      // 
      this.b_logonModeratorLogOn.Location = new System.Drawing.Point(400, 56);
      this.b_logonModeratorLogOn.Name = "b_logonModeratorLogOn";
      this.b_logonModeratorLogOn.Size = new System.Drawing.Size(112, 24);
      this.b_logonModeratorLogOn.TabIndex = 6;
      this.b_logonModeratorLogOn.Text = "Moderator Log On";
      this.b_logonModeratorLogOn.Click += new System.EventHandler(this.b_logonModeratorLogOn_Click);
      // 
      // gb_userMenu
      // 
      this.gb_userMenu.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                              this.b_userAddJoke,
                                                                              this.tb_userJoke,
                                                                              this.b_userGetJokes,
                                                                              this.nud_userHowMany});
      this.gb_userMenu.Enabled = false;
      this.gb_userMenu.Location = new System.Drawing.Point(4, 112);
      this.gb_userMenu.Name = "gb_userMenu";
      this.gb_userMenu.Size = new System.Drawing.Size(444, 168);
      this.gb_userMenu.TabIndex = 5;
      this.gb_userMenu.TabStop = false;
      this.gb_userMenu.Text = "User Menu";
      // 
      // b_userGetJokes
      // 
      this.b_userGetJokes.Location = new System.Drawing.Point(8, 16);
      this.b_userGetJokes.Name = "b_userGetJokes";
      this.b_userGetJokes.Size = new System.Drawing.Size(112, 24);
      this.b_userGetJokes.TabIndex = 8;
      this.b_userGetJokes.Text = "Get Jokes!";
      this.b_userGetJokes.Click += new System.EventHandler(this.b_userGetJokes_Click);
      // 
      // nud_userHowMany
      // 
      this.nud_userHowMany.Location = new System.Drawing.Point(136, 16);
      this.nud_userHowMany.Maximum = new System.Decimal(new int[] {
                                                                    10,
                                                                    0,
                                                                    0,
                                                                    0});
      this.nud_userHowMany.Minimum = new System.Decimal(new int[] {
                                                                    1,
                                                                    0,
                                                                    0,
                                                                    0});
      this.nud_userHowMany.Name = "nud_userHowMany";
      this.nud_userHowMany.Size = new System.Drawing.Size(40, 20);
      this.nud_userHowMany.TabIndex = 7;
      this.nud_userHowMany.Value = new System.Decimal(new int[] {
                                                                  1,
                                                                  0,
                                                                  0,
                                                                  0});
      // 
      // b_jokesAddRating
      // 
      this.b_jokesAddRating.Location = new System.Drawing.Point(312, 16);
      this.b_jokesAddRating.Name = "b_jokesAddRating";
      this.b_jokesAddRating.Size = new System.Drawing.Size(112, 24);
      this.b_jokesAddRating.TabIndex = 5;
      this.b_jokesAddRating.Text = "Rate This Joke!";
      this.b_jokesAddRating.Click += new System.EventHandler(this.b_jokesAddRating_Click);
      // 
      // l_logonPassword
      // 
      this.l_logonPassword.Location = new System.Drawing.Point(128, 56);
      this.l_logonPassword.Name = "l_logonPassword";
      this.l_logonPassword.Size = new System.Drawing.Size(64, 16);
      this.l_logonPassword.TabIndex = 3;
      this.l_logonPassword.Text = "Password";
      // 
      // b_logonRegisterNow
      // 
      this.b_logonRegisterNow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.b_logonRegisterNow.Location = new System.Drawing.Point(8, 22);
      this.b_logonRegisterNow.Name = "b_logonRegisterNow";
      this.b_logonRegisterNow.Size = new System.Drawing.Size(112, 24);
      this.b_logonRegisterNow.TabIndex = 5;
      this.b_logonRegisterNow.Text = "Register Now!";
      this.b_logonRegisterNow.Click += new System.EventHandler(this.b_logonRegisterNow_Click);
      // 
      // b_logonUserLogOn
      // 
      this.b_logonUserLogOn.Location = new System.Drawing.Point(400, 24);
      this.b_logonUserLogOn.Name = "b_logonUserLogOn";
      this.b_logonUserLogOn.Size = new System.Drawing.Size(112, 24);
      this.b_logonUserLogOn.TabIndex = 5;
      this.b_logonUserLogOn.Text = "User Log On";
      this.b_logonUserLogOn.Click += new System.EventHandler(this.b_logonUserLogOn_Click);
      // 
      // gb_jokes
      // 
      this.gb_jokes.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                           this.b_jokesRemove,
                                                                           this.b_jokesAddModerated,
                                                                           this.nud_jokesRating,
                                                                           this.b_jokesAddRating,
                                                                           this.l_jokesRating,
                                                                           this.l_jokesNumber,
                                                                           this.b_jokesNext,
                                                                           this.b_jokesPrev,
                                                                           this.tb_jokesJoke});
      this.gb_jokes.Enabled = false;
      this.gb_jokes.Location = new System.Drawing.Point(4, 288);
      this.gb_jokes.Name = "gb_jokes";
      this.gb_jokes.Size = new System.Drawing.Size(772, 200);
      this.gb_jokes.TabIndex = 8;
      this.gb_jokes.TabStop = false;
      this.gb_jokes.Text = "Jokes";
      // 
      // nud_jokesRating
      // 
      this.nud_jokesRating.Location = new System.Drawing.Point(312, 48);
      this.nud_jokesRating.Maximum = new System.Decimal(new int[] {
                                                                    5,
                                                                    0,
                                                                    0,
                                                                    0});
      this.nud_jokesRating.Minimum = new System.Decimal(new int[] {
                                                                    1,
                                                                    0,
                                                                    0,
                                                                    0});
      this.nud_jokesRating.Name = "nud_jokesRating";
      this.nud_jokesRating.Size = new System.Drawing.Size(40, 20);
      this.nud_jokesRating.TabIndex = 6;
      this.nud_jokesRating.Value = new System.Decimal(new int[] {
                                                                  3,
                                                                  0,
                                                                  0,
                                                                  0});
      // 
      // l_jokesRating
      // 
      this.l_jokesRating.Location = new System.Drawing.Point(160, 136);
      this.l_jokesRating.Name = "l_jokesRating";
      this.l_jokesRating.Size = new System.Drawing.Size(96, 16);
      this.l_jokesRating.TabIndex = 4;
      this.l_jokesRating.Text = "(no rating)";
      // 
      // b_jokesNext
      // 
      this.b_jokesNext.Location = new System.Drawing.Point(272, 128);
      this.b_jokesNext.Name = "b_jokesNext";
      this.b_jokesNext.Size = new System.Drawing.Size(32, 24);
      this.b_jokesNext.TabIndex = 2;
      this.b_jokesNext.Text = ">>";
      this.b_jokesNext.Click += new System.EventHandler(this.b_jokesNext_Click);
      // 
      // b_jokesPrev
      // 
      this.b_jokesPrev.Location = new System.Drawing.Point(8, 128);
      this.b_jokesPrev.Name = "b_jokesPrev";
      this.b_jokesPrev.Size = new System.Drawing.Size(32, 24);
      this.b_jokesPrev.TabIndex = 1;
      this.b_jokesPrev.Text = "<<";
      this.b_jokesPrev.Click += new System.EventHandler(this.b_jokesPrev_Click);
      // 
      // tb_jokesJoke
      // 
      this.tb_jokesJoke.Location = new System.Drawing.Point(8, 16);
      this.tb_jokesJoke.Multiline = true;
      this.tb_jokesJoke.Name = "tb_jokesJoke";
      this.tb_jokesJoke.ReadOnly = true;
      this.tb_jokesJoke.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.tb_jokesJoke.Size = new System.Drawing.Size(296, 104);
      this.tb_jokesJoke.TabIndex = 0;
      this.tb_jokesJoke.Text = "";
      // 
      // l_statusMessage
      // 
      this.l_statusMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.l_statusMessage.ForeColor = System.Drawing.Color.Red;
      this.l_statusMessage.Location = new System.Drawing.Point(8, 494);
      this.l_statusMessage.Name = "l_statusMessage";
      this.l_statusMessage.Size = new System.Drawing.Size(760, 16);
      this.l_statusMessage.TabIndex = 0;
      // 
      // gb_userInfo
      // 
      this.gb_userInfo.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                              this.tb_logonUserName,
                                                                              this.tb_logonPassword,
                                                                              this.l_logonUserName,
                                                                              this.l_logonPassword,
                                                                              this.b_logonModeratorLogOn,
                                                                              this.b_logonUserLogOn,
                                                                              this.b_logonRegisterNow});
      this.gb_userInfo.Location = new System.Drawing.Point(248, 8);
      this.gb_userInfo.Name = "gb_userInfo";
      this.gb_userInfo.Size = new System.Drawing.Size(524, 96);
      this.gb_userInfo.TabIndex = 4;
      this.gb_userInfo.TabStop = false;
      this.gb_userInfo.Text = "Log on information";
      // 
      // tb_logonUserName
      // 
      this.tb_logonUserName.Location = new System.Drawing.Point(208, 24);
      this.tb_logonUserName.Name = "tb_logonUserName";
      this.tb_logonUserName.Size = new System.Drawing.Size(176, 20);
      this.tb_logonUserName.TabIndex = 0;
      this.tb_logonUserName.Text = "Enter your user name here!";
      // 
      // l_logonUserName
      // 
      this.l_logonUserName.Location = new System.Drawing.Point(128, 24);
      this.l_logonUserName.Name = "l_logonUserName";
      this.l_logonUserName.Size = new System.Drawing.Size(72, 16);
      this.l_logonUserName.TabIndex = 2;
      this.l_logonUserName.Text = "User Name";
      // 
      // b_moderatorGetUnmoderated
      // 
      this.b_moderatorGetUnmoderated.Location = new System.Drawing.Point(8, 16);
      this.b_moderatorGetUnmoderated.Name = "b_moderatorGetUnmoderated";
      this.b_moderatorGetUnmoderated.Size = new System.Drawing.Size(112, 32);
      this.b_moderatorGetUnmoderated.TabIndex = 8;
      this.b_moderatorGetUnmoderated.Text = "Get Unmoderated Jokes";
      this.b_moderatorGetUnmoderated.Click += new System.EventHandler(this.b_moderatorGetUnmoderated_Click);
      // 
      // gb_moderatorMenu
      // 
      this.gb_moderatorMenu.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                   this.tb_moderatorNewModeratorUserName,
                                                                                   this.b_moderatorMakeModerator,
                                                                                   this.b_moderatorGetUnmoderated,
                                                                                   this.nud_moderatorHowMany});
      this.gb_moderatorMenu.Enabled = false;
      this.gb_moderatorMenu.Location = new System.Drawing.Point(456, 112);
      this.gb_moderatorMenu.Name = "gb_moderatorMenu";
      this.gb_moderatorMenu.Size = new System.Drawing.Size(320, 168);
      this.gb_moderatorMenu.TabIndex = 6;
      this.gb_moderatorMenu.TabStop = false;
      this.gb_moderatorMenu.Text = "Moderator Menu";
      // 
      // f_jokeClient
      // 
      this.AutoScale = false;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(776, 509);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.l_splash,
                                                                  this.gb_jokes,
                                                                  this.gb_moderatorMenu,
                                                                  this.gb_userMenu,
                                                                  this.gb_userInfo,
                                                                  this.l_statusMessage});
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(784, 536);
      this.MinimumSize = new System.Drawing.Size(784, 536);
      this.Name = "f_jokeClient";
      this.Text = "Jokes Client";
      ((System.ComponentModel.ISupportInitialize)(this.nud_moderatorHowMany)).EndInit();
      this.gb_userMenu.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.nud_userHowMany)).EndInit();
      this.gb_jokes.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.nud_jokesRating)).EndInit();
      this.gb_userInfo.ResumeLayout(false);
      this.gb_moderatorMenu.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.Run(new f_jokeClient());
		}

    private void displayJoke(
      string joke, int jokeNumber, int totalJokes, decimal rating, 
      bool moderatedJokes) {
      this.l_statusMessage.Text = "";

      if(totalJokes == 0) {
        this.gb_jokes.Enabled = false;
        this.tb_jokesJoke.Text = "";
        this.nud_jokesRating.Value = 3;
        this.l_jokesNumber.Text = "(no jokes)";
        this.l_jokesRating.Text = "(no rating)";
        return;
      }

      if(totalJokes > 0) {
        this.gb_jokes.Enabled = true;
        if (!moderatedJokes) {
          this.b_jokesAddModerated.Enabled = true;
          this.b_jokesRemove.Enabled = true;
          this.nud_jokesRating.Enabled = false;
          this.b_jokesAddRating.Enabled = false;
        } else {
          this.b_jokesAddModerated.Enabled = false;
          this.b_jokesRemove.Enabled = false;
          this.nud_jokesRating.Enabled = true;
          this.b_jokesAddRating.Enabled = true;
        }
      }

      if(totalJokes > 1) {
        if(jokeNumber == 1) {
          this.b_jokesNext.Enabled = true;
          this.b_jokesPrev.Enabled = false;
        } else {
          if(jokeNumber == totalJokes) {
            this.b_jokesNext.Enabled = false;
            this.b_jokesPrev.Enabled = true;
          } else {
            this.b_jokesNext.Enabled = true;
            this.b_jokesPrev.Enabled = true;
          }
        }
      } else {
        this.b_jokesNext.Enabled = false;
        this.b_jokesPrev.Enabled = false;
      }

      this.tb_jokesJoke.Text = joke;
      this.l_jokesNumber.Text = "Joke " + jokeNumber.ToString() 
        + " of " + totalJokes.ToString();
      this.l_jokesRating.Text = "Avg. rating: " + rating.ToString();            
    }

    private void logon(bool isModerator, bool register) {
      string userName = this.tb_logonUserName.Text;
      string password = this.tb_logonPassword.Text;
      if((userName.Length > 0) && (password.Length > 0)) {
        userName = userName.Substring(0,Math.Min(userName.Length, 20));
        password = password.Substring(0,Math.Min(password.Length, 20));
        if(!this.userAdminObjCreated) {
          this.userAdminObj = new userAdmin.userAdmin();        
          this.userAdminObjCreated = true;
        }
        try {
          // register new user?
          if(register) {
            // Call our Web Service method addUser
            this.userAdminObj.addUser(userName, password);
          } else {
            // Call our Web Service method checkUser
            this.userAdminObj.checkUser(
              userName.Substring(0,Math.Min(userName.Length, 20)),
              password.Substring(0,Math.Min(password.Length, 20)),
              isModerator);
          }
          // OK          
          this.userName = userName;
          this.password = password;
          this.isModerator = isModerator;
          if(isModerator) {
            this.gb_moderatorMenu.Enabled = true;
          } else {
            this.gb_moderatorMenu.Enabled = false;
          }
          this.gb_userMenu.Enabled = true;
          this.gb_userInfo.Enabled = false;
          this.l_statusMessage.Text = "";
          displayJoke("", 0, 0, 0, this.isModerator);
          if(register) {
            this.l_statusMessage.Text = "OK: you have successfully " +
              "registered with the system!";
          }
        } catch (SoapException ex) {
          XmlNode[] customErrorMsgs = ex.OtherElements;
          if(customErrorMsgs.Length > 0) {
            XmlNode customErrorMsg = customErrorMsgs[0];
            if (customErrorMsg.InnerText.Length > 0) {
              this.l_statusMessage.Text = "Error: " + 
                customErrorMsg.InnerText;
              return;
            }
          }
        } catch (Exception ex) {
          this.l_statusMessage.Text = "Error: " + ex.Message;
        }
      }
    }

    private void getJokes(int howMany, bool moderatedJokes) {        
        try {
          if(!this.jokesObjCreated) {
            this.jokesObj = new jokes.jokes();
            this.jokesObjCreated = true;
          }
          // Call our Web Service method getJokes
          if(moderatedJokes) {
            myJokes = this.jokesObj.getJokes(
              userName, password, howMany);
          } else {
            myJokes = this.jokesObj.getUnmoderated(
              userName, password, howMany);
          }
          // OK
          this.jokesReturned = myJokes.Length;
          if(this.jokesReturned == 0) {
            displayJoke("", 0, 0, 0, this.isModerator);
          } else {
            this.currentJoke = 1;
            displayJoke(
              myJokes[this.currentJoke - 1].joke,
              this.currentJoke,
              this.jokesReturned,
              // need leading zero in case NULL is returned from
              // the database, i.e. joke unrated (which
              // will come back as zero length string)
              Decimal.Parse(
                "0" + myJokes[this.currentJoke - 1].rating),
                moderatedJokes);
          }
        } catch (SoapException ex) {
          XmlNode[] customErrorMsgs = ex.OtherElements;
          if(customErrorMsgs.Length > 0) {
            XmlNode customErrorMsg = customErrorMsgs[0];
            if (customErrorMsg.InnerText.Length > 0) {
              this.l_statusMessage.Text = 
                "Error: " + customErrorMsg.InnerText;
              return;
            }
          }
        } catch (Exception ex) {
          this.l_statusMessage.Text = "Error: " + ex.Message;
        }
      }

    private void b_logonUserLogOn_Click(
      object sender, System.EventArgs e) {      
      logon(false, false); 
    }

    private void b_logonModeratorLogOn_Click(
      object sender, System.EventArgs e) {
      logon(true, false);
    }

    private void b_logonRegisterNow_Click(
      object sender, System.EventArgs e) {
      logon(false, true);      
    }

    private void b_moderatorMakeModerator_Click(
      object sender, System.EventArgs e) {
      displayJoke("", 0, 0, 0, this.isModerator);
      string newModeratorUserName = 
        this.tb_moderatorNewModeratorUserName.Text;
      if(newModeratorUserName.Length > 0) {
        newModeratorUserName = newModeratorUserName.Substring(
          0,Math.Min(newModeratorUserName.Length, 20));
        if(!this.userAdminObjCreated) {
          this.userAdminObj = new userAdmin.userAdmin();        
          this.userAdminObjCreated = true;
        }
        try {
            // Call our Web Service method addModerator
            this.userAdminObj.addModerator(
              this.userName, this.password, newModeratorUserName);
          // OK          
          this.l_statusMessage.Text = 
            "OK: " + newModeratorUserName + " is now a moderator";
        } catch (SoapException ex) {
          XmlNode[] customErrorMsgs = ex.OtherElements;
          if(customErrorMsgs.Length > 0) {
            XmlNode customErrorMsg = customErrorMsgs[0];
            if (customErrorMsg.InnerText.Length > 0) {
              this.l_statusMessage.Text = 
                "Error: " + customErrorMsg.InnerText;
              return;
            }
          }
        } catch (Exception ex) {
          this.l_statusMessage.Text = "Error: " + ex.Message;
        }
      }
    }

    private void b_userGetJokes_Click(
      object sender, System.EventArgs e) {
      displayJoke("", 0, 0, 0, this.isModerator);
      this.moderatedJokes = true;
      getJokes((int)this.nud_userHowMany.Value, this.moderatedJokes);
    }

    private void b_moderatorGetUnmoderated_Click(
      object sender, System.EventArgs e) {
      displayJoke("", 0, 0, 0, this.isModerator);
      this.moderatedJokes = false;
      getJokes(
        (int)this.nud_moderatorHowMany.Value, this.moderatedJokes);
    }

    private void b_jokesPrev_Click(object sender, System.EventArgs e) {
      // displayJoke() ONLY enables this button if there are jokes
      // to display, so we don't need a sanity check here.
      this.currentJoke = this.currentJoke - 1;
      displayJoke(
        myJokes[this.currentJoke - 1].joke,
        this.currentJoke,
        this.jokesReturned,
        Decimal.Parse("0" + myJokes[this.currentJoke - 1].rating),
        this.moderatedJokes);
    }

    private void b_jokesNext_Click(object sender, System.EventArgs e) {
      // displayJoke() ONLY enables this button if there are jokes
      // to display, so we don't need a sanity check here.
      this.currentJoke = this.currentJoke + 1;
      displayJoke(
        myJokes[this.currentJoke - 1].joke,
        this.currentJoke,
        this.jokesReturned,
        Decimal.Parse("0" + myJokes[this.currentJoke - 1].rating),
        this.moderatedJokes);
    }

    private void b_jokesAddRating_Click(
      object sender, System.EventArgs e) {
      try {
        if(!this.jokesObjCreated) {
          this.jokesObj = new jokes.jokes();
          this.jokesObjCreated = true;
        }
        // Call our Web Service method addRating
        this.jokesObj.addRating(
          userName, 
          password, 
          (int)this.nud_jokesRating.Value, 
          Int32.Parse(this.myJokes[this.currentJoke-1].jokeID));
        // OK
        // try to tell user not to rate the joke again...
        this.b_jokesAddRating.Enabled = false;
        this.l_statusMessage.Text = "Note: New rating is " +
          "reflected only once joke has been reloaded!";
      } catch (SoapException ex) {
        XmlNode[] customErrorMsgs = ex.OtherElements;
        if(customErrorMsgs.Length > 0) {
          XmlNode customErrorMsg = customErrorMsgs[0];
          if (customErrorMsg.InnerText.Length > 0) {
            this.l_statusMessage.Text = 
              "Error: " + customErrorMsg.InnerText;
            return;
          }
        }
      } catch (Exception ex) {
        this.l_statusMessage.Text = "Error: " + ex.Message;
      }
    }

    private void b_jokesAddModerated_Click(object sender, System.EventArgs e) {
      try {
        if(!this.jokesObjCreated) {
          this.jokesObj = new jokes.jokes();
          this.jokesObjCreated = true;
        }
        // Call our Web Service method addRating
        this.jokesObj.addModerated(
          userName, 
          password,
          Int32.Parse(this.myJokes[this.currentJoke-1].jokeID));
        // OK
        this.l_statusMessage.Text = 
          "OK: Joke is now available for registered users!";
      } catch (SoapException ex) {
        XmlNode[] customErrorMsgs = ex.OtherElements;
        if(customErrorMsgs.Length > 0) {
          XmlNode customErrorMsg = customErrorMsgs[0];
          if (customErrorMsg.InnerText.Length > 0) {
            this.l_statusMessage.Text = 
              "Error: " + customErrorMsg.InnerText;
            return;
          }
        }
      } catch (Exception ex) {
        this.l_statusMessage.Text = "Error: " + ex.Message;
      }
    }

    private void b_jokesRemove_Click(
      object sender, System.EventArgs e) {
      try {
        if(!this.jokesObjCreated) {
          this.jokesObj = new jokes.jokes();
          this.jokesObjCreated = true;;
        }
        // Call our Web Service method addRating
        this.jokesObj.deleteUnmoderated(
          userName, 
          password,
          Int32.Parse(this.myJokes[this.currentJoke-1].jokeID));
        // OK
        this.l_statusMessage.Text = "OK: Joke has been removed!";
      } catch (SoapException ex) {
        XmlNode[] customErrorMsgs = ex.OtherElements;
        if(customErrorMsgs.Length > 0) {
          XmlNode customErrorMsg = customErrorMsgs[0];
          if (customErrorMsg.InnerText.Length > 0) {
            this.l_statusMessage.Text = 
              "Error: " + customErrorMsg.InnerText;
            return;
          }
        }
      } catch (Exception ex) {
        this.l_statusMessage.Text = "Error: " + ex.Message;
      }
    }

    private void b_userAddJoke_Click(
      object sender, System.EventArgs e) {
      displayJoke("", 0, 0, 0, this.isModerator);
      string newJoke = this.tb_userJoke.Text;
      if(newJoke.Length > 0) {
        newJoke = newJoke.Substring(
          0,Math.Min(newJoke.Length, 3500));
        try {
          if(!this.jokesObjCreated) {
            this.jokesObj = new jokes.jokes();
            this.jokesObjCreated = true;
          }
          // Call our Web Service method addRating
          this.jokesObj.addJoke(
            userName, 
            password,
            newJoke);
          // OK
          this.l_statusMessage.Text = "OK: Joke has been " +
            "submitted for consideration to the system!";
          this.tb_userJoke.Text = "";
        } catch (SoapException ex) {
          XmlNode[] customErrorMsgs = ex.OtherElements;
          if(customErrorMsgs.Length > 0) {
            XmlNode customErrorMsg = customErrorMsgs[0];
            if (customErrorMsg.InnerText.Length > 0) {
              this.l_statusMessage.Text = 
                "Error: " + customErrorMsg.InnerText;
              return;
            }
          }
        } catch (Exception ex) {
          this.l_statusMessage.Text = "Error: " + ex.Message;
        }
      }
    }
	}
}
