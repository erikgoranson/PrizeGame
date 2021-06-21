using PrizeGame.Agents;
using PrizeGame.BoardObjects;
using PrizeGame.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizeGame
{

    public struct Direction 
    {
        public Direction(Board Grid, Agent Player, BoardObject Target) : this()
        {
            this.DetermineDirection(Player, Target);
            this.DetermineMovement(Player);
            this.DetermineNextPosition(Grid, Player);
            
        }

        /// <summary>
        /// The new X direction
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The new Y direction
        /// </summary>
        public int Y { get; set; }

        //the gross IF block:
        //determine which direction something is from the target
        public void DetermineDirection(BoardObject Agent, BoardObject Target) 
        {
            if (Target.Y < Agent.Y && Target.X == Agent.X)
            {
                this.Move_Direction = DIRECTIONS.North;
            }
            else if (Target.Y < Agent.Y && Target.X > Agent.X)
            {
                this.Move_Direction = DIRECTIONS.Northeast;
            }
            else if (Target.Y == Agent.Y && Target.X > Agent.X)
            {
                this.Move_Direction = DIRECTIONS.East;
            }
            else if (Target.Y > Agent.Y && Target.X > Agent.X)
            {
                this.Move_Direction = DIRECTIONS.Southeast;
            }
            else if (Target.Y > Agent.Y && Target.X == Agent.X)
            {
                this.Move_Direction = DIRECTIONS.South;
            }
            else if (Target.Y > Agent.Y && Target.X < Agent.X)
            {
                this.Move_Direction = DIRECTIONS.Southwest;
            }
            else if (Target.Y == Agent.Y && Target.X < Agent.X)
            {
                this.Move_Direction = DIRECTIONS.West;
            }
            else if (Target.Y < Agent.Y && Target.X < Agent.X)
            {
                this.Move_Direction = DIRECTIONS.Northwest;
            }
            else
            {
                this.Move_Direction = DIRECTIONS.NOT_SET;
            }
        }

        public BoardObject NextPosition { get;  set; }

        public BoardObject NextMovement { get;  set; }

        internal DIRECTIONS Move_Direction { get; set; } 

        //provide the proper coordinates for whichever direction something is from the target (i.e., North = x+0,y+1)
        //diagonals always try to move on the Y axis by default
        public void DetermineMovement(Agent Player) //rename me
        {
            BoardObject NextMovement = new BoardObject(); 

            switch (this.Move_Direction)
            {
                case DIRECTIONS.North:
                case DIRECTIONS.Northeast:
                case DIRECTIONS.Northwest:
                    NextMovement.X = 0;
                    NextMovement.Y = -Player.AllowedPace;
                    /*this.NextPosition = new Direction
                    {
                        X = 0,
                        Y = 1,
                    };*/
                    break;
                case DIRECTIONS.East:
                    NextMovement.X = Player.AllowedPace;
                    NextMovement.Y = 0;
                    break;
                case DIRECTIONS.West:
                    NextMovement.X = -Player.AllowedPace;
                    NextMovement.Y = 0;
                    break;
                case DIRECTIONS.South:
                case DIRECTIONS.Southeast:
                case DIRECTIONS.Southwest:
                    NextMovement.X = 0;
                    NextMovement.Y = Player.AllowedPace;
                    break;
                default:
                    throw new InvalidOperationException($"{nameof(this.Move_Direction)} failed - Unknown direction");
            }

            this.NextMovement = NextMovement;
        }

        public void DetermineNextPosition(Board Grid, BoardObject Agent) 
        {
            BoardObject NextPosition = new BoardObject
            {
                X = Agent.X + NextMovement.X,
                Y = Agent.Y + NextMovement.Y,
            };

            //fix this. so long
            if (Grid.Cells[NextPosition.X, NextPosition.Y] == null || Grid.Cells[NextPosition.X, NextPosition.Y].IsPrize)
            {
                this.NextPosition = NextPosition;
                this.X = NextPosition.X;
                this.Y = NextPosition.Y;
            }
        }

        public enum DIRECTIONS 
        {
            NOT_SET,
            North,
            Northeast,
            East,
            Southeast,
            South,
            Southwest,
            West,
            Northwest
        }

    }
}
