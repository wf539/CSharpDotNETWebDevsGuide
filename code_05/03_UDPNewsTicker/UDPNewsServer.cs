
using System;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Windows.Forms;

public class UDPNewsServer : Form
{
  public static void Main ( string[] args )
  {
    Application.Run ( new UDPNewsServer () );
  }
  
  // local port where the UDP server is bound to
  private const int LOCAL_PORT = 8080;
  // multicast group IP address
  private const string GROUP_IP = "225.0.0.1";
  // multicast group port
  private const int GROUP_PORT = 8081;
  // UDP server
  private UDPPeer server = null;
  // a thread for sending new continuously
  private Thread serverThread = null;
  // a data field for typing in a new message
  private TextBox text = null;
  // a button for setting the new message
  private Button setButton = null;
  // the news message
  private string news = "";
  
  public UDPNewsServer ()
  {
    Console.WriteLine ( "initializing server, local port=" + LOCAL_PORT + ", group IP=" + GROUP_IP + ", group port=" + GROUP_PORT + "..." );
    
    // create controls
    this.Text = "News Server";
    this.Size = new Size ( this.Size.Width, 100 );
    
    Label l = new Label ();
    l.Text = "News:";
    l.Location = new Point ( 10, 10 );
    l.Size = new Size ( 40, l.Size.Height );
    this.Controls.Add ( l );
    
    this.text = new TextBox ();
    this.text.Location = new Point ( 50, 10 );
    this.text.Size = new Size ( 225, this.text.Size.Height );
    this.text.Text = "(enter news)";
    this.Controls.Add ( this.text );
    
    this.setButton = new Button ();
    this.setButton.Text = "Set";
    this.setButton.Location = new Point ( 10, 40 );
    this.Controls.Add ( this.setButton );
    
    // add an event listener for click-event
    this.setButton.Click += new System.EventHandler ( OnSet );
    
    // add an event listener for close-event
    this.Closed += new System.EventHandler ( OnClosed );
    
    // create communication components
    this.server = new UDPPeer ( LOCAL_PORT, GROUP_IP, GROUP_PORT );

    // start communication thread
    this.serverThread = new Thread ( new ThreadStart ( Run ) );
    this.serverThread.Start ();
    
    Console.WriteLine ( "initialization complete" );
  }
  
  // server shut down
  public void OnClosed ( Object sender, EventArgs e )
  {
    Console.WriteLine ( "server shut down..." );
    
    // stop thread
    this.serverThread.Abort ();
    // wait until it's stopped
    this.serverThread.Join ();
    
    this.server.Close ();

    Application.Exit ();
  }
  
  // button click event handler
  public void OnSet ( Object sender, EventArgs e )
  {
    this.news = this.text.Text;
  }
  
  // sending thread
  public void Run ()
  {
    while ( true )
    {
      if ( !this.news.Equals ( "" ) )
      {
        Console.WriteLine ( "sending " + this.news );
        
        this.server.Send ( this.news );
      }
    
      // wait one second
      Thread.Sleep ( 1000 );
    }
  }
}