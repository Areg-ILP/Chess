using Newtonsoft.Json;

namespace Chess.Data.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Log
    {
        [JsonRequired]
        [JsonProperty("ColorTurn")]
        public bool ColorFlag { get; set; }

        [JsonRequired]
        [JsonProperty("Figure to move")]
        public string FigureToMove { get; set; }

        [JsonRequired]
        [JsonProperty("StartCoordinate")]
        public string StartCoordinate { get; set; }

        [JsonRequired]
        [JsonProperty("FinalCoordinate")]
        public string FinalCoordinate { get; set; }
    }
}
