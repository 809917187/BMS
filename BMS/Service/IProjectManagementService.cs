using BMS.Models.ProjectManagement;

namespace BMS.Service {
    public interface IProjectManagementService {
        public List<ProjectInfo> GetAllProjectInfo();
        public bool AddProjectInfo(ProjectInfo projectInfo);
        public ProjectInfo GetBindedProjectByDeviceSN(string sn);
        /*public List<int> GetBindedProjectIdsByCustomerId(int customerId);*/
        public List<int> GetBindedProjectIdsByCustomerIds(List<int> customerId);
        /*public List<int> GetBindedProjectIdsByGroupId(int groupId);*/
        public List<int> GetBindedProjectIdsByGroupIds(List<int> groupId);
    }
}
