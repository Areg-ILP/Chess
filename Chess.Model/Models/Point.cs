using Chess;

namespace Chess.Model.Helpers
{
    /// <summary>
    /// Class for calculate figure positions 
    /// </summary>
    public class Point
    {
        /// <summary>
        /// x coordinate of point
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// y coordinate of point
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// intial ctor
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        public Point(int x,int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Validation of point
        /// </summary>
        /// <param name="point">Some point</param>
        /// <returns>if point is valid return true else false</returns>
        public static bool IsValid(Point point)
        {
            return point.X >= 0 && point.X <= 7 && point.Y >= 0 && point.Y <= 7;
        }

        /// <summary>
        /// Operator overloading (==)
        /// </summary>
        /// <param name="firstPoint">first point</param>
        /// <param name="secondPoint">second point</param>
        /// <returns>return true if points are equals else false</returns>
        public static bool operator ==(Point firstPoint, Point secondPoint)
        {
            return firstPoint?.X == secondPoint?.X && firstPoint?.Y == secondPoint?.Y;
        }

        /// <summary>
        /// Operator overloading (==)
        /// </summary>
        /// <param name="firstPoint">first point</param>
        /// <param name="secondPoint">second point</param>
        /// <returns>true if points arent equal else false </returns>
        public static bool operator !=(Point firstPoint, Point secondPoint)
        {
            return firstPoint?.X != secondPoint?.X && firstPoint?.Y != secondPoint?.Y;
        }

        /// <summary>
        /// Operator overloading
        /// </summary>
        /// <param name="firstPoint">first point</param>
        /// <param name="secondPoint">second point</param>
        /// <returns>sum of two points</returns>
        public static Point operator +(Point firstPoint, Point secondPoint)
        {
            return new Point(firstPoint.X + secondPoint.X, firstPoint.Y + secondPoint.Y);
        }

    }
}
