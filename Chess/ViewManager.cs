using Chess.Logic;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{
    /// <summary>
    /// Class for view managment 
    /// </summary>
    public static class ViewManager
    {
        /// <summary>
        /// all path of images in project
        /// </summary>
        private const string _imagePathBlackKing = @"Img\BlackKing.png";
        private const string _imagePathBlackQueen = @"Img\BlackQueen.png";
        private const string _imagePathBlackRook = @"Img\BlackRook.png";
        private const string _imagePathBlackBishop = @"Img\BlackBishop.png";
        private const string _imagePathBlackHorse = @"Img\BlackHorse.png";
        private const string _imagePathBlackPawn = @"Img\BlackPawn.png";
        private const string _imagePathWhiteKing = @"Img\WhiteKing.png";
        private const string _imagePathWhiteQueen = @"Img\WhiteQueen.png";
        private const string _imagePathWhiteRook = @"Img\WhiteRook.png";
        private const string _imagePathWhiteHorse = @"Img\WhiteHorse.png";
        private const string _imagePathWhiteBishop = @"Img\WhiteBishop.png";
        private const string _imagePathWhitePawn = @"Img\WhitePawn.png";
        private const string _imagePathGrayRecomend = @"Img\GrayRecomend.png";

        /// <summary>
        /// Method to Get image by figure
        /// </summary>
        /// <param name="figure">figure</param>
        /// <returns>image by figure</returns>
        public static Image GetImage(FigureViewModel figure)
        {
            switch (figure.FigureName)
            {
                case "King":
                    if (!figure.ColorFlag)
                        return new Image() { Source = new BitmapImage(new Uri(_imagePathBlackKing, UriKind.Relative)) };
                    return new Image() { Source = new BitmapImage(new Uri(_imagePathWhiteKing, UriKind.Relative)) };
                case "Queen":
                    if (!figure.ColorFlag)
                        return new Image() { Source = new BitmapImage(new Uri(_imagePathBlackQueen, UriKind.Relative)) };
                    return new Image() { Source = new BitmapImage(new Uri(_imagePathWhiteQueen, UriKind.Relative)) };
                case "Rook":
                    if (!figure.ColorFlag)
                        return new Image() { Source = new BitmapImage(new Uri(_imagePathBlackRook, UriKind.Relative)) };
                    return new Image() { Source = new BitmapImage(new Uri(_imagePathWhiteRook, UriKind.Relative)) };
                case "Horse":
                    if (!figure.ColorFlag)
                        return new Image() { Source = new BitmapImage(new Uri(_imagePathBlackHorse, UriKind.Relative)) };
                    return new Image() { Source = new BitmapImage(new Uri(_imagePathWhiteHorse, UriKind.Relative)) };
                case "Bishop":
                    if (!figure.ColorFlag)
                        return new Image() { Source = new BitmapImage(new Uri(_imagePathBlackBishop, UriKind.Relative)) };
                    return new Image() { Source = new BitmapImage(new Uri(_imagePathWhiteBishop, UriKind.Relative)) };
                case "Pawn":
                    if (!figure.ColorFlag)
                        return new Image() { Source = new BitmapImage(new Uri(_imagePathBlackPawn, UriKind.Relative)) };
                    return new Image() { Source = new BitmapImage(new Uri(_imagePathWhitePawn, UriKind.Relative)) };
                default:
                    throw new Exception("Image not Found");
            }
        }

        /// <summary>
        /// Method to Get recomendation images 
        /// </summary>
        /// <returns>image recomendation</returns>
        public static Image GetRecomendedPoint()
        {
            return new Image() { Source = new BitmapImage(new Uri(_imagePathGrayRecomend, UriKind.Relative)) };
        }

        #region Methods to choosing figure (pawn template)

        public static Image GetChooseQueen(bool colorFlag)
        {
            return colorFlag ? new Image() { Source = new BitmapImage(new Uri(_imagePathWhiteQueen, UriKind.Relative)) } :
                new Image() { Source = new BitmapImage(new Uri(_imagePathBlackQueen, UriKind.Relative)) };
        }
        public static Image GetChooseRook(bool colorFlag)
        {
            return colorFlag ? new Image() { Source = new BitmapImage(new Uri(_imagePathWhiteRook, UriKind.Relative)) } :
                new Image() { Source = new BitmapImage(new Uri(_imagePathBlackRook, UriKind.Relative)) };
        }
        public static Image GetChooseBishop(bool colorFlag)
        {
            return colorFlag ? new Image() { Source = new BitmapImage(new Uri(_imagePathWhiteBishop, UriKind.Relative)) } :
                new Image() { Source = new BitmapImage(new Uri(_imagePathBlackBishop, UriKind.Relative)) };
        }

        public static Image GetChooseHorse(bool colorFlag)
        {
            return colorFlag ? new Image() { Source = new BitmapImage(new Uri(_imagePathWhiteHorse, UriKind.Relative)) } :
                new Image() { Source = new BitmapImage(new Uri(_imagePathBlackHorse, UriKind.Relative)) };
        }

        #endregion

        #region Methods to get player icons

        private const string _botImagePath = @"Img\Bot.png";
        private const string _unKnownImagePath = @"Img\Unknwon.png";

        private static int _currentplayer;
        private static string[] _players = new string[6]
        {
            @"Img\a1.png",
            @"Img\a2.png",
            @"Img\a3.png",
            @"Img\a4.png",
            @"Img\a5.png",
            @"Img\a6.png",
        };

        public static Image GetPlayerIconWithoutCounting()
        {
            return new Image() { Source = new BitmapImage(new Uri(_players[_currentplayer], UriKind.Relative)) };
        }

        public static string GetPlayerViewName()
        {
            var name = string.Empty;
            switch (_currentplayer)
            {
                case 1:
                    name += "German";
                    break;
                case 2:
                    name += "Katarin";
                    break;
                case 3:
                    name += "Jhon";
                    break;
                case 4:
                    name += "Donald";
                    break;
                case 5:
                    name += "Maria";
                    break;
                case 6:
                    name += "Annie";
                    break;
            }
            return name;
        }

        public static Image GetPlayerIcon()
        {
            if (_currentplayer == _players.Length) _currentplayer = 0;
            return new Image() { Source = new BitmapImage(new Uri(_players[_currentplayer++], UriKind.Relative)) };
        }

        public static Image GetBotIcon()
        {
            return new Image() { Source = new BitmapImage(new Uri(_botImagePath, UriKind.Relative)) };
        }

        public static Image GetUnKnownImage()
        {
            return new Image() { Source = new BitmapImage(new Uri(_unKnownImagePath, UriKind.Relative)) };
        }

        #endregion
    }
}
