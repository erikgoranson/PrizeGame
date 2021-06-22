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

        #region Board operations
        /// <summary>
        /// Int used to determine the x and y dimensions of the game board
        /// Will be squared
        /// </summary>
        public static int BoardDimensions { get; set; } = 11;

        /// <summary>
        /// 2D array used to contain all the <see cref="BoardObject"/>s in the game
        /// </summary>
        public BoardObject[,] Cells { get; set; } 

        /// <summary>
        /// Returns the <see cref="BoardObject"/> on the current board instance
        /// </summary>
        /// <param name="Target">The position to located on the board</param>
        /// <returns></returns>
        public BoardObject GetCell(BoardObject Target)
        {
            return Cells[Target.Y, Target.X];
        }

        public BoardObject GetCell(Direction Target)
        {
            return Cells[Target.Y, Target.X];
        }

        /// <summary>
        /// Sets the given cell on the game board using the coordinates provided in <see cref="NewPosition"/>
        /// </summary>
        /// <param name="NewPosition">The value to be set on the board</param>
        public void SetCell(BoardObject NewPosition)
        {
            //2D arrays are written as array[y][x] (backwards)
            this.Cells[NewPosition.Y, NewPosition.X] = NewPosition;
        }

        /// <summary>
        /// Sets the board cell located at the x/y of <see cref="NewPosition"/> with the <see cref="Value"/> object
        /// </summary>
        /// <param name="NewPosition"></param>
        /// <param name="Value"></param>
        public void SetCell(BoardObject NewPosition, Agent Value)
        {
            this.Cells[NewPosition.Y, NewPosition.X] = Value;
        }

        public void SetCell(Direction NewPosition, Agent Value)
        {
            this.Cells[NewPosition.Y, NewPosition.X] = Value;
        }

        /// <summary>
        /// Sets to board back to a starting position
        /// </summary>
        public void Reset()
        {
            Cells = new BoardObject[BoardDimensions, BoardDimensions];
            this.PlaceAgents();
            this.PlacePrizes();
        }

        /// <summary>
        /// Prints the current state of the game board in the Console
        /// </summary>
        public void PrintBoard()
        {
            string VerticalBorder = "+---+---+---+---+---+---+---+---+---+---+---+";
            string HorizontalBorder = "|";
            Console.WriteLine(VerticalBorder);
            for (int i = 0; i < BoardDimensions; i++)
            {
                string line = string.Empty;
                for (int j = 0; j < BoardDimensions; j++)
                {
                    line += (j == 0) ? HorizontalBorder : "";
                    if (Cells[i, j] == null)
                    {
                        line += $"   {HorizontalBorder}";
                    }
                    else
                    {
                        line += $" {Cells[i, j].Value} {HorizontalBorder}";
                    }

                }
                Console.WriteLine($"{line}\r\n{VerticalBorder}");
            }
        }

        /// <summary>
        /// Moves the <see cref="Player"/> to the coordinates given at <see cref="Direction"/>
        /// </summary>
        /// <param name="Player">The agent to move</param>
        /// <param name="direction">The target position</param>
        public void Move(Agent Player, Direction direction)
        {
            var TargetPosition = (this.GetCell(direction) != null) ? this.GetCell(direction) : new BoardObject();
            if (!TargetPosition.IsAgent)
            {
                if (TargetPosition.IsPrize)
                {
                    Player.Score += TargetPosition.PrizeValue;
                    Player.HasScored = TargetPosition.PrizeValue;
                    //remove prize from prizelist
                    this.Prizes.RemoveAll(item => item.ID == TargetPosition.ID);
                    //Console.WriteLine($"Agent {Player.Value} has scored {TargetPosition.PrizeValue}");
                }

                //move object to new position
                this.SetCell(direction, Player);

                //remove old position's value
                this.SetCell(Player, null);

                //update the position value stored in Player
                this.GetCell(direction).SetPosition(direction.NextPosition);
            }
        }
        #endregion

        #region Agent data
        private List<Agent> Agents { get; set; }

        /// <summary>
        /// A list of set spawn positions for agents to be placed at
        /// </summary>
        /// <returns></returns>
        internal List<BoardObject> StartingPoints()
        {
            List<BoardObject> StartPoints = new List<BoardObject>
            {
                //these are Y, X coordinates
                new BoardObject(4,4),
                new BoardObject(6,4),
                new BoardObject(4,6),
                new BoardObject(6,6),
            };
            return StartPoints;
        }

        /// <summary>
        /// Updates <see cref="Agents"/> with instances of Agent types and names to be used on placement
        /// Cannot exceed the count of spawn points
        /// </summary>
        internal void CreateAgents()
        {
            List<Agent> Agents = new List<Agent>
            {
                new MinDistanceAgent("A"),
                new MinDistanceAgent("B"),
                new MinDistanceAgent("C"),
                new MinDistanceAgent("D"),
            };
            this.Agents = Agents;
        }

        /// <summary>
        /// Retrieves all agents on the current gameboard with their current data
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Iterates through agents and spawn points to place new agents at each position
        /// </summary>
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
                agent.SetPosition(point); 
                this.SetCell(point, agent);
            }
        }
        #endregion

        #region Prize data
        /// <summary>
        /// Initializes a list of prizes to be placed on the board
        /// </summary>
        private List<Prize> Prizes { get; set; } = new Prizes(BoardDimensions).PrizeList;

        /// <summary>
        /// Returns a list of all unclaimed prizes
        /// </summary>
        /// <returns></returns>
        internal List<Prize> GetPrizes() 
        {
            return this.Prizes;
        }

        /// <summary>
        /// Distributes prizes on the board
        /// Will attempt to place the current prize in the next Northwest position if a spot is already occupied
        /// </summary>
        internal void PlacePrizes()
        {
            foreach (Prize element in this.GetPrizes())
            {
                while (this.GetCell(element) != null)
                {
                    element.X -= 1;
                    element.Y -= 1;
                }
                this.SetCell(element);
            }
        }
        #endregion
    }
}
