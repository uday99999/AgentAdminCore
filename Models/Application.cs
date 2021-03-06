using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AgentAdminCore.Models
{
    [Serializable]
    public class Application
    {
        [JsonPropertyName("id")]
        public int? ID { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("login_url")]
        public string LoginUrl { get; set; }
        [JsonPropertyName("modified_by")]
        public string ModifiedBy { get; set; }
        [JsonPropertyName("modified_on")]
        public DateTime ModifiedOn { get; set; }
    }
}
