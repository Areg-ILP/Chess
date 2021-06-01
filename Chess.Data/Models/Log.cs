using Newtonsoft.Json;

namespace Chess.Data.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Log
    {
        /// <summary>
        /// log color flag
        /// </summary>
        [JsonRequired]
        [JsonProperty("ColorTurn")]
        public bool ColorFlag { get; set; }

        /// <summary>
        /// figure to move (this log)
        /// </summary>
        [JsonRequired]
        [JsonProperty("Figure to move")]
        public string FigureToMove { get; set; }

        /// <summary>
        /// start coordinates of figure to move (this log)
        /// </summary>
        [JsonRequired]
        [JsonProperty("StartCoordinate")]
        public string StartCoordinate { get; set; }

        /// <summary>
        /// final coordinates of figure to move (this log)
        /// </summary>
        [JsonRequired]
        [JsonProperty("FinalCoordinate")]
        public string FinalCoordinate { get; set; }
    }
}
