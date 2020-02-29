using System;
using System.Drawing;
using System.Xml.Serialization;
using System.Collections;

namespace MSMQGraphics
{
	[XmlInclude(typeof(Line))]
	public class Drawing
	{
		public ArrayList lines;

		public Drawing()
		{
			lines = new ArrayList();
		}

		public void clear()
		{
			lines.Clear();
		}

		public void add(Line l)
		{
			lines.Add(l);
		}

		public void draw(Graphics g)
		{
			foreach (Line l in lines)
			{
				l.draw(g);
			}
		}
	}

	public class Line
	{
		public int x1;
		public int y1;
		public int x2;
		public int y2;
		public int Win32Color;

		public Line()
		{
		}

		public Line(int Win32Color,int x1,int y1,int x2,int y2)
		{
			this.x1 = x1;
			this.y1 = y1;
			this.x2 = x2;
			this.y2 = y2;
			this.Win32Color = Win32Color;
		}

		public void draw(Graphics g)
		{
			g.DrawLine(new Pen(ColorTranslator.FromWin32(Win32Color)),x1,y1,x2,y2);
		}
	}
}
