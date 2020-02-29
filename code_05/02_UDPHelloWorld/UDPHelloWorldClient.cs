
using System;

public class UDPHelloWorldClient
{
  public static void Main ()
  {
    Console.WriteLine ( "initializing client..." );
    
    UDPRemoteCommandProcessor proc = new UDPRemoteCommandProcessor ( 8081, "127.0.0.1", 8080  );
    
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