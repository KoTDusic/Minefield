using System.Windows;
using System.Windows.Input;
using MVVMFramevork;

namespace Game
{
    public class StartSettingsViewModel : NotifyObject
    {
        private bool _gameStarted;

        public bool GameStarted
        {
            get => _gameStarted;
            private set
            {
                if (value == _gameStarted)
                {
                    return;
                }

                _gameStarted = value;
                OnPropertyChanged();
            }
        }

        public GameSettings Settings { get; } = new GameSettings();

        public StartSettingsViewModel()
        {
            StartGameCommand = new RelayCommand(StartGameExecute, o => Settings.IsValid);
        }

        private void StartGameExecute()
        {
            var game = new GameWindow(new GameViewModel(Settings));
            var wnd = Application.Current.MainWindow;
            wnd.Visibility = Visibility.Hidden;
            game.ShowDialog();
            wnd.Visibility = Visibility.Visible;
            GameStarted = false;
        }

        public ICommand StartGameCommand { get; private set; }
    }
}