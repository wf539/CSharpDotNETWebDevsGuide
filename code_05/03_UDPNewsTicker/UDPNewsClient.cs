
using System;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Windows.Forms;

public class UDPNewsClient : Form
{
  public static void Main ( string[] args )
  {
    Application.Run ( new UDPNewsClient () );
  }

  // multicast group IP address
  private const string GROUP_IP = "225.0.0.1";
  // multicast group port
  private const int GROUP_PORT = 8081;
  // communication interface
  private UDPMulticastClient client = null;
  // ticker thread
  private Thread tickerThread = null;
  // new messages
  private TextBox text = null;
  // default news displayed at the beginning
  private string news = "Please wait...";
  
  public UDPNewsClient ()
  {
    Console.WriteLine ( "initializing client..." );
    
    // create controls
    this.Text = "News Client";
    this.Size = new Size ( this.Size.Width, 75 );
    
    Label l = new Label ();
    l.Text = "News:";
    l.Location = new Point ( 10, 10 );
    l.Size = new Size ( 40, l.Size.Height );
    this.Controls.Add ( l );
    
    this.text = new TextBox ();
    this.text.Font = new Font ( "Courier New", 9 );
    this.text.Text = "Please wait...";
    this.text.Location = new Point ( 50, 10 );
    this.text.Size = new Size ( 225, this.text.Size.Height );
    this.text.ReadOnly = true;
    this.Controls.Add ( this.text );
    
    // add an event listener for close-event
    this.Closed += new System.EventHandler ( OnClosed );
    
    // start communication thread
    this.client = new UDPMulticastClient ( GROUP_IP, GROUP_PORT, new Notify ( SetNews ) );
    
    // start ticker thread
    this.tickerThread = new Thread ( new ThreadStart ( RunTicker ) );
    this.tickerThread.Start ();
        
    Console.WriteLine ( "initialization complete" );
  }
  
  // stop client
  public void OnClosed ( Object sender, EventArgs e )
  {
    Console.WriteLine ( "client shut down" );
    
    this.client.Close ();
    
    this.tickerThread.Abort ();
    this.tickerThread.Join ();

    Application.Exit ();
  }
  
  // ticker thread
  public void RunTicker ()
  {
    // initialze the textbox with the default text
    this.text.Text = " -+-+- " + this.news + " -+-+- " + this.news + " -+-+- ";
    while ( true )
    {
      string data = this.news + " -+-+- ";
      
      // repeat as long as there are characters in the data string
      while ( !data.Equals ( "" ) )
      {
        // wait 500 milliseconds
        Thread.Sleep ( 500 );

        // remove the first character from the text field and add the 
        // first character of the data string
        this.text.Text = this.text.Text.Substring ( 1 ) + data[0];
        
        // remove the first character from the data string
        data = data.Substring ( 1 );
      }
    }
  }
  
  // notification method, used by multicast client
  public void SetNews ( string news )
  {
    this.news = news;
  }
}