using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;



namespace mb.components
{
	/// <summary>
	/// Summary description for mbdataaccess.
	/// </summary>
	public class mbdataaccess
	{
		protected string connection = "initial catalog=MessageDb;persist security info=False;user id=sa;password=password;Data Source=MAUNALOA;packet size=4096";
		public void addMessage(string MsName,string MsMessage, string MsSubject, string MsEmail, int GpID)
		{
			
			SqlConnection connAddMess = new SqlConnection(this.connection);
				
			SqlCommand cmdAddMess = new SqlCommand("addMessage",connAddMess);
			cmdAddMess.CommandType = CommandType.StoredProcedure;


			SqlParameter prmMsName = new SqlParameter("@MsName", SqlDbType.NVarChar, 50);
			prmMsName.Direction = ParameterDirection.Input;
			cmdAddMess.Parameters.Add(prmMsName);
			prmMsName.Value = 	MsName;
	
			SqlParameter prmMsMessage = new SqlParameter("@MsMessage", SqlDbType.Text, 500);
			prmMsMessage.Direction = ParameterDirection.Input;
			cmdAddMess.Parameters.Add(prmMsMessage);
			prmMsMessage.Value = 	MsMessage;

			SqlParameter prmMsSubject = new SqlParameter("@MsSubject", SqlDbType.NVarChar, 50);
			prmMsSubject.Direction = ParameterDirection.Input;
			cmdAddMess.Parameters.Add(prmMsSubject);
			prmMsSubject.Value = 	MsSubject;


			SqlParameter prmMsEmail = new SqlParameter("@MsEmail", SqlDbType.NVarChar, 50);
			prmMsEmail.Direction = ParameterDirection.Input;
			cmdAddMess.Parameters.Add(prmMsEmail);
			prmMsEmail.Value = 	MsEmail;


			SqlParameter prmGpID = new SqlParameter("@GpID", SqlDbType.Int, 4);
			prmGpID.Direction = ParameterDirection.Input;
			cmdAddMess.Parameters.Add(prmGpID);
			prmGpID.Value = 	GpID;


			connAddMess.Open();

			cmdAddMess.ExecuteNonQuery();
			
			connAddMess.Close();
					
		}

		public void addResponse(int MsID,string RsName, string RsEmail, string RsMessage)
		{
			
			SqlConnection connAddResp = new SqlConnection(this.connection);
				
			SqlCommand cmdAddResp = new SqlCommand("addResponse",connAddResp);
			cmdAddResp.CommandType = CommandType.StoredProcedure;


			SqlParameter prmMsID = new SqlParameter("@MsID", SqlDbType.Int, 4);
			prmMsID.Direction = ParameterDirection.Input;
			cmdAddResp.Parameters.Add(prmMsID);
			prmMsID.Value = 	MsID;
	
			SqlParameter prmRsName = new SqlParameter("@RsName", SqlDbType.NVarChar, 50);
			prmRsName.Direction = ParameterDirection.Input;
			cmdAddResp.Parameters.Add(prmRsName);
			prmRsName.Value = 	RsName;

			SqlParameter prmRsEmail = new SqlParameter("@RsEmail", SqlDbType.NVarChar, 50);
			prmRsEmail.Direction = ParameterDirection.Input;
			cmdAddResp.Parameters.Add(prmRsEmail);
			prmRsEmail.Value = 	RsEmail;


			SqlParameter prmRsMessage = new SqlParameter("@RsMessage", SqlDbType.Text, 500);
			prmRsMessage.Direction = ParameterDirection.Input;
			cmdAddResp.Parameters.Add(prmRsMessage);
			prmRsMessage.Value = 	RsMessage;


			connAddResp.Open();

			cmdAddResp.ExecuteNonQuery();
			
			connAddResp.Close();
					
		}
		
		public void addGroup(string GpTitle, string GpTopic, int MdId)
		{
			
			SqlConnection connAddGroup = new SqlConnection(this.connection);
				
			SqlCommand cmdAddGroup = new SqlCommand("addGroup",connAddGroup);
			cmdAddGroup.CommandType = CommandType.StoredProcedure;


			SqlParameter prmGpTitle = new SqlParameter("@GpTitle", SqlDbType.NVarChar, 50);
			prmGpTitle.Direction = ParameterDirection.Input;
			cmdAddGroup.Parameters.Add(prmGpTitle);
			prmGpTitle.Value = 	GpTitle;
	
			SqlParameter prmGpTopic = new SqlParameter("@GpTopic", SqlDbType.NVarChar, 50);
			prmGpTopic.Direction = ParameterDirection.Input;
			cmdAddGroup.Parameters.Add(prmGpTopic);
			prmGpTopic.Value = 	GpTopic;

			SqlParameter prmMdId = new SqlParameter("@MdId", SqlDbType.Int, 4);
			prmMdId.Direction = ParameterDirection.Input;
			cmdAddGroup.Parameters.Add(prmMdId);
			prmMdId.Value = 	MdId;
		

			connAddGroup.Open();

			cmdAddGroup.ExecuteNonQuery();
			
			connAddGroup.Close();
					
		}
		
		public void delMessage(int MsId)
		{
			
			SqlConnection connDelMessage = new SqlConnection(this.connection);
				
			SqlCommand cmdDelMessage = new SqlCommand("delMessage",connDelMessage);
			cmdDelMessage.CommandType = CommandType.StoredProcedure;


			SqlParameter prmMsId = new SqlParameter("@MsId", SqlDbType.Int, 4);
			prmMsId.Direction = ParameterDirection.Input;
			cmdDelMessage.Parameters.Add(prmMsId);
			prmMsId.Value = 	MsId;
	
			connDelMessage.Open();

			cmdDelMessage.ExecuteNonQuery();
			
			connDelMessage.Close();
					
		}

		public void delResponse(int RsId)
		{
			
			SqlConnection connDelResponse = new SqlConnection(this.connection);
				
			SqlCommand cmdDelResponse = new SqlCommand("delResponse",connDelResponse);
			cmdDelResponse.CommandType = CommandType.StoredProcedure;


			SqlParameter prmRsId = new SqlParameter("@RsId", SqlDbType.Int, 4);
			prmRsId.Direction = ParameterDirection.Input;
			cmdDelResponse.Parameters.Add(prmRsId);
			prmRsId.Value = 	RsId;
	
			connDelResponse.Open();

			cmdDelResponse.ExecuteNonQuery();
			
			connDelResponse.Close();
					
		}
		public void delGroup(int GpId)
		{
			
			SqlConnection connDelGroup = new SqlConnection(this.connection);
				
			SqlCommand cmdDelGroup = new SqlCommand("delGroup",connDelGroup);
			cmdDelGroup.CommandType = CommandType.StoredProcedure;


			SqlParameter prmGpId = new SqlParameter("@GpId", SqlDbType.Int, 4);
			prmGpId.Direction = ParameterDirection.Input;
			cmdDelGroup.Parameters.Add(prmGpId);
			prmGpId.Value = 	GpId;
	
			connDelGroup.Open();

			cmdDelGroup.ExecuteNonQuery();
			
			connDelGroup.Close();
					
		}

		public DataSet getAllGroups()
		{
		
			SqlConnection connGroup = new SqlConnection(this.connection);
			SqlCommand cmdGroup = new SqlCommand("allGroups", connGroup);
			cmdGroup.CommandType = CommandType.StoredProcedure;
			
			connGroup.Open();

			SqlDataAdapter da = new SqlDataAdapter (cmdGroup) ;
			DataSet ds = new DataSet();
			da.Fill (ds,"group");
			
			
			connGroup.Close();
						
			return ds;			
				
		}

		public int addMod(string MdEmail,string MdPassword )
		{
			
			SqlConnection connMod = new SqlConnection(this.connection);
				
			SqlCommand addMod = new SqlCommand("newModerator",connMod);
			addMod.CommandType = CommandType.StoredProcedure;


			SqlParameter prmMdEmail = new SqlParameter("@MdEmail", SqlDbType.NVarChar, 50);
			prmMdEmail.Direction = ParameterDirection.Input;
			addMod.Parameters.Add(prmMdEmail);
			prmMdEmail.Value = 	MdEmail;
	
			SqlParameter prmMdPassword = new SqlParameter("@MdPassword", SqlDbType.NVarChar, 50);
			prmMdEmail.Direction = ParameterDirection.Input;
			addMod.Parameters.Add(prmMdPassword);
			prmMdPassword.Value = 	MdPassword;

			SqlParameter prmMdID = new SqlParameter("@MdID", SqlDbType.Int, 4);
			prmMdID.Direction = ParameterDirection.Output;
			addMod.Parameters.Add(prmMdID);
		
			connMod.Open();

			addMod.ExecuteNonQuery();
			
			connMod.Close();
					
			return (int)prmMdID.Value;

		}

		public string loginDbConn(string email, string password)
		{
			SqlConnection connLogin = new SqlConnection(this.connection);
			SqlCommand cmdlogin = new SqlCommand("loginModerator", connLogin);
			cmdlogin.CommandType = CommandType.StoredProcedure;
			
			SqlParameter prmEmail = new SqlParameter("@MdEmail", SqlDbType.NVarChar, 50);
			prmEmail.Direction = ParameterDirection.Input;
			cmdlogin.Parameters.Add(prmEmail);
			prmEmail.Value = email;


			SqlParameter prmPassword = new SqlParameter("@MdPassword", SqlDbType.NVarChar, 50);
			prmPassword.Direction = ParameterDirection.Input;
			cmdlogin.Parameters.Add(prmPassword);
			prmPassword.Value = password;

			connLogin.Open();

			SqlDataAdapter da = new SqlDataAdapter (cmdlogin) ;
			DataSet ds = new DataSet ( ) ;
			da.Fill ( ds , "Moderator" ) ;
			
			
			connLogin.Close();
			try
			{
	
				string resultId = ds.Tables["Moderator"].Rows[0][0].ToString();
				return resultId;			
			}
			catch(Exception e)
			{
				string temp = e.Message;
				return "error";
				
			}
		}
		public DataTable getAllMess(int GpID)
		{

			SqlConnection connMess = new SqlConnection(this.connection);
			SqlCommand cmdMess = new SqlCommand("returnAllMess", connMess);
			cmdMess.CommandType = CommandType.StoredProcedure;
			
			SqlParameter prmGroupID = new SqlParameter("@GpID", SqlDbType.Int, 4);
			prmGroupID.Direction = ParameterDirection.Input;
			cmdMess.Parameters.Add(prmGroupID);
			prmGroupID.Value = 	GpID;		


			connMess.Open();

			SqlDataAdapter da = new SqlDataAdapter (cmdMess) ;
			DataSet ds = new DataSet ( ) ;
			da.Fill ( ds , "Message" ) ;
			
			
			connMess.Close();
						
			return ds.Tables["Message"];
			//return ds.GetXml();
		}


	}
}
