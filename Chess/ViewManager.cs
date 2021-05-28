using Chess.Logic;
using System;
using System.Windows.Media.Imaging;

namespace Chess
{
    public static class ViewManager
    {
        /// <summary>
        /// all path of images in project
        /// </summary>
        private const string _imagePathBlackKing = @"C:\Users\Admin\source\repos\Chess\Chess\Images\BlackKing.png";
        private const string _imagePathBlackQueen = @"C:\Users\Admin\source\repos\Chess\Chess\Images\BlackQueen.png";
        private const string _imagePathBlackRook = @"C:\Users\Admin\source\repos\Chess\Chess\Images\BlackRook.png";
        private const string _imagePathBlackBishop = @"C:\Users\Admin\source\repos\Chess\Chess\Images\BlackBishop.png";
        private const string _imagePathBlackHorse = @"C:\Users\Admin\source\repos\Chess\Chess\Images\BlackHorse.png";
        private const string _imagePathBlackPawn = @"C:\Users\Admin\source\repos\Chess\Chess\Images\BlackPawn.png";
        private const string _imagePathWhiteKing = @"C:\Users\Admin\source\repos\Chess\Chess\Images\WhiteKing.png";
        private const string _imagePathWhiteQueen = @"C:\Users\Admin\source\repos\Chess\Chess\Images\WhiteQueen.png";
        private const string _imagePathWhiteRook = @"C:\Users\Admin\source\repos\Chess\Chess\Images\WhiteRook.png";
        private const string _imagePathWhiteHorse = @"C:\Users\Admin\source\repos\Chess\Chess\Images\WhiteHorse.png";
        private const string _imagePathWhiteBishop = @"C:\Users\Admin\source\repos\Chess\Chess\Images\WhiteBishop.png";
        private const string _imagePathWhitePawn = @"C:\Users\Admin\source\repos\Chess\Chess\Images\WhitePawn.png";
        private const string _imagePathGrayRecomend = @"C:\Users\Admin\source\repos\Chess\Chess\Images\GrayRecomend.png";

        /// <summary>
        /// Method to Get image by figure
        /// </summary>
        /// <param name="figure">figure</param>
        /// <returns>image by figure</returns>
        public static BitmapImage GetImage(FigureViewModel figure)
        {
            switch (figure.FigureName)
            {
                case "King":
                    if (!figure.ColorFlag)
                        return new BitmapImage(new Uri(_imagePathBlackKing));
                    return new BitmapImage(new Uri(_imagePathWhiteKing));
                case "Queen":
                    if (!figure.ColorFlag)
                        return new BitmapImage(new Uri(_imagePathBlackQueen));
                    return new BitmapImage(new Uri(_imagePathWhiteQueen));
                case "Rook":
                    if (!figure.ColorFlag)
                        return new BitmapImage(new Uri(_imagePathBlackRook));
                    return new BitmapImage(new Uri(_imagePathWhiteRook));
                case "Horse":
                    if (!figure.ColorFlag)
                        return new BitmapImage(new Uri(_imagePathBlackHorse));
                    return new BitmapImage(new Uri(_imagePathWhiteHorse));
                case "Bishop":
                    if (!figure.ColorFlag)
                        return new BitmapImage(new Uri(_imagePathBlackBishop));
                    return new BitmapImage(new Uri(_imagePathWhiteBishop));
                case "Pawn":
                    if (!figure.ColorFlag)
                        return new BitmapImage(new Uri(_imagePathBlackPawn));
                    return new BitmapImage(new Uri(_imagePathWhitePawn));
                default:
                    throw new Exception("Image not Found");
            }
        }

        /// <summary>
        /// Method to Get recomendation images 
        /// </summary>
        /// <returns>image recomendation</returns>
        public static BitmapImage GetRecomendedPoint()
        {
            return new BitmapImage(new Uri(_imagePathGrayRecomend));
        }

        public static BitmapImage GetChooseQueen(bool colorFlag)
        {
            return colorFlag ? new BitmapImage(new Uri(_imagePathWhiteQueen)) : new BitmapImage(new Uri(_imagePathBlackQueen));
        }
        public static BitmapImage GetChooseRook(bool colorFlag)
        {
            return colorFlag ? new BitmapImage(new Uri(_imagePathWhiteRook)) : new BitmapImage(new Uri(_imagePathBlackRook));
        }
        public static BitmapImage GetChooseBishop(bool colorFlag)
        {
            return colorFlag ? new BitmapImage(new Uri(_imagePathWhiteBishop)) : new BitmapImage(new Uri(_imagePathBlackBishop));
        }
        public static BitmapImage GetChooseHorse(bool colorFlag)
        {
            return colorFlag ? new BitmapImage(new Uri(_imagePathWhiteHorse)) : new BitmapImage(new Uri(_imagePathBlackHorse));
        }
    }
}
