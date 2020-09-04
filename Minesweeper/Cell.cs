namespace Minesweeper
{
    class Cell
    {
        private bool isChecked;
        private bool isFlagged;
        private bool isMined;
        private int minesNearby;

        public Cell()
        {
            this.isChecked = false;
            this.isFlagged = false;
            this.isMined = false;
            this.minesNearby = 0;
        }

        public bool IsChecked { get => isChecked; }
        public bool IsFlagged { get => isFlagged; }
        public bool IsMined { get => isMined; }
        public int MinesNearby { get => minesNearby; }

        public void check()
        {
            this.isFlagged = false;
            this.isChecked = true;
        }

        public void toggleFlag()
        {
            if (!this.IsChecked)
                this.isFlagged = !this.isFlagged;
        }

        public void mine()
        {
            this.isMined = true;
        }

        public void setMinesNearby(int neighbors)
        {
            this.minesNearby = neighbors;
            if (this.minesNearby > 0)
                this.isFlagged = false;
        }
    }
}
