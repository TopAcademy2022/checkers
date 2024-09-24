using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Checkers
{
	public class GraphicalGameFigure : Button
	{
		private bool _isWhite;

		private double _radius;

		private Point _startPosition;

		public GraphicalGameFigure(bool color)
		{
			const byte START_CENTER_POSITION_X = 48;
			const byte START_CENTER_POSITION_Y = 47;

			this._isWhite = color;
			this._radius = 40;
			this._startPosition = new Point(START_CENTER_POSITION_X, START_CENTER_POSITION_Y);

			this.DefaultStyleKey = typeof(GraphicalGameFigure);
		}

		protected override void OnRender(DrawingContext drawingContext)
		{
			SolidColorBrush brush = new SolidColorBrush(Colors.Black);

			if (this._isWhite)
			{
				brush.Color = Colors.White;
			}
		   
			double borderThickness = 1;
			drawingContext.DrawEllipse(brush, new Pen(brush, borderThickness), this._startPosition, this._radius, this._radius);
		}
	}
}