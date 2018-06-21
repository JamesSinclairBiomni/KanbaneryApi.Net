using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KanbaneryApi.Net.Models
{
    public class TaskSubscription
    {
        [JsonProperty(PropertyName = "task_id")]
        public int TaskId { get; set; }

        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
