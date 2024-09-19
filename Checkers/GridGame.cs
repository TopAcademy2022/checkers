using System.Windows;
using System.Windows.Controls;

namespace Checkers
{
	public class GridGame : Grid
	{
		private const byte _countCell = 8;

		public GridGame(Window parentElement)
		{
			parentElement.Content = this;

			for (byte i = 0; i < _countCell; i++)
			{
				this.RowDefinitions.Add(new RowDefinition());
				this.ColumnDefinitions.Add(new ColumnDefinition());
			}
		}
	}
}