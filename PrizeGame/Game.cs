using PrizeGame.Agents;
using PrizeGame.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizeGame
{
    class Game
    {
        //do all the stuff here. build board, make prizes. keep score. instantiate players
        //turns, also, and scoring

        //how to handle ties goes here

        public bool Running
        {
            private set;
            get;
        }

        /// <summary>
        /// The game board
        /// </summary>
        private Board board;

        public Game()
        {
            //running state?
            //all new board
            //set current player
            //start turns (which is ended by endTurn, which calls start turn)

            Running = true;
            board = new Board();

        }

        public void Start()
        {

        }

        /// <summary>
        /// Called at beginning of each turn
        /// </summary>
        private void StartTurn()
        {
            //board.StartTurn();
            
            //board reset HasScored 
        }

        private void EndTurn()
        {

        }

        private void PrintScores()
        {
            //print score on every event of a prize being taken
            //values need to be stored somewhere else
        }

    }
}
