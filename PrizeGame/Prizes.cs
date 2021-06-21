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
                    PrizeValue = score,
                    X = x,
                    Y = y,
                    Value = score.ToString(),
                    ID = i,
                }); 
            }
        }

        public class Prize : BoardObject
        {
            public override int PrizeValue { get; set; }

            public override bool IsPrize { get; set; } = true;

            public override int ID { get; set; }
        }
    }
}
