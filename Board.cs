using static System.Console;
using static Minesweeper.TileStatus;

namespace Minesweeper {
    internal class Board {
        readonly int rows, cols, bombs;
        readonly Tile[,] _tile;

        public Board(int rows, int cols, int bombs) {
            this.rows = rows;
            this.cols = cols;
            this.bombs = bombs;
            _tile = new Tile[rows, cols];
            SetCursorPosition(0, 3);
            for (int r = 0; r < rows; r++) {
                for (int c = 0; c < cols; c++) {
                    _tile[r, c] = new Tile();
                    Write(" [ ] ");
                }
                WriteLine();
            }
            var placed = 0;
            var rnd = new System.Random();
            while (placed < bombs) {
                var row = rnd.Next() % rows;
                var col = rnd.Next() % cols;
                if (_tile[row, col].Mine) continue;
                _tile[row, col] = new Tile(true);
                placed++;
            }
        }

        public void Flag(int row, int col) {
            if (_tile[row, col].Status == Opened) {
                SetCursorPosition(0, 1);
                Write("Zone is already opened.");
                return;
            }
            SetCursorPosition(col * 5 + 2, row + 3);
            _tile[row, col].SetFlag();
        }

        public void Open(int row, int col) {
            var tile = _tile[row, col];
            if (tile.Status == Flagged) {
                SetCursorPosition(0, 1);
                Write("Zone is flagged.        ");
                return;
            }
            if (tile.Mine) {
                Game.EndGame();
                ForegroundColor = System.ConsoleColor.Red;
                SetCursorPosition(0, 3);
                for (int r = 0; r < rows; r++) {
                    for (int c = 0; c < cols; c++)
                        if (_tile[r, c].Mine)
                            Write("  X  ");
                        else SetCursorPosition(c * 5 + 5, r + 3);
                    WriteLine();
                }
                ResetColor();
                return;
            }
            var count = NumberOfSurroundingMines(row, col);
            SetCursorPosition(col * 5 + 1, row + 3);
            _tile[row, col].Open(count);
            if (count == 0)
                OpenEmpty(row, col);
        }

        public bool CheckWin() {
            var count = 0;
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    if (_tile[r, c].Status == Opened) count++;
            return count == rows * cols - bombs;
        }

        private int NumberOfSurroundingMines(int row, int col) {
            var count = 0;
            for (int r = row - 1; r <= row + 1; r++)
                for (int c = col - 1; c <= col + 1; c++)
                    if (!IsInside(r, c)) { }
                    else if (_tile[r, c].Mine)
                        count++;
            return count;
        }

        private void OpenEmpty(int row, int col) {
            for (int r = row - 1; r <= row + 1; r++)
                for (int c = col - 1; c <= col + 1; c++)
                    if (!IsInside(r, c)) { }
                    else if (_tile[r, c].Status == Flagged ||
                        _tile[r, c].Status == Opened) { }
                    else {
                        var count = NumberOfSurroundingMines(r, c);
                        SetCursorPosition(c * 5 + 1, r + 3);
                        _tile[r, c].Open(count);
                        if (count == 0) OpenEmpty(r, c);
                    }
        }

        private bool IsInside(int r, int c) =>
            r >= 0 && c >= 0 && r < rows && c < cols;
    }
}