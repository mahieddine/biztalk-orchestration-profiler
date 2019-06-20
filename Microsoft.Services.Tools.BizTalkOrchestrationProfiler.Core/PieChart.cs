using System;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Collections;

namespace Microsoft.Sdc.OrchestrationProfiler.Core
{
	/// <summary>
	/// 
	/// </summary>
	public class PieChart
	{
		private int diameter;
		private SegmentCollection segments;
		private Color backColor;
		private bool drawLegend;
		private Font font;
		private int border = 10;

		public PieChart()
		{
			this.font = new Font("Arial", 8);
			this.segments = new SegmentCollection();
			this.backColor = Color.White;
			this.diameter = 80;
		}

		#region Public Properties

		public SegmentCollection Segments
		{
			get { return this.segments; }
		}

		public int Diameter
		{
			get { return this.diameter; }
			set { this.diameter = value; }
		}

		public bool DrawLegend
		{
			get { return this.drawLegend; }
			set { this.drawLegend = value; }
		}

		public Font Font
		{
			get { return this.font; }
		}

		#endregion

		#region DrawGraph

		public void DrawGraph(Graphics g)
		{ 			
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

			// clear and fill surface with a specific color
			g.Clear(this.backColor);
 
			// look and feel : border and a shade for this chart
			int border = this.border;
			g.FillEllipse(new SolidBrush(Color.Silver), new Rectangle(border - 4, border + 4, this.diameter, this.diameter));
			g.FillEllipse(new SolidBrush(Color.White), new Rectangle(border, border, this.diameter, this.diameter));
 
			// calculate sum 
			double sum = this.segments.Sum;

			// draw pie chart
			float angle = -90;
			float sweep = 0;
 
			for(int i=0, icolor = 100; i < segments.Count; i++)
			{
				// current color
				Color color = Color.FromArgb(
					icolor,
					(int)Math.Round(icolor / 2.0),
					(int)Math.Round(icolor / 20.0));

				if (segments[i].ColorSpecified)
				{
					color = segments[i].Color;
				}

				icolor += 20;
				icolor %= 255;
      
				// draw a pie chart sector
				sweep = (float)(360f * segments[i].Value /sum);

				Rectangle rect = new Rectangle(border, border, this.diameter, this.diameter);

				g.FillPie(new SolidBrush(color), rect, angle, sweep);
				g.DrawPie(new Pen(Color.Black), rect, angle, sweep);

				angle += sweep;

				if (drawLegend)
				{
					// draw legend
					int squareSize = 10;
					Rectangle legendRect = new Rectangle(
						3*border + this.diameter, 
						border + (squareSize +7)*i, 
						squareSize, 
						squareSize);

					g.FillRectangle(new SolidBrush(color), legendRect); 

					// draw label legend
					g.DrawString(
						segments[i].Name, 
						this.font, 
						Brushes.Black, 
						new Point(legendRect.X + 15,
						legendRect.Y));
				}
			}

			g.Dispose();
		}

		#endregion

		public Bitmap GetImage()
		{
			int width = this.diameter + (this.border*2);
			int height = this.diameter + (this.border*2);

			if (this.drawLegend)
			{
				width += 100;
			}

			Bitmap bmp = new Bitmap(width,height);			

			try
			{
				Graphics memGraphic = Graphics.FromImage(bmp);
				this.DrawGraph(memGraphic);

				bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);

				return bmp;
			}
			catch
			{
				return null;
			}
		}

		public void SaveToFile(string fileName)
		{
			this.GetImage().Save(fileName);
		}
	}

	#region SegmentCollection

	public class SegmentCollection : CollectionBase
	{
		public SegmentCollection()
		{
		}

		public int Add(Segment segment)
		{
			// vérify if object is not already in
			if (this.List.Contains(segment))
				throw new InvalidOperationException();
 
			// adding it
			return this.List.Add(segment);
		}
 
		// overload adding method
		public int Add(string name, int value)
		{
			return this.Add(new Segment(name, value));
		}
 
		// overload adding method
		public int Add(string name, int value, Color color)
		{
			return this.Add(new Segment(name, value, color));
		}

		public Segment this[int index]
		{
			get
			{
				if (index < 0 || index > this.List.Count)
					throw new ArgumentOutOfRangeException();
 
				return (Segment)this.List[index];
			}
		}

		public double Sum
		{
			get 
			{
				double sum = 0;
				foreach(Segment segment in this.InnerList)  
				{
					sum += segment.Value;
				}

				return sum;
			}
		}
	}

	#endregion

	#region Segment

	public class Segment
	{
		private string name;
		private int value;
		private Color color = Color.WhiteSmoke;
		private bool colorSpecified;

		public Segment(string name, int value)
		{
			this.name = name;
			this.value = value;
		}

		public Segment(string name, int value, Color color)
		{
			this.name = name;
			this.value = value;
			this.color = color;
			this.colorSpecified = true;
		}

		public string Name
		{
			get { return this.name; }
		}

		public Color Color
		{
			get { return this.color; }
		}

		public bool ColorSpecified
		{
			get { return this.colorSpecified; }
		}

		public int Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException();

				this.value = value;
			}
		}
	}

	#endregion
}
