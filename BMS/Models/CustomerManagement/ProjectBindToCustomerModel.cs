namespace BMS.Models.CustomerManagement {
    public class ProjectBindToCustomerModel {
        public int customerId { get; set; }
        public List<int> projectIds { get; set; }
    }
}
