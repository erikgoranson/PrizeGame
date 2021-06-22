using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrizeGame.Agents;
using PrizeGame.BoardObjects;
using PrizeGame.Boards;
using static PrizeGame.Direction;

namespace PrizeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Board Test = new Board();

            while (Test.GetPrizes().Any())
            {
                Test.StartTurn();
            }

            Console.WriteLine("Game Over");

           Console.ReadLine();
        }
    }
}
