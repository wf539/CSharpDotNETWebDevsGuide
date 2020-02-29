using System;
using System.Data;
using System.Data.SqlClient;

using System.Diagnostics;

namespace jokesService
{
	/// <summary>
	///   Implementation class for jokes web service.
	/// </summary>
  /// <remarks>
  ///   Author: Adrian Turtschi; aturtschi@hotmail.com; Sept 2001
  /// </remarks>
  public class jokesImplement
	{

    /// <summary>
    ///   Public class constructor.
    /// </summary>
    protected internal jokesImplement() {
		}

    /// <summary>
    ///   The createSqlManageRating method sets up the SQL command 
    ///   object for the stored procedure sp_manageRating, which 
    ///   deals with adding and deleting user joke ratings
    /// </summary>
    /// <param name='jokeID'
    ///   type='string'
    ///   desc='the joke ID for the joke we would like to rate'>
    /// </param>
    /// <param name='rating'
    ///   type='string'
    ///   desc='the user rating for the joke (1-5)'>
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
    protected internal void createSqlManageRating(
      string jokeID, string rating, string action, 
      SqlCommand sqlCommand) {

      sqlCommand.CommandType = CommandType.StoredProcedure;
      sqlCommand.CommandText = "sp_manageRating" ;

      SqlParameter argJokeID = 
        new SqlParameter("@@jokeID", SqlDbType.Int);
      argJokeID.Value =  Int32.Parse(jokeID);
      sqlCommand.Parameters.Add(argJokeID);

      SqlParameter argRating = 
        new SqlParameter("@@rating",SqlDbType.TinyInt);
      argRating.Value =  Int32.Parse(rating);
      sqlCommand.Parameters.Add(argRating);

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
    ///   The createSqlManageJoke method sets up the SQL command object 
    ///   for the stored procedure sp_manageJoke, which deals with 
    ///   adding, updating, and deleting jokes
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of registered user (zero length if N/A)'>
    /// </param>
    /// <param name='joke'
    ///   type='string'
    ///   desc='the joke (zero length if N/A)'>
    /// </param>
    /// <param name='isModerated'
    ///   type='string'
    ///   desc='true/false if this is/is not a moderated joke 
    ///   (zero length if N/A)'>
    /// </param>
    /// <param name='jokeID'
    ///   type='string'
    ///   desc='the joke ID for the joke (zero length if N/A)'>
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
    protected internal void createSqlManageJoke(
      string userName, string joke, string isModerated, 
      string jokeID, string action, SqlCommand sqlCommand) {

      sqlCommand.CommandType = CommandType.StoredProcedure;
      sqlCommand.CommandText = "sp_manageJoke" ;

      SqlParameter argUserName = 
        new SqlParameter("@@userName", SqlDbType.NVarChar, 20);
      if(userName.Length > 0) {
        argUserName.Value =  userName;
      } else {
        argUserName.Value =  DBNull.Value;
      }
      sqlCommand.Parameters.Add(argUserName);

      SqlParameter argJoke = 
        new SqlParameter("@@joke",SqlDbType.NVarChar, 3500);
      if(joke.Length > 0) {
        argJoke.Value =  joke;
      } else {
        argJoke.Value =  DBNull.Value;
      }
      sqlCommand.Parameters.Add(argJoke);

      SqlParameter argIsModerated = 
        new SqlParameter("@@isModerated",SqlDbType.Bit);
      if(isModerated.Length > 0) {
        argIsModerated.Value = bool.Parse(isModerated);
      } else {          
        argIsModerated.Value =  DBNull.Value;
      }
      sqlCommand.Parameters.Add(argIsModerated);

      SqlParameter argJokeID = 
        new SqlParameter("@@jokeID",SqlDbType.Int);
      if(jokeID.Length > 0) {
        argJokeID.Value =  Int32.Parse(jokeID);
      } else {
        argJokeID.Value =  DBNull.Value;
      }
      sqlCommand.Parameters.Add(argJokeID);

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
    ///   The createSqlReturnJokes method sets up the SQL command object 
    ///   for the stored procedure sp_returnJokes, which returns jokes
    /// </summary>
    /// <param name='howMany'
    ///   type='string'
    ///   desc='how many jokes we would like (zero length if N/A)'>
    /// </param>
    /// <param name='isModerated'
    ///   type='string'
    ///   desc='true/false if we are interested in (not) moderated 
    ///   jokes (zero length if N/A)'>
    /// </param>
    /// <param name='returnRandom'
    ///   type='string'
    ///   desc='true/false if we are interested getting random jokes
    ///   (actually, only the starting position is random, from there
    ///   on we retrieve jokes in sequential order for practical 
    ///   reasons)'>
    /// </param>
    /// <param name='sqlCommand'
    ///   type='SqlCommand'
    ///   desc='a reference to a SQL command object'>
    /// </param>
    /// <returns>the prepared SQL command object</returns>
    protected internal void createSqlReturnJokes(
      string howMany, string isModerated, string returnRandom, 
      SqlCommand sqlCommand) {

      sqlCommand.CommandType = CommandType.StoredProcedure;
      sqlCommand.CommandText = "sp_returnJokes" ;

      SqlParameter argHowMany = 
        new SqlParameter("@@howMany", SqlDbType.Int);
      if(howMany.Length > 0) {
        argHowMany.Value =  Int32.Parse(howMany);
      } else {
        argHowMany.Value =  DBNull.Value;
      }
      sqlCommand.Parameters.Add(argHowMany);

      SqlParameter argIsModerated = 
        new SqlParameter("@@isModerated",SqlDbType.Bit);
      if(isModerated.Length > 0) {
        argIsModerated.Value =  bool.Parse(isModerated);
      } else {          
        argIsModerated.Value =  DBNull.Value;
      }
      sqlCommand.Parameters.Add(argIsModerated);

      SqlParameter argReturnRandom = 
        new SqlParameter("@@returnRandom",SqlDbType.Bit);
      argReturnRandom.Value =  bool.Parse(returnRandom);
      sqlCommand.Parameters.Add(argReturnRandom);
    }

    /// <summary>
    ///   The addJoke method lets registered users add a joke
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of registered user'>
    /// </param>
    /// <param name='password'
    ///   type='string'
    ///   desc='password of registered user'>
    /// </param>
    /// <param name='joke'
    ///   type='string'
    ///   desc='the joke we are adding'>
    /// </param>
    /// <returns>true</returns>
    protected internal bool addJoke(
      string userName, string password, string joke) {
      string retCode;

      try {
        // check if user is registered
        userAdminImplement myUser = new userAdminImplement();
        SqlCommand sqlCommand = new SqlCommand();

        myUser.createSqlCheckUser(userName, password, "", sqlCommand);

        databaseAccess myDatabase = new databaseAccess();
        sqlCommand.Connection = myDatabase.getConnection();      
        sqlCommand.Connection.Open();

        sqlCommand.ExecuteNonQuery();

        retCode = sqlCommand.Parameters["@@return"].Value.ToString();

        // exit, if user not registered
        if (retCode != "S_OK") {
          sqlCommand.Connection.Close();
          throw new jokeException(retCode);
        }

        // add the joke (unmoderated, at this point)
        sqlCommand.Parameters.Clear();
        createSqlManageJoke(
          userName, joke, "false", "", "add", sqlCommand);

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
    ///   The getJokes method returns howMany new jokes from the database
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of registered user'>
    /// </param>
    /// <param name='password'
    ///   type='string'
    ///   desc='password of registered user'>
    /// </param>
    /// <param name='howMany'
    ///   type='int'
    ///   desc='number of jokes to return (1-10)'>
    /// </param>
    /// <returns>an XML representation (xmlJokesReturn) of a single joke</returns>
    protected internal xmlJokesReturn[] getJokes(
      string userName, string password, int howMany) {
      string retCode;

      try {
        // check if user is registered
        userAdminImplement myUser = new userAdminImplement();
        SqlCommand sqlCommand = new SqlCommand();

        myUser.createSqlCheckUser(userName, password, "", sqlCommand);

        databaseAccess myDatabase = new databaseAccess();
        sqlCommand.Connection = myDatabase.getConnection();      
        sqlCommand.Connection.Open();

        sqlCommand.ExecuteNonQuery();

        retCode = sqlCommand.Parameters["@@return"].Value.ToString();

        // exit, if user not registered
        if (retCode != "S_OK") {
          sqlCommand.Connection.Close();
          throw new jokeException(retCode);
        }

        // retrieve a random joke

        // maximum is 10 jokes
        if((howMany < 1) || (howMany > 10)) {
          throw new jokeException("F_10JokesMax");
        }

        sqlCommand.Parameters.Clear();
        createSqlReturnJokes(
          howMany.ToString(), "true", "true", sqlCommand);

        sqlCommand.ExecuteNonQuery();

        sqlCommand.Connection.Close();

        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
        DataTable dataTable = new DataTable("sqlReturn");
        sqlDataAdapter.Fill(dataTable);

        // convert SQL table into xmlJokesReturn class
        int rowCount = dataTable.Rows.Count;
        xmlJokesReturn[] myJokes = new xmlJokesReturn[rowCount];
        for(int i = 0; i < rowCount; i++) {
          myJokes[i] = new xmlJokesReturn();
          myJokes[i].jokeID = dataTable.Rows[i][0].ToString();
          myJokes[i].joke   = dataTable.Rows[i][1].ToString();
          myJokes[i].rating = dataTable.Rows[i][2].ToString();
        }
        // catch problems within the stored procedure
        if(rowCount > 0) {
          return myJokes;
        } else {
          throw new jokeException("F_noJokes");
        }
        // catch problems with the database
      } catch (Exception e) {
        throw e;
      }      
    }

    /// <summary>
    ///   The addRating method lets registered users rate a joke
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of registered user'>
    /// </param>
    /// <param name='password'
    ///   type='string'
    ///   desc='password of registered user'>
    /// </param>
    /// <param name='rating'
    ///   type='int'
    ///   desc='the rating of the joke to rate (1-5)'>
    /// </param>
    /// <param name='jokeID'
    ///   type='int'
    ///   desc='the ID of the joke to rate'>
    /// </param>
    /// <returns>true</returns>
    protected internal bool addRating(
      string userName, string password, int rating, int jokeID) {
      string retCode;

      try {
        // check if user is registered
        userAdminImplement myUser = new userAdminImplement();
        SqlCommand sqlCommand = new SqlCommand();

        myUser.createSqlCheckUser(userName, password, "", sqlCommand);

        databaseAccess myDatabase = new databaseAccess();
        sqlCommand.Connection = myDatabase.getConnection();      
        sqlCommand.Connection.Open();

        sqlCommand.ExecuteNonQuery();

        retCode = sqlCommand.Parameters["@@return"].Value.ToString();

        // exit, if user not registered
        if (retCode != "S_OK") {
          sqlCommand.Connection.Close();
          throw new jokeException(retCode);
        }

        // add the joke rating
        sqlCommand.Parameters.Clear();
        createSqlManageRating(
          jokeID.ToString(), rating.ToString(), "add", sqlCommand);

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
    ///   The getUnmoderated method retrieves howMany jokes from 
    ///   the database
    ///   (for moderators only)
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of moderator'>
    /// </param>
    /// <param name='password'
    ///   type='string'
    ///   desc='password of moderator'>
    /// </param>
    /// <param name='howMany'
    ///   type='int'
    ///   desc='number of jokes to return'>
    /// </param>
    /// <returns>an XML representation (xmlJokesReturn) 
    ///   of a single joke</returns>
    protected internal xmlJokesReturn[] getUnmoderated(
      string userName, string password, int howMany) {
      string retCode;

      try {
        // check if user is a moderator
        userAdminImplement myUser = new userAdminImplement();
        SqlCommand sqlCommand = new SqlCommand();

        myUser.createSqlCheckUser(
          userName, password, "true", sqlCommand);

        databaseAccess myDatabase = new databaseAccess();
        sqlCommand.Connection = myDatabase.getConnection();      
        sqlCommand.Connection.Open();

        sqlCommand.ExecuteNonQuery();

        retCode = sqlCommand.Parameters["@@return"].Value.ToString();

        // exit, if user not a moderator
        if (retCode != "S_OK") {
          sqlCommand.Connection.Close();
          throw new jokeException(retCode);
        }

        // retrieve the first <howMany> unmoderated jokes

        // maximum is 10 jokes
        if((howMany < 1) || (howMany > 10)) {
          throw new jokeException("F_10JokesMax");
        }

        sqlCommand.Parameters.Clear();
        createSqlReturnJokes(
          howMany.ToString(), "false", "false", sqlCommand);

        sqlCommand.ExecuteNonQuery();

        sqlCommand.Connection.Close();

        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
        DataTable dataTable = new DataTable("sqlReturn");
        sqlDataAdapter.Fill(dataTable);

        // convert SQL table into xmlJokesReturn class
        int rowCount = dataTable.Rows.Count;
        xmlJokesReturn[] myJokes = new xmlJokesReturn[rowCount];
        for(int i = 0; i < rowCount; i++) {
          myJokes[i] = new xmlJokesReturn();
          myJokes[i].jokeID = dataTable.Rows[i][0].ToString();
          myJokes[i].joke   = dataTable.Rows[i][1].ToString();
          myJokes[i].rating = dataTable.Rows[i][2].ToString();
        }
        // catch problems within the stored procedure
        if(rowCount > 0) {
          return myJokes;
        } else {
          throw new jokeException("F_noJokes");
        }
        // catch problems with the database
      } catch (Exception e) {
        throw e;
      }      
    }
    
    /// <summary>
    ///   The addModerated method sets a previously submitted joke
    ///   to become a moderated joke
    ///   (for moderators only)
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of moderator'>
    /// </param>
    /// <param name='password'
    ///   type='string'
    ///   desc='password of moderator'>
    /// </param>
    /// <param name='jokeID'
    ///   type='int'
    ///   desc='joke ID of joke'>
    /// </param>
    /// <returns>an XML representation (xmlJokesReturn) 
    ///   of a single joke</returns>
    protected internal bool addModerated(
      string userName, string password, int jokeID) {
      string retCode;

      try {
        // check if user is a moderator
        userAdminImplement myUser = new userAdminImplement();
        SqlCommand sqlCommand = new SqlCommand();

        myUser.createSqlCheckUser(
          userName, password, "true", sqlCommand);

        databaseAccess myDatabase = new databaseAccess();
        sqlCommand.Connection = myDatabase.getConnection();      
        sqlCommand.Connection.Open();

        sqlCommand.ExecuteNonQuery();

        retCode = sqlCommand.Parameters["@@return"].Value.ToString();

        // exit, if user not a moderator
        if (retCode != "S_OK") {
          sqlCommand.Connection.Close();
          throw new jokeException(retCode);
        }

        // make the joke a moderated one
        sqlCommand.Parameters.Clear();
        createSqlManageJoke(userName, "", "true", jokeID.ToString(), 
          "modify", sqlCommand);

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
    ///   The deleteUnmoderated method deletes a previously 
    ///   submitted joke (unmoderated) joke
    ///   (for moderators only)
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of moderator'>
    /// </param>
    /// <param name='password'
    ///   type='string'
    ///   desc='password of moderator'>
    /// </param>
    /// <param name='jokeID'
    ///   type='int'
    ///   desc='joke ID of joke'>
    /// </param>
    /// <returns>true</returns>
    protected internal bool deleteUnmoderated(
      string userName, string password, int jokeID) {
      string retCode;

      try {
        // check if user is a moderator
        userAdminImplement myUser = new userAdminImplement();
        SqlCommand sqlCommand = new SqlCommand();

        myUser.createSqlCheckUser(
          userName, password, "true", sqlCommand);

        databaseAccess myDatabase = new databaseAccess();
        sqlCommand.Connection = myDatabase.getConnection();      
        sqlCommand.Connection.Open();

        sqlCommand.ExecuteNonQuery();

        retCode = sqlCommand.Parameters["@@return"].Value.ToString();

        // exit, if user not a moderator
        if (retCode != "S_OK") {
          sqlCommand.Connection.Close();
          throw new jokeException(retCode);
        }

        // delete the joke
        sqlCommand.Parameters.Clear();
        createSqlManageJoke(
          userName, "", "", jokeID.ToString(), "delete", sqlCommand);

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
  }
}
