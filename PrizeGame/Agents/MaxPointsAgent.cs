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
    public class MaxPointsAgent : Agent
    {
        public MaxPointsAgent(string Name) : base(Name)
        {
        }

        public override void Move(Board board)
        {
            int maxValue = Int32.MinValue;
            Nullable<Direction> direction = null;
            foreach (Prize element in board.GetPrizes())
            {
                if (element.PrizeValue > maxValue && GetDirection(board, element) != null)
                {
                    maxValue = element.PrizeValue;
                    direction = GetDirection(board, element);
                }
            }
            if (direction.HasValue)
            {
                board.Move(this, direction.GetValueOrDefault());
            }
        }
    }
}
