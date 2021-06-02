using System.Collections.Generic;

namespace Chess.Logic.Data_Managment
{
    public class PartyViewModel
    {
        public int wId { get; set; }
        public int bId { get; set; }
        public List<string> Logs { get; set; }
        public bool? MatchResult { get; set; }
    }
}
