namespace Minesweeper {
    internal class StartUp {
        private static void Main() {
            System.Console.Title = "Minesweeper";
            while (true) new Game();
        }
    }
}