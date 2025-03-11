using BMS.Models.ProjectManagement;

namespace BMS.Models.GroupManagement {
    public class GroupInfo {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public List<ProjectInfo> ProjectInfos { get; set; } = new List<ProjectInfo>();

        public DateTime CreateTime { get; set; }
    }
}
