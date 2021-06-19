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
        public Agent()
        {
        }

        public virtual int AllowedPace { get; set; } = 1;

        public override bool IsPlayer { get; set; } = true;

        //add previous move tracker here?

        internal Direction GetDirection(Board Grid, BoardObject Target)
        {
            return new Direction(Grid, this, Target);
        }

        public abstract void Move(Board board);
    }
}
