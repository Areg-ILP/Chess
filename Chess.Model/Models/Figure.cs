using Chess.Model.Helpers;
using System;
using System.Collections.Generic;

namespace Chess.Model.Models
{
    /// <summary>
    /// Base class for all figures
    /// </summary>
    public class Figure
    {
        /// <summary>
        /// figure position
        /// </summary>
        public Point Position { get; set; }
        /// <summary>
        /// figure color
        /// </summary>
        public ColorEnum Color { get; set; }
        
        /// <summary>
        /// possible points of figure
        /// </summary>
        protected virtual List<List<Point>> PossiblePoints
            => throw new NotImplementedException();

        /// <summary>
        /// value of chess rools
        /// </summary>
        public virtual int Value
            => throw new NotImplementedException();

        /// <summary>
        /// default ctor
        /// </summary>
        public Figure() { }

        /// <summary>
        /// Initial ctor
        /// </summary>
        /// <param name="startPosition">start position of figure</param>
        /// <param name="color">color of figure</param>
        protected Figure(Point startPosition, ColorEnum color)
        {
            Position = startPosition;
            Color = color;
        }

        /// <summary>
        /// Method vor moving figures
        /// </summary>
        /// <param name="nextPoint">next position</param>
        /// <param name="chessBoard">chessboard</param>
        /// <returns>true if move else false</returns>
        public virtual bool Move(Point nextPoint, ChessBoard chessBoard)
        {
            if (IsPossibleMove(nextPoint))
            {
                chessBoard.ChangeFigurePosition(this, nextPoint);
                Position = nextPoint;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get possible points
        /// </summary>
        /// <param name="chessBoard">chessboard</param>
        /// <returns>list of points</returns>
        public virtual List<Point> GetPossibleMoves(ChessBoard chessBoard)
        {
            var possibleMoves = new List<Point>();
            foreach (var currentPossibles in PossiblePoints)
            {
                foreach (var currentPoint in currentPossibles)
                {
                    var tempPoint = currentPoint + this.Position;
                    if (Point.IsValid(tempPoint) && IsPossibleMove(tempPoint))
                    {
                        if (chessBoard.GetFigureByPercolate(f => f.Position == tempPoint) == null)
                            possibleMoves.Add(tempPoint);
                        else
                        {
                            var figure = chessBoard.GetFigureByPercolate(f => f.Position == tempPoint);
                            if (figure.Color != this.Color)
                                possibleMoves.Add(tempPoint);
                            break;
                        }
                    }
                }
            }
            return possibleMoves;
        }

        public List<Point> GetSafeMovesFromCheck(ChessBoard chessBoard)
        {
            var safePossibles = new List<Point>();
            var possibleMoves = this.GetPossibleMoves(chessBoard);
            var kingToCheck = chessBoard.GetFigureByPercolate(f => f.ToString() == "King"
                                                                    && f.Color == this.Color);
            foreach (var possible in possibleMoves)
            {
                var startPosition = this.Position;
                chessBoard.RemovedFigure = default;
                this.MoveGod(possible, chessBoard);
                if (!chessBoard.IsPointUnderCheck(kingToCheck.Position, kingToCheck.Color))
                {
                    safePossibles.Add(possible);
                }
                this.MoveGod(startPosition, chessBoard);
                chessBoard.AddFigure(chessBoard.RemovedFigure);
                chessBoard.RemovedFigure = default;
            }
            return safePossibles;
        }

        /// <summary>
        /// Check is move is possible
        /// </summary>
        /// <param name="point">next point</param>
        /// <returns>true if point is possible else false</returns>
        protected bool IsPossibleMove(Point point)
        {
            foreach (var currentPossibles in this.PossiblePoints)
            {
                foreach (var currentPoint in currentPossibles)
                {
                    if (this.Position + currentPoint == point)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// MoveGod , for move like God
        /// </summary>
        /// <param name="nextPoint">next point</param>
        /// <param name="chessBoard">chessboard</param>
        public void MoveGod(Point nextPoint, ChessBoard chessBoard)
        {
            chessBoard.ChangeFigurePosition(this, nextPoint);
            Position = nextPoint;
        }
    }
}
