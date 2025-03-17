using BMS.Models.CustomerManagement;
using BMS.Models.GroupManagement;
using BMS.Models.ProjectManagement;

namespace BMS.ViewModel {
    public class HomeViewModel {
        public List<ProjectInfo> projectInfos { set; get; } = new List<ProjectInfo>();
        public List<CustomerInfo> customerInfos { set; get; } = new List<CustomerInfo>();
        public List<GroupInfo> groupInfos { set; get; } = new List<GroupInfo>();
    }
}
