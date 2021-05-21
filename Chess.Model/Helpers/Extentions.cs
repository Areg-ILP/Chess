using Chess.Model.Models;
using System.Collections.Generic;

namespace Chess.Model.Helpers
{
    /// <summary>
    /// Class for extentions
    /// </summary>
    public static class Extentions
    {
        /// <summary>
        /// Delegate for percolation
        /// </summary>
        /// <typeparam name="T">entity type</typeparam>
        /// <typeparam name="TResult">returned type</typeparam>
        /// <param name="item">entity</param>
        /// <returns>type of Tresult</returns>
        public delegate TResult Percolator<in T, out TResult>(T item) where T : Figure;

        /// <summary>
        /// Get figure by percolate delegate
        /// </summary>
        /// <param name="chessBoard">chessboard</param>
        /// <param name="percolator">percolator delegate</param>
        /// <returns>result figure</returns>
        public static Figure GetFigureByPercolate(this ChessBoard chessBoard,Percolator<Figure,bool> percolator)
        {
            foreach (var figure in chessBoard)
            {
                if (percolator(figure))
                    return figure;
            }
            return default;
        }

        /// <summary>
        /// Get all figures by percolation
        /// </summary>
        /// <param name="chessBoard">chessboard</param>
        /// <param name="percolator">percolator</param>
        /// <returns>List of figures</returns>
        public static List<Figure> GetAllFiguresByPercolate(this ChessBoard chessBoard,Percolator<Figure, bool> percolator)
        {
            var figures = new List<Figure>();
            foreach (var figure in chessBoard)
            {
                if(percolator(figure))
                {
                    figures.Add(figure);
                }
            }
            return figures;
        }
    }
}
