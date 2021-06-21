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

        public void SetPosition(BoardObject newPoint)
        {
            this.X = newPoint.X;
            this.Y = newPoint.Y;
            //this.Value = Name;
        }

        public virtual int AllowedPace { get; set; } = 1;

        public override bool IsAgent { get; set; } = true;

        public int Score { get; set; } = 0;

        public bool HasScored = false;

        //add previous move tracker here?

        internal Direction? GetDirection(Board Grid, BoardObject Target)
        {
            return new Direction(Grid, this, Target);
        }

        public abstract void Move(Board board);
    }
}
