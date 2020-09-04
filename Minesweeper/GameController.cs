using Minesweeper.Properties;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Minesweeper
{
    public class GameController
    {
        private readonly ImageBrush MINE = new ImageBrush(new BitmapImage(new Uri(@"Resources/mine.png", UriKind.Relative)));
        private readonly ImageBrush FLAG = new ImageBrush(new BitmapImage(new Uri(@"Resources/flag.png", UriKind.Relative)));

        private readonly Border[,] canvasGrid;
        private readonly Field field;
        private bool isFinished;

        public GameController(Border[,] canvasGrid)
        {
            this.canvasGrid = canvasGrid;
            this.isFinished = false;
            this.field = new Field(Settings.Default.size, Settings.Default.mines);
            this.refreshDisplay();
        }

        public void checkHandler(int x, int y)
        {
            if (!isFinished)
            {
                this.field.check(x, y);
                this.checkFinish();
                this.refreshDisplay();
            }
        }

        public void flagHandler(int x, int y)
        {
            if (!isFinished)
            {
                this.field.Grid[x, y].toggleFlag();
                this.checkFinish();
                this.refreshDisplay();
            }
        }

        private void refreshDisplay()
        {
            for (int y = 0; y < Settings.Default.size; y++)
                for (int x = 0; x < Settings.Default.size; x++)
                {
                    TextBlock currTextBlock = (TextBlock)this.canvasGrid[x, y].Child;

                    if (!this.field.Grid[x, y].IsChecked && !this.field.Grid[x, y].IsFlagged)
                    {
                        currTextBlock.Background = Brushes.LightGray;
                        currTextBlock.Text = "";
                    }
                    else if (this.field.Grid[x, y].IsFlagged)
                    {
                        currTextBlock.Background = FLAG;
                        currTextBlock.Text = "";
                    }
                    else if (this.field.Grid[x, y].IsMined)
                    {
                        currTextBlock.Background = MINE;
                        currTextBlock.Text = "";
                    }
                    else
                    {
                        currTextBlock.Background = Brushes.White;
                        currTextBlock.Text = this.field.Grid[x, y].MinesNearby.ToString();
                    }
                }
        }

        private void checkFinish()
        {
            if (this.field.IsLost)
            {
                this.isFinished = true;
                GameFinishedWindow f = new GameFinishedWindow("You lost XDDD");
                f.Show();
            }
            else if (this.field.checkWin())
            {
                this.isFinished = true;
                GameFinishedWindow f = new GameFinishedWindow("You won!");
                f.Show();
            }
        }
    }
}
