using System.Windows;
using System.Windows.Controls;
using Checkers.Logic;

namespace Checkers.Graphics
{
    public class GraphicalGameField : Grid
    {
        private GameField _gameField;

        public GraphicalGameField(Window parentElement)
        {
            this._gameField = new GameField();

			parentElement.Content = this;

            for (byte i = 0; i < GameField.COUNT_ROWS; i++)
            {
                RowDefinitions.Add(new RowDefinition());
                ColumnDefinitions.Add(new ColumnDefinition());
            }
        }
    }
}