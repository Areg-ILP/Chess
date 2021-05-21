using Chess.Model.Helpers;
using System.Collections.Generic;

namespace Chess.Model.Models
{
    public class Pawn : Figure
    {
        private enum PawnIdentifier
        {
            Top,
            Bot
        }

        /// <summary>
        /// Pawn identifier
        /// </summary>
        private PawnIdentifier identifier;

        /// <summary>
        /// Possible points of Figure
        /// </summary>
        protected override List<List<Point>> PossiblePoints => new List<List<Point>>()
        {
            new List<Point>()
            {
                new Point(1,0)
            },
            new List<Point>()
            {
                new Point(-1,0)
            },
            new List<Point>()
            {
                new Point(2,0)
            },
            new List<Point>()
            {
                new Point(-2,0)
            },
            new List<Point>()
            {
                new Point(1,1)
            },
            new List<Point>()
            {
                new Point(-1,-1)
            },
            new List<Point>()
            {
                new Point(1,-1)
            },
            new List<Point>()
            {
                new Point(-1,1)
            }
        };

        /// <summary>
        /// default ctor
        /// </summary>
        public Pawn()
        {

        }

        /// <summary>
        /// Initial ctor
        /// </summary>
        /// <param name="startPosition">start position</param>
        /// <param name="color">color of figure</param>
        public Pawn(Point startPosition, ColorEnum color) : base(startPosition, color)
        {
            identifier = (startPosition.X == 1) ? PawnIdentifier.Top : PawnIdentifier.Bot;
        }

        /// <summary>
        /// Value of figure
        /// </summary>
        public override int Value => 1;

        /// <summary>
        /// Method to get possible Moves of figure
        /// </summary>
        /// <param name="chessBoard">chessboard</param>
        /// <returns>return list of points</returns>
        public override List<Point> GetPossibleMoves(ChessBoard chessBoard)
        {
            var possibleMoves = GetPossiblePoints(chessBoard);
            possibleMoves.AddRange(GetEatPoints(chessBoard));
            return possibleMoves;
        }

        /// <summary>
        /// Standart possible points for figure
        /// </summary>
        /// <returns>list of points</returns>
        private List<Point> GetPossiblePoints(ChessBoard chessBoard)
        {
            var possiblePoints = new List<Point>();
            var possibleMoves = new List<Point>();

            //standart possible points
            if (identifier == PawnIdentifier.Top)
            {
                possiblePoints.Add(PossiblePoints[0][0]);
                //boost points
                if (this.Position.X == 1)
                    possiblePoints.Add(PossiblePoints[2][0]);
            }
            else
            {
                possiblePoints.Add(PossiblePoints[1][0]);
                //bost points
                if (this.Position.X == 6)
                    possiblePoints.Add(PossiblePoints[3][0]);
            }
            
            foreach (var currentPossible in possiblePoints)
            {
                var tempPoint = this.Position + currentPossible;
                if (Point.IsValid(tempPoint) && IsPossibleMove(tempPoint)
                     && chessBoard.GetFigureByPercolate(f => f.Position == tempPoint) == null)
                    possibleMoves.Add(tempPoint);
                else break;
            }

            return possibleMoves;
        }

        /// <summary>
        /// Get eat points of pawn
        /// </summary>
        /// <param name="chessBoard">chess board</param>
        /// <returns>list of points</returns>
        public List<Point> GetEatPoints(ChessBoard chessBoard)
        {
            var enemyFigures = chessBoard.GetAllFiguresByPercolate(f => f.Color != this.Color);
            var eatMoves = new List<Point>();
            //eat points
            for (int i = 4, j = 0; i < PossiblePoints.Count; i++)
            {
                if (identifier == PawnIdentifier.Top && i % 2 == 0
                    || identifier == PawnIdentifier.Bot && i % 2 != 0)
                {
                    var tempPoint = this.Position + PossiblePoints[i][j];
                    foreach (var enemy in enemyFigures)
                    {
                        if (enemy.Position == tempPoint)
                        {
                            eatMoves.Add(tempPoint);
                            break;
                        }
                    }
                }
            }
            return eatMoves;
        }

        /// <summary>
        /// Override to string to base
        /// </summary>
        /// <returns>short name of figure</returns>
        public override string ToString()
        {
            return "Pawn";
        }
    }
}
