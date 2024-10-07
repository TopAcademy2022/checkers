using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace Checkers.Graphics
{
    public class GraficalFigurePosibleMovement : Button
    {
        private double _radius;

        private Point _positionInCell;

        public GraficalFigurePosibleMovement()
        {
            const byte START_CENTER_POSITION_X = 48;
            const byte START_CENTER_POSITION_Y = 47;

            this._radius = 25;
            this._positionInCell = new Point(START_CENTER_POSITION_X, START_CENTER_POSITION_Y);

            this.DefaultStyleKey = typeof(GraphicalGameFigure);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            SolidColorBrush brush = new SolidColorBrush(Colors.Red);
            double borderThickness = 1;
            drawingContext.DrawEllipse(brush, new Pen(brush, borderThickness), this._positionInCell, this._radius, this._radius);
        }
    }
}