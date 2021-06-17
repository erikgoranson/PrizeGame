using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PrizeGame.Prizes;

namespace PrizeGame.Boards
{
    class Board
    {
        public Board()
        {
            this.Reset();
            this.PlacePrizes();
            this.PrintBoard();
        }

        public string[,] Cells { get; set; }

        public int BoardDimensions { get; set; } = 11;

        public void Reset()
        {
            Cells = new string[BoardDimensions, BoardDimensions];
            //rules were hardcoded to players

            Cells[4, 4] = "A"; //random
            Cells[4, 6] = "B"; //MinDistance
            Cells[6, 4] = "C"; //MaxDistance
            Cells[6, 6] = "D"; //MyAgent
        }

        public List<Prize> GetPrizes() //check for consistency
        {
            List<Prize> Prizes = new Prizes(BoardDimensions).PrizeList;
            return Prizes;
        }

        public void PlacePrizes()
        {
            foreach (Prize element in this.GetPrizes())
            {
                int x = element.X;
                int y = element.Y;

                //change to while loop
                if (Cells[x, y] == null)
                {
                    Cells[x, y] = element.Value;
                }
                else
                {
                    Cells[x + 1, y + 1] = element.Value;
                }
            }
        }

        public void PrintBoard()
        {
            string VerticalBorder = " +-+-+-+-+-+-+-+-+-+-+";
            Console.WriteLine(VerticalBorder);
            for (int i = 0; i < BoardDimensions; i++)
            {
                for (int j = 0; j < BoardDimensions; j++)
                {
                    string test = Cells[i, j];
                    if (Cells[i, j] == null)
                    {
                        Console.Write(Cells[i, j] + " |");
                    }
                    else
                    {
                        Console.Write(Cells[i, j] + "|");
                    }

                }
                Console.WriteLine($"\r\n{VerticalBorder}");
            }
            //Console.WriteLine(VerticalBorder);
        }

        //public Move(BoardOject target, Direction coordinates){}

        //get agent locations
        //get available/vacant spaces
    }
}
