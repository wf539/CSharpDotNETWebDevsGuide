using System;
using System.Data;
using System.Data.SqlClient;

namespace jokesService
{
	/// <summary>
	///   Implementation class for userAdmin web service.
	/// </summary>
  /// <remarks>
  ///   Author: Adrian Turtschi; aturtschi@hotmail.com; Sept 2001
  /// </remarks>
  public class userAdminImplement {

    /// <summary>
    ///   Public class constructor.
    /// </summary>
		protected internal userAdminImplement() {
		}

    /// <summary>
    ///   The createSqlManageUser method sets up the SQL command object 
    ///   for the stored procedure sp_manageUser, which deals with 
    ///   adding, updating, and deleting users and managers
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of registered user'>
    /// </param>
    /// <param name='password'
    ///   type='string'
    ///   desc='password of registered user (zero length if N/A)'>
    /// </param>
    /// <param name='isModerator'
    ///   type='string'
    ///   desc='true/false if this user is a moderator'>
    /// </param>
    /// <param name='action'
    ///   type='string'
    ///   desc='the action the SQL stored procedure should take 
    ///   (see the stored procedure definition for allowed action 
    ///   keywords)'>
    /// </param>
    /// <param name='sqlCommand'
    ///   type='SqlCommand'
    ///   desc='a reference to a SQL command object'>
    /// </param>
    /// <returns>the prepared SQL command object</returns>
    protected internal void createSqlManageUser(
      string userName, string password, 
      string isModerator, string action, SqlCommand sqlCommand) {

      sqlCommand.CommandType = CommandType.StoredProcedure;
      sqlCommand.CommandText = "sp_manageUser" ;

      SqlParameter argUserName = 
        new SqlParameter("@@userName", SqlDbType.NVarChar, 20);
      argUserName.Value = userName;
      sqlCommand.Parameters.Add(argUserName);

      SqlParameter argPassword = 
        new SqlParameter("@@password",SqlDbType.NVarChar, 20);
      if(password.Length > 0) {
        argPassword.Value =  password;
      } else {
        argPassword.Value =  DBNull.Value;
      }
      sqlCommand.Parameters.Add(argPassword);

      SqlParameter argIsModerator = 
        new SqlParameter("@@isModerator",SqlDbType.Bit);
      argIsModerator.Value =  bool.Parse(isModerator);
      sqlCommand.Parameters.Add(argIsModerator);

      SqlParameter argAction = 
        new SqlParameter("@@action",SqlDbType.NVarChar, 20);
      argAction.Value =  action;
      sqlCommand.Parameters.Add(argAction);

      SqlParameter argReturn = 
        new SqlParameter("@@return",SqlDbType.NVarChar, 20, 
        ParameterDirection.Output, true, 0, 0, "", 
        DataRowVersion.Current, "");
      sqlCommand.Parameters.Add(argReturn);
    }

    /// <summary>
    ///   The createSqlCheckUser method sets up the SQL command object 
    ///   for the stored procedure sp_checkUser, which verifies passed
    ///   user information with user information in the database
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of registered user (zero length if N/A)'>
    /// </param>
    /// <param name='password'
    ///   type='string'
    ///   desc='password of registered user (zero length if N/A)'>
    /// </param>
    /// <param name='isModerator'
    ///   type='string'
    ///   desc='true/false if this user is a moderator 
    ///   (zero length if N/A)'>
    /// </param>
    /// <param name='sqlCommand'
    ///   type='SqlCommand'
    ///   desc='a reference to a SQL command object'>
    /// </param>
    /// <returns>the prepared SQL command object</returns>
    protected internal void createSqlCheckUser(
      string userName, string password,
      string isModerator, SqlCommand sqlCommand) {

      sqlCommand.CommandType = CommandType.StoredProcedure;
      sqlCommand.CommandText = "sp_checkUser" ;

      SqlParameter argUserName = 
        new SqlParameter("@@userName", SqlDbType.NVarChar, 20);
      if(userName.Length > 0) {
        argUserName.Value =  userName;
      } else {
        argUserName.Value =  DBNull.Value;
      }
      sqlCommand.Parameters.Add(argUserName);

      SqlParameter argPassword = 
        new SqlParameter("@@password",SqlDbType.NVarChar, 20);
      if(password.Length > 0) {
        argPassword.Value =  password;
      } else {
        argPassword.Value =  DBNull.Value;
      }
      sqlCommand.Parameters.Add(argPassword);

      SqlParameter argIsModerator = 
        new SqlParameter("@@isModerator",SqlDbType.Bit);
      if(isModerator.Length > 0) {
        argIsModerator.Value = bool.Parse(isModerator);
      } else {          
          argIsModerator.Value =  DBNull.Value;
      }
      sqlCommand.Parameters.Add(argIsModerator);

      SqlParameter argReturn = 
        new SqlParameter("@@return",SqlDbType.NVarChar, 20, 
        ParameterDirection.Output, true, 0, 0, "", 
        DataRowVersion.Current, "");
      sqlCommand.Parameters.Add(argReturn);
    }

    /// <summary>
    ///   The addUser method adds a new user to the database
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of new user'>
    /// </param>
    /// <param name='password'
    ///   type='string'
    ///   desc='password of new user'>
    /// </param>
    /// <returns>true</returns>
    protected internal bool addUser(string userName, string password) {
      try {
        string retCode;
        SqlCommand sqlCommand = new SqlCommand();
        createSqlManageUser(
          userName, password, "false", "add", sqlCommand);

        databaseAccess myDatabase = new databaseAccess();
        sqlCommand.Connection = myDatabase.getConnection();      
        sqlCommand.Connection.Open();

        sqlCommand.ExecuteNonQuery();
        sqlCommand.Connection.Close();

        retCode = sqlCommand.Parameters["@@return"].Value.ToString();

        // catch problems within the stored procedure
        if (retCode == "S_OK") {
          return true;
        } else {
          throw new jokeException(retCode);
        }
      // catch problems with the database
      } catch (Exception e) {
        throw e;
      }      
    }

    /// <summary>
    ///   The addModerator method sets a previously added user to become 
    ///   a moderator
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of moderator making the call'>
    /// </param>
    /// <param name='password'
    ///   type='string'
    ///   desc='password of moderator making the call'>
    /// </param>
    /// <param name='newModerator'
    ///   type='string'
    ///   desc='user name of registered user who will become
    ///   a moderator'>
    /// </param>
    /// <returns>true</returns>
    protected internal bool addModerator(
      string userName, string password, string newModerator) {
      string retCode;

      try {
        // check if user is a moderator
        SqlCommand sqlCommand = new SqlCommand();
        createSqlCheckUser(userName, password, "true", sqlCommand);

        databaseAccess myDatabase = new databaseAccess();
        sqlCommand.Connection = myDatabase.getConnection();      
        sqlCommand.Connection.Open();

        sqlCommand.ExecuteNonQuery();

        retCode = sqlCommand.Parameters["@@return"].Value.ToString();

        // catch problems within the stored procedure
        if (retCode != "S_OK") {
          sqlCommand.Connection.Close();
          throw new jokeException(retCode);
        }

        // make newModerator a moderator
        sqlCommand.Parameters.Clear();
        createSqlManageUser(
          newModerator, "", "true", "modify", sqlCommand);

        sqlCommand.ExecuteNonQuery();
        sqlCommand.Connection.Close();

        retCode = sqlCommand.Parameters["@@return"].Value.ToString();

        // catch problems within the stored procedure
        if (retCode == "S_OK") {
          return true;
        } else {
          throw new jokeException(retCode);
        }
        // catch problems with the database
      } catch (Exception e) {
        throw e;
      }      
    }

    /// <summary>
    ///   The checkUser method checks if a user or moderator is
    ///   already defined in the database
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of user or moderator'>
    /// </param>
    /// <param name='password'
    ///   type='string'
    ///   desc='password of user or moderator'>
    /// </param>
    /// <param name='isModerator'
    ///   type='bool'
    ///   desc='check for moderator status (if false, 
    ///   we do not check)'>
    /// </param>
    /// <returns>nothing</returns>
    protected internal bool checkUser(
      string userName, string password, bool isModerator) {
      string retCode;

      try {
        SqlCommand sqlCommand = new SqlCommand();
        
        if(isModerator) {
          // check if user is a moderator...
          createSqlCheckUser(userName, password, "true", sqlCommand);
        } else {
          // ... or a registered user
          createSqlCheckUser(userName, password, "", sqlCommand);
        }

        databaseAccess myDatabase = new databaseAccess();
        sqlCommand.Connection = myDatabase.getConnection();      
        sqlCommand.Connection.Open();

        sqlCommand.ExecuteNonQuery();

        retCode = sqlCommand.Parameters["@@return"].Value.ToString();

        // catch problems within the stored procedure
        if (retCode == "S_OK") {
          return true;
        } else {
          throw new jokeException(retCode);
        }
        // catch problems with the database
      } catch (Exception e) {
        throw e;
      }      
    }  
  }
}
