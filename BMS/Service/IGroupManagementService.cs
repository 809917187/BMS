using BMS.Models.CustomerManagement;
using BMS.Models.GroupManagement;

namespace BMS.Service {
    public interface IGroupManagementService {
        public bool AddGroup(GroupInfo groupInfo);
        public List<GroupInfo> GetAllGroupInfos();

        public bool BindProjectToGroup(int customerId, List<int> projectIds);
    }
}
