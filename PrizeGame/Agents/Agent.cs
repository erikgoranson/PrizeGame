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
    public class Agent : BoardObject
    {
        /*internal Agent()
        {

        }*/



        public virtual int AllowedPace { get; set; }  = 1;

        public override bool IsPlayer { get; set; } = true;

        //add previous move tracker here?




        internal void GetDirection(Board B, BoardObject Target)
        {

        }
    }
}
