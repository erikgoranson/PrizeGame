using PrizeGame.BoardObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizeGame
{
    /// <summary>
    /// Defines and maintains the prize methods, properties used by the game board
    /// </summary>
    class Prizes
    {
        /// <summary>
        /// Initializes new instance of the <see cref="Prizes"/> class
        /// </summary>
        /// <param name="boardDimensions">Int used to determine the x and y dimensions of the game board</param>
        public Prizes(int boardDimensions)
        {
            this.GeneratePrizes(boardDimensions);
        }

        //add overload for prizecount and maxprizevalue here

        /// <summary>
        /// A list of prizes to be placed on the game board
        /// </summary>
        public List<Prize> PrizeList = new List<Prize>();

        /// <summary>
        /// Random element used to prize generation
        /// </summary>
        private Random rnd = new Random();
        
        /// <summary>
        /// The amount of prizes to be generated
        /// </summary>
        private int PrizeCount { get; set; } = 10;

        /// <summary>
        /// The maximum value of any given prize
        /// </summary>
        private int MaxPrizeValue { get; set; } = 10;

        /// <summary>
        /// Populates the <see cref="PrizeList"/> with the set amount of prizes
        /// </summary>
        /// <param name="boardDimensions">Int used to determine the x and y dimensions of the game board</param>
        private void GeneratePrizes(int boardDimensions)
        {
            for (int i = 0; i < PrizeCount; i++)
            {
                int x = rnd.Next(1, boardDimensions);
                int y = rnd.Next(1, boardDimensions);
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

        /// <summary>
        /// Extends the <see cref="BoardObject"/> class to include prize-related details
        /// </summary>
        public class Prize : BoardObject
        {
            public override int PrizeValue { get; set; }

            public override bool IsPrize { get; set; } = true;

            public override int ID { get; set; }
        }
    }
}
