using PrizeGame.BoardObjects;
using PrizeGame.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PrizeGame.Prizes;

namespace PrizeGame.Agents
{
    public class MinDistanceAgent : Agent
    {
        public MinDistanceAgent(string Name) : base(Name)
        {
        }

        public override void Move(Board board) 
        {
            int minDistance = Int32.MaxValue;
            Nullable<Direction> direction = null;
            foreach (Prize element in board.GetPrizes())
            {
                if (Distance(element) < minDistance && GetDirection(board, element) != null)
                {
                    minDistance = Distance(element);
                    direction = GetDirection(board, element);
                }
            }
            if (direction.HasValue)
            {
                board.Move(this, direction.GetValueOrDefault());
            }
        }

        private int Distance (BoardObject target)
        {
            var distance = Math.Sqrt((Math.Pow(target.X - this.X, 2) + Math.Pow(target.Y - this.Y, 2)));
            return Convert.ToInt32(distance);
        }
    }
}
