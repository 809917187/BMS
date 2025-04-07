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

        public bool AddDeviceInfo(List<DeviceInfo> deviceInfos) {
            if (deviceInfos == null || deviceInfos.Count == 0)
                return false; // 没有数据，直接返回

            using (var conn = new MySqlConnection(_connectionString)) {
                conn.Open();
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                using (var transaction = conn.BeginTransaction()) // 开启事务
                {
                    try {
                        string sql = @"
                        INSERT INTO device_info (battery_series_number, car_series_number, bms_series_number, activation_time)
                        VALUES (@BatterySeriesNumber, @CarSeriesNumber, @BMSSeriesNumber,@ActivationTime);";

                        conn.Execute(sql, deviceInfos, transaction);

                        transaction.Commit(); // 提交事务
                        return true;
                    } catch (Exception ex) {
                        transaction.Rollback(); // 发生异常时回滚
                        Console.WriteLine("数据库插入失败: " + ex.Message);
                        return false;
                    }
                }
            }
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
                        if (project != null) {
                            device.projectInfo.Id = project.ProjectId; // 将 project_id 赋给 device.projectInfo.Id
                        }
                        return device;
                    },
                    splitOn: "project_id" // 告诉 Dapper "project_id" 之后是 ProjectInfo 的数据
                ).ToList();
                return infos;
            }
        }


        public List<BatteryClusterInfo> GetBatteryClusterInfosBySn(string sn) {
            List<BatteryClusterInfo> ret = new List<BatteryClusterInfo>();

            try {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                // 创建 MySQL 连接
                using (MySqlConnection connection = new MySqlConnection(_connectionString)) {
                    connection.Open();

                    // 定义 SQL 查询
                    string sql = @"
                        SELECT * 
                        FROM battery_cluster_info
                        WHERE sn = @sn;
                    ";

                    ret = connection.Query<BatteryClusterInfo>(sql, new { sn = sn }).AsList();
                }
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return ret;
        }
        public List<BatteryClusterInfo> GetLatestBatteryClusterInfos() {
            List<BatteryClusterInfo> ret = new List<BatteryClusterInfo>();

            try {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                // 创建 MySQL 连接
                using (MySqlConnection connection = new MySqlConnection(_connectionString)) {
                    connection.Open();

                    // 定义 SQL 查询
                    string sql = @"
                        WITH LatestData AS (
                            SELECT *,
                                   ROW_NUMBER() OVER (PARTITION BY device_id ORDER BY upload_time DESC) AS rn
                            FROM battery_cluster_info
                        )
                        SELECT * 
                        FROM LatestData
                        WHERE rn = 1;
                    ";

                    ret = connection.Query<BatteryClusterInfo>(sql).AsList();
                }
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return ret;
        }
    }
}
