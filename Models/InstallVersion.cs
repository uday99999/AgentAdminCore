using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AgentAdminCore.Models
{
    public class InstallVersion
    {
        [JsonPropertyName("id")]
        public int? ID { get; set; }
        [JsonPropertyName("version")]
        public string Version { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("relative_path")]
        public string RelativePath { get; set; }
        [JsonPropertyName("is_retired")]
        public bool IsRetired { get; set; }
        //[JsonPropertyName("install_instance_teamcount")]
        ////public List<InstallInstanceTeamCount> InstallInstanceTeamCounts { get; set; }
    }
}
