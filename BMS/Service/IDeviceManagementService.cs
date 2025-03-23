using BMS.Models.Device;
using BMS.Models.ProjectManagement;

namespace BMS.Service {
    public interface IDeviceManagementService {
        public List<DeviceInfo> GetAllDeviceInfo();
        public bool AddDeviceInfo(List<DeviceInfo> deviceInfos);
        public bool DeviceBindToProject(int deviceId, int projectId);
        public List<BatteryClusterInfo> GetLatestBatteryClusterInfos();
    }
}
