using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace rsTest
{
  [WebServiceAttribute(Namespace="urn:schemas-syngress-com-soap")]    
	public class rsTest : System.Web.Services.WebService
	{
		public rsTest() {
		}

    [SoapDocumentMethodAttribute(Action="returnRS",
       RequestNamespace="urn:schemas-syngress-com-soap:rsTest",
       RequestElementName="returnRS",
       ResponseNamespace="urn:schemas-syngress-com-soap:rsTest",
       ResponseElementName="returnRSResponse")]
    [WebMethod]
    public DataSet returnRS() {
      try {
        string sqlConnectionString = 
          "server=(local)\\NetSDK;database=Northwind;User ID=SA;Password=";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(
            "SELECT * FROM shippers", sqlConnectionString);
        DataSet shippers = new DataSet();
        sqlDataAdapter.Fill(shippers, "shippers");
        return shippers;
      }
      catch (Exception e) {
        throw e;
      }
    }
	}
}
