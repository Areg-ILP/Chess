using Chess.Model.Models;
using System;

namespace Chess.Model.Helpers
{
    /// <summary>
    /// Helper class for create any figure 
    /// </summary>
    public static class FigureHelper
    {
        /// <summary>
        /// white king counter (ss)
        /// </summary>
        private static int _whiteKingCounter = 0;

        /// <summary>
        /// black king counter (ss)
        /// </summary>
        private static int _blackKingCounter = 0;

        /// <summary>
        /// Create figure and return
        /// </summary>
        /// <typeparam name="T">figure type</typeparam>
        /// <param name="position">figure position</param>
        /// <param name="color">figure color</param>
        /// <param name="chessBoard">chessboard</param>
        /// <returns>figure</returns>
        public static T GetFigure<T>(Point position, ColorEnum color, ChessBoard chessBoard) where T : Figure, new()
        {
            var figure = new T();

            if (figure is King && color == ColorEnum.Black)
            {
                if(_blackKingCounter < 1)
                    _blackKingCounter++;
                else                    
                {
                    throw new Exception("King was craeted");
                }
            }     
            
            if (figure is King && color == ColorEnum.White)
            {
                if (_whiteKingCounter < 1)
                    _whiteKingCounter++;
                else
                {
                    throw new Exception("King was craeted");
                }
            }           

            if (chessBoard.GetFigureByPercolate(f => f.Position == position) == null)
            {
                if (figure is King && chessBoard.IsPointUnderCheck(position,color))
                {
                    if (color == ColorEnum.Black)
                        _blackKingCounter--;
                    else
                        _whiteKingCounter--;
                    throw new Exception("King is under check");
                }
                figure.Position = position;
                figure.Color = color;                
            }

            return figure;
        }             

        /// <summary>
        /// Get count of kings
        /// </summary>
        /// <returns>tuple (black king counter,white king counter)</returns>
        public static (int WhiteCounter,int BlackCounter) GetCount()
        {
            return (_whiteKingCounter, _blackKingCounter);
        }

        /// <summary>
        /// Set zero all counters
        /// </summary>
        public static void SetCountZero()
        {
            _whiteKingCounter = default;
            _blackKingCounter = default;
        }
    }
}
