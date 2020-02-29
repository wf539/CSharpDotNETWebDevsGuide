using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Runtime.InteropServices;

namespace _sessionTest {
  /// <summary>
  /// Summary description for sessionTest.
  /// </summary>
  [WebServiceAttribute(
     Namespace="urn:schemas-syngress-com-soap")]    
  public class sessionTest : System.Web.Services.WebService {
    public sessionTest() {
      //CODEGEN: This call is required by the ASP.NET Web Services Designer
      InitializeComponent();
    }

    #region Component Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
    }
    #endregion

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing ) {
    }

    [SoapDocumentMethodAttribute(Action="sessionTest__URL",
       RequestNamespace="urn:schemas-syngress-com-soap:sessionTest",
       RequestElementName="sessionTest__URL",
       ResponseNamespace="urn:schemas-syngress-com-soap:sessionTest",
       ResponseElementName="sessionTest__URLResponse")]
    [WebMethod(EnableSession=true)]
    public string sessionTest__URL() {
      if (Session["HitCounter"] == null) {
        Session["HitCounter"] = 1;
      } else {
        Session["HitCounter"] = ((int) Session["HitCounter"]) + 1;
      }
      return (Session["HitCounter"].ToString());
    }

    [SoapDocumentMethodAttribute(Action="sessionTest__httpHeader",
       RequestNamespace="urn:schemas-syngress-com-soap:sessionTest",
       RequestElementName="sessionTest__httpHeader",
       ResponseNamespace="urn:schemas-syngress-com-soap:sessionTest",
       ResponseElementName="sessionTest__httpHeaderResponse")]
    [WebMethod(EnableSession=true)]
    public string sessionTest__httpHeader() {
      if (Session["HitCounter"] == null) {
        Session["HitCounter"] = 1;
      } else {
        Session["HitCounter"] = ((int) Session["HitCounter"]) + 1;
      }
      return (Session["HitCounter"].ToString());
    }

    public class soapHeader : SoapHeader {
      public string webState;
    }

    public soapHeader mySoapHeader;
    public static Hashtable userSessions = new Hashtable();        

    [SoapDocumentMethodAttribute(Action="sessionTest__soapHeader",
       RequestNamespace="urn:schemas-syngress-com-soap:sessionTest",
       RequestElementName="sessionTest__soapHeader",
       ResponseNamespace="urn:schemas-syngress-com-soap:sessionTest",
       ResponseElementName="sessionTest__soapHeaderResponse")]
    [SoapHeader("mySoapHeader",Direction=SoapHeaderDirection.InOut,
       Required=true)]
    [WebMethod]
    public string sessionTest__soapHeader() {
      // declare user session hit counter
      int hitCounter;
      // declare session identifier
      string sessionID;

      if ((mySoapHeader.webState == null) || 
        (mySoapHeader.webState.Trim().Length < 1)){
        sessionID = System.Guid.NewGuid().ToString().ToUpper();
        hitCounter = 1;
        // create a new user session, and set hit counter to one
        userSessions.Add(sessionID, hitCounter);
        // return session identifier to user
        mySoapHeader.webState = sessionID;
      } else {
        // valid user session?
        sessionID = mySoapHeader.webState.ToString().Trim();
        if(userSessions[sessionID] != null) {
          // get session hit counter
          hitCounter = (int)userSessions[sessionID];
          // save away incremented session hit counter
          userSessions[sessionID] = ++hitCounter;
        } else {
          // session identifier passed was invalid
          // throw error
          throw new Exception("Invalid session identifier passed!");
        }
      }
      // return session counter
      return hitCounter.ToString();
    }

  }
}
