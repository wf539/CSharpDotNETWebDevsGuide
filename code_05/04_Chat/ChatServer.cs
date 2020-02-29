
// problem: unconnected users can listen to the chat

using System;
using System.Collections;
using System.Net;

public class ChatServer : CommandProcessor
{
  public static void Main ()
  {
    ChatServer server = new ChatServer ();
  }
  
  // listening port for incoming connection requests
  private const int TCP_PORT = 8080;
  // local port for the UDP peer for sending new messages
  private const int UDP_LOCAL_PORT = 8081;
  // multicast group IP address
  private const string UDP_GROUP_IP = "224.0.0.1";
  // multicast group port
  private const int UDP_GROUP_PORT = 8082;
  // TCP server for incoming connection requests    
  private TCPServer tcpServer = null;
  // UDP peer for sending new messages
  private UDPPeer udpPeer = null;
  // list of currently connected users
  private ArrayList users = null;
  
  public ChatServer ()
  {
    this.users = new ArrayList ();
    
    this.tcpServer = new TCPServer ( TCP_PORT, new ExecuteCommand ( Execute ) );
    this.udpPeer = new UDPPeer ( UDP_LOCAL_PORT, UDP_GROUP_IP, UDP_GROUP_PORT );
  }
  
  public void Close ()
  {
    this.udpPeer.Close ();
    this.tcpServer.Close ();
  }
  
  public bool Execute ( string command, ref string result )
  {
    bool ret = true;
    
    Console.WriteLine ( "executing command: " + command );

    // split the command into parts    
    string[] cmd = command.Split ( new Char[] {':'} );
    string user = cmd[0];
    string operation = cmd[1];
    string message = null;
    
    // if the command string contains more than two ':' concatenate the 
    // splitted rest, this may happen if the message contains ':'
    if ( cmd.Length > 2 )
    {
      message = cmd[2];
      for ( int i = 3; i < cmd.Length; i++ )
        message += cmd[i];
    }

    // execute the command      
    switch ( operation )
    {
      // user enters the chat room
      case "HELLO":
        if ( !this.users.Contains ( user ) )
        {
          result = "HELLO";
          
          // add user to currently connected users list
          this.users.Add ( user );

          // send message to all users          
          this.udpPeer.Send ( user + " has joined the chat room" );
        }
        break;
      
      // user sent message to the chat room
      case "SAY":
        // execute only if user is currently connected
        if ( this.users.Contains ( user ) && ( message != null ) )
        {
          result = "OK";
          
          // send message to all users
          this.udpPeer.Send ( user + ": " + message );
        }
        break;
        
      case "BYE":
        // execute only if user is currently connected
        if ( this.users.Contains ( user ) )
        {
          result = "BYE";

          // remove user from currently connected users list          
          this.users.Remove ( user );
          
          // send message to all users
          this.udpPeer.Send ( user + " has left the chat room" );
        }
        break;

      // unknown command, return an error      
      default:
        result = "ERROR";
        break;
    }
    
    return ret;
  }
}