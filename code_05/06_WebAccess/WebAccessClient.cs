
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

public class WebAccessClientForm : Form
{
  public static void Main ( string[] args )
  {
    Application.Run ( new WebAccessClientForm () );
  }
  
  
  private TextBox proxyHost = null;
  private TextBox proxyPort = null;
  private TextBox proxyUser = null;
  private TextBox proxyPassword = null;
  private TextBox url = null;
  private TextBox text = null;
  
  public WebAccessClientForm ()
  {
    this.Text = "HTML Page Source Viewer";
    this.Size = new Size ( 390, 390 );
    
    GroupBox gb = new GroupBox ();
    gb.Text = "Proxy";
    gb.Location = new Point ( 3, 0 );
    gb.Size = new Size ( 377, 75 );
    this.Controls.Add ( gb );
    
    Label l = new Label ();
    l.Text = "Host:";
    l.Location = new Point ( 15, 20 );
    l.Size = new Size ( 35, 20 );
    gb.Controls.Add ( l );
    
    this.proxyHost = new TextBox ();
    this.proxyHost.Location = new Point ( 50, 18 );
    this.proxyHost.Size = new Size ( 90, this.proxyHost.Size.Height );
    //this.proxyHost.PasswordChar = '*';
    gb.Controls.Add ( this.proxyHost );
    
    l = new Label ();
    l.Text = "Port:";
    l.Location = new Point ( 155, 20 );
    l.Size = new Size ( 35, 20 );
    gb.Controls.Add ( l );
    
    this.proxyPort = new TextBox ();
    this.proxyPort.Location = new Point ( 215, 18 );
    this.proxyPort.Size = new Size ( 40, this.proxyPort.Size.Height );
    //this.proxyPort.PasswordChar = '*';
    gb.Controls.Add ( this.proxyPort );
    
    l = new Label ();
    l.Text = "User:";
    l.Location = new Point ( 15, 45 );
    l.Size = new Size ( 35, 20 );
    gb.Controls.Add ( l );
    
    this.proxyUser = new TextBox ();
    this.proxyUser.Location = new Point ( 50, 43 );
    this.proxyUser.Size = new Size ( 90, this.proxyUser.Size.Height );
    //this.proxyUser.PasswordChar = '*';
    gb.Controls.Add ( this.proxyUser );
    
    l = new Label ();
    l.Text = "Password:";
    l.Location = new Point ( 155, 45 );
    l.Size = new Size ( 55, 20 );
    gb.Controls.Add ( l );
    
    this.proxyPassword = new TextBox ();
    this.proxyPassword.Location = new Point ( 215, 43 );
    this.proxyPassword.Size = new Size ( 100, this.proxyPassword.Size.Height );
    //this.proxyPassword.PasswordChar = '*';
    this.proxyPassword.KeyUp += new KeyEventHandler ( OnKeyUp );
    gb.Controls.Add ( this.proxyPassword );
    
    
    l = new Label ();
    l.Text = "URL:";
    l.Location = new Point ( 5, 80 );
    l.Size = new Size ( 30, 20 );
    this.Controls.Add ( l );
    
    this.url = new TextBox ();
    this.url.Location = new Point ( 40, 78 );
    this.url.Size = new Size ( 340, this.url.Size.Height );
    this.url.KeyUp += new KeyEventHandler ( OnKeyUp );
    this.Controls.Add ( this.url );
    
    this.text = new TextBox ();
    this.text.Multiline = true;
    this.text.ScrollBars = ScrollBars.Vertical;
    this.text.WordWrap = true;
    this.text.ReadOnly = true;
    this.text.Location = new Point ( 5, 105 );
    this.text.Size = new Size ( 375, 255 );
    this.text.MaxLength = 0;
    this.Controls.Add ( this.text );
  }
  
  public void OnKeyUp ( Object sender, KeyEventArgs a )
  {
    // read a page if the return key was pressed
    if ( a.KeyCode == Keys.Return )
    {
      // clear the result field
      this.text.Text = "";
      
      // create a web access client
      WebAccessClient client = null;
      if ( this.proxyHost.Text.Equals ( "" ) )
        client = new WebAccessClient ();
      else
        client = new WebAccessClient ( this.proxyHost.Text, int.Parse ( this.proxyPort.Text ), this.proxyUser.Text, this.proxyPassword.Text );
      
      // get the response stream
      StreamReader s = new StreamReader ( client.Get ( this.url.Text ) );
    
      // read the response and write it to the text field
      int BUFFER_SIZE = 4096;
      Char[] buf = new Char[BUFFER_SIZE];
      int n = s.Read ( buf, 0, BUFFER_SIZE );
      while ( n > 0 )
      {
        this.text.Text += new String ( buf, 0, n );
      
        n = s.Read ( buf, 0, BUFFER_SIZE );
      }
      
      // close the stream
      s.Close ();
    }
  }
}