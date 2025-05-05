using BMS.MQTT.Model;
using ClickHouse.Client.Copy;
using System.Text.Json;

namespace BMS.MQTT {
    public class MQTTHelperClickHouse {
        private static string _connectionString_clickhouse;
        public static void SetConnectionString(string connectionString_clickhouse) {
            _connectionString_clickhouse = connectionString_clickhouse;
        }

        private static Dictionary<string, DataFromMqtt> SnTimestramp2DataFromMqtt = new Dictionary<string, DataFromMqtt>();
        private static Dictionary<string, int> SnTimestramp2count = new Dictionary<string, int>();

        public static bool SaveMqttPeriodDataToDB(int index) {
            try {
                string json = MQTTHelperClickHouse.GetPeriodData(index);
                var rootObject = JsonSerializer.Deserialize<DeviceDataFromMqtt>(json);
                if (rootObject != null) {

                    foreach (DataFromMqtt dataFromMqtt in rootObject.devData.FindAll(s => s.devType == "4")) {
                        string key = dataFromMqtt.sn + rootObject.timeStamp;
                        if (!SnTimestramp2DataFromMqtt.ContainsKey(key)) {
                            SnTimestramp2DataFromMqtt[key] = dataFromMqtt;
                            SnTimestramp2count[key] = 1;
                        } else {
                            SnTimestramp2DataFromMqtt[key].data = SnTimestramp2DataFromMqtt[key].data.Concat(dataFromMqtt.data).ToDictionary(kv => kv.Key, kv => kv.Value); ;
                            SnTimestramp2count[key] = SnTimestramp2count[key] + 1;
                        }
                        if (SnTimestramp2count[key] == dataFromMqtt.total) {
                            DateTime UploadTime = DateTimeOffset.FromUnixTimeSeconds(rootObject.timeStamp).LocalDateTime;
                            MQTTHelperClickHouse.SaveBatteryClusterInfoAsync(SnTimestramp2DataFromMqtt[key], UploadTime);
                        }

                    }

                }


            } catch (Exception e) {
                return false;
            }

            return true;
        }

        public static async Task<bool> SaveBatteryClusterInfoAsync(DataFromMqtt info, DateTime UploadTime) {
            if (info == null) {
                return false;
            }
            try {
                using var bulkCopyInterface = new ClickHouseBulkCopy(_connectionString_clickhouse) {
                    DestinationTableName = "bms.battery_cluster_information",
                    BatchSize = 100000
                };
                await bulkCopyInterface.InitAsync();
                List<object[]> input = new List<object[]>();
                float[] data = new float[2000];
                int index;
                foreach (var vk in info.data) {
                    if (vk.Key.Contains("_") && int.TryParse(vk.Key.Split('_')[1], out index)) {
                        data[index] = vk.Value;
                    }
                }
                input.Add(new object[] {
                    info.sn,
                    UploadTime,
                    info.devType,
                    info.devName,
                    info.devId,
                    data
                });


                await bulkCopyInterface.WriteToServerAsync(input);
                Console.WriteLine(bulkCopyInterface.RowsWritten);

                return true;
            } catch (Exception e) {
                Console.WriteLine(e);
                return false;
            }

        }

        public static string GetPeriodData(int index) {
            string filePath = Path.Combine(AppContext.BaseDirectory, "Assets", "JsonFile", "period" + index + ".json");
            // 读取文件内容
            if (File.Exists(filePath)) {
                return File.ReadAllText(filePath).Replace("\r\n", "").Replace("\n", ""); ;
            } else {
                return String.Empty;
            }
        }

        /*
             CREATE TABLE battery_cluster_information
            (
                sn String,
                upload_time DateTime DEFAULT now(),
                device_type String,
                device_name String,
                device_id String,
                data Array(Float32)
            )
            ENGINE = MergeTree()
            PARTITION BY toYYYYMM(upload_time)
            ORDER BY (sn, upload_time);

        */
    }
}
