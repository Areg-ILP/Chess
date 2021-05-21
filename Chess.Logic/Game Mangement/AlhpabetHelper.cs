namespace Chess.Logic.Game_Mangement
{
    /// <summary>
    /// Helper class for convert letter to number and the opposite
    /// </summary>
    public static class AlhpabetHelper
    {
        /// <summary>
        /// Convert symbol to number
        /// </summary>
        /// <param name="symbol">character (symbol)</param>
        /// <returns>result number</returns>
        public static int GetNumber(char symbol)
        {
            switch (symbol)
            {
                case 'a':
                case 'A':
                    return 1;
                case 'b':
                case 'B':
                    return 2;
                case 'c':
                case 'C':
                    return 3;
                case 'd':
                case 'D':
                    return 4;
                case 'E':
                case 'e':
                    return 5;
                case 'f':
                case 'F':
                    return 6;
                case 'g':
                case 'G':
                    return 7;
                case 'h':
                case 'H':
                    return 8;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Convert number coordinates to chess cordinate for logs
        /// </summary>
        /// <param name="x">number to convert letter</param>
        /// <param name="y">second number</param>
        /// <returns>chess coordinate (string)</returns>
        public static string GetCoordinates(int x,int y)
        {
            var result = string.Empty;
            switch (y)
            {
                case 0:
                    result += "A";
                    break;
                case 1:
                    result += "B";
                    break;
                case 2:
                    result += "C";
                    break;
                case 3:
                    result += "D";
                    break;
                case 4:
                    result += "E";
                    break;
                case 5:
                    result += "F";
                    break;
                case 6:
                    result += "G";
                    break;
                case 7:
                    result += "H";
                    break;
            }
            return result + (++x).ToString();
        }
    }
}
