using BMS.AttributeTag;
using BMS.Models.CustomerManagement;
using BMS.Models.Device;
using BMS.Models.ProjectManagement;
using ClickHouse.Client.ADO;
using Dapper;
using MySql.Data.MySqlClient;
using System.Reflection;

namespace BMS.Service {
    public class DeviceManagementService : IDeviceManagementService {
        private string _connectionString;
        private string _connectionStringClickHouse;
        public DeviceManagementService(IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString("bms");
            _connectionStringClickHouse = configuration.GetConnectionString("bms_clickhouse");
        }

        public bool AddDeviceInfo(List<DeviceInfo> deviceInfos) {
            if (deviceInfos == null || deviceInfos.Count == 0)
                return false; // 没有数据，直接返回

            using (var conn = new MySqlConnection(_connectionString)) {
                conn.Open();
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

                try {
                    string sql = @"
                        INSERT INTO device_info (sn, activation_time)
                        VALUES (@Sn,@ActivationTime);";

                    conn.Execute(sql, deviceInfos);


                    return true;
                } catch (Exception ex) {

                    Console.WriteLine("数据库插入失败: " + ex.Message);
                    return false;
                }
            }
        }

        public bool DeviceBindToProject(string sn, int projectId) {
            using (MySqlConnection conn = new MySqlConnection(_connectionString)) {
                conn.Open();
                try {
                    string sql = "UPDATE device_info SET project_id = @projectId WHERE sn=@sn";
                    // 使用 MySqlCommand 执行更新操作
                    using (var cmd = new MySqlCommand(sql, conn)) {
                        cmd.Parameters.AddWithValue("@sn", sn);
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
                    "device_info.sn," +
                    "device_info.activation_time," +
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

        public List<BatteryClusterInfo> GetDailyBatteryClusterInfos(List<string> snList) {
            List<BatteryClusterInfo> ret = new List<BatteryClusterInfo>();

            try {
                using (ClickHouseConnection connection = new ClickHouseConnection(_connectionStringClickHouse)) {
                    connection.Open();

                    // 定义 SQL 查询
                    using (var command = connection.CreateCommand()) {
                        var snListLiteral = string.Join(",", snList.Select(sn => $"'{sn.Replace("'", "''")}'"));

                        command.CommandText = $@"
                            SELECT sn, upload_time, device_type, device_name, device_id, data 
                            FROM battery_cluster_information 
                            WHERE sn IN ({snListLiteral})
                              AND toDate(upload_time) = today()
                            ORDER BY upload_time DESC";

                        using (var reader = command.ExecuteReader()) {
                            List<OrignialBatteryClusterData> info = new List<OrignialBatteryClusterData>();
                            while (reader.Read()) {
                                info.Add(new OrignialBatteryClusterData {
                                    Sn = reader.GetString(reader.GetOrdinal("sn")),
                                    UploadTime = reader.GetDateTime(reader.GetOrdinal("upload_time")),
                                    DeviceType = reader.GetString(reader.GetOrdinal("device_type")),
                                    DeviceName = reader.GetString(reader.GetOrdinal("device_name")),
                                    DeviceId = reader.GetString(reader.GetOrdinal("device_id")),
                                    PointData = (int[])reader.GetValue(reader.GetOrdinal("data"))
                                });
                            }

                            return this.PraseBatteryClusterInfo(info);
                        }

                    }

                }
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return ret;
        }
        public List<BatteryClusterInfo> GetBatteryClusterInfosBySn(string sn) {
            List<BatteryClusterInfo> ret = new List<BatteryClusterInfo>();

            try {
                using (ClickHouseConnection connection = new ClickHouseConnection(_connectionStringClickHouse)) {
                    connection.Open();

                    // 定义 SQL 查询
                    using (var command = connection.CreateCommand()) {
                        command.CommandText = @"
                            SELECT sn, upload_time,device_type,device_name ,device_id,data 
                            FROM battery_cluster_information 
                            WHERE sn = @sn 
                            ORDER BY upload_time DESC ";
                        var param = command.CreateParameter();
                        param.ParameterName = "sn";
                        param.Value = sn;
                        command.Parameters.Add(param);

                        using (var reader = command.ExecuteReader()) {
                            List<OrignialBatteryClusterData> info = new List<OrignialBatteryClusterData>();
                            while (reader.Read()) {
                                info.Add(new OrignialBatteryClusterData {
                                    Sn = sn,
                                    UploadTime = reader.GetDateTime(reader.GetOrdinal("upload_time")),
                                    DeviceType = reader.GetString(reader.GetOrdinal("device_type")),
                                    DeviceName = reader.GetString(reader.GetOrdinal("device_name")),
                                    DeviceId = reader.GetString(reader.GetOrdinal("device_id")),
                                    PointData = (int[])reader.GetValue(reader.GetOrdinal("data"))
                                });
                            }

                            return this.PraseBatteryClusterInfo(info);
                        }

                    }

                }
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return ret;
        }

        private List<BatteryClusterInfo> PraseBatteryClusterInfo(List<OrignialBatteryClusterData> orignialBatteryClusterDatas) {
            List<BatteryClusterInfo> ret = new List<BatteryClusterInfo>();

            foreach (OrignialBatteryClusterData orignialBatteryData in orignialBatteryClusterDatas) {
                BatteryClusterInfo batteryClusterInfo = new BatteryClusterInfo();
                SetModelPropertiesByMap(batteryClusterInfo, orignialBatteryData.PointData);
                batteryClusterInfo.Sn = orignialBatteryData.Sn;
                batteryClusterInfo.UploadTime = orignialBatteryData.UploadTime;
                batteryClusterInfo.DeviceType = orignialBatteryData.DeviceType;
                batteryClusterInfo.DeviceName = orignialBatteryData.DeviceName;
                batteryClusterInfo.DeviceId = orignialBatteryData.DeviceId;

                //解析
                batteryClusterInfo.BcuOperatingStatus = Convert.ToInt32(batteryClusterInfo.BcuOperatingStatus) & 0x0F;
                batteryClusterInfo.TotalVoltage = batteryClusterInfo.TotalVoltage * 0.1f;
                batteryClusterInfo.TotalCurrent = batteryClusterInfo.TotalCurrent * 0.1f;
                batteryClusterInfo.RatedTotalVoltage = batteryClusterInfo.RatedTotalVoltage * 0.1f;
                batteryClusterInfo.RatedCapacity = batteryClusterInfo.RatedCapacity * 0.1f;
                batteryClusterInfo.RemainingCapacity = batteryClusterInfo.RemainingCapacity * 0.1f;
                batteryClusterInfo.RatedEnergy = batteryClusterInfo.RatedEnergy * 0.1f;
                batteryClusterInfo.RemainingEnergy = batteryClusterInfo.RemainingEnergy * 0.1f;
                batteryClusterInfo.MaxAllowableDischargeCurrent = batteryClusterInfo.MaxAllowableDischargeCurrent * 0.1f;
                batteryClusterInfo.MaxAllowableDischargePower = batteryClusterInfo.MaxAllowableDischargePower * 0.1f;
                batteryClusterInfo.MaxAllowableChargeCurrent = batteryClusterInfo.MaxAllowableChargeCurrent * 0.1f;
                batteryClusterInfo.MaxAllowableChargePower = batteryClusterInfo.MaxAllowableChargePower * 0.1f;
                batteryClusterInfo.DailyChargeCapacity = batteryClusterInfo.DailyChargeCapacity * 0.1f;
                batteryClusterInfo.DailyChargeEnergy = batteryClusterInfo.DailyChargeEnergy * 0.1f;
                batteryClusterInfo.DailyDischargeCapacity = batteryClusterInfo.DailyDischargeCapacity * 0.1f;
                batteryClusterInfo.DailyDischargeEnergy = batteryClusterInfo.DailyDischargeEnergy * 0.1f;
                batteryClusterInfo.DailyChargeTime = batteryClusterInfo.DailyChargeTime * 0.1f;
                batteryClusterInfo.DailyDischargeTime = batteryClusterInfo.DailyDischargeTime * 0.1f;
                batteryClusterInfo.CumulativeChargeCapacity = batteryClusterInfo.CumulativeChargeCapacity * 0.1f;
                batteryClusterInfo.CumulativeChargeEnergy = batteryClusterInfo.CumulativeChargeEnergy * 0.1f;
                batteryClusterInfo.CumulativeDischargeCapacity = batteryClusterInfo.CumulativeDischargeCapacity * 0.1f;
                batteryClusterInfo.CumulativeDischargeEnergy = batteryClusterInfo.CumulativeDischargeEnergy * 0.1f;
                batteryClusterInfo.CumulativeChargeTime = batteryClusterInfo.CumulativeChargeTime * 0.1f;



                ret.Add(batteryClusterInfo);
            }


            return ret;
        }

        public void SetModelPropertiesByMap<T>(T model, int[] values) {
            var props = typeof(T).GetProperties();

            foreach (var prop in props) {
                if (Attribute.IsDefined(prop, typeof(NotPointDataAttribute)))
                    continue;

                if (Attribute.IsDefined(prop, typeof(PointRangeAttribute))) {
                    var attrRange = prop.GetCustomAttribute<PointRangeAttribute>();

                    prop.SetValue(model, values.Skip(attrRange.StartIndex).Take(attrRange.EndIndex - attrRange.StartIndex + 1).ToArray());
                }

                var attr = prop.GetCustomAttribute<PointIndexAttribute>();
                if (attr != null && attr.Index < values.Length) {
                    var value = values[attr.Index];
                    if (value != null && prop.CanWrite) {
                        object converted = Convert.ChangeType(value, prop.PropertyType);
                        prop.SetValue(model, converted);
                    }
                }
            }
        }


        public BatteryClusterInfo GetLatestBatteryClusterInfosBySn(string sn) {

            try {
                using (ClickHouseConnection connection = new ClickHouseConnection(_connectionStringClickHouse)) {
                    connection.Open();

                    // 定义 SQL 查询
                    using (var command = connection.CreateCommand()) {
                        command.CommandText = @"
                            SELECT TOP(1) sn, upload_time,device_type,device_name ,device_id,data 
                            FROM battery_cluster_information 
                            WHERE sn = @sn 
                            ORDER BY upload_time DESC ";
                        var param = command.CreateParameter();
                        param.ParameterName = "sn";
                        param.Value = sn;
                        command.Parameters.Add(param);

                        using (var reader = command.ExecuteReader()) {
                            List<OrignialBatteryClusterData> info = new List<OrignialBatteryClusterData>();
                            while (reader.Read()) {
                                info.Add(new OrignialBatteryClusterData {
                                    Sn = sn,
                                    UploadTime = reader.GetDateTime(reader.GetOrdinal("upload_time")),
                                    DeviceType = reader.GetString(reader.GetOrdinal("device_type")),
                                    DeviceName = reader.GetString(reader.GetOrdinal("device_name")),
                                    DeviceId = reader.GetString(reader.GetOrdinal("device_id")),
                                    PointData = (int[])reader.GetValue(reader.GetOrdinal("data"))
                                });
                            }

                            return this.PraseBatteryClusterInfo(info)[0];
                        }

                    }

                }
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return null;
        }
        public List<BatteryClusterInfo> GetLatestBatteryClusterInfos() {
            List<BatteryClusterInfo> ret = new List<BatteryClusterInfo>();

            try {
                using (ClickHouseConnection connection = new ClickHouseConnection(_connectionStringClickHouse)) {
                    connection.Open();

                    // 定义 SQL 查询
                    using (var command = connection.CreateCommand()) {
                        command.CommandText = @"
                            SELECT
                                sn,
                                argMax(device_type, upload_time) AS device_type,
                                argMax(device_name, upload_time) AS device_name,
                                argMax(device_id, upload_time) AS device_id,
                                argMax(data, upload_time) AS data,
                                max(upload_time) AS latest_upload_time
                            FROM battery_cluster_information
                            GROUP BY sn";

                        using (var reader = command.ExecuteReader()) {
                            List<OrignialBatteryClusterData> info = new List<OrignialBatteryClusterData>();
                            while (reader.Read()) {
                                info.Add(new OrignialBatteryClusterData {
                                    Sn = reader.GetString(reader.GetOrdinal("sn")),
                                    UploadTime = reader.GetDateTime(reader.GetOrdinal("latest_upload_time")),
                                    DeviceType = reader.GetString(reader.GetOrdinal("device_type")),
                                    DeviceName = reader.GetString(reader.GetOrdinal("device_name")),
                                    DeviceId = reader.GetString(reader.GetOrdinal("device_id")),
                                    PointData = (int[])reader.GetValue(reader.GetOrdinal("data"))
                                });
                            }

                            return this.PraseBatteryClusterInfo(info);
                        }

                    }

                }
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return ret;
        }
    }
}
