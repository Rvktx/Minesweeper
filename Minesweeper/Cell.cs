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

        public void Check()
        {
            this.isFlagged = false;
            this.isChecked = true;
        }

        public void ToggleFlag()
        {
            if (!this.IsChecked)
                this.isFlagged = !this.isFlagged;
        }

        public void Mine()
        {
            this.isMined = true;
        }

        public void SetMinesNearby(int neighbors)
        {
            this.minesNearby = neighbors;
            if (this.minesNearby > 0)
                this.isFlagged = false;
        }
    }
}
