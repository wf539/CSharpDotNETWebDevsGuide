namespace ListServer {
using System;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

    [SoapType(XmlNamespace="http://schemas.microsoft.com/clr/nsassem/ListServer/ListServer", XmlTypeNamespace="http://schemas.microsoft.com/clr/nsassem/ListServer/ListServer")]
    public class CompanyLists : System.Runtime.Remoting.Services.RemotingClientProxy
    {
        // Constructor
        public CompanyLists()
        {
            base.ConfigureProxy(this.GetType(), "http://169.254.49.60:8080/CompanyLists");
        }
        // Class Methods
        [SoapMethod(SoapAction="http://schemas.microsoft.com/clr/nsassem/ListServer.CompanyLists/ListServer#getCountryList")]
        public String[] getCountryList()
        {
            return ((CompanyLists) _tp).getCountryList();
        }

    }
}
