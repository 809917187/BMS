using BMS.Models.CustomerManagement;
using BMS.Models.Device;
using BMS.Models.ProjectManagement;
using Dapper;
using MySql.Data.MySqlClient;

namespace BMS.Service {
    public class DeviceManagementService : IDeviceManagementService {
        private string _connectionString;
        public DeviceManagementService(IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString("bms");
        }

        public bool DeviceBindToProject(int deviceId, int projectId) {
            using (MySqlConnection conn = new MySqlConnection(_connectionString)) {
                conn.Open();
                try {
                    string sql = "UPDATE device_info SET project_id = @projectId WHERE id=@deviceId";
                    // 使用 MySqlCommand 执行更新操作
                    using (var cmd = new MySqlCommand(sql, conn)) {
                        cmd.Parameters.AddWithValue("@deviceId", deviceId);
                        cmd.Parameters.AddWithValue("@projectId", projectId);

                        // 执行更新
                        cmd.ExecuteNonQuery();
                    }
                } catch (Exception ex) {
                    return false;
                }
                return true;
            }
        }

        public List<DeviceInfo> GetAllDeviceInfo() {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            using (var connection = new MySqlConnection(_connectionString)) {
                connection.Open();
                string query = "SELECT " +
                    "device_info.id,device_info.battery_series_number, device_info.car_series_number," +
                    "device_info.bms_series_number,device_info.activation_time," +
                    "project_info.id as project_id,project_info.customer_project_number,project_info.customer_project_name," +
                    "project_info.my_project_number,project_info.my_project_name,project_info.project_type,project_info.create_time " +
                    "FROM bms.device_info " +
                    "LEFT JOIN project_info ON project_info.id = device_info.project_id;";
                var infos = connection.Query<DeviceInfo, ProjectInfo, DeviceInfo>(
                    query,
                    (device, project) => {
                        device.projectInfo = project; // 绑定 projectInfo
                        return device;
                    },
                    splitOn: "project_id" // 告诉 Dapper "project_id" 之后是 ProjectInfo 的数据
                ).ToList();
                return infos;
            }
        }
    }
}
