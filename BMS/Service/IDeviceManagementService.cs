using BMS.Models.Device;
using BMS.Models.ProjectManagement;

namespace BMS.Service {
    public interface IDeviceManagementService {
        public List<DeviceInfo> GetAllDeviceInfo();
        //public bool AddProjectInfo(ProjectInfo projectInfo);
        public bool DeviceBindToProject(int deviceId, int projectId);
    }
}
