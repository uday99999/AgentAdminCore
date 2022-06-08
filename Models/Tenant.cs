using System;
using System.Text.Json.Serialization;

namespace AgentAdminCore.Models
{
    [Serializable]
    public class Tenant
    {
            [JsonPropertyName("id")]
            public int? ID { get; set; }
            [JsonPropertyName("name")]
            public string Name { get; set; }
            [JsonPropertyName("modified_by")]
            public string? ModifiedBy { get; set; }
            [JsonPropertyName("modified_on")]
            public DateTime ModifiedOn { get; set; }
    }
}
