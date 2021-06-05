using System.Collections.Generic;
using Newtonsoft.Json;

namespace Chess.Data.Models
{
    /// <summary>
    /// Entity - party 
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Party : Entity
    {
        /// <summary>
        /// white player id (this party)
        /// </summary>
        [JsonRequired]
        [JsonProperty("WhitePlayer")]
        public int wId { get; set; }

        /// <summary>
        /// black player id (this party)
        /// </summary>
        [JsonRequired]
        [JsonProperty("BlackPlayer")]
        public int bId { get; set; }

        /// <summary>
        /// Logs of party
        /// </summary>
        [JsonProperty("Logs")]
        public List<Log> Logs { get; set; }

        /// <summary>
        /// party result
        /// </summary>
        [JsonProperty("Result")]
        public bool? MatchResult { get; set; }

    }
}
