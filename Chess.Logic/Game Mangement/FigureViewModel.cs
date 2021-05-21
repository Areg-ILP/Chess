namespace Chess.Logic
{
    public class FigureViewModel
    {
        public string FigureName { get; set; }
        public (int X,int Y) Position { get; set; }
        public bool ColorFlag { get; set; }

        public static bool operator ==(FigureViewModel f1,FigureViewModel f2)
        {
            return f1.FigureName == f2.FigureName
                && f1.Position.X == f2.Position.X
                && f1.Position.Y == f2.Position.Y
                && f1.ColorFlag == f2.ColorFlag;
        }
        public static bool operator !=(FigureViewModel f1, FigureViewModel f2)
        {
            return f1.FigureName != f2.FigureName
                || f1.Position.X != f2.Position.X
                || f1.Position.Y != f2.Position.Y
                || f1.ColorFlag != f2.ColorFlag;
        }
    }
}
