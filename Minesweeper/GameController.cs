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
        private bool hasBegun;
        private bool isFinished;

        public GameController(Border[,] canvasGrid)
        {
            this.canvasGrid = canvasGrid;
            this.hasBegun = false;
            this.isFinished = false;
            this.field = new Field(Settings.Default.fieldWidth, Settings.Default.fieldHeight, Settings.Default.mines);
            this.RefreshDisplay();
        }

        public void CheckHandler(int x, int y)
        {
            if (!hasBegun)
            {
                this.hasBegun = true;
                this.field.DeployMines(x, y);
            }
            if (!isFinished)
            {
                this.field.Check(x, y);
                this.CheckFinish();
                this.RefreshDisplay();
            }
        }

        public void FlagHandler(int x, int y)
        {
            if (!this.isFinished && this.hasBegun)
            {
                this.field.Grid[x, y].ToggleFlag();
                this.CheckFinish();
                this.RefreshDisplay();
            }
        }

        private void RefreshDisplay()
        {
            for (int y = 0; y < Settings.Default.fieldHeight; y++)
                for (int x = 0; x < Settings.Default.fieldWidth; x++)
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
                        if (this.field.Grid[x, y].MinesNearby == 0)
                            currTextBlock.Text = " ";
                        else
                            currTextBlock.Text = this.field.Grid[x, y].MinesNearby.ToString();
                    }
                }
        }

        private void CheckFinish()
        {
            if (this.field.IsLost)
            {
                this.isFinished = true;
                GameFinishedWindow f = new GameFinishedWindow("You lost XDDD");
                f.Show();
            }
            else if (this.field.CheckWin())
            {
                this.isFinished = true;
                GameFinishedWindow f = new GameFinishedWindow("You won!");
                f.Show();
            }
        }
    }
}
