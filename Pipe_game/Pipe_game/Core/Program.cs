using Pipe_game.Service;
using static Pipe_game.ConsoleUI.ConsoleUI;

namespace Pipe_game.Core
{
    internal class Program
    {
        private static void Main()
        {

            var field = new Field(7, 9);
            ConsoleUI.ConsoleUI.play(field);
        }
    }
}