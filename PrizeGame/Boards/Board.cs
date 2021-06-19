using PrizeGame.Agents;
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
            this.PlaceAgents();
            this.PlacePrizes();
            this.PrintBoard();
        }

        public BoardObject[,] Cells { get; set; } 

        public int BoardDimensions { get; set; } = 11;

        public void Reset()
        {
            Cells = new BoardObject[BoardDimensions, BoardDimensions];
        }

        internal List<BoardObject> StartingPoints()
        {
            List<BoardObject> StartPoints = new List<BoardObject>
            {
                new BoardObject(4,4),
                new BoardObject(4,6),
                new BoardObject(6,4),
                new BoardObject(6,6),
            };
            return StartPoints;
        }

        internal List<Prize> GetPrizes() 
        {
            List<Prize> Prizes = new Prizes(this.BoardDimensions).PrizeList;
            return Prizes;
        }

        internal List<Agent> GetAgents()
        {
            List<Agent> Agents = new List<Agent>
            {
                new MinDistanceAgent("A"),
                new MinDistanceAgent("B"),
                new MinDistanceAgent("C"),
                new MinDistanceAgent("D"),
            };
            return Agents;
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

        internal void PlaceAgents()
        {
            for (int i = 0; i < this.StartingPoints().Count; i++)
            {
                if(this.StartingPoints().Count != this.GetAgents().Count)
                {
                    throw new ArgumentOutOfRangeException("PlaceAgent() failed - Agent count and starting points do not match");
                }

                var agent = this.GetAgents()[i];
                var point = this.StartingPoints()[i];
                agent.SetPosition(point);
                Cells[point.X, point.Y] = agent;
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

        public void Move(BoardObject Player, Direction direction)
        {
            //move object to new place
            //claim score if applicable
            //remove old position's value

            var NewPosition = Cells[direction.X, direction.Y];

            //assign score if exists
            int? claimedScore = (NewPosition.IsPrize) ? NewPosition.Score : (int?)null;

            //move to new position
            NewPosition = Player;

            //clear old position
            Player = null;

        }
    }
}
