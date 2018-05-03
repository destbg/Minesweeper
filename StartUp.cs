namespace Minesweeper {
    class StartUp {
        static void Main() {
            System.Console.Title = "Minesweeper";
            while (true) new StartGame();
        }
    }
}
