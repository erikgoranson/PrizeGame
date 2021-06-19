using PrizeGame.BoardObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PrizeGame.Prizes;

namespace PrizeGame.Boards
{
    public class Board
    {
        public Board()
        {
            this.Reset();
            this.PlacePrizes();
            this.PrintBoard();
        }

        public BoardObject[,] Cells { get; set; } //tiles?

        public int BoardDimensions { get; set; } = 11;

        public void Reset()
        {
            Cells = new BoardObject[BoardDimensions, BoardDimensions];
            //rules were hardcoded to players

            Cells[4, 4] = new BoardObject { Value = "A", }; //random
            Cells[4, 6] = new BoardObject { Value = "B", }; //MinDistance
            Cells[6, 4] = new BoardObject { Value = "C", }; //MaxDistance
            Cells[6, 6] = new BoardObject { Value = "D", }; //MyAgent
            //this will sometimes cause errors until a more permanent setup is complete
        }

        internal List<Prize> GetPrizes() 
        {
            List<Prize> Prizes = new Prizes(BoardDimensions).PrizeList;
            return Prizes;
        }

        internal void PlacePrizes()
        {
            foreach (Prize element in this.GetPrizes())
            {
                int x = element.X;
                int y = element.Y;

                //change to while loop
                if (Cells[x, y] == null)
                {
                    Cells[x, y] = element;
                }
                else
                {
                    Cells[x + 1, y + 1] = element;
                }
            }
        }

        private void PrintBoard()
        {
            ///string test = Cells[4, 4].Value;
            string VerticalBorder = " +-+-+-+-+-+-+-+-+-+-+";
            Console.WriteLine(VerticalBorder);
            for (int i = 0; i < BoardDimensions; i++)
            {
                for (int j = 0; j < BoardDimensions; j++)
                {
                    if (Cells[i, j] == null)
                    {
                        Console.Write(" |");
                    }
                    else
                    {
                        Console.Write(Cells[i, j].Value + "|");
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
