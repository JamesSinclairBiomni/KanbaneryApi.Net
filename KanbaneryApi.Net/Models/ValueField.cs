using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KanbaneryApi.Net.Models
{
    public class ValueField
    {
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "project_field_id")]
        public string ProjectFieldId { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "position")]
        public int Position { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
