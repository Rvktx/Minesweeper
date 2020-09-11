using System.Windows;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for GameFinishedWindow.xaml
    /// </summary>
    public partial class GameFinishedWindow : Window
    {
        public GameFinishedWindow(string text)
        {
            MainWindow.Disable();
            InitializeComponent();
            msg.Text = text;
        }

        private void RetryButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Restart();
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
