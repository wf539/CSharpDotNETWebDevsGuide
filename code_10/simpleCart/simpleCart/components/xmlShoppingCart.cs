using System;
using System.Xml;

namespace simpleCart.components
{
	/// <summary>
	/// This component is a wrapper class for XML functions
	/// it provides add, remove, clear, and view methods
	/// </summary>
	public class xmlShoppingCart
	{
		private XmlDocument myCart;
		private string elementType;

		/// <summary>
		/// Initializes the Cart. onpage load the cart can be initailized with an
		/// existing xmlCart string, this enables client caching of the cart.
		/// </summary>
		/// <param name="dataSource"></param>
		/// <param name="elementType"></param>
		public void initCart( string dataSource, string elementType )
		{
			this.elementType = elementType;
			myCart = new XmlDocument();
			if( dataSource != null )
			{
				myCart.LoadXml(dataSource);
			}
			else
			{
				//load default cart root
				myCart.LoadXml( "<shopcart-items></shopcart-items>" );
			}
		}

		/// <summary>
		/// appends the new node to the document
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public string addItem2Cart( XmlDocument item )
		{
			try
			{
				//Import the last item node from doc2 into the original document.
				//This assumes that "doc2" has item nodes with the same structure as xmlCart
				XmlNode newItem = myCart.ImportNode(item.DocumentElement.FirstChild, true);
				myCart.DocumentElement.AppendChild(newItem); 
				return "Success";
			}
			catch(Exception e)
			{
				return e.ToString();
			}
		}

		/// <summary>
		/// Removes a node with the given idvalue from the document
		/// </summary>
		/// <param name="idvalue"></param>
		/// <param name="attributename"></param>
		/// <returns></returns>
		public string removeItemFromCart( string idvalue, string attributename )
		{
			//XmlNode curnode = myCart.SelectSingleNode("//Books[isbn='0012-456-789x']");
			string XPathQuery = "//" + this.elementType + "[" + attributename + "='" + idvalue + "']";
			XmlNode curnode = myCart.SelectSingleNode(XPathQuery);
			try
			{
				myCart.DocumentElement.RemoveChild( curnode );
				return "Success";
			}
			catch(Exception e)
			{
				return e.ToString();
			}
		}

		/// <summary>
		/// removes all child nodes
		/// </summary>
		public void clearCart()
		{
			XmlElement root = myCart.DocumentElement;
			root.RemoveAll();
		}

		/// <summary>
		/// Returns the input data as an XmlDocument object
		/// </summary>
		/// <returns></returns>
		public XmlDocument getCartDescription()
		{
			return myCart;
		}

		/// <summary>
		/// Returns the input data as an XML formated string
		/// </summary>
		/// <returns></returns>
		public string getCartDescriptionString()
		{
			return myCart.OuterXml;
		}

	}

}
