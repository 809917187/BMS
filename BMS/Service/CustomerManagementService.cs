using BMS.Models.CustomerManagement;
using Dapper;
using MySql.Data.MySqlClient;

namespace BMS.Service {
    public class CustomerManagementService : ICustomerManagementService {
        private string _connectionString;
        public CustomerManagementService(IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString("bms");
        }
        public bool AddCustomerInfos(CustomerInfo customerInfo) {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            using (MySqlConnection conn = new MySqlConnection(_connectionString)) {
                conn.Open();
                using (var transaction = conn.BeginTransaction()) {
                    try {
                        string sql_main = "INSERT INTO " +
                            "customer_info " +
                            "(customer_name) " +
                            "VALUES " +
                            "(@CustomerName) ;" +
                            "SELECT LAST_INSERT_ID();";
                        customerInfo.Id = conn.ExecuteScalar<int>(sql_main, customerInfo, transaction);
                        transaction.Commit();
                    } catch (Exception ex) {
                        transaction.Rollback();
                        return false;
                    }
                    return true;
                }
            }
        }

        public List<CustomerInfo> GetAllCustomerInfos() {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            List<CustomerInfo> ret = new List<CustomerInfo>();
            using (var connection = new MySqlConnection(_connectionString)) {
                connection.Open();
                string query = "SELECT * FROM bms.customer_info;";
                ret = connection.Query<CustomerInfo>(query).ToList();
            }
            return ret;
        }
    }
}
