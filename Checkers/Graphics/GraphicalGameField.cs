using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Checkers.Logic;
using Checkers.Logic.Interfaces;
using System.Windows.Shapes;

namespace Checkers.Graphics
{
	public class GraphicalGameField : Grid
	{
		private ICheckersInterface _gameField;

		public GraphicalGameField(Window parentElement)
		{
			this._gameField = new CheckersInterface();

			parentElement.Content = this;

			for (byte i = 0; i < GameField.COUNT_ROWS; i++)
			{
				RowDefinitions.Add(new RowDefinition());
				ColumnDefinitions.Add(new ColumnDefinition());
			}
		}

		public void RenderGameField()
		{
			byte[,] logicalGameField = this._gameField.GetGameFieldAsByteArray();

			for (int i = 0; i < GameField.COUNT_ROWS; i++)
			{
				for (int k = 0; k < GameField.COUNT_COLLUMNS; ++k)
				{
					if (logicalGameField[i, k] == 1)
					{
						GraphicalGameFigure gameFigure = new GraphicalGameFigure(true);

						this.Children.Add(gameFigure);//< Белые
						Grid.SetRow(gameFigure, i);
						Grid.SetColumn(gameFigure, k);
					}
					else if (logicalGameField[i, k] == 2)
					{
						GraphicalGameFigure gameFigure = new GraphicalGameFigure(false);

						this.Children.Add(gameFigure);//< Черные
						Grid.SetRow(gameFigure, i);
						Grid.SetColumn(gameFigure, k);
					}

				}
			}
		}

		public void RerenderGameField()
		{
			// Clear
			this.Children.Clear();

			// Render again
			this.RenderGameField();
		}

		/*!
		* @brief Showing possible figure movements.
		 */
		public void ShowPossibleMoves(List<byte[]> positions)
		{
			foreach (byte[] position in positions) ///< Foreach through List
			{
				GraficalFigurePosibleMovement graficalFigurePosibleMovement = new GraficalFigurePosibleMovement();

				this.Children.Add(graficalFigurePosibleMovement);

				Grid.SetRow(graficalFigurePosibleMovement, position[0]);
				Grid.SetColumn(graficalFigurePosibleMovement, position[1]);
			}
		}
	}
}