using System;
using System.Data;
using System.Data.OleDb;

namespace OrdersDataSet
{
	/// <summary>
	/// Summary description for CDalOleDb.
	/// </summary>
	public class CDalOleDb
	{
		string strConStr;
		private	OleDbConnection cn;
		private OleDbDataAdapter adptr = new OleDbDataAdapter();
		private OleDbCommandBuilder cmdBldr;
		private DataSet ds = new DataSet();

		public CDalOleDb(string sConn)
		{
			this.strConnection = sConn;
		}

		public string strConnection
		{
			get
			{ 
				return strConStr;
			}
			set
			{
				strConStr = value;
				try
				{
					this.cn = new OleDbConnection(value);
				}
				catch (Exception e)
				{
					throw e;
				}
			}
		}

		public OleDbDataReader GetCustomers()
		{
			string sSQL = "SELECT CustomerID FROM Customers";
			OleDbCommand cmd = new OleDbCommand(sSQL, cn);
	
			try
			{
				if (cn.State != ConnectionState.Open)
				{
					cn.Open();
				}
				return cmd.ExecuteReader();
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public DataSet GetOrders(string sCustID)
		{
			string sSQL = "SELECT OrderID, EmployeeID, " +
				" OrderDate, RequiredDate, " +
				" ShippedDate, ShipVia " +
				" FROM Orders " +
				" WHERE CustomerID = '" + sCustID + "'";
	
			try
			{
				if (cn.State == ConnectionState.Closed)
				{
					cn.Open();
				}
				cmdBldr = new OleDbCommandBuilder(adptr);
				adptr.SelectCommand = new OleDbCommand(sSQL, cn);

				adptr.Fill(ds, "Orders");
			}
			catch (Exception e)
			{
				throw e;
			}

			return ds;

		}

		public string GetCustomerName(string sCustID)
		{
			string sSQL = "SELECT CompanyName FROM Customers WHERE CustomerID='" + sCustID + "'";
			OleDbCommand cmd = new OleDbCommand(sSQL, cn);
			if (cn.State == ConnectionState.Closed)
			{
				cn.Open();
			}
			try
			{
				return (string)cmd.ExecuteScalar();
			}
			catch (Exception e)
			{
				throw e;
			}

		}

		public void SaveRecords(string sTable)
		{
			try
			{
				adptr.Update(ds, sTable);
			}
			catch(Exception e)
			{
				throw e;
			}
			
		}

	}
}
