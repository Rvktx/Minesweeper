using System;

namespace Minesweeper
{
    class Field
    {
        private static readonly Random random = new Random();

        private readonly int mines;
        private readonly int size;
        private readonly Cell[,] grid;
        private bool isLost;

        public Field(int size, int mines)
        {
            this.isLost = false;
            this.size = size;
            this.mines = mines;
            this.grid = new Cell[this.size, this.size];

            this.generateField();
        }

        public Cell[,] Grid { get => grid; }

        public bool IsLost { get => isLost; }

        public void deployMines(int i, int j)
        {
            int count = 0;
            while (count < this.mines)
            {
                int x = random.Next(0, this.size);
                int y = random.Next(0, this.size);
                if (!this.grid[x, y].IsMined && x != i && y != j)
                {
                    this.grid[x, y].mine();
                    count++;
                }
            }
            this.setAllNeighbors();
        }

        public void check(int x, int y)
        {
            this.grid[x, y].check();

            if (this.grid[x, y].IsMined)
            {
                this.revealAllMines(x, y);
                this.isLost = true;
            }
            else if (this.grid[x, y].MinesNearby == 0)
            {
                for (int j = y - 1; j <= y + 1; j++)
                    for (int i = x - 1; i <= x + 1; i++)
                        if (i >= 0 && j >= 0 && i < this.size && j < this.size)
                            if (!this.grid[i, j].IsMined && !this.grid[i, j].IsChecked)
                                this.check(i, j);
            }
        }

        public bool checkWin()
        {
            for (int y = 0; y < this.size; y++)
                for (int x = 0; x < this.size; x++)
                {
                    if (!this.grid[x, y].IsMined && this.grid[x, y].IsFlagged)
                        return false;
                    else if (!this.grid[x, y].IsFlagged && this.grid[x, y].IsMined)
                        return false;
                }

            return true;
        }

        private void generateField()
        {
            for (int y = 0; y < this.size; y++)
                for (int x = 0; x < this.size; x++)
                    this.grid[x, y] = new Cell();
        }

        private void setAllNeighbors()
        {
            for (int y = 0; y < this.size; y++)
                for (int x = 0; x < this.size; x++)
                    this.grid[x, y].setMinesNearby(calcNeighbors(x, y));
        }

        private int calcNeighbors(int x, int y)
        {
            int neighbors = 0;

            if (!this.grid[x, y].IsMined)
                for (int j = y - 1; j <= y + 1; j++)
                    for (int i = x - 1; i <= x + 1; i++)
                        if (i >= 0 && j >= 0 && i < this.size && j < this.size && grid[i, j].IsMined)
                            neighbors++;

            return neighbors;
        }

        private void revealAllMines(int i, int j)
        {
            for (int y = 0; y < this.size; y++)
                for (int x = 0; x < this.size; x++)
                    if (x != i && y != j && this.grid[x, y].IsMined)
                        this.grid[x, y].check();
        }
    }
}
