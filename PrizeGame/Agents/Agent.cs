using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrizeGame.BoardObjects;
using PrizeGame.Boards;
using static PrizeGame.Prizes;

namespace PrizeGame.Agents
{
    public abstract class Agent : BoardObject 
    {
        public Agent(int X, int Y, string Name)
        {
            this.X = X;
            this.Y = Y;
            this.Value = Name;
        }

        public Agent(string Name) : this(0, 0, Name)
        {
            this.Value = Name;
        }

        /// <summary>
        /// The number of spaces at a time that this agent is allowed to advance
        /// </summary>
        public virtual int AllowedPace { get; set; } = 1;

        public override bool IsAgent { get; set; } = true;

        /// <summary>
        /// The total points an Agent has claimed by collecting prizes
        /// </summary>
        public int Score { get; set; } = 0;

        /// <summary>
        /// A bool to evaluate whether an agent claimed a prize in the last turn
        /// </summary>
        public int HasScored = 0;

        //add previous move tracker here?

        /// <summary>
        /// Returns the next position where an agent will move towards its targeted prize
        /// </summary>
        /// <param name="Grid">The current game board</param>
        /// <param name="Target">The target prize object</param>
        /// <returns></returns>
        internal Direction? GetDirection(Board Grid, BoardObject Target)
        {
            return new Direction(Grid, this, Target);
        }

        /// <summary>
        /// Determines which prize object to move towards and advances the agent in this direction
        /// </summary>
        /// <param name="board"></param>
        public abstract void Move(Board board);
    }
}
