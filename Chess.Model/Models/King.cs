using Chess.Model.Helpers;
using System;
using System.Collections.Generic;

namespace Chess.Model.Models
{
    /// <summary>
    /// Figure type: King
    /// </summary>
    public class King : Figure
    {
        /// <summary>
        /// Possible points for figure
        /// </summary>
        protected override List<List<Point>> PossiblePoints => new List<List<Point>>
        {
            new List<Point>()
            {
                new Point(1,0)
            },
            new List<Point>()
            {
                new Point(0,1)                
            },
            new List<Point>()
            {
                new Point(1,-1)
            },
            new List<Point>()
            {
                new Point(-1,1)
            },
            new List<Point>()
            {
                new Point(-1,0)
            },
            new List<Point>()
            {
                 new Point(0,-1),
            },
            new List<Point>()
            {
                new Point(-1,-1)
            },
            new List<Point>()
            {
                new Point(1,1)
            }
        };

        /// <summary>
        /// default ctor
        /// </summary>
        public King() { }

        /// <summary>
        /// Initial ctor
        /// </summary>
        /// <param name="startPosition">start position of figure</param>
        /// <param name="color">color of figure</param>
        public King(Point startPosition, ColorEnum color) : base(startPosition, color) { }

        /// <summary>
        /// Value of figure by chess rools
        /// </summary>
        public override int Value => 0;
        
        /// <summary>
        /// Method vor move figure
        /// </summary>
        /// <param name="nextPoint">next position</param>
        /// <param name="chessBoard">givenc chessboard</param>
        /// <returns>true if moved else false</returns>
        public override bool Move(Point nextPoint, ChessBoard chessBoard)
        {
            if(IsCastlingMove(nextPoint))
            {
                var rookPosition = (nextPoint.Y - this.Position.Y > 0) ? new Point(this.Position.X,7) : new Point(this.Position.X,0);
                var rookCastlingPosition = (nextPoint.Y - this.Position.Y > 0) ? new Point(this.Position.X, this.Position.Y + 1)
                                                                            : new Point(this.Position.X, this.Position.Y - 1);
                var rookToCastle = chessBoard.GetFigureByPercolate(r => r.Position == rookPosition);
                this.MoveGod(nextPoint, chessBoard);
                rookToCastle.MoveGod(rookCastlingPosition, chessBoard);
                _moved = true;
                return _moved;
            }
            bool movedFlag = base.Move(nextPoint, chessBoard);
            if (movedFlag) _moved = true;
            return movedFlag;
        }

        /// <summary>
        /// Standart possible points for figure
        /// </summary>
        /// <returns>list of points</returns>
        public List<Point> GetPossiblePoints(ChessBoard chessBoard)
        {
            var possibleMoves = new List<Point>();
            for (int i = 0; i < PossiblePoints.Count; i++)
            {
                var currentPossibles = PossiblePoints[i];
                for (int j = 0; j < currentPossibles.Count; j++)
                {
                    var tempPoint = Position + PossiblePoints[i][j];
                    var figureToCheck = chessBoard.GetFigureByPercolate(f => f.Position == tempPoint);
                    if (figureToCheck != null && figureToCheck?.Color == this.Color)
                        continue;
                    if (Point.IsValid(tempPoint) && IsPossibleMove(tempPoint))
                    {
                        possibleMoves.Add(tempPoint);
                    }
                }
            }
            return possibleMoves;
        }
        
        /// <summary>
        /// Get possible moves of figure
        /// </summary>
        /// <param name="chessBoard">chessboard</param>
        /// <returns>list of points</returns>
        public override List<Point> GetPossibleMoves(ChessBoard chessBoard)
        {
            var possiblePoints = GetPossiblePoints(chessBoard);
            var result = new List<Point>();
            foreach (var currentPossible in possiblePoints)
            {
                if (IsSafeMove(currentPossible, chessBoard))
                    result.Add(currentPossible);
            }
            return result;
        }

        /// <summary>
        /// Check is given point is safe
        /// </summary>
        /// <param name="point">next position</param>
        /// <param name="chessBoard">chessboard</param>
        /// <returns>true if given point is safe else false</returns>
        private bool IsSafeMove(Point point,ChessBoard chessBoard)
        {
            var enemyFigures = chessBoard.GetAllFiguresByPercolate(f => f.Color != this.Color);
            List<Point> dangerPoints;
            foreach (var enemy in enemyFigures)
            {
                switch(enemy.ToString())
                {
                    case "King":
                        dangerPoints = ((King)enemy).GetPossiblePoints(chessBoard);
                        break;
                    case "Pawn":
                        dangerPoints = ((Pawn)enemy).GetEatPoints(chessBoard);
                        break;
                    default:
                        dangerPoints = enemy.GetPossibleMoves(chessBoard);
                        break;
                }
                foreach (var dangerPoint in dangerPoints)
                {
                    if (point == dangerPoint)
                        return false;
                }
            }
            return true;
        }

        #region Castling

        /// <summary>
        /// flag to check is figure moved
        /// </summary>
        private bool _moved;

        /// <summary>
        /// Get castling points for king
        /// </summary>
        /// <param name="chessBoard">chessboard</param>
        /// <returns>list of points</returns>
        public List<Point> GetCastlingPoints(ChessBoard chessBoard)
        {
            var castlingPoints = new List<Point>();
            if( !_moved && !chessBoard.IsPointUnderCheck(this.Position, this.Color))
            {
                var rooks = chessBoard.GetAllFiguresByPercolate(r => r.ToString() == "Rook"
                                                                    && r.Color == this.Color);
                if (rooks != null)
                {
                    foreach (var rook in rooks)
                    {
                        if(!((Rook)rook).Moved)
                        {
                            if(rook.Position.Y == 0)
                            {
                                var leftPath = new List<Point>();
                                for (int y = 1; y < this.Position.Y; y++)
                                {
                                    leftPath.Add(new Point(this.Position.X, y));
                                }
                                if (IsSafePath(leftPath, chessBoard))
                                    castlingPoints.Add(new Point(this.Position.X, this.Position.Y - 2));
                            }
                            else if(rook.Position.Y == 7)
                            {
                                var rightPath = new List<Point>();
                                for (int y = this.Position.Y + 1; y < 7; y++)
                                {
                                    rightPath.Add(new Point(this.Position.X, y));
                                }
                                if (IsSafePath(rightPath, chessBoard))
                                    castlingPoints.Add(new Point(this.Position.X, this.Position.Y + 2));
                            }
                        }
                    }
                }
            }
            return castlingPoints;
        }

        /// <summary>
        /// Check is safe path to castling for king
        /// </summary>
        /// <param name="castlingPath">castling path of points list</param>
        /// <param name="chessBoard">chessboard</param>
        /// <returns>true if safe path else false</returns>
        private bool IsSafePath(List<Point> castlingPath,ChessBoard chessBoard)
        {
            foreach (var pathPoint in castlingPath)
            {
                if (chessBoard.GetFigureByPercolate(f => f.Position == pathPoint) != null
                        || chessBoard.IsPointUnderCheck(pathPoint, this.Color))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check is castling point
        /// </summary>
        /// <param name="nextPoint">next point</param>
        /// <returns>true if castling point else false</returns>
        private bool IsCastlingMove(Point nextPoint)
        {
            return Math.Abs(nextPoint.Y - this.Position.Y) == 2;
        }

        #endregion

        /// <summary>
        /// Convert name of figure to string
        /// </summary>
        /// <returns>name to string</returns>
        public override string ToString()
        {
            return "King";
        }
    }
}
