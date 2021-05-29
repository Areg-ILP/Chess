using Chess.Logic.Engine;
using Chess.Model;
using Chess.Model.Helpers;
using Chess.Model.Models;
using System.Collections.Generic;

namespace Chess.Logic.Game_Mangement
{
    /// <summary>
    /// Class to connect logic and view
    /// </summary>
    public partial class ChessMaster
    {
        /// <summary>
        /// Main logic of chess engine
        /// </summary>
        private ChessEngine _chessEngine;
        /// <summary>
        /// first player color
        /// </summary>
        private ColorEnum _firstPlayerColor;
        /// <summary>
        /// second player color
        /// </summary>
        private ColorEnum _secondPlayerColor;
        /// <summary>
        /// Turn to play
        /// </summary>
        public bool Turn { get; private set; }
        /// <summary>
        /// Chess board 
        /// </summary>
        public ChessBoard ChessBoard { get; private set; }

        /// <summary>
        /// Game type
        /// </summary>
        public GameType GameType { get; private set; }

        /// <summary>
        /// Initial ctor
        /// </summary>
        /// <param name="gameType">game type</param>
        /// <param name="colorFlag">color flag</param>
        public ChessMaster()
        {
            Initilize(GameType.PVP, true);
        }

        /// <summary>
        /// Initilze Chess master
        /// </summary>
        /// <param name="gameType">game type</param>
        /// <param name="colorFlag">color flag</param>
        private void Initilize(GameType gameType, bool colorFlag)
        {
            this.GameType = gameType;
            if (gameType == GameType.PVP)
            {
                SetChessboardByDefault(colorFlag);
                Turn = true;
            }
            else
            {
                SetChessboardByComputerMode(colorFlag);
                Turn = _firstPlayerColor == ColorEnum.White;
            }
        }

        #region Player Vs Player

        /// <summary>
        /// Set standart chess board
        /// </summary>
        /// <param name="colorFlag">color flag</param>
        private void SetChessboardByDefault(bool colorFlag)
        {
            _firstPlayerColor = colorFlag ? ColorEnum.White : ColorEnum.Black;
            _secondPlayerColor = colorFlag ? ColorEnum.Black : ColorEnum.White;
            var figures = GetStandartFiguresPositionsBot(_firstPlayerColor);
            figures.AddRange(GetStandartFiguresPositionsTop(_secondPlayerColor));
            ChessBoard = new ChessBoard(figures);
        }

        /// <summary>
        /// Set stanndart position of figure by color (top)
        /// </summary>
        /// <param name="color">color flag</param>
        /// <returns>list of figures</returns>
        private List<Figure> GetStandartFiguresPositionsTop(ColorEnum color)
        {
            var figures = new List<Figure>();
            //pawns
            for (int i = 0, j = 1; i < 8; i++)
            {
                figures.Add(new Pawn(new Point(j, i), color));
            }
            //rooks
            figures.Add(new Rook(new Point(0, 0), color));
            figures.Add(new Rook(new Point(0, 7), color));
            //horses
            figures.Add(new Horse(new Point(0, 1), color));
            figures.Add(new Horse(new Point(0, 6), color));
            //bishops
            figures.Add(new Bishop(new Point(0, 2), color));
            figures.Add(new Bishop(new Point(0, 5), color));

            //king and queen
            if (color == ColorEnum.White)
            {
                figures.Add(new King(new Point(0, 3), color));
                figures.Add(new Queen(new Point(0, 4), color));
            }
            else
            {
                figures.Add(new King(new Point(0, 4), color));
                figures.Add(new Queen(new Point(0, 3), color));
            }

            return figures;
        }

        /// <summary>
        /// Set standart position of figure by color (bot)
        /// </summary>
        /// <param name="color">color flag</param>
        /// <returns>list of figures</returns>
        private List<Figure> GetStandartFiguresPositionsBot(ColorEnum color)
        {
            var figures = new List<Figure>();
            //pawns
            for (int i = 0, j = 6; i < 8; i++)
            {
                figures.Add(new Pawn(new Point(j, i), color));
            }
            //rooks
            figures.Add(new Rook(new Point(7, 0), color));
            figures.Add(new Rook(new Point(7, 7), color));
            //horses
            figures.Add(new Horse(new Point(7, 1), color));
            figures.Add(new Horse(new Point(7, 6), color));
            //bishops
            figures.Add(new Bishop(new Point(7, 2), color));
            figures.Add(new Bishop(new Point(7, 5), color));

            //king and queen
            if (color == ColorEnum.White)
            {
                figures.Add(new King(new Point(7, 4), color));
                figures.Add(new Queen(new Point(7, 3), color));
            }
            else
            {
                figures.Add(new Queen(new Point(7, 4), color));
                figures.Add(new King(new Point(7, 3), color));
            }
            return figures;
        }

        #endregion

        #region Player Vs Computer

        /// <summary>
        /// Initilize chess board and chess engine by default
        /// </summary>
        /// <param name="colorFlag">color flag</param>
        private void SetChessboardByComputerMode(bool colorFlag)
        {
            _firstPlayerColor = colorFlag ? ColorEnum.White : ColorEnum.Black;
            _secondPlayerColor = colorFlag ? ColorEnum.Black : ColorEnum.White;

            _chessEngine = new ChessEngine(_secondPlayerColor);
            ChessBoard = new ChessBoard();
        }


        #endregion

        #region Game Managment

        /// <summary>
        /// Move given figure to given position and return flag
        /// </summary>
        /// <param name="figureToMove">figure to move</param>
        /// <param name="pointToMove">position to move</param>
        /// <returns>true if move else false</returns>
        public bool Play(FigureViewModel figureToMove, (int x, int y) pointToMove)
        {
            if (Turn == figureToMove.ColorFlag)
            {
                var figure = GetFigureByViewModel(figureToMove);
                if (figure.Move(new Point(pointToMove.x, pointToMove.y), ChessBoard))
                {
                    Turn = !Turn;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Move given figure to given position and do work chess engine
        /// </summary>
        /// <param name="figureToMove">figure to move</param>
        /// <param name="pointToMove">position to move</param>
        /// <param name="brainFigureToMove">brain figure</param>
        /// <param name="brainPointToMove">position to move brain</param>
        /// <returns>true if move else false</returns>
        public bool PlayWithBrain(FigureViewModel figureToMove, (int x, int y) pointToMove,
                                    out FigureViewModel brainFigureToMove, out (int x, int y) brainPointToMove)
        {
            brainFigureToMove = default;
            brainPointToMove = default;
            if (Turn == figureToMove.ColorFlag)
            {
                var figure = GetFigureByViewModel(figureToMove);
                if (figure.Move(new Point(pointToMove.x, pointToMove.y), ChessBoard))
                {
                    var brainTuple = _chessEngine.RunEngine(ChessBoard);
                    brainFigureToMove = new FigureViewModel()
                    {
                        FigureName = brainTuple.Figure.ToString(),
                        Position = (brainTuple.Figure.Position.X, brainTuple.Figure.Position.Y),
                        ColorFlag = brainTuple.Figure.Color == ColorEnum.White
                    };
                    brainPointToMove = (brainTuple.MoveTo.X, brainTuple.MoveTo.Y);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get path for horse task
        /// </summary>
        /// <param name="start">start position (tuple)</param>
        /// <param name="end">end position (tuple)</param>
        /// <returns>list of points</returns>
        public List<(int x,int y)> GetPathForHorseTask((int x,int y) start, (int x, int y) end)
        {
            var path = _chessEngine.RunHorseTaskSolver(new Point(start.x, start.y), new Point(end.x, end.y), ChessBoard);
            var frontPath = new List<(int, int)>();
            foreach (var point in path)
            {
                frontPath.Add((point.X, point.Y));
            }
            return frontPath;
        }

        /// <summary>
        /// Change color of players
        /// </summary>
        /// <param name="colorFlag">color flag</param>
        public void ChangePlayerColor(bool colorFlag)
        {
            _firstPlayerColor = colorFlag ? ColorEnum.White : ColorEnum.Black;
            _secondPlayerColor = colorFlag ? ColorEnum.Black : ColorEnum.White;
        }

        /// <summary>
        /// Current position of changable figure
        /// </summary>
        private (int x, int y) _currentPosition;
        /// <summary>
        /// Current color flag of changable figure
        /// </summary>
        private bool _currentColorFlag;

        /// <summary>
        /// Delete pawn with given position
        /// </summary>
        /// <param name="pointOfPawn">position of pawn</param>
        public void ChangePawn((int x, int y) pointOfPawn)
        {
            _currentPosition = pointOfPawn;
            var point = new Point(pointOfPawn.x, pointOfPawn.y);
            var pawnToRemove = ChessBoard.GetFigureByPercolate(p => p.Position == point);
            _currentColorFlag = (pawnToRemove.Color == ColorEnum.White);
            ChessBoard.DeleteFigure(pawnToRemove);
        }

        /// <summary>
        /// Add figure by given string
        /// </summary>
        /// <param name="figureName">figure name</param>
        public void AddChoosedFigure(string figureName)
        {
            var position = new Point(_currentPosition.x, _currentPosition.y);
            var color = (_currentColorFlag) ? ColorEnum.White : ColorEnum.Black;
            switch (figureName)
            {
                case "Queen":
                    ChessBoard.AddFigure(new Queen(position, color));
                    break;
                case "Rook":
                    ChessBoard.AddFigure(new Rook(position, color));
                    break;
                case "Horse":
                    ChessBoard.AddFigure(new Horse(position, color));
                    break;
                case "Bishop":
                    ChessBoard.AddFigure(new Bishop(position, color));
                    break;
            }
        }

        #endregion

        #region Mode changes

        public void SetModeEndgame()
        {
            this.GameType = GameType.Endgame;
            SetChessboardByComputerMode(true);
            Turn = _firstPlayerColor == ColorEnum.White;
        }

        public void SetModePVP()
        {
            this.GameType = GameType.PVP;
            SetChessboardByDefault(true);
            Turn = true;
        }

        public void SetModeHorsePath()
        {

        }

        #endregion
    }
}
