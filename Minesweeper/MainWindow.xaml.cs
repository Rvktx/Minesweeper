using Minesweeper.Properties;
using System;
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
        private const int CELL_SIZE = 25;
        private const int WIDTH_OFFSET = 17;
        private const int HEIGHT_OFFSET = 84;

        private Border[,] canvasGrid;
        private GameController controller;

        public MainWindow()
        {
            w = this;
            InitializeComponent();
            mainWindow.Width = Settings.Default.size * CELL_SIZE + WIDTH_OFFSET;
            mainWindow.Height = Settings.Default.size * CELL_SIZE + HEIGHT_OFFSET;
        }

        public static void restart()
        {
            w.canvasGrid = null;
            w.controller = null;
            w.initCanvasCells();
            w.controller = new GameController(w.canvasGrid);
        }

        private void canvas_Initialized(object sender, EventArgs e)
        {
            this.initCanvasCells();
            this.controller = new GameController(this.canvasGrid);
        }

        private void initCanvasCells()
        {
            canvasGrid = new Border[Settings.Default.size, Settings.Default.size];
            for (int y = 0; y < Settings.Default.size; y++)
                for (int x = 0; x < Settings.Default.size; x++)
                {
                    TextBlock tb = new TextBlock();
                    tb.Width = CELL_SIZE;
                    tb.Height = CELL_SIZE;
                    tb.FontSize = 18;
                    tb.TextAlignment = TextAlignment.Center;

                    tb.Background = Brushes.LightGray;
                    tb.Text = "";

                    canvasGrid[x, y] = new Border();
                    canvasGrid[x, y].BorderThickness = new Thickness(1);
                    canvasGrid[x, y].BorderBrush = Brushes.Black;
                    canvasGrid[x, y].Child = tb;

                    canvas.Children.Add(canvasGrid[x, y]);
                    Canvas.SetTop(canvasGrid[x, y], y * CELL_SIZE);
                    Canvas.SetLeft(canvasGrid[x, y], x * CELL_SIZE);
                }
        }

        private void coordinatesLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            coordinatesLabel.Content = "XD";
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            int xPos = (int)Math.Floor(e.GetPosition(canvas).X / CELL_SIZE);
            int yPos = (int)Math.Floor(e.GetPosition(canvas).Y / CELL_SIZE);

            coordinatesLabel.Content = String.Format("(X:{0}, Y:{1})", xPos, yPos);
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int xPos = (int)Math.Floor(e.GetPosition(canvas).X / CELL_SIZE);
            int yPos = (int)Math.Floor(e.GetPosition(canvas).Y / CELL_SIZE);

            if (e.ChangedButton == MouseButton.Left)
                controller.checkHandler(xPos, yPos);
            else if (e.ChangedButton == MouseButton.Right)
                controller.flagHandler(xPos, yPos);
        }
    }
}
