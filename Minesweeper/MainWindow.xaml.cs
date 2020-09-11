using Minesweeper.Properties;
using System;
using System.DirectoryServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow w;
        private static SettingsWindow settings;

        private const int CELL_SIZE = 25;
        private const int WIDTH_OFFSET = 17;
        private const int HEIGHT_OFFSET = 64;

        private Border[,] canvasGrid;
        private GameController controller;

        private bool disabled;
        private bool restarting;

        public MainWindow()
        {
            w = this;
            InitializeComponent();
            mainWindow.Width = Settings.Default.fieldWidth * CELL_SIZE + WIDTH_OFFSET;
            mainWindow.Height = Settings.Default.fieldHeight * CELL_SIZE + HEIGHT_OFFSET;
        }

        public static void Disable()
        {
            w.disabled = true;
        }

        public static void Restart()
        {
            w.restarting = true;
            MainWindow prev = w;
            w = new MainWindow();
            prev.Close();
            w.Show();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (!this.restarting)
                Application.Current.Shutdown();
        }

        private void Canvas_Initialized(object sender, EventArgs e)
        {
            this.InitCanvasCells();
            this.controller = new GameController(this.canvasGrid);
        }

        private void InitCanvasCells()
        {
            canvasGrid = new Border[Settings.Default.fieldWidth, Settings.Default.fieldHeight];
            for (int y = 0; y < Settings.Default.fieldHeight; y++)
                for (int x = 0; x < Settings.Default.fieldWidth; x++)
                {
                    TextBlock tb = new TextBlock
                    {
                        Width = CELL_SIZE,
                        Height = CELL_SIZE,
                        FontSize = 18,
                        TextAlignment = TextAlignment.Center,

                        Background = Brushes.LightGray,
                        Text = ""
                    };

                    canvasGrid[x, y] = new Border
                    {
                        BorderThickness = new Thickness(1),
                        BorderBrush = Brushes.Black,
                        Child = tb
                    };

                    canvas.Children.Add(canvasGrid[x, y]);
                    Canvas.SetTop(canvasGrid[x, y], y * CELL_SIZE);
                    Canvas.SetLeft(canvasGrid[x, y], x * CELL_SIZE);
                }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int xPos = (int)Math.Floor(e.GetPosition(canvas).X / CELL_SIZE);
            int yPos = (int)Math.Floor(e.GetPosition(canvas).Y / CELL_SIZE);

            if (e.ChangedButton == MouseButton.Left)
                controller.CheckHandler(xPos, yPos);
            else if (e.ChangedButton == MouseButton.Right)
                controller.FlagHandler(xPos, yPos);
        }

        private void SettingsButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.disabled)
                return;
            if (settings != null)
                settings.Close();
            settings = new SettingsWindow();
            settings.Show();
            settings.Activate();
        }
    }
}
