namespace ProjectManagementApp.ProjectManagementApp.Entities
{
    public class UsersInfo
    {
        public User userInfo { get; set; }
        public Project lastProject { get; set; }
        public int tasksAmount { get; set; }
        public int notFinishedTasks { get; set; }
        public Task theLongestTask { get; set; }

        public override string ToString()
        {
            return string.Format($"User Id: {userInfo.Id}\n" +
                                 $"Last Project Id: {lastProject.Id}\n" +
                                 $"Tasks Amount: {tasksAmount}\n" +
                                 $"Not Finished Tasks: {notFinishedTasks}\n" +
                                 $"The Longest Task: {theLongestTask.Id}\n");
        }
    }
}
