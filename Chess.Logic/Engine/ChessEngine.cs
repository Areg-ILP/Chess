using Chess.Model;
using Chess.Model.Helpers;
using Chess.Model.Models;
using System.Collections.Generic;

namespace Chess.Logic.Engine
{
    /// <summary>
    /// Chess engine
    /// </summary>
    internal class ChessEngine
    {
        /// <summary>
        /// Brain to move with player
        /// </summary>
        private readonly Brain _brain;
        /// <summary>
        /// Horse task solver class
        /// </summary>
        private readonly HorseTaskSolver _horseTaskSolver;

        /// <summary>
        /// Initial ctor
        /// </summary>
        /// <param name="engineColor">engine color</param>
        public ChessEngine(ColorEnum engineColor)
        {
            _brain = new Brain(engineColor);
            _horseTaskSolver = new HorseTaskSolver(engineColor);
        }

        /// <summary>
        /// Run brain move function
        /// </summary>
        /// <param name="chessBoard">chess board</param>
        /// <returns></returns>
        public (Figure Figure,Point MoveTo) RunEngine(ChessBoard chessBoard)
        {
            return _brain.Move(chessBoard);
        }

        /// <summary>
        /// Run hors task solver main function
        /// </summary>
        /// <param name="start">start position</param>
        /// <param name="end">end position</param>
        /// <param name="chessBoard">chessboard</param>
        /// <returns></returns>
        public List<Point> RunHorseTaskSolver(Point start,Point end,ChessBoard chessBoard)
        {
            return _horseTaskSolver.GetShortestPath(start,end,chessBoard);
        }

        /// <summary>
        /// Change engine color
        /// </summary>
        /// <param name="color"></param>
        public void ChangeEngineColor(ColorEnum color)
        {
            _brain.ChangeBrainColor(color);
            _horseTaskSolver.ChangeSolverColor(color);
        }
    }
}
