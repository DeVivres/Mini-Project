using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.ProjectManagementApp.Entities
{
    public class TaskState
    {
        [Required]
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [MinLength(1)]
        [JsonProperty("value")]
        public string Value { get; set; }
        public override string ToString()
        {
            return string.Format($"Task State Id {Id}\n" +
                                 $"Value: {Value}\n");
        }
    }
}
