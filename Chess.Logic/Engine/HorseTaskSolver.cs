using Chess.Model;
using Chess.Model.Helpers;
using Chess.Model.Models;
using System.Collections.Generic;

namespace Chess.Logic.Engine
{
    /// <summary>
    /// Class for solving horse task 
    /// </summary>
    internal class HorseTaskSolver
    {
        /// <summary>
        /// Color of engine
        /// </summary>
        private ColorEnum _color;

        /// <summary>
        /// Initial ctor
        /// </summary>
        public HorseTaskSolver(ColorEnum color)
        {
            _color = color;
        }

        /// <summary>
        /// Get shortes path for horse task
        /// </summary>
        /// <param name="start">start position</param>
        /// <param name="end">end position</param>
        /// <param name="chessBoard">chessboard</param>
        /// <returns>list of points</returns>
        public List<Point> GetShortestPath(Point start,Point end,ChessBoard chessBoard)
        {
            var path = new List<Point>();
            var finalPos = end;
            while(CalculateCount(start,end,chessBoard,out Point xFactor) != 0)
            {
                path.Add(xFactor);
                end = xFactor;
            }
            var horseToMove = chessBoard.GetFigureByPercolate(h => h.Position == start);
            horseToMove.MoveGod(finalPos, chessBoard);
            return path;
        }

        /// <summary>
        /// Get count of moves 
        /// </summary>
        /// <param name="start">start position of horse</param>
        /// <param name="end">final position of horse</param>
        /// <returns>return count of moves</returns>
        private int CalculateCount(Point start, Point end,ChessBoard chessBoard,out Point xFactor)
        {
            if (IsIntersectMoves(start, end))
            {
                xFactor = end;
                return 0;
            }
            return Calculating(new List<Horse>()
                               {
                                   new Horse(start, _color)
                               },
                               end, chessBoard,out xFactor);
        }

        /// <summary>
        /// count of moves (global by using of recursion)
        /// </summary>
        private int _count;

        /// <summary>
        /// main method to calculate count of moves by recursion
        /// </summary>
        /// <param name="possiblePoints">possible points of horse by his position</param>
        /// <param name="end">final position</param>
        /// <returns>count of moves</returns>
        private int Calculating(List<Horse> horses, Point end,ChessBoard chessBoard,out Point xFactor)
        {
            _count++;
            var _horses = new List<Horse>();
            foreach (var currentHorse in horses)
            {
                foreach (var currentPossibles in currentHorse.GetPossibleMoves(chessBoard))
                {
                    _horses.Add(new Horse(currentPossibles, _color));
                    if (IsIntersectMoves(currentPossibles, end))
                    {
                        xFactor = currentHorse.Position;
                        return _count;
                    }
                }
            }

            return Calculating(_horses, end,chessBoard,out xFactor);
        }

        /// <summary>
        /// Check intersect moves of start and end
        /// </summary>
        /// <param name="horsePossibleMove">start position</param>
        /// <param name="targetPossibleMove">end position</param>
        /// <returns>true if positions are intersect else false</returns>
        private bool IsIntersectMoves(Point horsePossibleMove, Point targetPossibleMove)
        {
            return horsePossibleMove == targetPossibleMove;
        }

        /// <summary>
        /// Change brain color figures
        /// </summary>
        /// <param name="color">current color</param>
        public void ChangeSolverColor(ColorEnum color)
        {
            _color = color;
        }

    }
}
