using BMS.Models.Device;
using BMS.MQTT.Model;
using MySql.Data.MySqlClient;
using System.Text.Json;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace BMS.MQTT {
    public class MQTTHelper {
        private static string _connectionString;
        public static void SetConnectionString(string connectionString) {
            _connectionString = connectionString;
        }
        public static bool SaveMqttPeriodDataToDB() {
            try {
                string json = MQTTHelper.GetPeriodData();
                var rootObject = JsonSerializer.Deserialize<DeviceDataFromMqtt>(json);
                if (rootObject != null) {
                    //DateTime UploadTime = DateTimeOffset.FromUnixTimeSeconds(rootObject.timeStamp).DateTime;
                    DateTime UploadTime = DateTime.Now;
                    //004储能簇控
                    MQTTHelper.SaveBatteryClusterInfo(MQTTHelper.ConvertMqttDataToBatteryClusterInfos(rootObject.devData.FindAll(s => s.devType == "4").ToList(), UploadTime));
                }


            } catch (Exception e) {
                return false;
            }

            return true;
        }

        public static bool SaveBatteryClusterInfo(List<BatteryClusterInfo> infos) {
            if (infos == null || infos.Count == 0) {
                return false;
            }
            try {
                using (var connection = new MySqlConnection(_connectionString)) {
                    connection.Open();
                    const string insertQuery = @"
                        INSERT INTO battery_cluster_info (
                            upload_time, device_type, device_name, device_id, sn, device_enable, device_online, 
                            total_alarm, total_fault, cell_overvoltage_alarm_level1, cell_undervoltage_alarm_level1,
                            cell_overtemperature_alarm_level1, cell_low_temperature_alarm_level1, cell_voltage_difference_alarm_level1,
                            charge_overcurrent_alarm_level1, discharge_overcurrent_alarm_level1, soc_low_alarm_level1, 
                            soc_difference_too_large_alarm_level1, insulation_low_alarm_level1, cell_overvoltage_alarm_level2,
                            cell_undervoltage_alarm_level2, cell_overtemperature_alarm_level2, cell_low_temperature_alarm_level2,
                            cell_voltage_difference_alarm_level2, charge_overcurrent_alarm_level2, discharge_overcurrent_alarm_level2,
                            soc_low_alarm_level2, soc_difference_too_large_alarm_level2, insulation_low_alarm_level2, 
                            cell_overvoltage_alarm_level3, cell_undervoltage_alarm_level3, cell_overtemperature_alarm_level3,
                            cell_low_temperature_alarm_level3, cell_voltage_difference_alarm_level3, charge_overcurrent_alarm_level3,
                            discharge_overcurrent_alarm_level3, soc_low_alarm_level3, soc_difference_too_large_alarm_level3, 
                            insulation_low_alarm_level3, cell_temperature_limit_alarm, cell_voltage_limit_alarm,
                            inter_cluster_circulation_alarm_level1, inter_cluster_circulation_alarm_level2, inter_cluster_circulation_alarm_level3,
                            inter_cluster_current_difference_alarm_level1, inter_cluster_current_difference_alarm_level2,
                            inter_cluster_current_difference_alarm_level3, group_terminal_overvoltage_alarm_level1,
                            group_terminal_overvoltage_alarm_level2, group_terminal_overvoltage_alarm_level3,
                            group_terminal_undervoltage_alarm_level1, group_terminal_undervoltage_alarm_level2,
                            group_terminal_undervoltage_alarm_level3, pole_overtemperature_alarm_level1,
                            pole_overtemperature_alarm_level2, pole_overtemperature_alarm_level3, afe_temperature_sensor_cable_abnormal,
                            afe_voltage_cable_abnormal, battery_cluster_communication_alarm, master_slave_communication_alarm,
                            relay_sticking_alarm, battery_limit_fault, fuse_fault, circuit_breaker_fault,
                            air_conditioner_fault, firefighting_equipment_fault, fire_alarm, fire_sprinkler,
                            afe_fault, high_voltage_abnormal, pre_charge_alarm, open_circuit_fault,
                            total_voltage, total_current, soc, soh, soe,
                            rated_total_voltage, rated_capacity, remaining_capacity, rated_energy, remaining_energy,
                            total_slaves_bmu, online_slaves_bmu, total_batteries, online_batteries,
                            total_temperature_sensors, online_temperature_sensors, max_allowable_discharge_current,
                            max_allowable_discharge_power, max_allowable_charge_current, max_allowable_charge_power,
                            positive_insulation_resistance, negative_insulation_resistance, cell_average_voltage,
                            cell_max_voltage_difference, highest_cell_voltage, highest_cell_voltage_slave_id,
                            highest_cell_voltage_id, lowest_cell_voltage, lowest_cell_voltage_slave_id,
                            lowest_cell_voltage_id, cell_average_temperature, max_temperature_difference,
                            highest_cell_temperature, highest_cell_temperature_slave_id, highest_cell_temperature_id,
                            lowest_cell_temperature, lowest_cell_temperature_slave_id, lowest_cell_temperature_id,
                            daily_charge_capacity, daily_charge_energy, daily_discharge_capacity, daily_discharge_energy,
                            daily_charge_time, daily_discharge_time, cumulative_charge_capacity, cumulative_charge_energy,
                            cumulative_discharge_capacity, cumulative_discharge_energy, cumulative_charge_time,
                            cumulative_discharge_time, bcu_operating_status, fault_word1, fault_word2,
                            fault_word3, fault_word4, fault_word5, fault_word6, fault_word7, fault_word8, fault_word9,
                            high_voltage_power_off_command
                        ) VALUES 
                        (@UploadTime, @DeviceType, @DeviceName, @DeviceId, @Sn, @DeviceEnable, @DeviceOnline,
                         @TotalAlarm, @TotalFault, @CellOvervoltageAlarmLevel1, @CellUndervoltageAlarmLevel1,
                         @CellOvertemperatureAlarmLevel1, @CellLowTemperatureAlarmLevel1, @CellVoltageDifferenceAlarmLevel1,
                         @ChargeOvercurrentAlarmLevel1, @DischargeOvercurrentAlarmLevel1, @SocLowAlarmLevel1,
                         @SocDifferenceTooLargeAlarmLevel1, @InsulationLowAlarmLevel1, @CellOvervoltageAlarmLevel2,
                         @CellUndervoltageAlarmLevel2, @CellOvertemperatureAlarmLevel2, @CellLowTemperatureAlarmLevel2,
                         @CellVoltageDifferenceAlarmLevel2, @ChargeOvercurrentAlarmLevel2, @DischargeOvercurrentAlarmLevel2,
                         @SocLowAlarmLevel2, @SocDifferenceTooLargeAlarmLevel2, @InsulationLowAlarmLevel2,
                         @CellOvervoltageAlarmLevel3, @CellUndervoltageAlarmLevel3, @CellOvertemperatureAlarmLevel3,
                         @CellLowTemperatureAlarmLevel3, @CellVoltageDifferenceAlarmLevel3, @ChargeOvercurrentAlarmLevel3,
                         @DischargeOvercurrentAlarmLevel3, @SocLowAlarmLevel3, @SocDifferenceTooLargeAlarmLevel3,
                         @InsulationLowAlarmLevel3, @CellTemperatureLimitAlarm, @CellVoltageLimitAlarm,
                         @InterClusterCirculationAlarmLevel1, @InterClusterCirculationAlarmLevel2, @InterClusterCirculationAlarmLevel3,
                         @InterClusterCurrentDifferenceAlarmLevel1, @InterClusterCurrentDifferenceAlarmLevel2,
                         @InterClusterCurrentDifferenceAlarmLevel3, @GroupTerminalOvervoltageAlarmLevel1,
                         @GroupTerminalOvervoltageAlarmLevel2, @GroupTerminalOvervoltageAlarmLevel3,
                         @GroupTerminalUndervoltageAlarmLevel1, @GroupTerminalUndervoltageAlarmLevel2,
                         @GroupTerminalUndervoltageAlarmLevel3, @PoleOvertemperatureAlarmLevel1,
                         @PoleOvertemperatureAlarmLevel2, @PoleOvertemperatureAlarmLevel3, @AfeTemperatureSensorCableAbnormal,
                         @AfeVoltageCableAbnormal, @BatteryClusterCommunicationAlarm, @MasterSlaveCommunicationAlarm,
                         @RelayStickingAlarm, @BatteryLimitFault, @FuseFault, @CircuitBreakerFault,
                         @AirConditionerFault, @FirefightingEquipmentFault, @FireAlarm, @FireSprinkler,
                         @AfeFault, @HighVoltageAbnormal, @PreChargeAlarm, @OpenCircuitFault,
                         @TotalVoltage, @TotalCurrent, @Soc, @Soh, @Soe,
                         @RatedTotalVoltage, @RatedCapacity, @RemainingCapacity, @RatedEnergy, @RemainingEnergy,
                         @TotalSlavesBmu, @OnlineSlavesBmu, @TotalBatteries, @OnlineBatteries,
                         @TotalTemperatureSensors, @OnlineTemperatureSensors, @MaxAllowableDischargeCurrent,
                         @MaxAllowableDischargePower, @MaxAllowableChargeCurrent, @MaxAllowableChargePower,
                         @PositiveInsulationResistance, @NegativeInsulationResistance, @CellAverageVoltage,
                         @CellMaxVoltageDifference, @HighestCellVoltage, @HighestCellVoltageSlaveId,
                         @HighestCellVoltageId, @LowestCellVoltage, @LowestCellVoltageSlaveId,
                         @LowestCellVoltageId, @CellAverageTemperature, @MaxTemperatureDifference,
                         @HighestCellTemperature, @HighestCellTemperatureSlaveId, @HighestCellTemperatureId,
                         @LowestCellTemperature, @LowestCellTemperatureSlaveId, @LowestCellTemperatureId,
                         @DailyChargeCapacity, @DailyChargeEnergy, @DailyDischargeCapacity, @DailyDischargeEnergy,
                         @DailyChargeTime, @DailyDischargeTime, @CumulativeChargeCapacity, @CumulativeChargeEnergy,
                         @CumulativeDischargeCapacity, @CumulativeDischargeEnergy, @CumulativeChargeTime,
                         @CumulativeDischargeTime, @BcuOperatingStatus, @FaultWord1, @FaultWord2,
                         @FaultWord3, @FaultWord4, @FaultWord5, @FaultWord6, @FaultWord7, @FaultWord8, @FaultWord9,
                         @HighVoltagePowerOffCommand
                         );

                    ";
                    using (var transaction = connection.BeginTransaction()) {
                        connection.Execute(insertQuery, infos, transaction: transaction);
                        transaction.Commit();
                    }
                    return true;
                }
            } catch (Exception ex) {
                return false;
            }

        }


        private static string GetDataKeyInMqtt(string deviceName, int num) {
            return deviceName + "_" + num;
        }

        private static List<BatteryClusterInfo> ConvertMqttDataToBatteryClusterInfos(List<DataFromMqtt> deviceDatasFromMqtt, DateTime UploadTime) {
            List<BatteryClusterInfo> ret = new List<BatteryClusterInfo>();


            foreach (DataFromMqtt dataFromMqtt in deviceDatasFromMqtt) {
                string deviceName = dataFromMqtt.devName;
                bool GetBoolean(int index) => dataFromMqtt.data.ContainsKey(GetDataKey(index)) ? Convert.ToBoolean(dataFromMqtt.data[GetDataKey(index)]) : false; // 默认值为 false
                Random random = new Random();
                double GetDouble(int index) => dataFromMqtt.data.ContainsKey(GetDataKey(index)) ? Convert.ToDouble(dataFromMqtt.data[GetDataKey(index)]) + (random.NextDouble() * 50) : 0.0; // 默认值为 0.0
                string GetString(int index) => dataFromMqtt.data.ContainsKey(GetDataKey(index)) ? (Convert.ToDouble(dataFromMqtt.data[GetDataKey(index)]) + (random.NextDouble() * 50)).ToString("F3") : "0.0"; // 默认值为 0.0
                string GetDataKey(int index) => MQTTHelper.GetDataKeyInMqtt(deviceName, index);
                try {
                    BatteryClusterInfo temp = new BatteryClusterInfo() {
                        UploadTime = UploadTime,
                        DeviceType = dataFromMqtt.devType,
                        DeviceName = deviceName,
                        DeviceId = dataFromMqtt.devId,
                        Sn = dataFromMqtt.sn,
                        DeviceEnable = GetBoolean(0),
                        DeviceOnline = GetBoolean(1),
                        TotalAlarm = GetString(2),
                        TotalFault = GetString(3),
                        CellOvervoltageAlarmLevel1 = GetString(4),
                        CellUndervoltageAlarmLevel1 = GetString(5),
                        CellOvertemperatureAlarmLevel1 = GetString(6),
                        CellLowTemperatureAlarmLevel1 = GetString(7),
                        CellVoltageDifferenceAlarmLevel1 = GetString(8),
                        ChargeOvercurrentAlarmLevel1 = GetString(9),
                        DischargeOvercurrentAlarmLevel1 = GetString(10),
                        SocLowAlarmLevel1 = GetString(11),
                        SocDifferenceTooLargeAlarmLevel1 = GetString(12),
                        InsulationLowAlarmLevel1 = GetString(13),
                        CellOvervoltageAlarmLevel2 = GetString(14),
                        CellUndervoltageAlarmLevel2 = GetString(15),
                        CellOvertemperatureAlarmLevel2 = GetString(16),
                        CellLowTemperatureAlarmLevel2 = GetString(17),
                        CellVoltageDifferenceAlarmLevel2 = GetString(18),
                        ChargeOvercurrentAlarmLevel2 = GetString(19),
                        DischargeOvercurrentAlarmLevel2 = GetString(20),
                        SocLowAlarmLevel2 = GetString(21),
                        SocDifferenceTooLargeAlarmLevel2 = GetString(22),
                        InsulationLowAlarmLevel2 = GetString(23),
                        CellOvervoltageAlarmLevel3 = GetString(24),
                        CellUndervoltageAlarmLevel3 = GetString(25),
                        CellOvertemperatureAlarmLevel3 = GetString(26),
                        CellLowTemperatureAlarmLevel3 = GetString(27),
                        CellVoltageDifferenceAlarmLevel3 = GetString(28),
                        ChargeOvercurrentAlarmLevel3 = GetString(29),
                        DischargeOvercurrentAlarmLevel3 = GetString(30),
                        SocLowAlarmLevel3 = GetString(31),
                        SocDifferenceTooLargeAlarmLevel3 = GetString(32),
                        InsulationLowAlarmLevel3 = GetString(33),
                        CellTemperatureLimitAlarm = GetString(34),
                        CellVoltageLimitAlarm = GetString(35),
                        InterClusterCirculationAlarmLevel1 = GetString(36),
                        InterClusterCirculationAlarmLevel2 = GetString(37),
                        InterClusterCirculationAlarmLevel3 = GetString(38),
                        InterClusterCurrentDifferenceAlarmLevel1 = GetString(39),
                        InterClusterCurrentDifferenceAlarmLevel2 = GetString(40),
                        InterClusterCurrentDifferenceAlarmLevel3 = GetString(41),
                        GroupTerminalOvervoltageAlarmLevel1 = GetString(42),
                        GroupTerminalOvervoltageAlarmLevel2 = GetString(43),
                        GroupTerminalOvervoltageAlarmLevel3 = GetString(44),
                        GroupTerminalUndervoltageAlarmLevel1 = GetString(45),
                        GroupTerminalUndervoltageAlarmLevel2 = GetString(46),
                        GroupTerminalUndervoltageAlarmLevel3 = GetString(47),
                        PoleOvertemperatureAlarmLevel1 = GetString(48),
                        PoleOvertemperatureAlarmLevel2 = GetString(49),
                        PoleOvertemperatureAlarmLevel3 = GetString(50),
                        AfeTemperatureSensorCableAbnormal = GetString(51),
                        AfeVoltageCableAbnormal = GetString(52),
                        BatteryClusterCommunicationAlarm = GetString(53),
                        MasterSlaveCommunicationAlarm = GetString(54),
                        RelayStickingAlarm = GetString(65),
                        BatteryLimitFault = GetString(66),
                        FuseFault = GetString(55),
                        CircuitBreakerFault = GetString(56),
                        AirConditionerFault = GetString(57),
                        FirefightingEquipmentFault = GetString(58),
                        FireAlarm = GetString(59),
                        FireSprinkler = GetString(60),
                        AfeFault = GetString(61),
                        HighVoltageAbnormal = GetString(62),
                        PreChargeAlarm = GetString(63),
                        OpenCircuitFault = GetString(64),
                        TotalVoltage = GetString(101),
                        TotalCurrent = GetString(102),
                        Soc = GetString(103),
                        Soh = GetString(104),
                        Soe = GetString(105),
                        RatedTotalVoltage = GetString(106),
                        RatedCapacity = GetString(107),
                        RemainingCapacity = GetString(108),
                        RatedEnergy = GetString(109),
                        RemainingEnergy = GetString(110),
                        TotalSlavesBmu = GetString(111),
                        OnlineSlavesBmu = GetString(112),
                        TotalBatteries = GetString(113),
                        OnlineBatteries = GetString(114),
                        TotalTemperatureSensors = GetString(115),
                        OnlineTemperatureSensors = GetString(116),
                        MaxAllowableDischargeCurrent = GetString(117),
                        MaxAllowableDischargePower = GetString(118),
                        MaxAllowableChargeCurrent = GetString(119),
                        MaxAllowableChargePower = GetString(120),
                        PositiveInsulationResistance = GetString(121),
                        NegativeInsulationResistance = GetString(122),
                        CellAverageVoltage = GetString(123),
                        CellMaxVoltageDifference = GetString(124),
                        HighestCellVoltage = GetString(125),
                        HighestCellVoltageSlaveId = GetString(126),
                        HighestCellVoltageId = GetString(127),
                        LowestCellVoltage = GetString(128),
                        LowestCellVoltageSlaveId = GetString(129),
                        LowestCellVoltageId = GetString(130),
                        CellAverageTemperature = GetString(131),
                        MaxTemperatureDifference = GetString(132),
                        HighestCellTemperature = GetString(133),
                        HighestCellTemperatureSlaveId = GetString(134),
                        HighestCellTemperatureId = GetString(135),
                        LowestCellTemperature = GetString(136),
                        LowestCellTemperatureSlaveId = GetString(137),
                        LowestCellTemperatureId = GetString(138),
                        DailyChargeCapacity = GetString(139),
                        DailyChargeEnergy = GetString(140),
                        DailyDischargeCapacity = GetString(141),
                        DailyDischargeEnergy = GetString(142),
                        DailyChargeTime = GetString(143),
                        DailyDischargeTime = GetString(144),
                        CumulativeChargeCapacity = GetString(145),
                        CumulativeChargeEnergy = GetString(146),
                        CumulativeDischargeCapacity = GetString(147),
                        CumulativeDischargeEnergy = GetString(148),
                        CumulativeChargeTime = GetString(149),
                        CumulativeDischargeTime = GetString(150),
                        BcuOperatingStatus = GetString(151),
                        FaultWord1 = GetString(152),
                        FaultWord2 = GetString(153),
                        FaultWord3 = GetString(154),
                        FaultWord4 = GetString(155),
                        FaultWord5 = GetString(156),
                        FaultWord6 = GetString(157),
                        FaultWord7 = GetString(158),
                        FaultWord8 = GetString(159),
                        FaultWord9 = GetString(160),
                        HighVoltagePowerOffCommand = GetString(1001),

                    };
                    ret.Add(temp);
                } catch (Exception e) {
                    Console.WriteLine(e.ToString());
                }

            }

            return ret;
        }

        public static string GetPeriodData() {
            string filePath = Path.Combine(AppContext.BaseDirectory, "Assets", "JsonFile", "period.json");
            // 读取文件内容
            if (File.Exists(filePath)) {
                return File.ReadAllText(filePath).Replace("\r\n", "").Replace("\n", ""); ;
            } else {
                return String.Empty;
            }
        }
    }
}
