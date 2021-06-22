using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizeGame.BoardObjects
{
    public class BoardObject
    {
        public BoardObject()
        {
            this.X = 0;
            this.Y = 0;
        }

        public BoardObject(int X, int Y) : this(X, Y, string.Empty)
        {
            this.X = X;
            this.Y = Y;
        }

        public BoardObject(int X, int Y, string Value)
        {
            this.X = X;
            this.Y = Y;
            this.Value = Value;
        }

        public void SetPosition(BoardObject newPoint)
        {
            this.X = newPoint.X;
            this.Y = newPoint.Y;
            //this.Value = Name;
        }

        public string Value { get; set; }

        /// <summary>
        /// This object's X position
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// This object's Y position
        /// </summary>
        public int Y { get; set; }

        public virtual bool IsAgent { get; set; } = false;

        public virtual bool IsPrize { get; set; } = false;

        public virtual int PrizeValue { get; set; } = 0;

        public virtual int ID { get; set; }
    }
}
