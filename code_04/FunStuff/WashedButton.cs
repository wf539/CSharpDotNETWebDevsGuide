using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FunStuff
{
	/// <summary>
	/// Summary description for WashedButton.
	/// </summary>
	public class WashedButton : System.Windows.Forms.Button
	{
		public WashedButton()
		{
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
         base.OnPaint (pe);
         // Create two semi-transparent colors
         Color c1 = Color.FromArgb (64, Color.Blue);
         Color c2 = Color.FromArgb (64, Color.Crimson);
         Brush b = new System.Drawing.Drawing2D.LinearGradientBrush
            (ClientRectangle, c1, c2, 10);
         pe.Graphics.FillRectangle (b, ClientRectangle);
         b.Dispose();
      }
	}
}
