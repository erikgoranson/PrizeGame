using PrizeGame.BoardObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizeGame
{
    class Prizes
    {
        public Prizes(int BoardConfines)
        {
            this.GeneratePrizes(BoardConfines);
        }

        //add overload for prizecount and maxprizevalue here

        public List<Prize> PrizeList = new List<Prize>();

        private Random rnd = new Random();
        
        private int PrizeCount { get; set; } = 10;

        private int MaxPrizeValue { get; set; } = 10;

        private void GeneratePrizes(int boardCells)
        {
            for (int i = 0; i < PrizeCount; i++)
            {
                int x = rnd.Next(1, boardCells);
                int y = rnd.Next(1, boardCells);
                int score = rnd.Next(1, MaxPrizeValue);
                PrizeList.Add(new Prize
                {
                    Score = score,
                    X = x,
                    Y = y,
                    Value = score.ToString(),
                }); 
            }
        }

        public class Prize : BoardObject
        {
            public int Score { get; set; }
        }
    }
}
