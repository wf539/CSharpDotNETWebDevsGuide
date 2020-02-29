using System;
using System.Data;
using System.Data.SqlClient;

namespace OrdersDataSet
{
	/// <summary>
	/// Summary description for CDalSql.
	/// </summary>
	public class CDalSql
	{
		string strConStr;
		private	SqlConnection cn;
		private SqlDataAdapter adptr = new SqlDataAdapter();

		public CDalSql(string sConn)
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
					this.cn = new SqlConnection(value);
				}
				catch (Exception e)
				{
					throw e;
				}
			}
		}

		public SqlDataReader GetCustomers()
		{
			string sSQL = "SELECT CustomerID FROM Customers";
			SqlCommand cmd = new SqlCommand(sSQL, cn);

			cmd.CommandType = CommandType.StoredProcedure;
	
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
			DataSet ds = new DataSet();
			string sSQL = "EXEC uspGetOrdersByCustID '" + sCustID + "'";

			try
			{
				if (cn.State == ConnectionState.Closed)
				{
					cn.Open();
				}

				adptr.SelectCommand = new SqlCommand(sSQL, cn);

				adptr.Fill(ds, "Orders");
			}
			catch (Exception e)
			{
				throw e;
			}

			return ds;

		}

		public DataSet GetOrders1(string sCustID)
		{
			DataSet ds = new DataSet();
			SqlCommand cmd = new SqlCommand();
			SqlParameter param;

			cmd.CommandText = "uspGetOrdersByCustID";
			cmd.CommandType = CommandType.StoredProcedure;
			param = cmd.Parameters.Add(new SqlParameter("@sCustID", SqlDbType.NChar, 5));

			param.Direction = ParameterDirection.Input;
			param.Value = sCustID;

			try
			{
				if (cn.State == ConnectionState.Closed)
				{
					cn.Open();
				}

				adptr.SelectCommand = cmd;

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
			SqlCommand cmd = new SqlCommand(sSQL, cn);
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


	}
}
