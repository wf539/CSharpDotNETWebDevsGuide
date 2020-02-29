
using System;
using System.Collections;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Windows.Forms;


public delegate void Notify ( string text );
public delegate bool ExecuteCommand ( string command, ref string result );

public interface CommandProcessor
{
  // execute a command and return the result
  // if the return value is false the command processing loop should stop
  bool Execute ( string command, ref string result );
}

public class UDPMulticastClient
{
  // notification delegate
  private Notify notify = null;
  // communication interface
  private UDPPeer peer = null;
  // receiving thread
  private Thread clientThread = null;
  
  public UDPMulticastClient ( string groupIP, int groupPort, Notify notify )
  {
    // add parameter validation here
    
    Console.WriteLine ( "initializing UDP multicast client, group=" + groupIP + ", port=" + groupPort + "..." );
    
    this.notify = notify;
    
    // create communication components
    this.peer = new UDPPeer ( groupPort, groupIP, groupPort );
    
    // start listener thread
    this.clientThread = new Thread ( new ThreadStart ( Run ) );
    this.clientThread.Start ();
    
    Console.WriteLine ( "UDP multicast client initialized" );
  }
  
  public void Close ()
  {
    this.clientThread.Abort ();
    this.clientThread.Join ();
    
    this.peer.Close ();
  }
  
  public void Run ()
  {
    while ( true )
      this.notify ( this.peer.Receive () );
  }
}

public class UDPPeer
{
  // udp peer
  private UdpClient peer = null;
  // multicast group IP address
  private IPAddress groupAddress = null;
  // multicast group endpoint (IP address and port)
  private IPEndPoint group = null;
  
  public UDPPeer ( int localPort )
  {
    // add parameter checking here
    
    Console.WriteLine ( "initializing UDP peer, port=" + localPort + "..." );
    
    this.peer = new UdpClient ( localPort );
    
    Console.WriteLine ( "UDP peer initialized" );
  }
  
  public UDPPeer ( int localPort, string groupIP, int groupPort ) : this ( localPort )
  {
    // add parameter checking here
    
    Console.WriteLine ( "adding UDP peer to multicast group, IP=" + groupIP + ", port=" + groupPort + "..." );

    this.groupAddress = IPAddress.Parse ( groupIP );
    this.group = new IPEndPoint ( this.groupAddress, groupPort );
    
    this.peer.JoinMulticastGroup ( this.groupAddress );
    
    Console.WriteLine ( "UDP peer added to group" );
  }
  
  public void Close ()
  {
    if ( this.groupAddress != null )
      this.peer.DropMulticastGroup ( this.groupAddress );
    
    this.peer.Close ();
  }
  
  public String Receive ()
  {
    IPEndPoint dummy = null;

    // receive datagram
    byte[] data = this.peer.Receive ( ref dummy );
    
    return new System.Text.ASCIIEncoding ().GetString ( data );
  }
  
  public void Send ( string message )
  {
    // add parameter checking here
    
    Console.WriteLine ( "sending " + message + "..." );
    
    // convert news string to a byte array
    Byte[] d = System.Text.Encoding.ASCII.GetBytes ( message.ToCharArray () );
            
    this.peer.Send ( d, d.Length, this.group );
    
    Console.WriteLine ( "message sent" );
  }
}

public class UDPRemoteCommandProcessor : CommandProcessor
{
  // the local port where the processor is bound to
  private int localPort = -1;
  // the remote host
  private string remoteHost = null;
  // the remote port
  private int remotePort = -1;
  // communication interface
  private UdpClient client = null;
  
  public UDPRemoteCommandProcessor ( int localPort, string remoteHost, int remotePort )
  {
    // add here parameter checking
    
    this.localPort = localPort;
    this.remoteHost = remoteHost;
    this.remotePort = remotePort;
    
    this.client = new UdpClient ( localPort );
  }
  
  public void Close ()
  {
    this.client.Close ();
  }
  
  public bool Execute ( string command, ref string result )
  {
    // add here parameter checking
    
    bool ret = true;
    
    Console.WriteLine ( "executing command: " + command );
    
    // build the request string
    string request = "127.0.0.1:" + this.localPort.ToString () + ":" + command;
    
    Byte[] req = System.Text.Encoding.ASCII.GetBytes ( request.ToCharArray () );
    
    client.Send ( req, req.Length, this.remoteHost, this.remotePort );
    
    // we don't need an endpoint
    IPEndPoint dummy = null;
          
    // receive datagram from server
    byte[] res = client.Receive ( ref dummy );
    result = System.Text.Encoding.ASCII.GetString ( res );
    
    ret = !result.Equals ( "BYE" );
    
    return ret;
  }
}

public class TCPRemoteCommandProcessor : CommandProcessor
{
  // remote host
  private string host = null;
  // remote port
  private int port = -1;
  // connection mode
  private bool releaseConnection = false;
  // communication interface
  private TcpClient client = null;
  // outgoing data stream
  private NetworkStream outStream = null;
  // ingoing data stream
  private StreamReader inStream = null;
  
  public TCPRemoteCommandProcessor ( string host, int port, bool releaseConnection )
  {
    // add here parameter checking
    
    this.host = host;
    this.port = port;
    this.releaseConnection = releaseConnection;
    
    if ( !this.releaseConnection )
    {
      Console.WriteLine ( "connecting to " + this.host + ":" + this.port + "..." );
      
      this.client = new TcpClient ( this.host, this.port );
      this.outStream = this.client.GetStream ();
      this.inStream = new StreamReader ( this.outStream );
      
      Console.WriteLine ( "connected to " + this.host + ":" + this.port );
    }
  }
  
  public void Close ()
  {
    if ( this.client != null )
    {
      this.client.Close ();
      
      Console.WriteLine ( "connection closed: " + this.host + ":" + this.port );
    }
  }
  
  public bool Execute ( string command, ref string result )
  {
    // add here parameter checking
    
    bool ret = true;

    if ( this.releaseConnection )
    {
      Console.WriteLine ( "connecting to " + this.host + ":" + this.port + "..." );
      
      // open connection to the server
      this.client = new TcpClient ( this.host, this.port );
      this.outStream = this.client.GetStream ();
      this.inStream = new StreamReader ( this.outStream );
      
      Console.WriteLine ( "connected to " + this.host + ":" + this.port );
    }
    
    // add a CRLF to command to indicate end
    command += "\r\n";
    
    // convert command string to byte array
    Byte[] cmd = System.Text.Encoding.ASCII.GetBytes ( command.ToCharArray () );
    
    // send request
    this.outStream.Write ( cmd, 0, cmd.Length );
    
    // get response
    result = this.inStream.ReadLine ();
    
    if ( this.releaseConnection )
    {
      // close connection
      this.client.Close ();
      
      Console.WriteLine ( "connection closed: " + host + ":" + port );
    }
    
    ret = !result.Equals ( "BYE" );
    
    return ret;
  }
}


public class TCPServerSession
{
  // command processor
  private ExecuteCommand executeCommand = null;
  // communication interface
  private Socket socket = null;
  // open flag
  private bool open = false;
  
  public TCPServerSession ( Socket socket, ExecuteCommand executeCommand )
  {
    this.socket = socket;
    this.executeCommand = executeCommand;
    this.open = true;
  }
  
  public void Close ()
  {
    if ( this.open )
    {
      Console.WriteLine ( "TCP session is closing..." );
      
      this.open = false;
      
      this.socket.Close ();
      
      Console.WriteLine ( "TCP session closed" );
    }
  }
  
  public void Run ()
  {
    NetworkStream ns = new NetworkStream ( this.socket );
    StreamReader reader = new StreamReader ( ns );
    
    bool loop = this.open;
    while ( loop )
    {
      if ( ns.DataAvailable )
      {
        // read command from client
        string cmd = reader.ReadLine ();
        string result = "";
      
        // execute command
        loop = this.executeCommand ( cmd, ref result );
      
        Console.WriteLine ( "sending result, result=" + result );
      
        result += "\r\n";

        Byte[] res = System.Text.Encoding.ASCII.GetBytes ( result.ToCharArray () );
        
        // return result to client
        this.socket.Send ( res );
      
        Console.WriteLine ( "result sent" );
      }
      
      loop = loop && this.open;
    }
    
    Close ();
  }
}

public class TCPServer
{
  // sessions list
  private ArrayList sessions = null;
  // session threads list
  private ArrayList sessionThreads = null;
  // command processor
  private ExecuteCommand executeCommand = null;
  // connection listener
  private TcpListener listener = null;
  // server thread
  private Thread server = null;
  // open flag
  private bool open = false;
  
  public TCPServer ( int port, ExecuteCommand executeCommand )
  {
    this.sessions = new ArrayList ();
    this.sessionThreads = new ArrayList ();
    
    this.executeCommand = executeCommand;
    
    Console.WriteLine ( "initializing TCP server..." );
    
    Console.WriteLine ( "creating listener..." );
    this.listener = new TcpListener ( port );
      
    Console.WriteLine ( "starting listener..." );
    this.listener.Start ();
      
    this.open = true;
    
    this.server = new Thread ( new ThreadStart ( Run ) );
    this.server.Start ();

    Console.WriteLine ( "TCP server initialization complete, port=" + port );
  }
  
  public void Close ()
  {
    Console.WriteLine ( "TCP server is closing..." );
    
    if ( this.open )
    {
      this.open = false;

      // stop listner      
      this.listener.Stop ();
      
      // stop server thread
      this.server.Abort ();
      this.server.Join ();
    
      // stop all session threads and close the sessions
      while ( this.sessions.Count > 0 )
      {
        // stop session thread
        Thread th = (Thread)this.sessionThreads[0];
        th.Abort ();
        th.Join ();
        this.sessionThreads.Remove ( th );
        
        // close session
        TCPServerSession s = (TCPServerSession)this.sessions[0];
        s.Close ();
        this.sessions.Remove ( s );
      }
    }

    Console.WriteLine ( "TCP server closed" );
  }
  
  public void Run ()
  {
    while ( this.open )
    {
      Console.WriteLine ( "listening for incomming connections..." );
      
      // wait for incoming client connection requests
      Socket s = this.listener.AcceptSocket ();
      if ( s.Connected )
      {
        Console.WriteLine ( "client connected, starting client session..." );
        
        // create a client session
        TCPServerSession session = new TCPServerSession ( s, this.executeCommand );
        // add it to the session list
        this.sessions.Add ( session );
        
        // create a thread for the session
        Thread th = new Thread ( new ThreadStart ( session.Run ) );
        // start it
        th.Start ();
        // add it to the session thread list
        this.sessionThreads.Add ( th );
      }
    }
  }
}


public class FileSharingPeer
{
  // listener for incoming connections
  private TcpListener listener = null;
  // listening server thread
  private Thread server = null;
  
  public FileSharingPeer ( int localPort )
  {
    Console.WriteLine ( "initializing file sharing peer, local port=" + localPort );
    
    // initialize proxy listener
    this.listener = new TcpListener ( localPort );
    this.listener.Start ();
    
    // start listening thread for incoming connection requests
    this.server = new Thread ( new ThreadStart ( Run ) );
    this.server.Start ();
    
    Console.WriteLine ( "file sharing peer initialized" );
  }
  
  public void Close ()
  {
    // stop proxy listener
    this.listener.Stop ();
    
    // stop server
    this.server.Abort ();
    this.server.Join ();
  }
  
  public void Run ()
  {
    while ( true )
    {
      Socket s = this.listener.AcceptSocket ();
      
      Console.WriteLine ( "client connected" );
      
      RemoteFileStreamServer srv = new RemoteFileStreamServer ( s );
      Thread th = new Thread ( new ThreadStart ( srv.Run ) );
      th.Start ();
    }
  }
  
  public void Download ( string remoteHost, int remotePort, string file )
  {
    Console.WriteLine ( "downloading file, host=" + remoteHost + ", port=" + remotePort + ", file=" + file + "..." );
    
    Stream sin = new RemoteFileStreamProxy ( remoteHost, remotePort, file, true );
    Stream sout = new FileStream ( ".\\destination\\" + file, FileMode.Create );
    
    Copy ( sin, sout );
    
    Console.WriteLine ( "file downloaded, host=" + remoteHost + ", port=" + remotePort + ", file=" + file  );
  }
  
  public void Upload ( string remoteHost, int remotePort, string file )
  {
    Console.WriteLine ( "uploading file, host=" + remoteHost + ", port=" + remotePort + ", file=" + file + "..." );
    
    Stream sin = new FileStream ( ".\\upload\\" + file, FileMode.Open );
    Stream sout = new RemoteFileStreamProxy ( remoteHost, remotePort, file, false );
    
    Copy ( sin, sout );
    
    Console.WriteLine ( "file uploaded, host=" + remoteHost + ", port=" + remotePort + ", file=" + file  );
  }
  
  
  // internal methods
  
  protected void Copy ( Stream sin, Stream sout )
  {
    byte[] buf = new byte[4096];
    long l = 0;
    while ( l < sin.Length )
    {
      int n = sin.Read ( buf, 0, 4096 );
      sout.Write ( buf, 0, n );
      
      l += n;
    }
    
    sout.Close ();
    sin.Close ();
  }
}

public class RemoteFileStreamProxy : Stream
{
  // attributes
  
  private bool open = false;
  private bool read = false;
  private long length = -1;
  private NetworkStream remoteFile = null;
  
  
  // properties
  
  public override bool CanRead
  {
    get
    {
      return this.read;
    }
  }
  
  public override bool CanSeek
  {
    get
    {
      return false;
    }
  }
  
  public override bool CanWrite
  {
    get
    {
      return !this.read;
    }
  }
  
  public override long Length
  {
    get
    {
      return this.length;
    }
  }
  
  public override long Position
  {
    get
    {
      throw new NotSupportedException ( "stream cannot seek" );
    }
    set
    {
      throw new NotSupportedException ( "stream cannot seek" );
    }
  }
  
  
  // constructors
  
  public RemoteFileStreamProxy ( string host, int port, string file, bool read )
  {
    this.read = read;
    
    this.remoteFile = new TcpClient ( host, port ).GetStream ();
    this.open = true;
    
    Send ( "OPEN:" + file + ":" + read );

    if ( read )
    {
      this.length = int.Parse ( new StreamReader ( this.remoteFile ).ReadLine () );
    }
  }
  
  
  // methods
  
  public override void Close ()
  {
    this.open = false;

    Send ( "CLOSE" );

    this.remoteFile.Close ();
  }
  
  public override void Flush ()
  {
    if ( !this.open )
      throw new IOException ( "stream is not open" );
  }
  
  public override long Seek ( long offset, SeekOrigin origin )
  {
    throw new NotSupportedException ( "stream cannot seek" );
  }
  
  public override void SetLength ( long value )
  {
    throw new NotSupportedException ( "stream cannot seek" );
  }
  
  public override int Read ( byte[] buffer, int offset, int count )
  {
    // to do: implement here exceptions as described in .NET reference
    
    if ( !CanRead )
      throw new NotSupportedException ( "stream cannot read" );
  
    Send ( "READ:" + count ); 
    
    return this.remoteFile.Read ( buffer, offset, count );
  }
  
  public override void Write ( byte[] buffer, int offset, int count )
  {
    // to do: implement here exceptions as described in .NET reference
    
    if ( !CanWrite )
      throw new NotSupportedException ( "stream cannot write" );
    
    Send ( "WRITE:" + count );
    
    this.remoteFile.Write ( buffer, offset, count );
  }
  
  
  // internal methods
  
  protected void Send ( string command )
  {
    command += "\r\n";
    
    // convert data string to byte array
    Byte[] t = System.Text.Encoding.ASCII.GetBytes ( command.ToCharArray () );
    
    this.remoteFile.Write ( t, 0, t.Length );
  }
}

public class RemoteFileStreamServer
{
  private NetworkStream client = null;
  
  public RemoteFileStreamServer ( Socket socket )
  {
    Console.WriteLine ( "initializing remote filestream server..." );
    
    this.client = new NetworkStream ( socket );
    
    Console.WriteLine ( "remote filestream server initialized" );
  }
  
  public void Run ()
  {
    Console.WriteLine ( "starting remote filestream server..." );
    
    StreamReader cmdIn = new StreamReader ( this.client );
    
    FileStream f = null;
    
    int count = -1;
    byte[] buffer = null;
    
    bool loop = true;
    while ( loop )
    {
      // read the request line
      string[] buf = cmdIn.ReadLine ().Split ( new Char[] {':'} );
      
      Console.WriteLine ( "request received, req=" + buf[0] );
      
      // buf[0] is the command
      switch ( buf[0] )
      {
        case "OPEN":
          // the name of the local file to open
          string file = buf[1];
          
          // open for reading or writing
          bool read = bool.Parse ( buf[2] );
          
          // open the local file
          f = new FileStream ( ".\\" + ( read ? "download" : "destination" ) + "\\" + file, ( read ? FileMode.Open : FileMode.Create ) );
          
          // return the file length to client
          if ( read )
          {
            string length = f.Length.ToString () + "\r\n";
            Byte[] l = System.Text.Encoding.ASCII.GetBytes ( length.ToCharArray () );
            this.client.Write ( l, 0, l.Length );
          }
          break;
        
        case "READ":
          // number of bytes to read
          count = int.Parse ( buf[1] );
          
          // read/write buffer
          buffer = new byte[count];
          
          // read from the local file
          count = f.Read ( buffer, 0, count );
          
          // return the bytes to the client
          this.client.Write ( buffer, 0, count );
          break;
        
        case "WRITE":
          // number of bytes to write
          count = int.Parse ( buf[1] );
          
          // read/write buffer
          buffer = new byte[count];
          
          // read bytes from the client
          count = this.client.Read ( buffer, 0, count );
          
          // write bytes to the local file
          f.Write ( buffer, 0, count );
          break;
        
        case "CLOSE":
          // close local file
          f.Close ();
          
          // close connection to the client
          this.client.Close ();
          
          // stop the loop
          loop = false;
          break;
       }
       
       Console.WriteLine ( "request executed, req=" + buf[0] );
    }
    
    Console.WriteLine ( "stopping remote filestream server..." );
  }
}


public class WebAccessClient
{
  // no proxy
  public WebAccessClient ()
  {}
  
  // with proxy
  public WebAccessClient ( string proxyHost, int proxyPort, string proxyUser, string proxyPassword )
  {
    // create a proxy
    WebProxy proxy = new WebProxy ( proxyHost, proxyPort );
    
    // set user namen and password for proxy
    proxy.Credentials = new NetworkCredential ( proxyUser, proxyPassword );
    
    // disable proxy use when the host is local
    proxy.BypassProxyOnLocal = true;
    
    // all new requests use this proxy info
    GlobalProxySelection.Select = proxy;
  }
  
  public Stream Get ( string url )
  {
    // create a request based on the URL
    WebRequest req = WebRequest.Create ( url );
  
    // get the response
    WebResponse res = req.GetResponse ();

    // return a stream containing the response
    return res.GetResponseStream ();
  }
}