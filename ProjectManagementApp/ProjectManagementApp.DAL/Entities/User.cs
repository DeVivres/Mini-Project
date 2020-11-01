using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.ProjectManagementApp.Entities
{
    public class User
    {
        [Required]
        [JsonProperty("id")]
        public int Id { get; set; }

        [MinLength(1)]
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [MinLength(1)]
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        
        [DataType(DataType.EmailAddress)]
        [JsonProperty("email")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; }

        [DataType(DataType.Date)]
        [JsonProperty("registeredAt")]
        public DateTime RegisteredAt { get; set; }

        [JsonProperty("teamId")]
        public int? TeamId { get; set; }
        public Team Team { get; set; }
        public ICollection<Task> UserTasks { get; set; }

        public override string ToString()
        {
            return string.Format($"User Id: {Id}\n" +
                                 $"First Name: {FirstName}\n" +
                                 $"Last Name: {LastName}\n" +
                                 $"Email: {Email}\n" +
                                 $"Birthday: {Birthday}\n" +
                                 $"Registered At: {RegisteredAt}\n" +
                                 $"Team Id: {TeamId}\n");
        }
    }
}
