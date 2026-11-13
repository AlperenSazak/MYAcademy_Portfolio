using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.Models
{
    public class DashboardViewModel
    {
        public int TotalProjectCount { get; set; }
        public int ActiveProjectCount { get; set; }
        public int SkillCount { get; set; }
        public int ExperienceCount { get; set; }
        public int EducationCount { get; set; }
        public int TotalMessageCount { get; set; }
        public int UnreadMessageCount { get; set; }

        public List<Message> LastMessages { get; set; } = new List<Message>();
        public List<Project> LastProjects { get; set; } = new List<Project>();
        public List<CategoryProjectCount> CategorySummaries { get; set; } = new List<CategoryProjectCount>();
    }

    public class CategoryProjectCount
    {
        public string CategoryName { get; set; } = string.Empty;
        public int ProjectCount { get; set; }
    }
}
