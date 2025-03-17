using BMS.Models.Device;

namespace BMS.Models.ProjectManagement {
    public class ProjectInfo {
        public int Id { get; set; }
        public string CustomerProjectNumber { get; set; } = String.Empty;
        public string CustomerProjectName { get; set; } = String.Empty;
        public string MyProjectNumber { get; set; } = String.Empty;
        public string MyProjectName { get; set; } = String.Empty;
        public string ProjectType { get; set; } = String.Empty;
        public List<DeviceInfo> DeviceInfos { get; set; } = new List<DeviceInfo>();
        public DateTime CreateTime { get; set; }
        public bool IsSelected { get; set; } = false;
        public int DeviceCount { get; set; }

    }
}
