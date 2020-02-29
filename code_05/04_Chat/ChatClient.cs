
using System;
using System.Drawing;
using System.Windows.Forms;

public class ChatClient : Form
{
  public static void Main ()
  {
    Application.Run ( new ChatClient () );
  }
  
  // controls
  private TextBox name = null;
  private Button connect = null;
  private Button disconnect = null;
  private Button send = null;
  private TextBox message = null;
  private TextBox messages = null;
  
  // communication
  private const int TCP_PORT = 8080;
  private const string UDP_GROUP_IP = "224.0.0.1";
  private const int UDP_GROUP_PORT = 8082;
  
  private TCPRemoteCommandProcessor proc = null;
  private UDPMulticastClient group = null;
  private bool connected = false;
  
  public ChatClient ()
  {
    // create controls
    this.Text = "Chat Client";
    this.Size = new Size ( 385, 385 );
    
    Label l = new Label ();
    l.Text = "Name:";
    l.Location = new Point ( 10, 10 );
    l.Size = new Size ( 55, l.Size.Height );
    this.Controls.Add ( l );
    
    this.name = new TextBox ();
    this.name.Location = new Point ( 65, 10 );
    this.name.Size = new Size ( 75, this.name.Size.Height );
    this.name.KeyUp += new KeyEventHandler ( OnKeyUpName );
    this.Controls.Add ( this.name );
    
    this.connect = new Button ();
    this.connect.Text = "Connect";
    this.connect.Location = new Point ( 145, 9 );
    this.connect.Click += new EventHandler ( OnConnect );
    this.Controls.Add ( this.connect );
    
    this.disconnect = new Button ();
    this.disconnect.Text = "Disconnect";
    this.disconnect.Location = new Point ( 225, 9 );
    this.disconnect.Click += new EventHandler ( OnDisconnect );
    this.Controls.Add ( this.disconnect );
    
    l = new Label ();
    l.Text = "Message:";
    l.Location = new Point ( 10, 35 );
    l.Size = new Size ( 55, l.Size.Height );
    this.Controls.Add ( l );
    
    this.message = new TextBox ();
    this.message.Location = new Point ( 65, 35 );
    this.message.Size = new Size ( 300, this.message.Size.Height );
    this.Controls.Add ( this.message );
    
    this.send = new Button ();
    this.send.Text = "Send";
    this.send.Location = new Point ( 290, 59 );
    this.send.Click += new System.EventHandler ( OnSend );
    this.Controls.Add ( this.send );
    
    l = new Label ();
    l.Text = "Messages:";
    l.Location = new Point ( 10, 60 );
    l.Size = new Size ( 60, l.Size.Height );
    this.Controls.Add ( l );
    
    this.messages = new TextBox ();
    this.messages.Location = new Point ( 10, 85 );
    this.messages.Size = new Size ( 355, 260 );
    this.messages.Multiline = true;
    this.messages.WordWrap = true;
    this.messages.ScrollBars = ScrollBars.Vertical;
    this.messages.Enabled = false;
    this.messages.BackColor = Color.White;
    this.Controls.Add ( this.messages );
    
    EnableControls ();
    
    // add an event listener for close-event
    this.Closed += new EventHandler ( OnClosed );
    
    // create communication components
    this.group = new UDPMulticastClient ( UDP_GROUP_IP, UDP_GROUP_PORT, new Notify ( SetMessage ) );
  }
  
  protected void EnableControls ()
  {
    if ( this.connected )
    {
      this.name.Enabled = false;
      this.connect.Enabled = false;
      this.disconnect.Enabled = true;
      this.message.Enabled = true;
      this.send.Enabled = true;
    }
    else
    {
      this.name.Enabled = true;
      this.connect.Enabled = !this.name.Text.Equals ( "" );
      this.disconnect.Enabled = false;
      this.message.Enabled = false;
      this.send.Enabled = false;
    }
  }
  
  public void SetMessage ( string text )
  {
    if ( !this.messages.Text.Equals ( "" ) )
      this.messages.Text += "\r\n";
    this.messages.Text += text;
  }
  
  public void OnClosed ( Object sender, EventArgs e )
  {
    OnDisconnect ( null, null );

    this.group.Close ();
  }
  
  public void OnKeyUpName ( Object sender, KeyEventArgs e )
  {
    EnableControls ();
  }
  
  public void OnConnect ( Object sender, EventArgs e )
  {
    this.proc = new TCPRemoteCommandProcessor ( "127.0.0.1", TCP_PORT, true );
    
    string result = null;
    this.proc.Execute ( this.name.Text + ":HELLO", ref result );
      
    this.connected = result.Equals ( "HELLO" );
    
    EnableControls ();
  }
  
  public void OnDisconnect ( Object sender, EventArgs e )
  {
    if ( this.connected )
    {
      string result = null;
      this.proc.Execute ( this.name.Text + ":BYE", ref result );
    
      this.proc.Close ();
    
      this.connected = false;
    
      EnableControls ();
    }
  }
  
  public void OnSend ( Object sender, EventArgs e )
  {
    string result = null;
    this.proc.Execute ( this.name.Text + ":SAY:" + this.message.Text, ref result );
  }
}