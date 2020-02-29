
using System;
using System.Net;
using System.Net.Sockets;

public class UDPHelloWorldServer
{
  public static void Main ()
  {
    Console.WriteLine ( "initializing server" );
    
    UdpClient server = new UdpClient ( 8080 );
    
    // an endpoint is not needed the data will be sent 
    // to the port where the server is bound to
    IPEndPoint dummy = null;
    
    bool loop = true;
    while ( loop )
    {
      Console.WriteLine ( "waiting for request..." );
    
      byte[] datagram = server.Receive ( ref dummy );
    
      // split request string into parts, part1=client IP address or 
      // DNS name, part2=client port, part3=command
      string dg = 
        new System.Text.ASCIIEncoding ().GetString ( datagram );
      string[] cmd = dg.Split ( new Char[] {':'} );
      string remoteClientHost = cmd[0];
      int remoteClientPort = Int32.Parse ( cmd[1] );
      string command = cmd[2];
      string result = null;
        
      Console.WriteLine ( "executing remote command:" + command );
      
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
        Console.WriteLine ( "sending result to (" + remoteClientHost + ":" + remoteClientPort + "): " + result );
    
        // convert data string to byte array
        Byte[] d = System.Text.Encoding.ASCII.GetBytes ( result.ToCharArray () );
        
        // send result to the client
        server.Send ( d, d.Length, remoteClientHost, remoteClientPort );
      }
    }
    
    Console.WriteLine ( "clearing up server..." );
    server.Close ();
    
    Console.Write ( "press return to exit" );
    Console.ReadLine ();
  }
}