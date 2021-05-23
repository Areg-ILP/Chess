using System.Collections.Generic;
using Newtonsoft.Json;

namespace Chess.Data.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Party : Entity
    {
        [JsonRequired]
        [JsonProperty("WhitePlayer")]
        public int wId { get; set; }

        [JsonRequired]
        [JsonProperty("BlackPlayer")]
        public int bId { get; set; }

        [JsonRequired]
        [JsonProperty("Logs")]
        public List<Log> Logs { get; set; }

        [JsonProperty("Result")]
        public bool? MatchResult { get; set; }

    }
}
