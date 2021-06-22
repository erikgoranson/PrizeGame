using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizeGame.BoardObjects
{
    /// <summary>
    /// Contains data about a given cell on the game board
    /// </summary>
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

        /// <summary>
        /// Resets this cell's position to the provided xy coordinates
        /// </summary>
        /// <param name="NewPosition">The new position to overwrite this cell's contain X/Y data</param>
        public void SetPosition(BoardObject NewPosition)
        {
            this.X = NewPosition.X;
            this.Y = NewPosition.Y;
            //this.Value = Name;
        }

        /// <summary>
        /// The string value returned by this cell
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// This object's X position
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// This object's Y position
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Bool for evaluating whether a cell is an agent
        /// </summary>
        public virtual bool IsAgent { get; set; } = false;

        /// <summary>
        /// Bool for evaluating whether a cell is a prize value
        /// </summary>
        public virtual bool IsPrize { get; set; } = false;

        /// <summary>
        /// Contains a cell's prize value, if applicable
        /// </summary>
        public virtual int PrizeValue { get; set; } = 0;

        /// <summary>
        /// Contains a cell's ID value
        /// Used by prizes
        /// </summary>
        public virtual int ID { get; set; }
    }
}
