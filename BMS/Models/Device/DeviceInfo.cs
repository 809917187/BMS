
using BMS.Models.ProjectManagement;

namespace BMS.Models.Device {
    public class DeviceInfo {
        public int Id { get; set; }
        public string CarSeriesNumber { get; set; }
        public string BatterySeriesNumber { get; set; }
        public string BMSSeriesNumber { get; set; }
        public DateTime ActivationTime { get; set; }
        public ProjectInfo projectInfo { get; set; }
        List<BatteryClusterInfo> batteryClusterInfos { get; set; } = new List<BatteryClusterInfo>();//历史数据
    }
}
