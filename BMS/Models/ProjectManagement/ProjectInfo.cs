namespace BMS.Models.ProjectManagement {
    public class ProjectInfo {
        public int Id { get; set; }
        public string CustomerProjectNumber { get; set; }
        public string CustomerProjectName { get; set; }
        public string MyProjectNumber { get; set; }
        public string MyProjectName { get; set; }
        public string ProjectType { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
