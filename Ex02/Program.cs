using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    class Program
    {
        public static void Main()
        {
            //GameStarter.startGame("memory_game");
            MemoryGameUI.startMemoryGame();

            /*            int[] a = { 6, 6 };
                        Board<char> board = new Board<char>(a);
                        board.printBoard();*/
            Console.ReadLine();

        }
    }
}