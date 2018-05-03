namespace Minesweeper {
    public enum TileStatus {
        CLOSED, OPEN, FLAGGED
    }

    public class Tile {
        private bool mine;
        private TileStatus status;
        private int mines;

        public bool Mine {
            get => mine;
        }
        public TileStatus Status {
            get => status;
        }
        public int Mines {
            get => mines;
        }

        public Tile(bool mine = false) {
            status = TileStatus.CLOSED;
            this.mine = mine;
        }

        public void Open(int mines) {
            status = TileStatus.OPEN;
            this.mines = mines;
        }

        public void SetFlag() {
            if (status != TileStatus.OPEN)
                status = status != TileStatus.FLAGGED ? TileStatus.FLAGGED : TileStatus.CLOSED;
        }
    }
}