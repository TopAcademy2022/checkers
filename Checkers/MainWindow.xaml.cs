using System.Windows;

namespace Checkers
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			GridGame game = new GridGame(this);
		}
	}
}