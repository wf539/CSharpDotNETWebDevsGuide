using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace jokesService
{
	/// <summary>
	///   The jokes web service provides methods to manage and retrieve 
	///   jokes from the database.
	/// </summary>
  /// <remarks>
  ///   Author: Adrian Turtschi; aturtschi@hotmail.com; Sept 2001
  /// </remarks>
  [WebServiceAttribute(Description="The jokes web service " + 
     "provides methods to manage and retrieve jokes from the database",
     Namespace="urn:schemas-syngress-com-soap")]    
  public class jokes : System.Web.Services.WebService {
    // SOAP error handling return document structure
    /// <value>error document throwm by SOAP exception</value>
    public XmlDocument soapErrorDoc;
    /// <value>text node wit user friendly error message</value>
    public XmlNode xmlFailReasonNode;

    /// <summary>
    ///   Public class constructor.
    /// </summary>
    public jokes() {
      InitializeComponent();
      // initialize SOAP error handling return document
      soapErrorDoc = new System.Xml.XmlDocument();
      xmlFailReasonNode = soapErrorDoc.CreateNode(
        XmlNodeType.Element, "failReason", "");
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
    ///   The addJoke method adds a new joke to the database
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of registered user'>
    /// </param>
    /// <param name='password'
    ///   type='string'
    ///   desc='password of registered user'>
    /// </param>
    /// <param name='joke'
    ///   type='string'
    ///   desc='the joke'>
    /// </param>
    /// <returns>nothing</returns>
    [SoapDocumentMethodAttribute(Action="addJoke",
       RequestNamespace="urn:schemas-syngress-com-soap:jokes",
       RequestElementName="addJoke",
       ResponseNamespace="urn:schemas-syngress-com-soap:jokes",
       ResponseElementName="addJokeResponse")]
    [WebMethod(Description="The addJoke method adds a new joke " +
       "to the database")]
    public void addJoke(
      string userName, string password, string joke) {
      jokesImplement jokesObj = new jokesImplement();
      try {
        jokesObj.addJoke(userName, password, joke);
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
    ///   The getJokes method gets howMany (moderated) jokes 
    ///   from the database
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of registered user'>
    /// </param>
    /// <param name='password'
    ///   type='string'
    ///   desc='password of registered user'>
    /// </param>
    /// <param name='howMany'
    ///   type='int'
    ///   desc='how many jokes we would like'>
    /// </param>
    /// <returns>an XML representation (xmlJokesReturn) 
    ///   of howMany jokes</returns>
    [SoapDocumentMethodAttribute(Action="getJokes",
       RequestNamespace="urn:schemas-syngress-com-soap:jokes",
       RequestElementName="getJokes",
       ResponseNamespace="urn:schemas-syngress-com-soap:jokes",
       ResponseElementName="getJokesResponse")]
    [WebMethod(Description="The getJokes method gets <howMany> " +
       "(moderated) jokes from the database")]
    [return: XmlElementAttribute("jokeData", IsNullable=false)]
    public xmlJokesReturn[] getJokes(
      string userName, string password, int howMany) {
      jokesImplement jokesObj = new jokesImplement();
      try {
        xmlJokesReturn[] myJokes = 
          jokesObj.getJokes(userName, password, howMany);
        return myJokes;
      // catch jokeExceptions
      } catch (jokeException e) {
        throwFault("Fault occurred", e.failReason, userName);
        return null;  // code never reached, but needed by compiler
      }
        // then, catch general System Exceptions
      catch (Exception e) {
        throwFault(e.Message, "F_System", userName);
        return null;  // code never reached, but needed by compiler
      }
    }
  
    /// <summary>
    ///   The addRating method lets a user add a rating 
    ///   for a joke to the database
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of registered user'>
    /// </param>
    /// <param name='password'
    ///   type='string'
    ///   desc='password of registered user'>
    /// </param>
    /// <param name='rating'
    ///   type='int'
    ///   desc='rating of the joke (1-5)'>
    /// </param>
    /// <param name='jokeID'
    ///   type='int'
    ///   desc='ID of the joke'>
    /// </param>
    /// <returns>nothing</returns>
    [SoapDocumentMethodAttribute(Action="addRating",
       RequestNamespace="urn:schemas-syngress-com-soap:jokes",
       RequestElementName="addRating",
       ResponseNamespace="urn:schemas-syngress-com-soap:jokes",
       ResponseElementName="addRatingResponse")]
    [WebMethod(Description="The addRating method lets a user add a " +
       "rating for a joke to the database")]
    public void addRating(
      string userName, string password, int rating, int jokeID) {
      jokesImplement jokesObj = new jokesImplement();
      try {
        if((rating < 1) && (rating > 5)) {
          throwFault("Fault occurred", "F_ratingInvalid", userName);
        } else {
          jokesObj.addRating(userName, password, rating, jokeID);
        }
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
    ///   The getUnmoderated method lets a moderator retrieve 
    ///   howMany unmoderated jokes from the database
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of moderator'>
    /// </param>
    /// <param name='password'
    ///   type='string'
    ///   desc='password of moderator'>
    /// </param>
    /// <param name='howMany'
    ///   type='int'
    ///   desc='how many jokes we would like'>
    /// </param>
    /// <returns>an XML representation (xmlJokesReturn) 
    ///   of howMany jokes</returns>
    [SoapDocumentMethodAttribute(Action="getUnmoderated",
       RequestNamespace="urn:schemas-syngress-com-soap:jokes",
       RequestElementName="getUnmoderated",
       ResponseNamespace="urn:schemas-syngress-com-soap:jokes",
       ResponseElementName="getUnmoderatedResponse")]
    [WebMethod(Description="The getUnmoderated method lets a " +
       "moderator retrieve <howMany> unmoderated jokes from " +
       "the database")]
    [return: XmlElementAttribute("jokeData", IsNullable=false)]
    public xmlJokesReturn[] getUnmoderated(
      string userName, string password, int howMany) {
      jokesImplement jokesObj = new jokesImplement();
      try {
        xmlJokesReturn[] myJokes = 
          jokesObj.getUnmoderated(userName, password, howMany);
        return myJokes;
        // catch jokeExceptions
      } catch (jokeException e) {
        throwFault("Fault occurred", e.failReason, userName);
        return null;  // code never reached, but needed by compiler
      }
        // then, catch general System Exceptions
      catch (Exception e) {
        throwFault(e.Message, "F_System", userName);
        return null;  // code never reached, but needed by compiler
      }
    }
  
    /// <summary>
    ///   The addModerated method lets a moderator set a joke to be 
    /// 'moderated', i.e. accessible to regular users
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of moderator'>
    /// </param>
    /// <param name='password'
    ///   type='string'
    ///   desc='password of moderator'>
    /// </param>
    /// <param name='jokeID'
    ///   type='int'
    ///   desc='ID of joke'>
    /// </param>
    /// <returns>nothing</returns>
    [SoapDocumentMethodAttribute(Action="addModerated",
       RequestNamespace="urn:schemas-syngress-com-soap:jokes",
       RequestElementName="addModerated",
       ResponseNamespace="urn:schemas-syngress-com-soap:jokes",
       ResponseElementName="addModeratedResponse")]
    [WebMethod(Description="The addModerated method lets a " +
       "moderator set a joke to be 'moderated', i.e. accessible " +
       "to regular users")]
    public void addModerated(
      string userName, string password, int jokeID) {
      jokesImplement jokesObj = new jokesImplement();
      try {
        jokesObj.addModerated(userName, password, jokeID);
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
    ///   The deleteUnmoderated method lets a moderator delete a 
    ///   (unmoderated) joke from the database
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of moderator'>
    /// </param>
    /// <param name='password'
    ///   type='string'
    ///   desc='password of moderator'>
    /// </param>
    /// <param name='jokeID'
    ///   type='int'
    ///   desc='ID of joke'>
    /// </param>
    /// <returns>nothing</returns>
    [SoapDocumentMethodAttribute(Action="deleteUnmoderated",
       RequestNamespace="urn:schemas-syngress-com-soap:jokes",
       RequestElementName="deleteUnmoderated",
       ResponseNamespace="urn:schemas-syngress-com-soap:jokes",
       ResponseElementName="deleteUnmoderatedResponse")]
    [WebMethod(Description="The deleteUnmoderated method lets a " +
       "moderator delete a (unmoderated) joke from the database")]
    public void deleteUnmoderated(
      string userName, string password, int jokeID) {
      jokesImplement jokesObj = new jokesImplement();
      try {
        jokesObj.deleteUnmoderated(userName, password, jokeID);
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
