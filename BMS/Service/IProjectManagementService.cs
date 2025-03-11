using BMS.Models.ProjectManagement;

namespace BMS.Service {
    public interface IProjectManagementService {
        public List<ProjectInfo> GetAllProjectInfo();
        public bool AddProjectInfo(ProjectInfo projectInfo);
        public ProjectInfo GetBindedProjectByDeviceId(int id);
        public List<int> GetBindedProjectIdsByCustomerId(int customerId);
        public List<int> GetBindedProjectIdsByGroupId(int groupId);
    }
}
