using Chess.Model;
using Chess.Model.Helpers;
using Chess.Model.Models;
using System.Collections.Generic;

namespace Chess.Logic.Game_Mangement
{
    /// <summary>
    /// Partial class of ChessMaster for API
    /// </summary>
    public partial class ChessMaster
    {
        #region API

        /// <summary>
        /// Get all figure view models
        /// </summary>
        /// <returns>list of view model</returns>
        public List<FigureViewModel> GetAllFiguresViewModel()
        {
            var figureViewModels = new List<FigureViewModel>();
            foreach (var figure in ChessBoard)
            {
                figureViewModels.Add(
                    new FigureViewModel()
                    {
                        FigureName = figure.ToString(),
                        Position = (figure.Position.X, figure.Position.Y),
                        ColorFlag = figure.Color == ColorEnum.White
                    }
                );
            }
            return figureViewModels;
        }

        /// <summary>
        /// Append figure by view model
        /// </summary>
        /// <param name="figure">figure view model</param>
        /// <returns>if apeended true else false</returns>
        public bool AppendFigure(FigureViewModel figure)
        {
            var position = new Point(figure.Position.X, figure.Position.Y);
            var color = figure.ColorFlag ? ColorEnum.White : ColorEnum.Black;

            if (ChessBoard.GetFigureByPercolate(f => f.Position == position) != null)
            {
                return false;
            }

            Figure newFigure;
            try
            {
                switch (figure.FigureName)
                {
                    case "King":
                        newFigure = FigureHelper.GetFigure<King>(position, color, ChessBoard);
                        break;
                    case "Queen":
                        newFigure = FigureHelper.GetFigure<Queen>(position, color, ChessBoard);
                        break;
                    case "Rook":
                        newFigure = FigureHelper.GetFigure<Rook>(position, color, ChessBoard);
                        break;
                    case "Horse":
                        newFigure = FigureHelper.GetFigure<Horse>(position, color, ChessBoard);
                        break;
                    case "Bishop":
                        newFigure = FigureHelper.GetFigure<Bishop>(position, color, ChessBoard);
                        break;
                    case "Pawn":
                        newFigure = FigureHelper.GetFigure<Pawn>(position, color, ChessBoard);
                        break;
                    default:
                        return false;
                }
            }
            catch
            {
                return false;
            }

            ChessBoard.AddFigure(newFigure);
            return true;
        }

        /// <summary>
        /// Check is startable game
        /// </summary>
        /// <returns>true if startable else false</returns>
        public bool IsStartableGame()
        {
            var counterTuple = FigureHelper.GetCount();
            return counterTuple.BlackCounter > 0 && counterTuple.WhiteCounter > 0;
        }

        /// <summary>
        /// Set chessboard default parametrs
        /// </summary>
        public void SetChessboardDefaultParametrs()
        {
            FigureHelper.SetCountZero();
            SetChessboardByDefault(_firstPlayerColor == ColorEnum.White);
            Turn = true;
        }

        /// <summary>
        /// Set empty board
        /// </summary>
        public void SetEmptyBoard()
        {
            FigureHelper.SetCountZero();
            ChessBoard = new ChessBoard();
            Turn = _firstPlayerColor == ColorEnum.White;
        }

        /// <summary>
        /// Get figure by given position
        /// </summary>
        /// <param name="point">point (tuple)</param>
        /// <returns>figure view model</returns>
        public FigureViewModel GetFigureByPosition((int x, int y) point)
        {
            var position = new Point(point.x, point.y);
            var figure = ChessBoard.GetFigureByPercolate(f => f.Position == position);

            return new FigureViewModel()
            {
                FigureName = figure.ToString(),
                ColorFlag = figure.Color == ColorEnum.White,
                Position = point
            };
        }

        /// <summary>
        /// Get list of points(tuples)
        /// </summary>
        /// <param name="figureViewModel">view model</param>
        /// <returns>list of points</returns>
        public List<(int x, int y)> GetRecomendationsByFigure(FigureViewModel figureViewModel)
        {
            var figure = GetFigureByViewModel(figureViewModel);
            var possiblePoints = new List<(int x, int y)>();
            var safePoints = figure.GetSafeMovesFromCheck(ChessBoard);
            if (figure.ToString() == "King")
                safePoints.AddRange(((King)figure).GetCastlingPoints(ChessBoard));
            foreach (var point in safePoints)
            {
                possiblePoints.Add((point.X, point.Y));
            }
            return possiblePoints;
        }

        /// <summary>
        /// Get figure by view model
        /// </summary>
        /// <param name="viewModel">view model</param>
        /// <returns>fgiure</returns>
        private Figure GetFigureByViewModel(FigureViewModel viewModel)
        {
            var position = new Point(viewModel.Position.X, viewModel.Position.Y);
            var figure = ChessBoard.GetFigureByPercolate(f => f.Position == position);
            return figure;
        }

        /// <summary>
        /// Get events in board (string)
        /// </summary>
        /// <param name="colorFlag">color of team</param>
        /// <returns>Event string</returns>
        public string GetEventsInBoard(bool colorFlag)
        {
            var colorBy = colorFlag ? ColorEnum.Black : ColorEnum.White;
            var kingToCheck = ChessBoard.GetFigureByPercolate(f => f.Color == colorBy &&
                                                                    f.ToString() == "King");
            var message = string.Empty;
            if (ChessBoard.IsPointUnderCheck(kingToCheck.Position, kingToCheck.Color))
            {
                message = "Check";
                if (ChessBoard.IsMate(colorBy))
                    message = "Mate";
            }
            else
                if (ChessBoard.IsStaleMate(colorBy))
                message = "StaleMate";
            return message;
        }

        public (int w,int b) GetStatisticsForPredictor()
        {
            int whiteCounter, blackCounter;
            whiteCounter = blackCounter = 0;
            foreach (var figure in ChessBoard)
            {
                if (figure.Color == ColorEnum.White)
                    whiteCounter += figure.Value;
                else
                    blackCounter += figure.Value;
            }
            return (whiteCounter, blackCounter);
        }

        #endregion
    }
}
