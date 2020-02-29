using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Xsl;
using System.Text;

namespace simpleCart
{
	/// <summary>
	/// Summary description for page1.
	/// </summary>
	public class page1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Xml catalog;
		protected System.Web.UI.WebControls.Xml cart;
		protected System.Web.UI.WebControls.Label lblFeedBack;
		protected System.Web.UI.WebControls.TextBox addItem;
		protected System.Web.UI.WebControls.TextBox removeItem;
		protected System.Web.UI.WebControls.TextBox firstRecord;
		protected System.Web.UI.WebControls.TextBox lastRecord;
		protected System.Web.UI.WebControls.TextBox direction;
		protected System.Web.UI.WebControls.TextBox recordCount;
		protected System.Web.UI.WebControls.TextBox categoryState;
		protected System.Web.UI.WebControls.TextBox Ready4Checkout;
	
		protected XmlDocument		catalogContent;
		protected XslTransform		catalogDisplay;	
		protected DataSet			dsBookList;
		public int					defaultRange = 5; //number of books to store at a time
		protected int				customerID;
			
		protected simpleCart.components.bookCatalog BookList;
		protected simpleCart.components.xmlShoppingCart BookCart;

		private void Page_Load(object sender, System.EventArgs e)
		{
			
			BookList = new simpleCart.components.bookCatalog();
			BookCart = new simpleCart.components.xmlShoppingCart();

			showCatalog();		//initialize catalog
			showCart();			//initialize cart   

			if( addItem.Text != null && addItem.Text !="" )
			{
				//add item isbn to cart
				XmlDocument newBook = new XmlDocument();
				newBook.LoadXml( BookList.catalogItemDetails( (string)addItem.Text ).GetXml() );
				BookCart.addItem2Cart(newBook);
				//update Session variable that holds cart state
				Session["myShoppingCart"] = BookCart.getCartDescriptionString();
				//rewrite cart to page
				showCart();
			}

			if( removeItem.Text != null && removeItem.Text != "" )
			{
				//remove item isbn from cart
				BookCart.removeItemFromCart( removeItem.Text, "isbn" );
				//update Session variable that holds cart state
				Session["myShoppingCart"] = BookCart.getCartDescriptionString();
				//rewrite cart to page
				showCart();

			}

			if( Ready4Checkout.Text == "true")
			{
				//(1) code to login customer could go here
								
				//(2) code to process order could go here
				    				
				//(3) build the feedback table
				XmlDocument myOrder = BookCart.getCartDescription();

				StringBuilder feedback = new StringBuilder();
				feedback.Append("<table border='1' cellspacing='0' bordercolor='silver' width='300px' bgcolor='#ffffff' style='margin:3px'>" );
				feedback.Append("<tr><td colspan=2 bgcolor='silver'>Thank you for your order. The following items are being shipped to you:</td></tr>");

				XmlNodeList Books = myOrder.SelectNodes("//Books");
				for( int i=0; i < Books.Count; i++)
				{
					string title = Books.Item(i).SelectSingleNode("title").InnerText;
					string price = Books.Item(i).SelectSingleNode("price").InnerText;
					feedback.Append("<tr><td style='font-size:8pt'>");
					feedback.Append(title);
					feedback.Append("</td><td>");
					feedback.Append(price);
					feedback.Append("</td></tr>");
				}

				feedback.Append("</table>");
				lblFeedBack.Text = feedback.ToString();
				
				//(4) clear the cart
				BookCart.clearCart(); // empty virtual cart
				showCart();			  // reinitialize the cart
				Session["myShoppingCart"] = BookCart.getCartDescription().OuterXml; // update server variable to prevent refilling of cart
				Ready4Checkout.Text = "false"; 
			}
			
		}

		public void showCatalog()
		{
			BookList.initCatalog( (DataSet)Application["catalog"]);
			XmlDocument catalogContent;
			XslTransform catalogDisplay;

			catalogContent = new XmlDocument();	
			try
			{	
				string xstrBooklist;
				if(!IsPostBack)
				{
					//	on first load, load first few books 
					xstrBooklist = BookList.catalogRange(0,this.defaultRange).GetXml();
				}
				else
				{
					string prevNext  = direction.Text;	 // "previous" or "next"
					int totalRecords = int.Parse(recordCount.Text); // number of records from previous load
					int prevFirst	 = int.Parse(firstRecord.Text); // first record # from previous load
					int prevLast	 = int.Parse(lastRecord.Text);	// last record # from previous load
					int range		 = prevLast - prevFirst;

					switch(prevNext)
					{	
						case "previous":
						{
							if(prevFirst <= range)
							{
								xstrBooklist = BookList.catalogRange(0,range).GetXml();
							}
							else
							{
								if( range != defaultRange ) range = defaultRange;
								xstrBooklist = BookList.catalogRange((prevFirst-range-1), range).GetXml();
							}
						}break;
						case "next":
						{
							if( (prevLast + range) >= totalRecords)
							{
								int nextRange = totalRecords-prevLast-1;
								xstrBooklist = BookList.catalogRange(prevLast+1, nextRange).GetXml();
							}
							else
							{
								if( range != defaultRange ) range = defaultRange;
								xstrBooklist = BookList.catalogRange(prevLast+1, range).GetXml();
							}
						}break;							
						default: xstrBooklist = BookList.catalogRange(0,this.defaultRange).GetXml();
							break;
					}
				}
				catalogContent.LoadXml( xstrBooklist );
			}
			catch(Exception error)
			{
				Response.Write("an Error occured<br>" + error.ToString());
				Response.End();
			}
					
			catalogDisplay = new XslTransform();
			try
			{	//Load XSLT display data
				catalogDisplay.Load( Server.MapPath("catalog.xslt") );
			}
			catch(Exception error)
			{
				Response.Write("an Error occured<br>" + error.ToString());
				Response.End();
			}

			//update asp:Xml component properties so that the transform is applied in the page
			catalog.Document = catalogContent;
			catalog.Transform = catalogDisplay;
		}

		

		public void showCart()
		{
			XmlDocument cartContent;	
			XslTransform cartDisplay;

			string xmlCart = (string)Session["myShoppingCart"];
			BookCart.initCart(xmlCart, "Books");
			
			cartContent = new XmlDocument();
			try
			{	
				cartContent = BookCart.getCartDescription();
			}
			catch(Exception error)
			{
				Response.Write("an Error occured<br>" + error.ToString());
				Response.End();
			}

			cartDisplay = new XslTransform();
			try
			{	//Load XSLT display data
				cartDisplay.Load( Server.MapPath("cart.xslt") );
			}
			catch(Exception error)
			{
				Response.Write("an Error occured<br>" + error.ToString());
				Response.End();
			}
			Response.Write(cartContent.OuterXml);
			
			//update asp:Xml component properties so that the transform is applied in the page
			cart.Document = cartContent;
			cart.Transform = cartDisplay;
		}


		public page1()
		{
			Page.Init += new System.EventHandler(Page_Init);
		}

		

		private void Page_Init(object sender, EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
