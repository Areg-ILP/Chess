using System;

namespace Chess.Logic.Data_Managment
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PartyCount { get; set; }
        public int WinCount { get; set; }
        public int LoseCount { get; set; }
        public int DrawCount { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
