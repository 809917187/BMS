using BMS.Models.Device;
using BMS.Models.ProjectManagement;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace BMS.Service {
    public class ProjectManagementService : IProjectManagementService {
        private string _connectionString;
        public ProjectManagementService(IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString("bms");
        }
        public bool AddProjectInfo(ProjectInfo projectInfo) {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            using (MySqlConnection conn = new MySqlConnection(_connectionString)) {
                conn.Open();
                using (var transaction = conn.BeginTransaction()) {
                    try {
                        string sql_main = "INSERT INTO " +
                            "project_info " +
                            "(customer_project_number,customer_project_name,my_project_number,my_project_name,project_type) " +
                            "VALUES " +
                            "(@CustomerProjectNumber,@CustomerProjectName,@MyProjectNumber,@MyProjectName,@ProjectType) ;" +
                            "SELECT LAST_INSERT_ID();";
                        projectInfo.Id = conn.ExecuteScalar<int>(sql_main, projectInfo, transaction);
                        transaction.Commit();
                    } catch (Exception ex) {
                        transaction.Rollback();
                        return false;
                    }
                    return true;
                }
            }
        }

        public List<ProjectInfo> GetAllProjectInfo() {
            List<ProjectInfo> ret = new List<ProjectInfo>();
            using (var connection = new MySqlConnection(_connectionString)) {
                connection.Open();
                string sql = @"
                                    SELECT 
                                        p.id AS ProjectId, p.customer_project_number AS CustomerProjectNumber,
                                        p.customer_project_name AS CustomerProjectName, p.my_project_number AS MyProjectNumber,
                                        p.my_project_name AS MyProjectName, p.project_type AS ProjectType,
                                        p.create_time AS ProjectCreateTime,
                                        d.sn AS BatterySeriesNumber,
                                        d.activation_time AS ActivationTime 
                                    FROM 
                                        bms.project_info p 
                                    LEFT JOIN device_info d ON p.id = d.project_id
                                    GROUP BY p.id,d.sn;
                                ";
                using (MySqlDataAdapter sda = new MySqlDataAdapter(sql, connection)) {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    foreach (DataRow dr in dt.Rows) {
                        int projectId = (int)dr["ProjectId"];
                        var existingProject = ret.Find(c => c.Id == projectId);

                        if (existingProject == null) {
                            // 创建新 CustomerInfo 对象
                            existingProject = new ProjectInfo {
                                Id = dr["ProjectId"] != DBNull.Value ? Convert.ToInt32(dr["ProjectId"]) : 0,  // 使用默认值0处理null
                                CustomerProjectNumber = dr["CustomerProjectNumber"] != DBNull.Value ? (string)dr["CustomerProjectNumber"] : string.Empty,  // 如果为null，则使用空字符串
                                CustomerProjectName = dr["CustomerProjectName"] != DBNull.Value ? (string)dr["CustomerProjectName"] : string.Empty,  // 如果为null，则使用空字符串
                                MyProjectNumber = dr["MyProjectNumber"] != DBNull.Value ? (string)dr["MyProjectNumber"] : string.Empty,  // 如果为null，则使用空字符串
                                MyProjectName = dr["MyProjectName"] != DBNull.Value ? (string)dr["MyProjectName"] : string.Empty,  // 如果为null，则使用空字符串
                                ProjectType = dr["ProjectType"] != DBNull.Value ? (string)dr["ProjectType"] : string.Empty,  // 如果为null，则使用空字符串
                                CreateTime = dr["ProjectCreateTime"] != DBNull.Value ? (DateTime)dr["ProjectCreateTime"] : DateTime.MinValue
                            };
                            ret.Add(existingProject);
                        }

                        // 创建 ProjectInfo 对象
                        var device = new DeviceInfo {
                            Sn = dr["BatterySeriesNumber"] != DBNull.Value ? (string)dr["BatterySeriesNumber"] : string.Empty,  // 如果为null，则使用空字符串
                            ActivationTime = dr["ActivationTime"] != DBNull.Value ? (DateTime)dr["ActivationTime"] : DateTime.MinValue,  // 如果为null，则使用默认日期
                        };


                        // 如果项目存在，加入该客户的项目列表
                        if (device.Sn != string.Empty) {
                            existingProject.DeviceInfos.Add(device);
                        }
                    }

                }

                return ret;
            }

        }

        public ProjectInfo GetBindedProjectByDeviceSN(string sn) {
            ProjectInfo? ret = null;
            using (var connection = new MySqlConnection(_connectionString)) {
                string sql = "SELECT * FROM bms.project_info where id=(SELECT project_id FROM device_info Where sn = @sn)";
                ret = connection.Query<ProjectInfo>(sql, new { sn = sn }).FirstOrDefault();
            }
            return ret;
        }

        /*public List<int> GetBindedProjectIdsByCustomerId(int customerId) {
            List<int> ret = new List<int>();
            using (var connection = new MySqlConnection(_connectionString)) {
                string sql = "SELECT project_id FROM bms.customer_bind_project where customer_id=@customerId";
                ret = connection.Query<int>(sql, new { customerId = customerId }).ToList();
            }
            return ret;
        }*/
        public List<int> GetBindedProjectIdsByCustomerIds(List<int> customerIds) {
            using (var connection = new MySqlConnection(_connectionString)) {
                string sql = "SELECT project_id FROM bms.customer_bind_project WHERE customer_id IN @customerIds";
                return connection.Query<int>(sql, new { customerIds }).ToList();
            }
        }


        /*public List<int> GetBindedProjectIdsByGroupId(int groupId) {
            List<int> ret = new List<int>();
            using (var connection = new MySqlConnection(_connectionString)) {
                string sql = "SELECT project_id FROM bms.group_bind_project where group_id=@groupId";
                ret = connection.Query<int>(sql, new { groupId = groupId }).ToList();
            }
            return ret;
        }*/
        public List<int> GetBindedProjectIdsByGroupIds(List<int> groupIds) {
            List<int> ret = new List<int>();
            using (var connection = new MySqlConnection(_connectionString)) {
                string sql = "SELECT project_id FROM bms.group_bind_project where group_id IN @groupIds";
                ret = connection.Query<int>(sql, new { groupIds = groupIds }).ToList();
            }
            return ret;
        }
    }
}
