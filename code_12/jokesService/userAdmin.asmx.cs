using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;

namespace jokesService {
	/// <summary>
	///   The userAdmin class provides methods to manage users and 
	///   moderators in the database.
	/// </summary>
  /// <remarks>
  ///   Author: Adrian Turtschi; aturtschi@hotmail.com; Sept 2001
  /// </remarks>
  [WebServiceAttribute(Description="The userAdmin web service " +
     "provides methods to manage users and moderators in the database",
     Namespace="urn:schemas-syngress-com-soap")]    
  public class userAdmin : System.Web.Services.WebService {
    // SOAP error handling return document structure
    /// <value>error document thrown by SOAP exception</value>
    public XmlDocument soapErrorDoc;
    /// <value>text node with user friendly error message</value>
    public XmlNode xmlFailReasonNode;

    /// <summary>
    ///   Public class constructor.
    /// </summary>
    public userAdmin() {
			InitializeComponent();
      // initialize SOAP error handling return document
      soapErrorDoc = new System.Xml.XmlDocument();
      xmlFailReasonNode = 
        soapErrorDoc.CreateNode(XmlNodeType.Element, "failReason", "");
    }

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion

		/// <summary>
		///   Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
		}

    /// <summary>
    ///   The addUser method adds a new user to the database
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of new user'>
    /// </param>
    /// <param name='password'
    ///   type='string'
    ///   desc='password of new user'>
    /// </param>
    /// <returns>nothing</returns>
    [SoapDocumentMethodAttribute(Action="addUser",
      RequestNamespace="urn:schemas-syngress-com-soap:userAdmin",
      RequestElementName="addUser",
      ResponseNamespace="urn:schemas-syngress-com-soap:userAdmin",
      ResponseElementName="addUserResponse")]
    [WebMethod(Description="The addUser method adds a new user to " +
       "the database")]
    public void addUser(string userName, string password) {
      userAdminImplement userAdminObj = new userAdminImplement();
      try {
        userAdminObj.addUser(userName, password);
        // catch jokeExceptions
      } catch (jokeException e) {
        throwFault("Fault occurred", e.failReason, userName);
      }
      // then, catch general System Exceptions
      catch (Exception e) {
        throwFault(e.Message, "F_System", userName);
      }
    }

    /// <summary>
    ///   The addModerator method adds a new moderator to the database
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of moderator'>
    /// </param>
    /// <param name='password'
    ///   type='string'
    ///   desc='password of moderator'>
    /// </param>
    /// <param name='newModerator'
    ///   type='string'
    ///   desc='user name of user who will become a moderator'>
    /// </param>
    /// <returns>nothing</returns>
    [SoapDocumentMethodAttribute(Action="addModerator",
       RequestNamespace="urn:schemas-syngress-com-soap:userAdmin",
       RequestElementName="addModerator",
       ResponseNamespace="urn:schemas-syngress-com-soap:userAdmin",
       ResponseElementName="addModeratorResponse")]
    [WebMethod(Description="The addModerator method adds a new " +
       "moderator to the database")]
    public void addModerator(
      string userName, string password, string newModerator) {
      userAdminImplement userAdminObj = new userAdminImplement();
      try {
        userAdminObj.addModerator(userName, password, newModerator);
        // catch jokeExceptions
      } catch (jokeException e) {
        throwFault("Fault occurred", e.failReason, userName);
      }
        // then, catch general System Exceptions
      catch (Exception e) {
        throwFault(e.Message, "F_System", userName);
      }
    }
    
    /// <summary>
    ///   The checkUser method checks if a user or moderator is
    ///   already defined in the database
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of user or moderator'>
    /// </param>
    /// <param name='password'
    ///   type='string'
    ///   desc='password of user or moderator'>
    /// </param>
    /// <param name='isModerator'
    ///   type='bool'
    ///   desc='check for moderator status (if false, we do 
    ///   not check)'>
    /// </param>
    /// <returns>nothing</returns>
    [SoapDocumentMethodAttribute(Action="checkUser",
       RequestNamespace="urn:schemas-syngress-com-soap:userAdmin",
       RequestElementName="checkUser",
       ResponseNamespace="urn:schemas-syngress-com-soap:userAdmin",
       ResponseElementName="checkUserResponse")]
    [WebMethod(Description="The checkUser method checks if a user " +
       "or moderator is already defined in the database")]
    public void checkUser(
      string userName, string password, bool isModerator) {
      userAdminImplement userAdminObj = new userAdminImplement();
      try {
        userAdminObj.checkUser(userName, password, isModerator);
        // catch jokeExceptions
      } catch (jokeException e) {
        throwFault("Fault occurred", e.failReason, userName);
      }
        // then, catch general System Exceptions
      catch (Exception e) {
        throwFault(e.Message, "F_System", userName);
      }
    }

    /// <summary>
    ///   The throwFault method throws a SOAP fault end ends 
    ///   execution of the web service method
    /// </summary>
    /// <param name='message'
    ///   type='string'
    ///   desc='start of text node of faultstring element in 
    ///   SOAP fault message'>
    /// </param>
    /// <param name='failReason'
    ///   type='string'
    ///   desc='text node for custom failReason element in SOAP 
    ///   fault message'>
    /// </param>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of registered user'>
    /// </param>
    /// <returns>nothing</returns>
    private void throwFault(
      string message, string failReason, string userName) {
      xmlFailReasonNode.AppendChild(soapErrorDoc.CreateTextNode(
        jokeException.getNiceErrorMessage(failReason)));
      jokeException.writeEventLogEntry(userName, failReason);
      throw new SoapException(message, SoapException.ServerFaultCode,
        Context.Request.Url.AbsoluteUri,null, 
        new System.Xml.XmlNode[]{xmlFailReasonNode});
    }
  }
}

