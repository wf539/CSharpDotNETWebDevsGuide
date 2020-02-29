
using System;

public class TCPHelloWorldClient
{
  public static void Main ()
  {
    Console.WriteLine ( "initializing client..." );
    
    TCPRemoteCommandProcessor proc = new TCPRemoteCommandProcessor ( "127.0.0.1", 8080, false );
    
    string result = null;
    
    Console.WriteLine ( "requesting..." );
    proc.Execute ( "GET", ref result );
    Console.WriteLine ( "result: " + result );
    
    Console.WriteLine ( "closing connection..." );
    proc.Execute ( "EXIT", ref result );
    
    proc.Close ();
    
    Console.Write ( "press return to exit" );
    Console.ReadLine ();
  }
}