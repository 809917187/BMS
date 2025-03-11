using BMS.Models.CustomerManagement;
using BMS.Models.ProjectManagement;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

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

        public bool BindProjectToCustomer(int customerId, List<int> projectIds) {
            using (MySqlConnection conn = new MySqlConnection(_connectionString)) {
                conn.Open();
                using (var transaction = conn.BeginTransaction()) {
                    try {
                        // 第一步：删除已绑定的项目记录
                        string sql_delete = "DELETE FROM customer_bind_project WHERE customer_id=@customerId";
                        using (MySqlCommand cmd = new MySqlCommand(sql_delete, conn, transaction)) {
                            cmd.Parameters.AddWithValue("@customerId", customerId);
                            cmd.ExecuteNonQuery();
                        }

                        if (projectIds.Count > 0) {

                            // 第二步：插入新的客户-项目绑定记录
                            string sql_insert = "INSERT INTO customer_bind_project(customer_id, project_id) VALUES ";
                            List<MySqlParameter> parameters = new List<MySqlParameter>();

                            for (int i = 0; i < projectIds.Count; i++) {
                                sql_insert += "(@customerId, @projectId" + i + "),";
                                parameters.Add(new MySqlParameter("@projectId" + i, projectIds[i]));
                            }

                            sql_insert = sql_insert.TrimEnd(',');

                            parameters.Add(new MySqlParameter("@customerId", customerId));  // 使用传入的 customerId

                            using (MySqlCommand cmd = new MySqlCommand(sql_insert, conn, transaction)) {
                                cmd.Parameters.AddRange(parameters.ToArray());
                                cmd.ExecuteNonQuery();
                            }
                        }


                        transaction.Commit();
                        return true;
                    } catch (Exception ex) {
                        transaction.Rollback();
                        Console.WriteLine(ex.Message);
                        return false;
                    }
                }

            }

        }

        public List<CustomerInfo> GetAllCustomerInfos() {
            List<CustomerInfo> ret = new List<CustomerInfo>();
            using (var connection = new MySqlConnection(_connectionString)) {
                connection.Open();
                string sql = @"
                SELECT 
                    c.id AS Id, c.customer_name AS CustomerName, c.create_time AS CreateTime,
                    p.id AS ProjectId, p.customer_project_number AS CustomerProjectNumber, 
                    p.customer_project_name AS CustomerProjectName, p.my_project_number AS MyProjectNumber,
                    p.my_project_name AS MyProjectName, p.project_type AS ProjectType, 
                    p.create_time AS ProjectCreateTime,
                    COUNT(d.id) AS DeviceCount
                FROM bms.customer_info c
                LEFT JOIN customer_bind_project cbp ON c.id = cbp.customer_id
                LEFT JOIN project_info p ON cbp.project_id = p.id 
                LEFT JOIN device_info d ON d.project_id = p.id 
                GROUP BY c.id, p.id;
        ";

                using (MySqlDataAdapter sda = new MySqlDataAdapter(sql, connection)) {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    foreach (DataRow dr in dt.Rows) {
                        int customerId = (int)dr["Id"];
                        var existingCustomer = ret.Find(c => c.Id == customerId);

                        if (existingCustomer == null) {
                            // 创建新 CustomerInfo 对象
                            existingCustomer = new CustomerInfo {
                                Id = customerId,
                                CustomerName = (string)dr["CustomerName"],
                                CreateTime = (DateTime)dr["CreateTime"]
                            };
                            ret.Add(existingCustomer);
                        }

                        // 创建 ProjectInfo 对象
                        var project = new ProjectInfo {
                            Id = dr["ProjectId"] != DBNull.Value ? Convert.ToInt32(dr["ProjectId"]) : 0,  // 使用默认值0处理null
                            CustomerProjectNumber = dr["CustomerProjectNumber"] != DBNull.Value ? (string)dr["CustomerProjectNumber"] : string.Empty,  // 如果为null，则使用空字符串
                            CustomerProjectName = dr["CustomerProjectName"] != DBNull.Value ? (string)dr["CustomerProjectName"] : string.Empty,  // 如果为null，则使用空字符串
                            MyProjectNumber = dr["MyProjectNumber"] != DBNull.Value ? (string)dr["MyProjectNumber"] : string.Empty,  // 如果为null，则使用空字符串
                            MyProjectName = dr["MyProjectName"] != DBNull.Value ? (string)dr["MyProjectName"] : string.Empty,  // 如果为null，则使用空字符串
                            ProjectType = dr["ProjectType"] != DBNull.Value ? (string)dr["ProjectType"] : string.Empty,  // 如果为null，则使用空字符串
                            CreateTime = dr["ProjectCreateTime"] != DBNull.Value ? (DateTime)dr["ProjectCreateTime"] : DateTime.MinValue,  // 如果为null，则使用默认日期
                            DeviceCount = dr["DeviceCount"] != DBNull.Value ? Convert.ToInt32(dr["DeviceCount"]) : 0  // 如果为null，则使用默认值0
                        };


                        // 如果项目存在，加入该客户的项目列表
                        if (project.Id != 0) {
                            existingCustomer.ProjectInfos.Add(project);
                        }
                    }

                }

                return ret;
            }
        }
    }
}
