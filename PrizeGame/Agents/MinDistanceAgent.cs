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
        public override void Move(Board board) 
        {
            int minDistance = Int32.MaxValue;
            Nullable<Direction> direction = null;
            foreach (Prize element in board.GetPrizes())
            {
                if(Distance(element) < minDistance /*&& GetDirection(board, element) == null*/)
                {
                    minDistance = Distance(element);
                    direction = GetDirection(board, element);
                }
            }
            if (direction.HasValue)
            {
                //board.Move(this, direction.GetValueOrDefault());
            }
        }

        private int Distance (BoardObject target)
        {
            return 1;
        }


    }
}
