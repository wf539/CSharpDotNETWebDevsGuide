using System;

namespace jokesService
{
  /// <summary>
  ///   The xmlJokesReturn class is the return type of all public 
  ///   methods returning joke data.  
  /// </summary>
  /// <remarks>
  ///   Author: Adrian Turtschi; aturtschi@hotmail.com; Sept 2001
  /// </remarks>
  public class xmlJokesReturn {
    /// <value>ID of joke returned</value>
    public string jokeID;
    /// <value>the actual joke</value>
    public string joke;
    /// <value>average rating of the joke (can be empty)</value>
    public string rating;

    /// <summary>
    ///   Public class constructor.
    /// </summary>
    public xmlJokesReturn() {
    }
  }
}
