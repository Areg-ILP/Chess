using Chess.Model.Helpers;
using System.Collections.Generic;

namespace Chess.Model.Models
{
    /// <summary>
    /// Figure type: Bishop 
    /// </summary>
    public class Bishop : Figure
    {
        /// <summary>
        /// intial constructor
        /// </summary>
        /// <param name="startPositon"></param>
        /// <param name="color"></param>
        public Bishop(Point startPositon, ColorEnum color) : base(startPositon, color) { }

        /// <summary>
        /// Possible points of figure
        /// </summary>
        protected override List<List<Point>> PossiblePoints => new List<List<Point>>()
        {
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
        /// Initial ctor
        /// </summary>
        public Bishop()
        {

        }

        /// <summary>
        /// Value of figure by chess rools
        /// </summary>
        public override int Value => 3;

        /// <summary>
        /// Convert tos tring figure name
        /// </summary>
        /// <returns>Name of figure</returns>
        public override string ToString()
        {
            return "Bishop";
        }
    }
}
