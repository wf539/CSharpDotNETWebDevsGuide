using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FunStuff
{
   public class ScrollingText : System.Windows.Forms.Control
   {
      Timer timer;            // this will animate the text
      string scroll = null;   // the text we're going to animate
   
      public ScrollingText()
      {
         timer = new Timer();
         timer.Interval = 200;
         timer.Enabled = true;
         timer.Tick += new EventHandler (Animate);
      }

      void Animate (object sender, EventArgs e)
      {
         // Create scroll string field from Text property
         if (scroll == null) scroll = Text + "   ";

         // Trim one character from the left, and add it to the right.
         scroll = scroll.Substring (1, scroll.Length-1)
            + scroll.Substring (0, 1);

         // This tells Windows Forms our control needs re-painting.
         Invalidate();
      }

      void StartStop (object sender, EventArgs e)
      { timer.Enabled = !timer.Enabled; }

      // When Text is changed, we must update the scroll string.
      protected override void OnTextChanged (EventArgs e)
      {
         scroll = null;
         base.OnTextChanged (e);
      }

      protected override void OnClick (EventArgs e)
      {
         timer.Enabled = !timer.Enabled;
         base.OnClick (e);
      }

      public override void Dispose()
      {
         // Since the timer hasn't been added to a collection (because
         // we don’t have one!) we have to dispose it manually.
         timer.Dispose();
         base.Dispose();
      }

      protected override void OnPaint(PaintEventArgs pe)
      {
         // This is a fancy brush that does graded colors.
         Brush b = new System.Drawing.Drawing2D.LinearGradientBrush
            (ClientRectangle, Color.Blue, Color.Crimson, 10);

         // Use the control’s font, resized to the height of the
         // control (actually slightly less to avoid truncation)
         Font f = new Font
            (Font.Name, Height*3/4, Font.Style, GraphicsUnit.Pixel);

         pe.Graphics.DrawString (scroll, f, b, 0, 0);
         base.OnPaint (pe);

         b.Dispose(); f.Dispose();
      }
   }
}
