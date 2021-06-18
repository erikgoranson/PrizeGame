using PrizeGame.BoardObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizeGame
{


    public class Directions //this was Direction.cs in the game
    {
        public int allowedMovePace = 1;

        //the gross IF block:
        //determine which direction something is from the target
        public void DetermineDirection(BoardObject Agent, BoardObject Target) //add an overload to allow +1 move in the future? was that part of the test?
        {
            if (Target.Y > Agent.Y && Target.X == Agent.X)
            {
                this.Direction = DIRECTIONS.North;
            }
            else if (Target.Y > Agent.Y && Target.X > Agent.X)
            {
                this.Direction = DIRECTIONS.Northeast;
            }
            else if (Target.Y == Agent.Y && Target.X > Agent.X)
            {
                this.Direction = DIRECTIONS.East;
            }
            else if (Target.Y < Agent.Y && Target.X > Agent.X)
            {
                this.Direction = DIRECTIONS.Southeast;
            }
            else if (Target.Y < Agent.Y && Target.X == Agent.X)
            {
                this.Direction = DIRECTIONS.South;
            }
            else if (Target.Y < Agent.Y && Target.X < Agent.X)
            {
                this.Direction = DIRECTIONS.Southwest;
            }
            else if (Target.Y == Agent.Y && Target.X < Agent.X)
            {
                this.Direction = DIRECTIONS.West;
            }
            else if (Target.Y > Agent.Y && Target.X < Agent.X)
            {
                this.Direction = DIRECTIONS.Northwest;
            }
            else
            {
                this.Direction = DIRECTIONS.NOT_SET;
            }
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
        public Position NextPosition { get; set; } //maybe rename

        internal DIRECTIONS Direction { get; set; } //the naming is getting awful here

        //provide the proper coordinates for whichever direction something is from the target (i.e., North = x+0,y+1)
        //diagonals always try to move on the Y axis by default
        public void DetermineNextPosition() //rename me
        {
            Position NextPosition = new Position { }; 

            switch (this.Direction)
            {
                case DIRECTIONS.North:
                case DIRECTIONS.Northeast:
                case DIRECTIONS.Northwest:
                    NextPosition.X = 0;
                    NextPosition.Y = 1;
                    /*this.NextPosition = new Direction
                    {
                        X = 0,
                        Y = 1,
                    };*/
                    break;
                case DIRECTIONS.East:
                    NextPosition.X = 1;
                    NextPosition.Y = 0;
                    break;
                case DIRECTIONS.West:
                    NextPosition.X = -1;
                    NextPosition.Y = 0;
                    break;
                case DIRECTIONS.South:
                case DIRECTIONS.Southeast:
                case DIRECTIONS.Southwest:
                    NextPosition.X = 0;
                    NextPosition.Y = -1;
                    break;
                default:
                    throw new InvalidOperationException($"{nameof(this.Direction)} failed - Unknown direction");
            }

            this.NextPosition = NextPosition;
        }

        public Position DetermineDifferenceCoordinates()
        {
            Position direction = new Position { }; //should this be something the class creates automatically, instead? on initialzing? 

            return direction;
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
