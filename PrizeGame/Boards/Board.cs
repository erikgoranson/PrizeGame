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

        public BoardObject GetCell(BoardObject Target)
        {
            return Cells[Target.Y, Target.X];
        }

        public BoardObject GetCell(Direction Target)
        {
            return Cells[Target.Y, Target.X];
        }

        public void SetCell(BoardObject Obj)
        {
            //2D arrays are written as array[y][x] (backwards)
            this.Cells[Obj.Y, Obj.X] = Obj;
        }

        public void SetCell(BoardObject Address, Agent Value)
        {
            //2D arrays are written as array[y][x] (backwards)
            this.Cells[Address.Y, Address.X] = Value;
        }

        public void SetCell(Direction Address, Agent Value)
        {
            //2D arrays are written as array[y][x] (backwards)
            BoardObject test = this.Cells[Address.Y, Address.X] = Value;
            //this.Cells[Address.Y, Address.X] = Value;
            this.Cells[Address.Y, Address.X] = test;
            //Cells.SetValue(Value, Address.Y, Address.X);
        }


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
                //these are Y, X coordinates
                new BoardObject(4,4),
                new BoardObject(4,6),
                new BoardObject(6,4),
                new BoardObject(6,6),
            };
            return StartPoints;
        }

        private List<Agent> Agents { get; set; }

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

        internal void CreateAgents()
        {
            List<Agent> Agents = new List<Agent>
            {
                new MinDistanceAgent("A"),
                new MinDistanceAgent("B"),
                new MinDistanceAgent("C"),
                new MinDistanceAgent("D"),
            };
            //return Agents;
            this.Agents = Agents;
        }

        internal void PlacePrizes()
        {
            foreach (Prize element in this.GetPrizes())
            {
                /*/2D arrays are written as array[y][x]
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
                    element.X = x;
                    element.Y = y;
                    Cells[y, x] = element; 
                }*/

                if (this.GetCell(element) != null)
                {
                    element.X -= 1;
                    element.Y -= 1;
                }
                this.SetCell(element);
                //bug to fix: this sometimes overwrites agents during placement?
            }
        }

        internal void PlaceAgents()
        {
            this.CreateAgents();
            for (int i = 0; i < this.StartingPoints().Count; i++)
            {
                if(this.StartingPoints().Count != this.Agents.Count)
                {
                    throw new ArgumentOutOfRangeException("PlaceAgent() failed - Agent count and starting points do not match");
                }

                var agent = this.Agents[i];
                var point = this.StartingPoints()[i];
                agent.SetPosition(point); //maybe add to base class or refactor
                //Cells[point.X, point.Y] = agent;
                this.SetCell(point, agent);
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
            //store data
            //var TargetPosition = this.GetCell(direction);
            var TargetPosition = (this.GetCell(direction) != null) ? this.GetCell(direction) : new BoardObject();

            //add score if exists
            if (!TargetPosition.IsAgent)
            {
                if (TargetPosition.IsPrize)
                {
                    Player.Score += TargetPosition.PrizeValue;
                    Player.HasScored = true;
                    //remove prize from prizelist
                    this.Prizes.RemoveAll(item => item.ID == TargetPosition.ID);
                    Console.WriteLine($"Agent {Player.Value} has scored {TargetPosition.PrizeValue}");
                }

                //move object to new position
                this.SetCell(direction, Player);

                //remove old position's value
                this.SetCell(Player, null);

                if(direction.NextMovement == null)
                {
                    Console.WriteLine("wut");
                }

                //update the position value stored in Player
                this.GetCell(direction).SetPosition(direction.NextPosition);
            }
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
                    Console.WriteLine("Current Scores: ");
                    foreach (Agent a in this.GetAgents())
                    {
                        Console.WriteLine($"Agent {a.Value} = {a.Score}");
                    }

                    //Console.WriteLine($"Agent {agent.Value} has scored {agent.Score}");
                    agent.HasScored = false; //add this to scoring method later
                }
            }
        }
    }
}
