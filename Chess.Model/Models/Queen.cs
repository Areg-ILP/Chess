using Chess.Model.Helpers;
using System.Collections.Generic;

namespace Chess.Model.Models
{
    /// <summary>
    /// Figure type: Queen
    /// </summary>
    public class Queen : Figure
    {
        /// <summary>
        /// Possible points of figure
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
            new List<Point>()
            {
                new Point(1,1),new Point(2,2),new Point(3,3),new Point(4,4),new Point(5,5),new Point(6,6),new Point(7,7)
            },
            new List<Point>()
            {
                new Point(-1,-1),new Point(-2,-2),new Point(-3,-3),new Point(-4,-4),new Point(-5,-5),new Point(-6,-6),new Point(-7,-7)
            },
            new List<Point>()
            {
                new Point(-1,1),new Point(-2,2),new Point(-3,3),new Point(-4,4),new Point(-5,5),new Point(-6,6),new Point(-7,7)
            },
            new List<Point>()
            {
                new Point(1,-1),new Point(2,-2),new Point(3,-3),new Point(4,-4),new Point(5,-5),new Point(6,-6),new Point(7,-7)
            }
        };

        /// <summary>
        /// default ctor
        /// </summary>
        public Queen() { }

        /// <summary>
        /// Initial ctor
        /// </summary>
        /// <param name="startPosition">start position of figure</param>
        /// <param name="color">color of figure</param>
        public Queen(Point startPosition, ColorEnum color) : base(startPosition, color) { }

        /// <summary>
        /// Value of figure by chess rools
        /// </summary>
        public override int Value => 9;

        /// <summary>
        /// Convert name of figure to string
        /// </summary>
        /// <returns>name to string</returns>
        public override string ToString()
        {
            return "Queen";
        }
    }
}
