using BMS.Models.Device;
using BMS.Models.ProjectManagement;

namespace BMS.Service {
    public interface IDeviceManagementService {
        public List<DeviceInfo> GetAllDeviceInfo();
        public bool AddDeviceInfo(List<DeviceInfo> deviceInfos);
        public bool DeviceBindToProject(string sn, int projectId);
        public List<BatteryClusterInfo> GetLatestBatteryClusterInfos();
        public List<BatteryClusterInfo> GetDailyBatteryClusterInfos(List<string> sns);
        public List<BatteryClusterInfo> GetBatteryClusterInfosBySn(string sn);
        public BatteryClusterInfo GetLatestBatteryClusterInfosBySn(string sn);
    }
}
