using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.ProjectManagementApp.Entities
{
    public class Team
    {
        [Required]
        [JsonProperty("id")]
        public int Id { get; set; }

        [MinLength(1)]
        [JsonProperty("name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        public ICollection<Project> Projects { get; set; }
        public ICollection<User> Users { get; set; }

        public override string ToString()
        {
            return string.Format($"Team Id {Id}\n" +
                                 $"Team Name: {Name}\n" +
                                 $"Created At: {CreatedAt}\n");
        }
    }
}
