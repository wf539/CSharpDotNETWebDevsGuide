
using System;
using System.IO;
using System.Net.Sockets;

public class TCPHelloWorldServer
{
  public static void Main ()
  {
    Console.WriteLine ( "initializing server..." );

    TcpListener listener = new TcpListener ( 8080 );
    listener.Start ();
    
    Console.WriteLine ( "server initialized, waiting for incoming connections..." );

    Socket s = listener.AcceptSocket ();

    Console.WriteLine ( "client connected, waiting for requests..." );
    
    // create a NetworkStream for easier access
    NetworkStream ns = new NetworkStream ( s );
      
    // use a stream reader because of ReadLine()-method
    StreamReader r = new StreamReader ( ns );
      
    bool loop = true;
    while ( loop )
    {
      // read a line until CRLF
      string command = r.ReadLine ();
      string result = null;
        
      Console.WriteLine ( "executing remote command: " + command );
        
      switch ( command )
      {
        case "GET":
          result = "Hello World !";
          break;
          
        // finish communication
        case "EXIT":
          result = "BYE";
          loop = false;
          break;
        
        // invalid command
        default:
          result = "ERROR";
          break;
      }
        
      if ( result != null )
      {
        Console.WriteLine ( "sending result: " + result );
      
        // add a CRLF to the result
        result += "\r\n";
      
        // convert data string to byte array
        Byte[] res = System.Text.Encoding.ASCII.GetBytes ( result.ToCharArray () );
        
        // send result to the client
        s.Send ( res, res.Length, 0 );
      }
    }

    Console.WriteLine ( "clearing up server..." );
    s.Close ();
    
    Console.Write ( "press return to exit" );
    Console.ReadLine ();
  }
}