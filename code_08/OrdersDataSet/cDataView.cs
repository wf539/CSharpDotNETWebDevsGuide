using System;
using System.Data;

namespace OrdersDataSet
{
	
	public class cDataView
	{
		public cDataView()
		{
		}

		public DataView filterCustomerByID(DataSet ds, string sCustID)
		{
			DataView dv = new DataView();
			
			dv.Table = ds.Tables["Orders"];
			dv.AllowDelete = true;
			dv.AllowEdit = true;
			dv.AllowNew = true;
			dv.RowFilter = "CustomerID = '" + sCustID + "'";
			dv.RowStateFilter = DataViewRowState.ModifiedCurrent;
			dv.Sort = "OrderDate DESC";
		
			return dv;

		}
	}
}
