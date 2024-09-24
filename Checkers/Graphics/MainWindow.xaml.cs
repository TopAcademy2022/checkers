using System.Windows;
using Checkers.Graphics;

namespace Checkers
{
    public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			GraphicalGameField game = new GraphicalGameField(this);
			game.RenderGameField();
		}
	}
}