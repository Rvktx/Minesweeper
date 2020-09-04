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
            InitializeComponent();
            msg.Text = text;
        }

        private void retryButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.restart();
            this.Close();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
