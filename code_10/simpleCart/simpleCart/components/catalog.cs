using System;
using System.Data;


namespace simpleCart.components
{
	/// <summary>
	/// bookCatalog acts as cached datasource. Enables retreival of data in data ranges
	/// </summary>	
	public class bookCatalog
	{
		private DataSet dsAllBooks;
			
		/// <summary>
		/// Initalizes bookCatalog by reading in a dataset
		/// </summary>
		/// <param name="ds">DataSet</param>
		public void initCatalog(DataSet ds )
		{
			dsAllBooks = ds;
			int recordCount = dsAllBooks.Tables[0].Rows.Count;
			try
			{
				dsAllBooks.Tables.Add( createSummaryTable(0, recordCount-1, recordCount) );
			}
			catch(Exception e)
			{
				string temp = e.ToString();
				//this fails when attempting to add the table twice
			}
		}

		/// <summary>
		/// Creates a table that is added to the DataSet. This table contains some metadata 
		/// about the dataset returned.
		/// </summary>
		/// <param name="startPos">int</param>
		/// <param name="range">int</param>
		/// <param name="RecordCount">int</param>
		/// <returns>DataTable</returns>
		public DataTable createSummaryTable(int startPos, int range, int RecordCount)
		{
			DataTable dtSummary = new DataTable("Summary");
			DataRow drSummary;
			dtSummary.Columns.Add(new DataColumn("RecordCount", typeof(int)));
			dtSummary.Columns.Add(new DataColumn("FirstItemIndex", typeof(int)));
			dtSummary.Columns.Add(new DataColumn("LastItemIndex", typeof(int)));

			drSummary = dtSummary.NewRow();
			drSummary["RecordCount"]    = RecordCount;
			drSummary["FirstItemIndex"] = startPos;
			drSummary["LastItemIndex"]  = startPos + range;
			dtSummary.Rows.Add( drSummary );

			return dtSummary;
		}

		/// <summary>
		/// This Method returns the input DataSet
		/// </summary>
		/// <returns>DataSet</returns>
		public DataSet catalog()
		{
			return dsAllBooks;
		}

		/// <summary>
		/// Specialized interface to catalogRangeByCategory.
		/// This Method returns all the data for only the given book
		/// </summary>
		/// <param name="book_isbn">string</param>
		/// <returns>DataSet</returns>
		public DataSet catalogItemDetails( string book_isbn )
		{
			return catalogRangeByCategory( -1, -1, book_isbn);
		}

		/// <summary>
		/// Specialized interface to catalogRangeByCategory.
		/// This Method returns all the books within the given range
		/// </summary>
		/// <param name="startPos">int</param>
		/// <param name="range">int</param>
		/// <returns>DataSet</returns>
		public DataSet catalogRange(int startPos, int range)
		{
			return catalogRangeByCategory( startPos, range, null);
		}
		
		/// <summary>
		/// This function returns a DataSet containing a subset of the larger dataset specified by the startPos and range parameters, along with a metadata table 
		/// </summary>
		/// <param name="startPos">int</param>
		/// <param name="range">int</param>
		/// <param name="book_isbn">int</param>
		/// <returns>DataSet</returns>
		protected DataSet catalogRangeByCategory(int startPos, int range, string book_isbn)
		{
			DataSet				dsBookRange;
			DataTable			dtBooks;
			DataTable			dtTemp;
			string				strExpr;
			string				strSort;
			DataRow[]			foundRows;
			int					endPos;
			int					RecordCount;
			DataViewRowState recState;

			dtTemp = dsAllBooks.Tables["Books"];
			dtBooks	= dtTemp.Clone();//create Empty Books Table

			if( book_isbn != null)
			{
				//return a single item
				strExpr = "isbn='" + book_isbn + "'";
			}
			else
			{
				strExpr = "";
			}

			strSort ="title";
			recState = DataViewRowState.CurrentRows;
			foundRows = dtTemp.Select(strExpr, strSort, recState);
			
			RecordCount = foundRows.Length;

			if( (startPos == -1) && (range == -1))
			{
				startPos = 0;
				range = RecordCount;
			}

			if( (startPos + range) > RecordCount)
			{
				endPos = RecordCount;
			}
			else
			{
				endPos = startPos + range;
			}

			for(int i = startPos; i < endPos; i ++)
			{
				dtBooks.ImportRow( (DataRow)foundRows[i] );
			}
			
			dsBookRange = new DataSet();
			dsBookRange.Tables.Add(	dtBooks );

			// add a summary table to the dataset
			dsBookRange.Tables.Add(  createSummaryTable( startPos, range, RecordCount)  ); 
			return dsBookRange;
		}
	}	
}
