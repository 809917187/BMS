using BMS.Models.ProjectManagement;
using Dapper;
using MySql.Data.MySqlClient;

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
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            using (var connection = new MySqlConnection(_connectionString)) {
                connection.Open();
                string query = "SELECT * FROM project_info";
                var projectInfos = connection.Query<ProjectInfo>(query).ToList();
                return projectInfos;
            }

        }

        public ProjectInfo GetBindedProjectByDeviceId(int DeviceId) {
            ProjectInfo? ret = null;
            using (var connection = new MySqlConnection(_connectionString)) {
                string sql = "SELECT * FROM bms.project_info where id=(SELECT project_id FROM device_info Where id = @id)";
                ret = connection.Query<ProjectInfo>(sql, new { id = DeviceId }).FirstOrDefault();
            }
            return ret;
        }

        public List<int> GetBindedProjectIdsByCustomerId(int customerId) {
            List<int> ret = new List<int>();
            using (var connection = new MySqlConnection(_connectionString)) {
                string sql = "SELECT project_id FROM bms.customer_bind_project where customer_id=@customerId";
                ret = connection.Query<int>(sql, new { customerId = customerId }).ToList();
            }
            return ret;
        }

        public List<int> GetBindedProjectIdsByGroupId(int groupId) {
            List<int> ret = new List<int>();
            using (var connection = new MySqlConnection(_connectionString)) {
                string sql = "SELECT project_id FROM bms.group_bind_project where group_id=@groupId";
                ret = connection.Query<int>(sql, new { groupId = groupId }).ToList();
            }
            return ret;
        }
    }
}
