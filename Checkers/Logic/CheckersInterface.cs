using Checkers.Logic.Interfaces;

namespace Checkers.Logic
{
	public class CheckersInterface : ICheckersInterface
	{
		private GameField _gameField;

		public CheckersInterface()
		{
			this._gameField = new GameField();
		}

		public byte[,] GetGameFieldAsByteArray()
		{
			const byte COUNT_ROWS = 8;
			const byte COUNT_COLLUMNS = 8;

			byte[,] result = new byte[COUNT_ROWS, COUNT_COLLUMNS];

			for (int i = 0; i < COUNT_ROWS; i++)
			{
				for (int k = 0; k < COUNT_COLLUMNS; k++)
				{
					result[i, k] = 0;
				}
			}

			foreach (KeyValuePair<GameFigure, KeyValuePair<char, int>> figureWithPosition in this._gameField.CheckersField)
			{
				result[-(figureWithPosition.Value.Value - 8), figureWithPosition.Value.Key - 97] = (byte)(figureWithPosition.Key.GetColor() + 1);
			}

			return result;
		}

		public List<byte[]> GetPossibleMovementsAsByteArrayList(byte[] figurePositions)
		{
			List<byte[]> result = new List<byte[]>();

			char gameFigureChar = char(figurePositions[0] + 97);
			int gameFigureNum = figurePositions[0] % 10;

			GameFigure f = this._gameField.CheckersField.Where(el => el.Value.Key == gameFigureChar && el.Value.Value == gameFigureNum).First().Key;
			var nedoRezultat = this._gameField.GetPossibleFigureMovement(f);

			foreach (var v in nedoRezultat)
			{
				result.Add([v.Key - 97, v.Value % 10]);
			}

			return result;
		}
	}
}
