using Checkers.Logic.Interfaces;

namespace Checkers.Logic
{
	public class CheckersInterface : ICheckersInterface
	{
		public byte[,] GetGameFieldAsByteArray()
		{
			GameField gameField = new GameField();

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

			foreach (KeyValuePair<GameFigure, KeyValuePair<char, int>> figureWithPosition in gameField.CheckersField)
			{
				result[-(figureWithPosition.Value.Value - 8), figureWithPosition.Value.Key - 97] = (byte)(figureWithPosition.Key.GetColor() + 1);
			}

			return result;
		}
	}
}
