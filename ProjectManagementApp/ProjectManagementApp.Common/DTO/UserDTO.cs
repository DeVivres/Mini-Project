using System;

namespace ProjectManagementApp.Common.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime RegisteredAt { get; set; }
        public int? TeamId { get; set; }
    }
}
