    using Newtonsoft.Json;
using System;

namespace Chess.Data.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Entity
    {
        [JsonRequired]
        [JsonProperty("UnicalId")]
        public int Id { get; set; }

        [JsonRequired]
        [JsonProperty("CreationDate")]
        public DateTime CreationDate { get; set; }
    }
}
