using static System.ConsoleColor;
using static System.Console;

namespace Minesweeper {
    public enum TileStatus {
        Closed, Opened, Flagged
    }

    internal class Tile {
        public bool Mine { get; }
        public TileStatus Status { get; private set; }
        public int Mines { get; private set; }

        public Tile(bool mine = false) {
            Status = TileStatus.Closed;
            Mine = mine;
        }

        public void Open(int mines) {
            ForegroundColor = Green;
            Write($" {mines} ");
            Status = TileStatus.Opened;
            Mines = mines;
            ResetColor();
        }

        public void SetFlag() {
            Status = Status != TileStatus.Flagged ? TileStatus.Flagged : TileStatus.Closed;
            if (Status == TileStatus.Closed) {
                Write(' ');
                return;
            }
            ForegroundColor = Yellow;
            Write('F');
            ResetColor();
        }
    }
}