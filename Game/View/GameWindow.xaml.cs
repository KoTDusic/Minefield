using System.Windows;

namespace Game
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow(GameViewModel gameViewModel)
        {
            InitializeComponent();
            DataContext = gameViewModel;
        }
    }
}
