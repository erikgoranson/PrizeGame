using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrizeGame.Agents;
using PrizeGame.Boards;

namespace PrizeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //Board Test = new Board();

            //Nullable<Directions> direction = null;

            Agent CoolGuy = new Agent()
            {
                X = 0,
                Y = 0,
            };

            int allowed = 1;

            int testInt = 10;

            testInt = 0-allowed;

            

            //bool boolTest = (Test.Cells[4, 4] != null) ? true : false;
            //test = (Grid.Cells[Target.X, Target.Y] != null) ? true : false;
            //Console.WriteLine(boolTest);


            Console.ReadLine();

            /*Direction test = new Direction
            {
                X = 0,
                Y = 0,
            };*/
        }
    }
}
