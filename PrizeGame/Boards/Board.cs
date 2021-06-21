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
            this.PrintBoard();
        }

        public BoardObject[,] Cells { get; set; } 

        public static int BoardDimensions { get; set; } = 11;

        public void Reset()
        {
            Cells = new BoardObject[BoardDimensions, BoardDimensions];
            this.PlaceAgents();
            this.PlacePrizes();
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
            //List<Prize> Prizes = new Prizes(this.BoardDimensions).PrizeList;
            return this.Prizes;
        }

        private List<Prize> Prizes { get; set; } = new Prizes(BoardDimensions).PrizeList;

        internal List<BoardObject> GetAgents()
        {
            List<BoardObject> Agents = new List<BoardObject>();
            foreach (BoardObject Agent in this.Cells)
            {
                if (Agent != null && Agent.IsAgent == true)
                {
                    Agents.Add(Agent);
                }
            }
            return Agents;
        }

        internal List<Agent> CreateAgents()
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
                //2D arrays are written as array[y][x]
                int x = element.Y;
                int y = element.X;

                //change to while loop
                if (Cells[x, y] == null)
                {
                    Cells[x, y] = element;
                }
                else
                {
                    x = x - 1; //need a better idea for moving coordinates when null but avoiding out of bounds
                    y = y - 1;
                    element.X = y;
                    element.Y = x;
                    Cells[x, y] = element; 
                }
                //bug to fix: this sometimes overwrites agents during placement?
            }
        }

        internal void PlaceAgents()
        {
            for (int i = 0; i < this.StartingPoints().Count; i++)
            {
                if(this.StartingPoints().Count != this.CreateAgents().Count)
                {
                    throw new ArgumentOutOfRangeException("PlaceAgent() failed - Agent count and starting points do not match");
                }

                var agent = this.CreateAgents()[i];
                var point = this.StartingPoints()[i];
                agent.SetPosition(point);
                Cells[point.X, point.Y] = agent;
            }
        }

        private void PrintBoard()
        {
            ///string test = Cells[4, 4].Value;
            string VerticalBorder = "+---+---+---+---+---+---+---+---+---+---+---+";
            string HorizontalBorder = "|";
            Console.WriteLine(VerticalBorder);
            for (int i = 0; i < BoardDimensions; i++)
            {
                string line = string.Empty;
                //Console.WriteLine((i == 0) ? HorizontalBorder : "");
                for (int j = 0; j < BoardDimensions; j++)
                {
                    line += (j == 0) ? HorizontalBorder : "";
                    if (Cells[i, j] == null)
                    {
                        //Console.Write(" |");
                        line += $"   {HorizontalBorder}";
                    }
                    else
                    {
                        //Console.Write(Cells[i, j].Value + "|");
                        line += $" {Cells[i, j].Value} {HorizontalBorder}";
                    }

                }
                //Console.WriteLine($"\r\n{VerticalBorder}");
                Console.WriteLine($"{line}\r\n{VerticalBorder}");
            }
            //Console.WriteLine(VerticalBorder);
        }

        public void Move(Agent Player, Direction direction)
        {
            //move object to new place
            //claim score if applicable
            //remove old position's value

            //store data
            var TargetPosition = Cells[direction.X, direction.Y];

            //add score if exists
            if (TargetPosition != null && TargetPosition.IsPrize)
            {
                Player.Score += TargetPosition.PrizeValue;
                Player.HasScored = true;
                //remove prize from prizelist
                this.Prizes.RemoveAll(item => item.ID == TargetPosition.ID);
            }

            //move object to new position
            Cells[direction.X, direction.Y] = Player;

            //remove old position's value
            Cells[Player.X, Player.Y] = null;

        }

        public void StartTurn()
        {
            foreach (Agent agent in this.GetAgents())
            {
                agent.Move(this);
                this.PrintBoard();
                Console.WriteLine("\r\n\r\n");
                if (agent.HasScored)
                {
                    //print scores
                    Console.WriteLine($"Agent {agent.Value} has scored {agent.Score}");
                }
            }
        }
    }
}
