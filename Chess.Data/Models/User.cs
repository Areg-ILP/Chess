using Newtonsoft.Json;

namespace Chess.Data.Models
{
    /// <summary>
    /// Entity - user
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class User : Entity
    {
        /// <summary>
        /// Login (short name ) of user
        /// </summary>
        [JsonRequired]
        [JsonProperty("Login")]
        public string Login { get; set; }

        /// <summary>
        /// Password of user
        /// </summary>
        [JsonRequired]
        [JsonProperty("Password")]
        public string Password { get; set; }

        /// <summary>
        /// user parrty count
        /// </summary>
        [JsonRequired]
        [JsonProperty("PartyCounter")]
        public int PartyCount { get; set; }

        /// <summary>
        /// user win party count
        /// </summary>
        [JsonRequired]
        [JsonProperty("WinCounter")]
        public int WinCount { get; set; }

        /// <summary>
        /// user lose party count
        /// </summary>
        [JsonRequired]
        [JsonProperty("LoseCounter")]
        public int LoseCount { get; set; }

        /// <summary>
        /// user draw party count
        /// </summary>
        [JsonIgnore]
        public int DrawCount => PartyCount - WinCount - LoseCount;

    }
}
