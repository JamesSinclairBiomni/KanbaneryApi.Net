using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KanbaneryApi.Net.Models
{
    public class Subtask
    {
        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

        [JsonProperty(PropertyName = "task_id")]
        public int TaskId { get; set; }

        [JsonProperty(PropertyName = "creator_id")]
        public int CreatorId { get; set; }

        [JsonProperty(PropertyName = "completed")]
        public bool Completed { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
