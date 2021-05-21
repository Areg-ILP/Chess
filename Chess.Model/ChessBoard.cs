using Chess.Model.Helpers;
using Chess.Model.Models;
using System.Collections;
using System.Collections.Generic;

namespace Chess.Model
{
    /// <summary>
    /// Chess board 
    /// </summary>
    public class ChessBoard : IEnumerable<Figure>
    {
        /// <summary>
        /// all figures in board
        /// </summary>
        public List<Figure> Figures = new List<Figure>();

        /// <summary>
        /// default ctor
        /// </summary>
        public ChessBoard()
        {

        }

        /// <summary>
        /// inital ctor
        /// </summary>
        public ChessBoard(List<Figure> figures)
        {
            Figures = figures;
        }

        #region board 

        /// <summary>
        /// Last removed figure
        /// </summary>
        internal Figure RemovedFigure { get; set; }

        /// <summary>
        /// Change figure position by given point
        /// </summary>
        /// <param name="figure">figure to change position</param>
        /// <param name="nextPoint">next position</param>
        public void ChangeFigurePosition(Figure figure, Point nextPoint)
        {
            var figureToCheck = this.GetFigureByPercolate(f => f.Position == nextPoint);
            if (figureToCheck != null)
            {
                if (figureToCheck?.ToString() != "King" && figureToCheck?.Color != figure.Color)
                {
                    this.DeleteFigure(figureToCheck);
                    RemovedFigure = figureToCheck;
                }
            }
        }

        /// <summary>
        /// Add figure in board
        /// </summary>
        /// <param name="figure">figure to add</param>
        public void AddFigure(Figure figure)
        {
            if (figure == null)
                return;
            Figures?.Add(figure);
        }

        /// <summary>
        /// Delete figure in board
        /// </summary>
        /// <param name="figure">figure to delete</param>
        public void DeleteFigure(Figure figure)
        {
            if (figure == null)
                return;
            Figures?.Remove(figure);
        }

        #endregion

        #region checkings      

        /// <summary>
        /// Check is mate in game
        /// </summary>
        /// <returns>true if mate else false</returns>
        public bool IsMate(ColorEnum colorBy)
        {
            var figures = this.GetAllFiguresByPercolate(f => f.Color == colorBy);
            for (int i = 0; i < figures.Count; i++)
            {
                if (figures[i].GetSafeMovesFromCheck(this).Count > 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check is stale Mate in game
        /// </summary>
        /// <returns>true if stale mate else false</returns>
        public bool IsStaleMate(ColorEnum colorBy)
        {
            var checkFigures = this.GetAllFiguresByPercolate(f => f.Color == colorBy);
            var kingToCheck = this.GetFigureByPercolate(f => f.Color == colorBy
                                                             && f.ToString() == "King");
            if (!IsPointUnderCheck(kingToCheck.Position, kingToCheck.Color))
            {
                foreach (var figure in checkFigures)
                {
                    if (figure.GetPossibleMoves(this).Count > 0)
                        return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check is point by color of team is under check
        /// </summary>
        /// <param name="point">point to check</param>
        /// <param name="enemyColor"></param>
        /// <returns>true if point under check else false</returns>
        public bool IsPointUnderCheck(Point point, ColorEnum safeColor)
        {
            var enemyFigures = this.GetAllFiguresByPercolate(f => f.Color != safeColor);
            foreach (var enemyFigure in enemyFigures)
            {
                foreach (var currentPossible in enemyFigure.GetPossibleMoves(this))
                {
                    if (point == currentPossible)
                        return true;
                }
            }
            return false;
        }

        #endregion

        #region Enumerator

        /// <summary>
        /// Get enumerator
        /// </summary>
        /// <returns>enumerator</returns>
        public IEnumerator<Figure> GetEnumerator()
        {
            return ((IEnumerable<Figure>)Figures).GetEnumerator();
        }

        /// <summary>
        /// Get enumerator
        /// </summary>
        /// <returns>enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Figures).GetEnumerator();
        }

        #endregion
    }
}
