namespace ProjectManagementApp.Common.DTO
{
    public class UsersInfoDTO
    {
        public UserDTO userInfo { get; set; }
        public ProjectDTO lastProject { get; set; }
        public int tasksAmount { get; set; }
        public int notFinishedTasks { get; set; }
        public TaskDTO theLongestTask { get; set; }
    }
}
