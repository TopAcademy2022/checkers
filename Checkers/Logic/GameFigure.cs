namespace Checkers.Logic
{
	public class GameFigure
	{
		private bool _isQueen;

		public enum GameColor : byte
		{
			White = 0,
			Black = 1
		}

		private GameColor _color;

		public enum DirectionMovement
		{
			Up = 0,
			Down = 1
		}

		private DirectionMovement _directionMovement;

		public GameFigure(GameColor color, DirectionMovement directionMovement)
		{
			this._isQueen = false;
			this._color = color;
			this._directionMovement = directionMovement;
		}

		public GameColor GetColor()
		{
			return this._color;
		}

		public void SetQueen()
		{
			this._isQueen = true;
		}

		public bool GetQueen()
		{
			return this._isQueen;
		}

		public DirectionMovement GetDirectionMovement()
		{
			return this._directionMovement;
		}
	}
}