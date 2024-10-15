using Checkers.Logic.Interfaces;

namespace Checkers.Logic
{
	public class CheckersInterface : ICheckersInterface
	{
		public byte[,] GetGameFieldAsByteArray()
		{
			GameField gameField = new GameField();
			gameField.PlaceFigures();

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

		public List<byte[]> GetPossibleMovementsAsByteArrayList(List<KeyValuePair<char, int>> positions)
		{
			List<byte[]> result = new List<byte[]>();

			foreach (KeyValuePair<char, int> pair in positions)
			{
				byte column = (byte)(pair.Key - 'A');

				byte row = (byte)(8 - pair.Value);

				byte[] array = { row, column };
				result.Add(array);
			}

			return result;
		}
	}
}
