using System;
using System.Security.Cryptography;

namespace Minesweeper
{
    class Field
    {
        private static readonly Random random = new Random();

        private readonly int mines;
        private readonly int xSize;
        private readonly int ySize;
        private readonly Cell[,] grid;
        private bool isLost;

        public Field(int xSize, int ySize, int mines)
        {
            this.isLost = false;
            this.xSize = xSize;
            this.ySize = ySize;
            this.mines = mines;
            this.grid = new Cell[this.xSize, this.ySize];

            this.GenerateField();
        }

        public Cell[,] Grid { get => grid; }

        public bool IsLost { get => isLost; }

        public void DeployMines(int i, int j)
        {
            int count = 0;
            while (count < this.mines)
            {
                int x = random.Next(0, this.xSize);
                int y = random.Next(0, this.ySize);
                if (!this.grid[x, y].IsMined && x != i && y != j)
                {
                    this.grid[x, y].Mine();
                    count++;
                }
            }
            this.SetAllNeighbors();
        }

        public void Check(int x, int y)
        {
            this.grid[x, y].Check();

            if (this.grid[x, y].IsMined)
            {
                this.RevealAllMines();
                this.isLost = true;
            }
            else if (this.grid[x, y].MinesNearby == 0)
            {
                for (int j = y - 1; j <= y + 1; j++)
                    for (int i = x - 1; i <= x + 1; i++)
                        if (i >= 0 && j >= 0 && i < this.xSize && j < this.ySize)
                            if (!this.grid[i, j].IsMined && !this.grid[i, j].IsChecked)
                                this.Check(i, j);
            }
        }

        public bool CheckWin()
        {
            for (int y = 0; y < this.ySize; y++)
                for (int x = 0; x < this.xSize; x++)
                {
                    if (!this.grid[x, y].IsMined && this.grid[x, y].IsFlagged)
                        return false;
                    else if (!this.grid[x, y].IsFlagged && this.grid[x, y].IsMined)
                        return false;
                }

            return true;
        }

        private void GenerateField()
        {
            for (int y = 0; y < this.ySize; y++)
                for (int x = 0; x < this.xSize; x++)
                    this.grid[x, y] = new Cell();
        }

        private void SetAllNeighbors()
        {
            for (int y = 0; y < this.ySize; y++)
                for (int x = 0; x < this.xSize; x++)
                    this.grid[x, y].SetMinesNearby(CalcNeighbors(x, y));
        }

        private int CalcNeighbors(int x, int y)
        {
            int neighbors = 0;

            if (!this.grid[x, y].IsMined)
                for (int j = y - 1; j <= y + 1; j++)
                    for (int i = x - 1; i <= x + 1; i++)
                        if (i >= 0 && j >= 0 && i < this.xSize && j < this.ySize && grid[i, j].IsMined)
                            neighbors++;

            return neighbors;
        }

        private void RevealAllMines()
        {
            for (int y = 0; y < this.ySize; y++)
                for (int x = 0; x < this.xSize; x++)
                    if (this.grid[x, y].IsMined)
                        this.grid[x, y].Check();
        }
    }
}
