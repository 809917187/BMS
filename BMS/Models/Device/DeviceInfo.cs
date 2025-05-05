
using BMS.Models.ProjectManagement;

namespace BMS.Models.Device {
    public class DeviceInfo {
        public string Sn { get; set; }
        public DateTime ActivationTime { get; set; }
        public ProjectInfo projectInfo { get; set; }
        List<BatteryClusterInfo> batteryClusterInfos { get; set; } = new List<BatteryClusterInfo>();//历史数据
    }
}
