using PrizeGame.BoardObjects;
using PrizeGame.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizeGame
{
    public class Direction 
    {
        private int AllowedPace = 1; //add an overload to allow +1 move in the future? was that part of the test?

        public Direction()
        {

        }

        //the gross IF block:
        //determine which direction something is from the target
        public void DetermineDirection(BoardObject Agent, BoardObject Target) 
        {
            if (Target.Y > Agent.Y && Target.X == Agent.X)
            {
                this.Move_Direction = DIRECTIONS.North;
            }
            else if (Target.Y > Agent.Y && Target.X > Agent.X)
            {
                this.Move_Direction = DIRECTIONS.Northeast;
            }
            else if (Target.Y == Agent.Y && Target.X > Agent.X)
            {
                this.Move_Direction = DIRECTIONS.East;
            }
            else if (Target.Y < Agent.Y && Target.X > Agent.X)
            {
                this.Move_Direction = DIRECTIONS.Southeast;
            }
            else if (Target.Y < Agent.Y && Target.X == Agent.X)
            {
                this.Move_Direction = DIRECTIONS.South;
            }
            else if (Target.Y < Agent.Y && Target.X < Agent.X)
            {
                this.Move_Direction = DIRECTIONS.Southwest;
            }
            else if (Target.Y == Agent.Y && Target.X < Agent.X)
            {
                this.Move_Direction = DIRECTIONS.West;
            }
            else if (Target.Y > Agent.Y && Target.X < Agent.X)
            {
                this.Move_Direction = DIRECTIONS.Northwest;
            }
            else
            {
                this.Move_Direction = DIRECTIONS.NOT_SET;
            }
        }

        public bool CellSpaceAvailable(Board Grid)
        {
            return (Grid.Cells[NextMovement.X, NextMovement.Y] != null) ? true : false;
        }

        public struct Position 
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        /*
        Y + 1, X + 0 = up
        Y - 0, X + 0 = down
        Y + 0, X - 1 = right
        Y + 0, X + 1 = left
         */

        //need class to determine everything on new instance. new instance would then require board and target element(s)

        /// <summary>
		/// Gets or sets the direction wrapped by this class.
		/// </summary>
        public Position NextPosition { get; set; }

        public Position NextMovement { get; set; }

        internal DIRECTIONS Move_Direction { get; set; } 

        //provide the proper coordinates for whichever direction something is from the target (i.e., North = x+0,y+1)
        //diagonals always try to move on the Y axis by default
        public void DetermineNextMovement() //rename me
        {
            Position NextMovement = new Position(); 

            switch (this.Move_Direction)
            {
                case DIRECTIONS.North:
                case DIRECTIONS.Northeast:
                case DIRECTIONS.Northwest:
                    NextMovement.X = 0;
                    NextMovement.Y = AllowedPace;
                    /*this.NextPosition = new Direction
                    {
                        X = 0,
                        Y = 1,
                    };*/
                    break;
                case DIRECTIONS.East:
                    NextMovement.X = AllowedPace;
                    NextMovement.Y = 0;
                    break;
                case DIRECTIONS.West:
                    NextMovement.X = -AllowedPace;
                    NextMovement.Y = 0;
                    break;
                case DIRECTIONS.South:
                case DIRECTIONS.Southeast:
                case DIRECTIONS.Southwest:
                    NextMovement.X = 0;
                    NextMovement.Y = -AllowedPace;
                    break;
                default:
                    throw new InvalidOperationException($"{nameof(this.Move_Direction)} failed - Unknown direction");
            }

            this.NextMovement = NextMovement;
        }

        public void DetermineNextPosition(Board Grid, BoardObject Agent) //is this GetDirection()?
        {
            if (this.CellSpaceAvailable(Grid))
            {
                this.NextPosition = new Position
                {
                    X = Agent.X + NextMovement.X,
                    Y = Agent.Y + NextMovement.Y,
                };
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
