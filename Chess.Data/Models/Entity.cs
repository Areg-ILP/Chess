using Newtonsoft.Json;
using System;

namespace Chess.Data.Models
{
    /// <summary>
    /// Root class for entities
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Entity
    {
        /// <summary>
        /// entity id
        /// </summary>
        [JsonRequired]
        [JsonProperty("UnicalId")]
        public int Id { get; set; }

        /// <summary>
        /// entity creation date
        /// </summary>
        [JsonRequired]
        [JsonProperty("CreationDate")]
        public DateTime CreationDate { get; set; }
    }
}
