using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;


namespace simpleCart.components
{
	/// <summary>
	/// This Class is used for all interactions with the data source
	/// </summary>
	public class dataaccess
	{
		private string connection = "Persist Security Info=False;User ID=sa; password=password; Initial Catalog=shopDb;Data Source=MAUNAKEA";
		
		/// <summary>
		/// getAllBooks connects to the SQL database and executes the stored procedure "GetAllBooks"
		/// </summary>
		/// <returns>DataSet containing the DataTable "Books"</returns>
		public DataSet getAllBooks()
		{
			// shopDb string "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=sa; password= password; Initial Catalog=shopDb;Data Source=MAUNALANI" ;
				
			SqlConnection conn = new SqlConnection ( this.connection ) ;
			
			conn.Open ( ) ;
			SqlCommand  cmd = new SqlCommand ( "GetAllBooks" , conn ) ;
			cmd.CommandType = CommandType.StoredProcedure;

			SqlDataAdapter da = new SqlDataAdapter (cmd) ;
			DataSet ds = new DataSet ( ) ;
			da.Fill ( ds , "Books" ) ;
			conn.Close();
				
			return ds;
		}
	}
}
