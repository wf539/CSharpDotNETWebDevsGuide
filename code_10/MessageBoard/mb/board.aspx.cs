using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using mb.components;
using System.Text;

namespace mb
{
	/// <summary>
	/// Summary description for board.
	/// </summary>
	public class xBoard : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button deleteMessage;
		protected System.Web.UI.WebControls.Xml xMsgBoard;
		
		protected XmlDocument messagedoc;
		private bool IsAdmin;

		protected System.Web.UI.HtmlControls.HtmlInputHidden msgID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden responseMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden responderName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden responderEmail;
		protected System.Web.UI.HtmlControls.HtmlInputHidden selection;
		protected System.Web.UI.HtmlControls.HtmlInputHidden itemType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden ClientPost;
			
		protected System.Web.UI.HtmlControls.HtmlInputButton responseButton;
		protected System.Web.UI.WebControls.Label reponseButtonWrapper;
		protected mb.components.mbdataaccess dbMethod;
		protected System.Web.UI.WebControls.Label groupTitle;
		private int moderatorID;
	
		public xBoard()
		{
			Page.Init += new System.EventHandler(Page_Init);
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			messagedoc = new XmlDocument();
			int boardID;
			this.IsAdmin = (bool)Session["IsAdmin"];
			this.moderatorID = (int)Session["moderatorID"];
					
			if( (Request.QueryString["mod"] != null) && ( Request.QueryString["board"] != null))
			{
				groupTitle.CssClass = "title";
				groupTitle.Text = (string)Request.QueryString["title"];
				if((this.IsAdmin) || (this.moderatorID == int.Parse(Request.QueryString["mod"]) ))
				{
					//show the delete button
					deleteMessage.Visible = true;
				}
			
			
				boardID = int.Parse( Request.QueryString["board"]);	
				Session["groupID"] = boardID; //used in message response
			
				
				
				//Get Message and Response Data
				dbMethod = new mb.components.mbdataaccess();
				DataTable result = dbMethod.getAllMess( boardID );

				StringBuilder xstr = new StringBuilder();

				xstr.Append("<messages>");
				for( int i=0; i < result.Rows.Count; i++)
				{
					for(int j=0; j < result.Columns.Count; j++)
					{
						xstr.Append( result.Rows[i][j]);
					}
				}
				xstr.Append("</messages>");
				messagedoc.LoadXml(  xstr.ToString() );
				
				//check for messages in the current group( message board )
				if( messagedoc.SelectNodes("//m").Count > 0 )
				{
					//only write the button to the page when there are messages displayed
					reponseButtonWrapper.Text = "<input type='button' id='responseButton' value='respond to selected message' onclick='respond()' style='width:180px'>";
				}

				//write the messages to the page
				initalize_MessageBoard();
			}
		}

		private void Page_Init(object sender, EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.deleteMessage.Click += new System.EventHandler(this.deleteMessage_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void initalize_MessageBoard()
		{
			xMsgBoard.Document = messagedoc;
			xMsgBoard.TransformSource = "message_board_list.xslt";
		}

		private void deleteMessage_Click(object sender, System.EventArgs e)
		{
			//itemType holds [ Response | Message ]
			//selection holds [ RsID | MsID ]
			//(1)call appropriate delete method
			//(2)remove the node from messagedoc 
			if( ClientPost.Value == "noDelete" )
			{
				return;
			}
			int id = int.Parse( selection.Value );
			if( itemType.Value =="m" )
			{
				dbMethod.delMessage( id );
			}
			else if( itemType.Value =="r")
			{
				dbMethod.delResponse( id  );			
			}
			Server.Transfer("board.aspx");
		}
	}
}
