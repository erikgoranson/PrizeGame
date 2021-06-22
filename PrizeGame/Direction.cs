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

        /// <summary>
        /// Evaluates the difference between the <see cref="Agent"/> obj and <see cref="Target"/> cell space to determine which direction the Agent will move
        /// </summary>
        /// <param name="Agent">The active agent that will need to move</param>
        /// <param name="Target">The position where the agent wants to move</param>
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

        /// <summary>
        /// Stores the actual calculated x/y position of an agent's next move
        /// </summary>
        public BoardObject NextPosition { get;  set; }

        /// <summary>
        /// Stores the difference returned by <see cref="DetermineMovement(Agent)"/> to be added to an active agent's current X/Y position for determining the next position where they will move
        /// </summary>
        private BoardObject NextMovement { get;  set; }

        internal DIRECTIONS Move_Direction { get; set; }

        //provide the proper coordinates for whichever direction something is from the target (i.e., North = x+0,y+1)
        //diagonals always try to move on the Y axis by default
        /// <summary>
        /// References possible <see cref="DIRECTIONS"/> and returns the correct int needed on the X or Y axis in order for the agent to move that direction
        /// The int returned depends on the agent's allowed pace
        /// Diagonal movements are not allowed and will be changed to the North or South
        /// </summary>
        /// <param name="Player">The active agent that will need to move</param>
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

        /// <summary>
        /// Adds <see cref="NextMovement"/> to the agent's current position to determine where they will move
        /// </summary>
        /// <param name="Grid">The current game board</param>
        /// <param name="Agent">The active agent that will need to move</param>
        public void DetermineNextPosition(Board Grid, BoardObject Agent) 
        {
            BoardObject NextPosition = new BoardObject
            {
                X = Agent.X + NextMovement.X,
                Y = Agent.Y + NextMovement.Y,
            };

            if (Grid.GetCell(NextPosition) == null || Grid.GetCell(NextPosition).IsPrize) 
            {
                this.NextPosition = NextPosition;
                this.X = NextPosition.X;
                this.Y = NextPosition.Y;
            } 
            else
            {
                this.NextPosition = new BoardObject();
                this.X = NextPosition.X;
                this.Y = NextPosition.Y;
            }
        }

        /// <summary>
        /// The possible directions an agent can move across the board
        /// </summary>
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
