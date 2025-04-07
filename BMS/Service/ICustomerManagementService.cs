using BMS.Models.CustomerManagement;

namespace BMS.Service {
    public interface ICustomerManagementService {
        public List<CustomerInfo> GetAllCustomerInfos();
        public bool AddCustomerInfos(CustomerInfo customerInfo);
        public bool BindProjectToCustomer(int customerId, List<int> projectIds);

    }
}
