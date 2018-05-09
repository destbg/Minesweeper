using System;

namespace Minesweeper {
    public class Board {
        private readonly int rows, cols, bombs;
        private readonly Tile[,] tile;

        public Board(int rows, int cols, int bombs) {
            if (rows < 3 || rows > 20)
                throw new ArgumentException("Rows can't be less then 3 or more then 20.");
            else if (cols < 3 || cols > 20)
                throw new ArgumentException("Cols can't be less then 3 or more then 20.");
            else if (bombs < 1 || bombs > rows * cols)
                throw new ArgumentException("Too many or or little bombs placed.");
            this.rows = rows;
            this.cols = cols;
            this.bombs = bombs;
            tile = new Tile[rows, cols];
            SetDimansions();
            SetMines();
        }

        private void SetDimansions() {
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    tile[r, c] = new Tile();
        }

        private void SetMines() {
            int placed = 0;
            var rnd = new Random();
            while (placed < bombs) {
                int row = rnd.Next() % rows;
                int col = rnd.Next() % cols;
                if (!tile[row, col].Mine) {
                    tile[row, col] = new Tile(true);
                    placed++;
                }
            }
        }

        public void Flag(int row, int col) {
            if (!IsInside(row, col))
                throw new ArgumentException("The row or the col is outside the tile.");
            tile[row, col].SetFlag();
        }

        public void Open(int row, int col) {
            int count = NumberOfSurroundingMines(row, col);
            if (!IsInside(row, col))
                throw new ArgumentException("The row or the col is outside the tile.");
            else if (tile[row, col].Status == TileStatus.FLAGGED)
                throw new ArgumentException("Zone is flagged.");
            else if (tile[row, col].Mine)
                throw new ArgumentException("mine");
            tile[row, col].Open(count);
            if (count == 0)
                OpenEmpty(row, col);
        }

        public bool CheckWin() {
            int count = 0;
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    if (tile[r, c].Status == TileStatus.OPEN) count++;
            return count == rows * cols - bombs;
        }

        public string PrintEnd() {
            string toSay = "\n\n  ";
            for (int i = 0; i < cols; i++)
                toSay += i < 9 ? "  0" + (i + 1) + " " : "  " + (i + 1) + " ";
            toSay += "\n";
            for (int r = 0; r < rows; r++) {
                toSay += r < 9 ? "0" + (r + 1).ToString().PadRight(2)
                    : (r + 1).ToString().PadRight(3);
                for (int c = 0; c < cols; c++) {
                    var status = tile[r, c].Status;
                    toSay += tile[r, c].Mine ? "  X  "
                        : status == TileStatus.CLOSED ? " [ ] "
                        : status == TileStatus.FLAGGED ? " [F] "
                        : $"  {tile[r, c].Mines}  ";
                }
                toSay += "\n";
            }
            return toSay + "\n\n";
        }

        public override string ToString() {
            string toSay = "\n\n  ";
            for (int i = 0; i < cols; i++)
                toSay += i < 9 ? "  0" + (i + 1) + " " : "  " + (i + 1) + " ";
            toSay += "\n";
            for (int r = 0; r < rows; r++) {
                toSay += r < 9 ? "0" + (r + 1).ToString().PadRight(2)
                    : (r + 1).ToString().PadRight(3);
                for (int c = 0; c < cols; c++) {
                    var status = tile[r, c].Status;
                    toSay += status == TileStatus.CLOSED ? " [ ] "
                        : status == TileStatus.FLAGGED ? " [F] "
                        : $"  {tile[r, c].Mines}  ";
                }
                toSay += "\n";
            }
            return toSay + "\n";
        }

        private int NumberOfSurroundingMines(int row, int col) {
            int count = 0;
            for (int r = row - 1; r <= row + 1; r++)
                for (int c = col - 1; c <= col + 1; c++)
                    if (!IsInside(r, c)) continue;
                    else if (tile[r, c].Mine)
                        count++;
            return count;
        }

        private void OpenEmpty(int row, int col) {
            for (int r = row - 1; r <= row + 1; r++)
                for (int c = col - 1; c <= col + 1; c++)
                    if (!IsInside(r, c)) continue;
                    else if (tile[r, c].Status == TileStatus.FLAGGED ||
                        tile[r, c].Status == TileStatus.OPEN) continue;
                    else {
                        int count = NumberOfSurroundingMines(r, c);
                        tile[r, c].Open(count);
                        if (count == 0) OpenEmpty(r, c);
                    }
        }

        private bool IsInside(int r, int c) =>
            r >= 0 && c >= 0 && r < rows && c < cols;
    }
}
