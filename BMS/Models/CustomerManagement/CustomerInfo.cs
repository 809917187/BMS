using BMS.Models.Device;
using BMS.Models.ProjectManagement;

namespace BMS.Models.CustomerManagement {
    public class CustomerInfo {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public List<ProjectInfo> ProjectInfos { get; set; }
        
        public DateTime CreateTime { get; set; }

    }
}
