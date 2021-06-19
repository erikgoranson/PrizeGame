using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizeGame.BoardObjects
{
    public class BoardObject
    {
        public string Value { get; set; }

        /// <summary>
        /// This object's X position
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// This object's Y position
        /// </summary>
        public int Y { get; set; }

        public virtual bool IsPlayer { get; set; } = false;
    }
}
