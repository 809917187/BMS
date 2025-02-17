using BMS.Models.ProjectManagement;

namespace BMS.Service {
    public interface IProjectManagementService {
        public List<ProjectInfo> GetAllProjectInfo();
        public bool AddProjectInfo(ProjectInfo projectInfo);
    }
}
