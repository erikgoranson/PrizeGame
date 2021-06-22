using PrizeGame.Agents;
using PrizeGame.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizeGame
{
    /// <summary>
    /// Initializes the components of the PrizeGame and runs the activity
    /// </summary>
    class Game
    {
        /// <summary>
        /// The game board
        /// </summary>
        public Board board; //set to private after debug

        public Game()
        {
            board = new Board();
        }

        /// <summary>
        /// Resets the current board instance
        /// </summary>
        public void Reset()
        {
            board.Reset();
            TopPlayer = null;
        }

        /// <summary>
        /// Begins game
        /// </summary>
        public void Start()
        {
            this.Play();
            this.End();
        }

        /// <summary>
        /// Initiates turns until game is complete
        /// </summary>
        private void Play()
        {
            while (board.GetPrizes().Any())
            {
                foreach (Agent Player in board.GetAgents())
                {
                    Player.Move(board);
                    board.PrintBoard();
                    Console.WriteLine("\r\n\r\n");
                    this.PrintScore(Player);
                }
            }
        }

        /// <summary>
        /// Prints final score details and declares winner
        /// </summary>
        private void End()
        {
            Console.WriteLine("\r\nFinal Results:");
            this.GetScore();
            Console.WriteLine($"\r\nAgent {TopPlayer.Value} wins");
        }

        /// <summary>
        /// The agent/player which has attained the highest overall score during this game
        /// </summary>
        private Agent TopPlayer { get; set; } 

        /// <summary>
        /// Retrieves scores for each player
        /// Prints individual values, stores highest scoring player for later use
        /// </summary>
        private void GetScore()
        {
            foreach (Agent agent in board.GetAgents())
            {
                Console.WriteLine($"Agent {agent.Value} = {agent.Score}");
                this.TopPlayer = (this.TopPlayer == null || agent.Score > this.TopPlayer.Score) ? agent : this.TopPlayer;
            }
        }

        /// <summary>
        /// Prints the current score when a <see cref="Player"/> has scored
        /// </summary>
        /// <param name="Player">The current agent/player that is active</param>
        private void PrintScore(Agent Player)
        {
            if (Player.HasScored != 0)
            {
                Console.WriteLine($"Agent {Player.Value} has scored {Player.HasScored}");
                Player.HasScored = 0;
                Console.WriteLine("\r\nCurrent Scores: ");
                this.GetScore();
            }
        }
    }
}
