using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AgentAdminCore.Models
{
    [Serializable]
    public class IdleCode
    {
        [JsonPropertyName("id")]
        public int? ID{ get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("modified_by")]
        public string ModifiedBy { get; set; }
        [JsonPropertyName("modified_on")]
        public DateTime ModifiedOn { get; set; }
        [JsonPropertyName("reason_code")]
        public int ReasonCode { get; set; }
        [JsonPropertyName("is_default")]
        public bool IsDefault { get; set; }
        [JsonPropertyName("timer_format")]
        public int TimerFormat { get; set; }
    }
}
