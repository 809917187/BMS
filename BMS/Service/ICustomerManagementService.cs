using BMS.Models.CustomerManagement;

namespace BMS.Service {
    public interface ICustomerManagementService {
        public List<CustomerInfo> GetAllCustomerInfos();
        public bool AddCustomerInfos(CustomerInfo customerInfo);
    }
}
