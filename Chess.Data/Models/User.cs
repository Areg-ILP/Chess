using Newtonsoft.Json;

namespace Chess.Data.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class User : Entity
    {
        [JsonRequired]
        [JsonProperty("Login")]
        public string Login { get; set; }

        [JsonRequired]
        [JsonProperty("PasswordHashCode")]
        public int Password { get; set; }

        [JsonRequired]
        [JsonProperty("PartyCounter")]
        public int PartyCount { get; set; }

        [JsonRequired]
        [JsonProperty("WinCounter")]
        public int WinCount { get; set; }

        [JsonRequired]
        [JsonProperty("LoseCounter")]
        public int LoseCount { get; set; }

        [JsonIgnore]
        public int DrawCount => PartyCount - WinCount - LoseCount;

        [JsonIgnore]
        public decimal WinRate => WinCount / PartyCount * 100;

    }
}
