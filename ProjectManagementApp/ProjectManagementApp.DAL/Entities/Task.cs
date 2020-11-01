using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementApp.ProjectManagementApp.Entities
{
    public class Task
    {
        [Required]
        [JsonProperty("id")]
        public int Id { get; set; }

        [MinLength(1)]
        [JsonProperty("name")]
        public string Name { get; set; }

        [MaxLength(500)]
        [JsonProperty("description")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        [JsonProperty("finishedAt")]
        public DateTime FinishedAt { get; set; }
        
        [Required]
        [JsonProperty("state")]
        public int State { get; set; }

        [JsonProperty("projectId")]
        public int? ProjectId { get; set; }
        public Project Project { get; set; }

        [JsonProperty("performerId")]
        public int? PerformerId { get; set; }
        public User Performer { get; set; }

        public override string ToString()
        {
            return string.Format($"Project Id: {Id}\n" +
                                 $"Name: {Name}\n" +
                                 $"Description: {Description}\n" +
                                 $"Created At: {CreatedAt}\n" +
                                 $"Finished At: {FinishedAt}\n" +
                                 $"State: {State}\n" +
                                 $"Project Id: {ProjectId}\n" +
                                 $"Performer Id: {PerformerId}\n");
        }
    }
}
