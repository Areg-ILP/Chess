using Chess.Model.Helpers;
using System.Collections.Generic;

namespace Chess.Model.Models
{
    /// <summary>
    /// Figure type: Rook
    /// </summary>
    public class Rook : Figure
    {
        /// <summary>
        /// Possible points for figure
        /// </summary>
        protected override List<List<Point>> PossiblePoints => new List<List<Point>>
        {
            new List<Point>()
            {
                new Point(0,1),new Point(0,2),new Point(0,3),new Point(0,4),new Point(0,5),new Point(0,6),new Point(0,7)
            },
            new List<Point>()
            {
                new Point(1,0),new Point(2,0),new Point(3,0),new Point(4,0),new Point(5,0),new Point(6,0),new Point(7,0)
            },
            new List<Point>()
            {
                new Point(0,-1),new Point(0,-2),new Point(0,-3),new Point(0,-4),new Point(0,-5),new Point(0,-6),new Point(0,-7)
            },
            new List<Point>()
            {
                new Point(-1,0),new Point(-2,0),new Point(-3,0),new Point(-4,0),new Point(-5,0),new Point(-6,0),new Point(-7,0)
            },
        };

        /// <summary>
        /// Default ctor
        /// </summary>
        public Rook() { }

        /// <summary>
        /// initial ctor
        /// </summary>
        /// <param name="startPosition">start position of figure</param>
        /// <param name="color">color of figure</param>
        public Rook(Point startPosition, ColorEnum color) : base(startPosition, color) { }

        /// <summary>
        /// Method vor moving rook
        /// </summary>
        /// <param name="nextPoint">next position</param>
        /// <param name="chessBoard">chessboard</param>
        /// <returns>true if move else false</returns>
        public override bool Move(Point nextPoint, ChessBoard chessBoard)
        {
            bool result = base.Move(nextPoint, chessBoard);
            if (result) Moved = result;
            return result;
        }

        /// <summary>
        /// Move counter for castling
        /// </summary>
        public bool Moved { get; private set; }

        /// <summary>
        /// Value of figure by chess rools
        /// </summary>
        public override int Value => 5;

        /// <summary>
        /// Convert name of figure to string
        /// </summary>
        /// <returns>name to string</returns>
        public override string ToString()
        {
            return "Rook";
        }
    }
}
