using Chess.Model;
using Chess.Model.Helpers;
using Chess.Model.Models;
using System;
using System.Collections.Generic;

namespace Chess.Logic.Engine
{
    /// <summary>
    //  Class for calculating and playing chess with you my friend :D
    /// </summary>
    internal class Brain
    {
        /// <summary>
        /// color of brain
        /// </summary>
        private ColorEnum _brainColor;

        /// <summary>
        /// inital ctor
        /// </summary>
        /// <param name="color">color</param>
        public Brain(ColorEnum color)
        {
            _brainColor = color;
        }

        /// <summary>
        /// Main method of class , for move figures
        /// </summary>
        /// <param name="chessBoard">chessboard</param>
        /// <returns>playing figure and start position of figure (tuple)</returns>
        public (Figure Figure, Point MoveTo) Move(ChessBoard chessBoard)
        {
            //get optimal statistics
            OptimalStatistics(chessBoard, out Figure optimalFigure, out Point optimalPoint);

            //for logs
            var startPoint = optimalFigure.Position;
            optimalFigure.Move(optimalPoint, chessBoard);
            return (optimalFigure, startPoint);
        }

        /// <summary>
        /// Choose strategy and run 
        /// </summary>
        /// <param name="chessBoard">chessBoard</param>
        /// <param name="optimalFigure">optimal figure for search</param>
        /// <param name="optimalPoint">otpimal point for search</param>
        private void OptimalStatistics(ChessBoard chessBoard, out Figure optimalFigure, out Point optimalPoint)
        {
            if (RetreatStrategy(chessBoard, out optimalFigure, out optimalPoint))
                return;
            if (MateStrategy(chessBoard, out optimalFigure, out optimalPoint))
                return;
            StandartStrategy(chessBoard, out optimalFigure, out optimalPoint);
        }

        /// <summary>
        /// Get optimal figure by optimalpoint
        /// </summary>
        /// <param name="optimalPoint">optimal point by all searchings</param>
        /// <param name="chessBoard">chessBoard</param>
        /// <returns>figure who can move by that optimal point</returns>
        private Figure GetOptimalFigure(Point optimalPoint, ChessBoard chessBoard)
        {
            Figure optimalFigure = default;
            var brainFigures = chessBoard.GetAllFiguresByPercolate(f => f.Color == this._brainColor);
            for (int i = 0; i < brainFigures.Count; i++)
            {
                var tempPoints = brainFigures[i].GetPossibleMoves(chessBoard);
                for (int j = 0; j < tempPoints.Count; j++)
                {
                    if (optimalPoint == tempPoints[j])
                    {
                        optimalFigure = brainFigures[i];
                    }
                }
            }
            return optimalFigure;
        }

        /// <summary>
        /// Get impact by numbers 
        /// </summary>
        /// <param name="figure">figure for calculating impact</param>
        /// <param name="chessBoard">chessboard</param>
        /// <param name="optimalPoint">optimal point to move and calcuate impact</param>
        /// <returns>impact for given figure and point</returns>
        private int GetImpact(Figure figure, ChessBoard chessBoard, out Point optimalPoint)
        {
            var startPosition = figure.Position;
            var safeMoves = figure.GetPossibleMoves(chessBoard);
            optimalPoint = default;

            int min = 8;
            for (int i = 0; i < safeMoves.Count; i++)
            {
                figure.Move(safeMoves[i], chessBoard);
                var kingToCheck = chessBoard.GetFigureByPercolate(k => k.Color != _brainColor &&
                                                                            k.ToString() == "King");
                int kingSafeMovesCount = kingToCheck.GetPossibleMoves(chessBoard).Count;
                if (kingSafeMovesCount == 0 && figure.Position != kingToCheck.Position)
                {
                    figure.MoveGod(startPosition, chessBoard);
                    continue;
                }
                if (kingSafeMovesCount < min)
                {
                    min = kingSafeMovesCount;
                    optimalPoint = safeMoves[i];
                }
                figure.MoveGod(startPosition, chessBoard);
            }
            return min;
        }

        /// <summary>
        /// Standart strategy for searching optimal figure and optimal point
        /// </summary>
        /// <param name="chessBoard">chessboard</param>
        /// <param name="optimalFigure">optimal figure for searching</param>
        /// <param name="optimalPoint">optimal point for searching</param>
        private void StandartStrategy(ChessBoard chessBoard, out Figure optimalFigure, out Point optimalPoint)
        {
            var brainFigures = chessBoard.GetAllFiguresByPercolate(f => f.Color == this._brainColor);
            var optimalPoints = new List<Point>();
            var maxImpacts = new List<int>();

            for (int i = 0; i < brainFigures.Count; i++)
            {
                maxImpacts.Add(GetImpact(brainFigures[i], chessBoard, out optimalPoint));
                optimalPoints.Add(optimalPoint);
            }

            int optimalIndex = GetMinIndex(maxImpacts);
            optimalPoint = optimalPoints[optimalIndex];
            optimalFigure = GetOptimalFigure(optimalPoint, chessBoard);
        }

        /// <summary>
        /// Check if can be mate by one move
        /// </summary>
        /// <param name="chessBoard">chessboard</param>
        /// <param name="optimalFigure">optimalfigure for searching </param>
        /// <param name="optimalPoint">optimal point for searching</param>
        /// <returns>true if can be mate by one move else false</returns>
        private bool MateStrategy(ChessBoard chessBoard, out Figure optimalFigure, out Point optimalPoint)
        {
            optimalPoint = default;
            optimalFigure = default;
            var brainFigures = chessBoard.GetAllFiguresByPercolate(f => f.Color == this._brainColor);
            var kingToCheck = chessBoard.GetFigureByPercolate(k => k.Color != _brainColor &&
                                                                            k.ToString() == "King");
            if (kingToCheck.GetPossibleMoves(chessBoard).Count <= 1)
            {
                if (IsForceMate(chessBoard, out optimalFigure, out optimalPoint))
                    return true;
                else
                {
                    for (int i = 0; i < brainFigures.Count; i++)
                    {
                        var currentSafeMoves = brainFigures[i].GetPossibleMoves(chessBoard);
                        for (int j = 0; j < currentSafeMoves.Count; j++)
                        {
                            if (kingToCheck.Position.X == currentSafeMoves[j].X
                                || kingToCheck.Position.Y == currentSafeMoves[j].Y)
                            {
                                optimalFigure = brainFigures[i];
                                optimalPoint = currentSafeMoves[j];
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Is force mate given figure and given point
        /// </summary>
        /// <param name="chessBoard">chesboard</param>
        /// <param name="optimalFigure">optimal figure by search</param>
        /// <param name="optimalPoint">optimal point by search</param>
        /// <returns></returns>
        private bool IsForceMate(ChessBoard chessBoard, out Figure optimalFigure, out Point optimalPoint)
        {
            optimalFigure = default;
            optimalPoint = default;

            var brainFigures = chessBoard.GetAllFiguresByPercolate(f => f.Color == this._brainColor);

            for (int i = 1; i < brainFigures.Count; i++)
            {
                optimalFigure = brainFigures[i];
                var startPosition = optimalFigure.Position;
                var optimalSafeMoves = optimalFigure.GetPossibleMoves(chessBoard);
                for (int j = 0; j < optimalSafeMoves.Count; j++)
                {
                    optimalFigure.Move(optimalSafeMoves[j], chessBoard);
                    var kingToCheck = chessBoard.GetFigureByPercolate(k => k.Color != _brainColor &&
                                                                            k.ToString() == "King");
                    if (kingToCheck.GetPossibleMoves(chessBoard).Count == 0
                        && optimalFigure.Position != kingToCheck.Position)
                    {
                        optimalFigure.MoveGod(startPosition, chessBoard);
                        continue;
                    }
                    if (kingToCheck.GetPossibleMoves(chessBoard).Count == 0 &&
                        (kingToCheck.Position.X == optimalFigure.Position.X ||
                        kingToCheck.Position.Y == optimalFigure.Position.Y))
                    {
                        optimalPoint = optimalSafeMoves[j];
                        optimalFigure.MoveGod(startPosition, chessBoard);
                        return true;
                    }
                    optimalFigure.MoveGod(startPosition, chessBoard);
                }
            }
            return false;
        }

        /// <summary>
        /// Check is figure of computer allocate in danger zone
        /// </summary>
        /// <param name="chessBoard">chessboard</param>
        /// <param name="optimalFigure">optimal figure for searching</param>
        /// <param name="optimalPoint">otpimal point for searching</param>
        /// <returns>true if figures in danger zone else false</returns>
        private bool RetreatStrategy(ChessBoard chessBoard, out Figure optimalFigure, out Point optimalPoint)
        {
            optimalFigure = default;
            optimalPoint = default;

            var brainFigures = chessBoard.GetAllFiguresByPercolate(f => f.Color == this._brainColor);

            for (int i = 0; i < brainFigures.Count; i++)
            {
                if (!IsSafeFigure(brainFigures[i], chessBoard))
                {
                    optimalFigure = brainFigures[i];
                    var safeMoves = optimalFigure.GetPossibleMoves(chessBoard);
                    optimalPoint = safeMoves[new Random().Next(0, safeMoves.Count - 1)];
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check is figure safe
        /// </summary>
        /// <param name="figure">some figure</param>
        /// <param name="chessBoard">chessboard</param>
        /// <returns>true if safe else false</returns>
        private bool IsSafeFigure(Figure figure, ChessBoard chessBoard)
        {
            var enemyFigures = chessBoard.GetAllFiguresByPercolate(f => f.Color != this._brainColor);
            var dangerMoves = new List<Point>();

            foreach (var enemyFigure in enemyFigures)
            {
                dangerMoves.AddRange(enemyFigure.GetPossibleMoves(chessBoard));
            }

            for (int i = 0; i < dangerMoves.Count; i++)
            {
                if (figure.Position == dangerMoves[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Get minimum index of list
        /// </summary>
        /// <param name="list">list</param>
        /// <returns>min index</returns>
        private int GetMinIndex(List<int> list)
        {
            int minIdx = 0;
            for (int i = 1; i < list.Count; i++)
                if (list[i] < list[minIdx])
                    minIdx = i;
            return minIdx;
        }

        /// <summary>
        /// Change brain color figures
        /// </summary>
        /// <param name="color">current color</param>
        public void ChangeBrainColor(ColorEnum color)
        {
            _brainColor = color;
        }
    }
}
