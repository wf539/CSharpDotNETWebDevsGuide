using System;
using System.Data.Odbc;

namespace OrdersDataSet
{
	public class CDalOdbc
	{
		string strConStr;
		private	OdbcConnection cn;
		private OdbcDataAdapter adptr = new OdbcDataAdapter();

		public CDalOdbc(string sConn)
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
					this.cn = new OdbcConnection(value);
				}
				catch (Exception e)
				{
					throw e;
				}
			}
		}

	}
}

