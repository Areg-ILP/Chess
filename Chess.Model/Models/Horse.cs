using Chess.Model.Helpers;
using System.Collections.Generic;

namespace Chess.Model.Models
{
    /// <summary>
    /// Figure type: Horse
    /// </summary>
    public class Horse : Figure
    {
        /// <summary>
        /// Possible points of figure
        /// </summary>
        protected override List<List<Point>> PossiblePoints => new List<List<Point>>
        {
            new List<Point>()
            {
                new Point(2,1)
            },
            new List<Point>()
            {
                new Point(1,2),
            },
            new List<Point>()
            {
                new Point(-1,2)
            },
            new List<Point>()
            {
                new Point(-2,1),
            },
            new List<Point>()
            {
                new Point(-2,-1)
            },
            new List<Point>()
            {
                new Point(-1,-2),
            },
            new List<Point>()
            {
                new Point(1,-2)
            },
            new List<Point>()
            {
                new Point(2,-1)
            }
        };

        /// <summary>
        /// default ctor
        /// </summary>
        public Horse() {}

        /// <summary>
        /// Initial ctor
        /// </summary>
        /// <param name="startPosition">start position of figure</param>
        /// <param name="color">color of figure</param>
        public Horse(Point startPosition, ColorEnum color) : base(startPosition, color) { }

        /// <summary>
        /// Value of figure by chess rools
        /// </summary>
        public override int Value => 3;

        /// <summary>
        /// Convert name of figure too string
        /// </summary>
        /// <returns>name to string</returns>
        public override string ToString()
        {
            return "Horse";
        }
    }
}
