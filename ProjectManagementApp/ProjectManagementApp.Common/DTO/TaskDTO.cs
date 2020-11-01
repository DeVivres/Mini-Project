using System;

namespace ProjectManagementApp.Common.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public int State { get; set; }
        public int ProjectId { get; set; }
        public int PerformerId { get; set; }
        public UserDTO Performer { get; set; }
    }
}
