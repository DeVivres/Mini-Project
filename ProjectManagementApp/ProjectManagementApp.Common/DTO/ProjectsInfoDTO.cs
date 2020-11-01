namespace ProjectManagementApp.Common.DTO
{
    public class ProjectsInfoDTO
    {
        public ProjectDTO Project { get; set; }
        public TaskDTO TaskWithLongestDescription { get; set; }
        public TaskDTO TaskWithShortestName { get; set; }
        public int? MembersCountByTasksCountAndProjDescLength { get; set; }
    }
}
