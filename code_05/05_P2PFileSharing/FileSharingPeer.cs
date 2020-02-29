
using System;
using System.Collections;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

public class FileSharingPeerForm : Form
{
  public static void Main ( string[] args )
  {
    // add parameter validation here

    Application.Run ( new FileSharingPeerForm ( Int32.Parse ( args[0] ), args[1], Int32.Parse ( args[2] ) ) );
  }
  
  private FileSharingPeer peer = null;
  
  // controls
  private TextBox remoteHost = null;
  private TextBox remotePort = null;
  private ComboBox downloadFiles = null;
  private ComboBox uploadFiles = null;
  private Button download = null;
  private Button upload = null;
  
  public FileSharingPeerForm ( int localPort, string remoteHost, int remotePort )
  {
    // add an event listener for close-event
    this.Closed += new EventHandler ( OnClosed );
    
    // create controls
    this.Text = "File Sharing Peer (" + localPort + ")";
    this.Size = new Size ( 260, 160 );
    
    Label l = new Label ();
    l.Text = "Remote IP:";
    l.Location = new Point ( 10, 10 );
    l.Size = new Size ( 60, l.Size.Height );
    this.Controls.Add ( l );
    
    this.remoteHost = new TextBox ();
    this.remoteHost.Text = remoteHost;
    this.remoteHost.Location = new Point ( 75, 10 );
    this.remoteHost.Size = new Size ( 90, this.remoteHost.Size.Height );
    this.Controls.Add ( this.remoteHost );
    
    l = new Label ();
    l.Text = "Port:";
    l.Location = new Point ( 170, 10 );
    l.Size = new Size ( 30, l.Size.Height );
    this.Controls.Add ( l );
    
    this.remotePort = new TextBox ();
    this.remotePort.Text = remotePort.ToString ();
    this.remotePort.Location = new Point ( 200, 10 );
    this.remotePort.Size = new Size ( 40, this.remotePort.Size.Height );
    this.Controls.Add ( this.remotePort );
    
    this.download = new Button ();
    this.download.Text = "Download";
    this.download.Location = new Point ( 165, 69 );
    this.download.Click += new EventHandler ( OnDownload );
    this.Controls.Add ( this.download );
    
    this.downloadFiles = new ComboBox ();
    this.downloadFiles.Items.Add ( "Download1.txt" );
    this.downloadFiles.Items.Add ( "Download2.txt" );
    this.downloadFiles.Location = new Point ( 10, 70 );
    this.Controls.Add ( this.downloadFiles );
     
    this.uploadFiles = new ComboBox ();
    this.uploadFiles.Items.Add ( "Upload1.txt" );
    this.uploadFiles.Items.Add ( "Upload2.txt" );
    this.uploadFiles.Location = new Point ( 10, 100 );
    this.Controls.Add ( this.uploadFiles );
    
    this.upload = new Button ();
    this.upload.Text = "Upload";
    this.upload.Location = new Point ( 165, 99 );
    this.upload.Click += new EventHandler ( OnUpload );
    this.Controls.Add ( this.upload );
    
    this.peer = new FileSharingPeer ( localPort );
  }
  
  public void OnClosed ( Object sender, EventArgs e )
  {
    this.peer.Close ();
  }
  
  public void OnDownload ( Object sender, EventArgs e )
  {
    if ( !this.downloadFiles.Text.Equals ( "" ) )
      this.peer.Download ( this.remoteHost.Text, int.Parse ( this.remotePort.Text ), this.downloadFiles.Text );
    else
      MessageBox.Show ( this, "Please choose a file to download !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
  }
  
  public void OnUpload ( Object sender, EventArgs e )
  {
    if ( !this.uploadFiles.Text.Equals ( "" ) )
      this.peer.Upload ( this.remoteHost.Text, int.Parse ( this.remotePort.Text ), this.uploadFiles.Text );
    else
      MessageBox.Show ( this, "Please choose a file to upload !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
  }
}