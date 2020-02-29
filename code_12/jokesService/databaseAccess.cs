using System;
using System.Data.SqlClient;

namespace jokesService
{
	/// <summary>
	///   The databaseAccess sets up the connection to the
	///   data repository.
	/// </summary>
  /// <remarks>
  ///   Author: Adrian Turtschi; aturtschi@hotmail.com; Sept 2001
  /// </remarks>
	public class databaseAccess {
	
    private static string sqlConnectionString = 
      "server=(local)\\NetSDK;database=Jokes;User ID=SA;Password=";
    private SqlConnection sqlConnection;

    /// <summary>
    ///   Public class constructor.
    /// </summary>
    protected internal databaseAccess() {
      sqlConnection = 
        new SqlConnection(databaseAccess.sqlConnectionString);
		}

    /// <summary>
    ///   The getConnection method sets up the database connection
    /// </summary>
    /// <returns>the (closed) SQL connection object</returns>
    protected internal SqlConnection getConnection() {
      return sqlConnection;
    }
	}
}
