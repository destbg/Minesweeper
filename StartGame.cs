﻿using System;
using static System.Console;

namespace Minesweeper {
    class StartGame {
        public StartGame() {
            WriteLine("*** Minesweeper ***\n" +
                "Type <rows>, <cols>, <bombs> to start the game.\n");
            Board board;
            while (true) {
                Write("Input: ");
                string[] str = ReadLine().Trim()
                    .Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                try {
                    board = new Board(int.Parse(str[0]), int.Parse(str[1]), int.Parse(str[2]));
                    break;
                } catch (Exception ex) {
                    WriteLine(ex.Message + "\n");
                }
            }
            string[] input = { "hi" };
            bool loop = true, checkWin = false;
            while (loop) {
                string command = "";
                switch (input[0].ToLower()) {
                    case "o": {
                        try {
                            board.Open(int.Parse(input[1]) - 1, int.Parse(input[2]) - 1);
                        } catch (Exception ex) {
                            if (ex.Message == "mine") loop = false;
                            command = ex.Message;
                        }
                        break;
                    }
                    case "f": {
                        try {
                            board.Flag(int.Parse(input[1]) - 1, int.Parse(input[2]) - 1);
                        } catch (Exception ex) {
                            command = ex.Message;
                        }
                        break;
                    }
                    default: {
                        command = "Wrong input.";
                        break;
                    }
                }
                checkWin = board.CheckWin();
                if (checkWin) loop = false;
                else if (loop) {
                    string toSay = "Use {o/f <row> <col>}" + board + command;
                    Clear();
                    WriteLine(toSay);
                    Write("Input: ");
                    input = ReadLine().Trim()
                        .Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                }
            }
            Clear();
            WriteLine(board.PrintEnd() + (checkWin ? "You won the game!" : "You lost the game!"));
            ReadKey();
            Clear();
        }
    }
}
