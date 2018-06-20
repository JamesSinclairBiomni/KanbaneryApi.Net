using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KanbaneryApi.Net.Models
{
    public class Task
    {
        [JsonProperty(PropertyName = "id")]
        public int TaskId { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "task_type_id")]
        public int? TaskTypeId { get; set; }

        [JsonProperty(PropertyName = "column_id")]
        public int? ColumnId { get; set; }

        [JsonProperty(PropertyName = "creator_id")]
        public int? CreatorId { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "estimate_id")]
        public int? EstimateId { get; set; }

        [JsonProperty(PropertyName = "owner_id")]
        public int? OwnerId { get; set; }

        [JsonProperty(PropertyName = "position")]
        public int? Position { get; set; }

        [JsonProperty(PropertyName = "priority")]
        public int? Priority { get; set; }

        [JsonProperty(PropertyName = "ready_to_pull")]
        public bool? ReadyToPull { get; set; }

        [JsonProperty(PropertyName = "blocked")]
        public bool? Blocked { get; set; }

        [JsonProperty(PropertyName = "moved_at")]
        public DateTime? MovedAt { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
