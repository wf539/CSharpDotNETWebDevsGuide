using System;
using System.Collections;
using System.Diagnostics;

namespace jokesService
{
  /// <summary>
  ///   Custom error handling class
  /// </summary>
  /// <remarks>
  ///   Author: Adrian Turtschi; aturtschi@hotmail.com; Sept 2001
  /// </remarks>
  public class jokeException : Exception {
    /// <value>
    ///   fail reason error code
    /// </value>
    public string failReason;
    private static Hashtable errorCodes = new Hashtable(); 
    private static bool isInit = false;

    /// <summary>
    ///   Public class constructor.
    /// </summary>
    /// <param name='failReason'
    ///   type='string'
    ///   desc='fail reason error code'>
    /// </param>
    protected internal jokeException(string failReason) {
      this.failReason = failReason;
    }

    private static void initErrorCodes() {
      errorCodes.Add("S_OK", 
        "Operation completed successfully!");
      errorCodes.Add("F_System", 
        "An unknown system error occurred!");
      errorCodes.Add("F_ratingInvalid", 
        "Joke rating must be between 1 and 5!");
      errorCodes.Add("F_jokeDoesNotExist", 
        "Joke selected does not exist in the system!");
      errorCodes.Add("F_unknownAction" , 
        "Internal error when accessing the database!");
      errorCodes.Add("F_userDoesNotExist", 
        "This is not a registered user!");
      errorCodes.Add("F_userExists", 
        "Somebody has already registered under this name!");
      errorCodes.Add("F_userInfoWrong", 
        "You are not authorized to do this action. Change " +
        "user name or password!");
      errorCodes.Add("F_noJokes", 
        "No matching jokes in the system at this moment in time!");      
      errorCodes.Add("F_10JokesMax", 
        "You can only retrieve up to 10 jokes at one time!");      
    }

    /// <summary>
    ///   The getNiceErrorMessage method converts an error code into
    ///   a user friendly error message, returned through a SOAP fault.
    /// </summary>
    /// <param name='errorCode'
    ///   type='string'
    ///   desc='error code'>
    /// </param>
    /// <returns>a friendly user error message</returns>
    protected internal static string getNiceErrorMessage(
      string errorCode) {
      if (!isInit) {
        // initialize error look up table once and for all
        initErrorCodes();
        isInit = true;
      }
      string temp = errorCodes[errorCode].ToString();
      if(temp.Length < 1) {
        // generic error, if error code unknown...
        return errorCodes["F_System"].ToString();
      } else {
        return temp;
      }      
    }

    /// <summary>
    ///   The writeEventLogEntry method writes an error log entry
    ///   into the Application event log
    /// </summary>
    /// <param name='userName'
    ///   type='string'
    ///   desc='name of registered user'>
    /// </param>
    /// <param name='failReason'
    ///   type='string'
    ///   desc='fail reason error code'>
    /// </param>
    /// <returns>nothing</returns>
    protected internal static void writeEventLogEntry(
      string userName, string failReason) {
      //Create the source, if it does not already exist.
      if(!EventLog.SourceExists("jokeService")) {
        EventLog.CreateEventSource("jokeService", "Application");
      }
      //Create an EventLog instance and assign its source.
      EventLog eventLog = new EventLog();
      eventLog.Source = "jokeService";
  
      //Write an informational entry to the event log.    
      eventLog.WriteEntry(userName + ": " + failReason);
    }
  }
}
