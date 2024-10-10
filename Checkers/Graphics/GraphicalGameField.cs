using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Checkers.Logic;
using Checkers.Logic.Interfaces;


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
						gameFigure.Click += this.OnFigureClick;

						this.Children.Add(gameFigure);//< Белые
						Grid.SetRow(gameFigure, i);
						Grid.SetColumn(gameFigure, k);
					}
					else if (logicalGameField[i, k] == 2)
					{
						GraphicalGameFigure gameFigure = new GraphicalGameFigure(false);
						gameFigure.Click += this.OnFigureClick;

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
				graficalFigurePosibleMovement.Click += this.IgorFigureMovement;


				this.Children.Add(graficalFigurePosibleMovement);

				Grid.SetRow(graficalFigurePosibleMovement, position[0]);
				Grid.SetColumn(graficalFigurePosibleMovement, position[1]);
			}
		}

		public void OnFigureClick(object sender, EventArgs e)
		{
			Button myButton = sender as Button;
			byte row = Grid.GetRow(myButton);
			byte coll = Grid.GetColumn(myButton);

			ShowPossibleMoves(this._gameField.GetPossibleMovementsAsByteArrayList([row, coll]));
		}

		public void DisplayMoveFigure(object sender, KeyEventArgs e)
		{
			GraficalFigurePosibleMovement graficalFigurePosibleMovement = new GraficalFigurePosibleMovement();

			byte[] logicalGameField = [Convert.ToByte(Canvas.GetLeft(graficalFigurePosibleMovement)), Convert.ToByte(Canvas.GetTop(graficalFigurePosibleMovement))];

			ShowPossibleMoves(this._gameField.GetPossibleMovementsAsByteArrayList());

			
			//Canvas.SetLeft(MyButton, left + 10);
			//Canvas.SetTop(MyButton, top + 10);
		}
	}
}