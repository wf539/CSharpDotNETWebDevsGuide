using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace soapExamples {
  /// <summary>
  ///   Sample Web Service illustrating common concepts.
  /// </summary>
  /// <remarks>
  ///   Author: Adrian Turtschi; aturtschi@hotmail.com; Sept 2001
  /// </remarks>
  [WebServiceAttribute(
     Namespace="urn:schemas-syngress-com-soap")]
  public class simpleService : System.Web.Services.WebService {
    public simpleService() {
    }

    [WebMethod]
    public string echo(string input) {
      return input;
    }

    [SoapDocumentMethodAttribute(Action="arithmeticMean",
       RequestNamespace="urn:schemas-syngress-com-soap",
       RequestElementName="arithmeticMean",
       ResponseNamespace="urn:schemas-syngress-com-soap",
       ResponseElementName="arithmeticMeanResponse")]
    [WebMethod(Description="Computes the " +
       "arithmetic means of an array of input parameters")]
    public float arithmeticMean (int[] arrayInput) {
      if ((arrayInput == null) || (arrayInput.Length < 1)) {
        throw new Exception("No input data...");
      } else {
        int sum = 0;
        for(int i=0; i<arrayInput.Length; i++) {
          sum += arrayInput[i];
        }
        return (float)((float)sum / (float)arrayInput.Length);
      }
    }

    [SoapDocumentMethodAttribute(Action="getCounterInfo",
       RequestNamespace="urn:schemas-syngress-com-soap",
       RequestElementName="getCounterInfo",
       ResponseNamespace="urn:schemas-syngress-com-soap",
       ResponseElementName="getCounterInfoResponse")]
    [WebMethod(Description="Returns performance " +
       "counter information")]
    public System.Diagnostics.PerformanceCounter getCounterInfo(
      string categoryName, string counterName, string instanceName) {

      try {
        System.Diagnostics.PerformanceCounter perfCounter 
          = new System.Diagnostics.PerformanceCounter();

        perfCounter.CategoryName = categoryName;
        perfCounter.CounterName = counterName;
        perfCounter.InstanceName = instanceName;

        if (perfCounter.CounterType < 0) {
          // counter is not a valid counter
          throw new Exception("Counter Data Invalid!");
        }

        return perfCounter ;
      } catch(Exception ex) {
        throw ex;
      }
    }

    [SoapDocumentMethodAttribute(Action="xmlTester",
       RequestNamespace="urn:schemas-syngress-com-soap",
       ResponseNamespace="urn:schemas-syngress-com-soap",
       ParameterStyle = SoapParameterStyle.Bare)]
    [WebMethod(Description="XML echo service that " +
       "adds a dateProcessed attribute.")]
    [return: XmlAnyElement]
    public XmlElement xmlTester([XmlAnyElement]XmlElement inputXML){
      try {
        inputXML.SetAttribute("dateProcessed", 
          System.DateTime.Now.ToUniversalTime().ToString("r"));
        return inputXML;
      } catch(Exception ex) {
        throw ex;
      }
    }
  }
}
