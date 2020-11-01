namespace ProjectManagementApp.ProjectManagementApp.Entities
{
    public class ProjectsInfo
    {
        public Project Project { get; set; }
        public Task TaskWithLongestDescription { get; set; }
        public Task TaskWithShortestName { get; set; }
        public int? MembersCountByTasksCountAndProjDescLength { get; set; }
        public override string ToString()
        {
            return string.Format($"Project Id: {Project.Id}\n" +
                                 $"Description Legnth: {TaskWithLongestDescription?.Description.Length}\n" +
                                 $"Name Length: {TaskWithShortestName?.Name.Length}\n" +
                                 $"Team Members: {MembersCountByTasksCountAndProjDescLength}\n");
        }
    }
}
