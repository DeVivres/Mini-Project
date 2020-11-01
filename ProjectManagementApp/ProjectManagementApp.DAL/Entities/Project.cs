using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.ProjectManagementApp.Entities
{
    public class Project
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
        [JsonProperty("deadline")]
        public DateTime Deadline { get; set; }

        [JsonProperty("authorId")]
        public int? AuthorId { get; set; }       
        public User Author { get; set;  }

        [JsonProperty("teamId")]
        public int? TeamId { get; set; }
        public Team Team { get; set; }

        public ICollection<Task> Tasks { get; set; }

        public override string ToString()
        {
            return string.Format($"Project Id: {Id}\n" +
                                 $"Name: {Name}\n" +
                                 $"Description: {Description}\n" +
                                 $"Created At: {CreatedAt}\n" +
                                 $"Deadline: {Deadline}\n" +
                                 $"Author Id: {AuthorId}\n" +
                                 $"Team Id: {TeamId}\n");
        }
    }
}
