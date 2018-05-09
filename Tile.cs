namespace Minesweeper {
    public enum TileStatus {
        CLOSED, OPEN, FLAGGED
    }

    public class Tile {
        public bool Mine { get; }
        public TileStatus Status { get; private set; }
        public int Mines { get; private set; }

        public Tile(bool mine = false) {
            Status = TileStatus.CLOSED;
            Mine = mine;
        }

        public void Open(int mines) {
            Status = TileStatus.OPEN;
            Mines = mines;
        }

        public void SetFlag() {
            if (Status != TileStatus.OPEN)
                Status = Status != TileStatus.FLAGGED ? TileStatus.FLAGGED : TileStatus.CLOSED;
        }
    }
}