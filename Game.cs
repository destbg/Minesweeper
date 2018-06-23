using static System.Console;
using static System.ConsoleKey;

namespace Minesweeper {
    internal class Game {
        Board board;
        int pos1, pos2;
        int rows, cols;

        private static bool loop;
        public static void EndGame() =>
            loop = false;

        public Game() {
            StartGame();
            while (loop) {
                var input = ReadKey(true).Key;
                switch (input) {
                    case A:
                        if (pos1 - 1 == -1) break;
                        pos1--;
                        break;

                    case S:
                        if (pos2 + 1 == rows) break;
                        pos2++;
                        break;

                    case D:
                        if (pos1 + 1 == cols) break;
                        pos1++;
                        break;

                    case W:
                        if (pos2 - 1 == -1) break;
                        pos2--;
                        break;

                    case F:
                        board.Flag(pos2, pos1);
                        break;

                    case Enter:
                        board.Open(pos2, pos1);
                        break;

                    default:
                        SetCursorPosition(0, 1);
                        Write("Wrong input.            ");
                        break;
                }
                if (board.CheckWin()) break;
                if (!loop) continue;
                SetCursorPosition(pos1 * 5 + 2, pos2 + 3);
            }
            WriteLine('\n' + (loop ? "You won the game!" : "You lost the game!")
                            + "\nPress 'R' to end the game.");
            while (ReadKey(true).Key != R) { }
        }

        private void StartGame() {
            Clear();
            WriteLine("*** Minesweeper ***\n" +
                      "Type <rows>, <cols>, <bombs> to start the game.\n");
            loop = true;
            while (true) {
                Write("Input: ");
                var str = ReadLine().Trim()
                    .Split(", ".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
                if (str.Length < 3) continue;
                if (int.TryParse(str[0], out var rowResult) &&
                    int.TryParse(str[1], out var colResult) &&
                    int.TryParse(str[2], out var bombResult)) {
                    if (rowResult < 3 || rowResult > 20)
                        WriteLine("Rows can't be less then 3 or more then 20.");
                    else if (colResult < 3 || colResult > 20)
                        WriteLine("Cols can't be less then 3 or more then 20.");
                    else if (bombResult < 1 || bombResult > rowResult * colResult)
                        WriteLine("Too many or or little bombs placed.");
                    else {
                        rows = rowResult;
                        cols = colResult;
                        Clear();
                        Write("Use W, A, S, D to move, 'F' to flag and 'Enter' to open.");
                        board = new Board(rowResult, colResult, bombResult);
                        pos1 = cols / 2;
                        pos2 = rows / 2;
                        SetCursorPosition(pos1 * 5 + 2, pos2 + 3);
                        return;
                    }
                    continue;
                }
                WriteLine("Wrong input.");
            }
        }
    }
}