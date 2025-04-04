﻿namespace BMS.Models.Device {
    public class BatteryClusterInfo {
        public int Id { get; set; }
        public DateTime UploadTime { get; set; }
        public string DeviceType { get; set; } = string.Empty;
        public string DeviceName { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public string Sn { get; set; } = string.Empty;
        public bool DeviceEnable { get; set; }    //	设备启用	0
        public bool DeviceOnline { get; set; }    //	设备在线	1
        public string TotalAlarm { get; set; }  //	总告警	2
        public string TotalFault { get; set; }  //	总故障	3
        public string CellOvervoltageAlarmLevel1 { get; set; }  //	单体过压告警一级	4
        public string CellUndervoltageAlarmLevel1 { get; set; } //	单体欠压告警一级	5
        public string CellOvertemperatureAlarmLevel1 { get; set; }  //	单体过温告警一级	6
        public string CellLowTemperatureAlarmLevel1 { get; set; }   //	单体低温告警一级	7
        public string CellVoltageDifferenceAlarmLevel1 { get; set; }    //	单体压差告警一级	8
        public string ChargeOvercurrentAlarmLevel1 { get; set; }    //	充电过流告警一级	9
        public string DischargeOvercurrentAlarmLevel1 { get; set; } //	放电过流告警一级	10
        public string SocLowAlarmLevel1 { get; set; }   //	SOC过低告警一级	11
        public string SocDifferenceTooLargeAlarmLevel1 { get; set; }    //	SOC差异过大告警一级	12
        public string InsulationLowAlarmLevel1 { get; set; }    //	绝缘过低告警一级	13
        public string CellOvervoltageAlarmLevel2 { get; set; }  //	单体过压告警二级	14
        public string CellUndervoltageAlarmLevel2 { get; set; } //	单体欠压告警二级	15
        public string CellOvertemperatureAlarmLevel2 { get; set; }  //	单体过温告警二级	16
        public string CellLowTemperatureAlarmLevel2 { get; set; }   //	单体低温告警二级	17
        public string CellVoltageDifferenceAlarmLevel2 { get; set; }    //	单体压差告警二级	18
        public string ChargeOvercurrentAlarmLevel2 { get; set; }    //	充电过流告警二级	19
        public string DischargeOvercurrentAlarmLevel2 { get; set; } //	放电过流告警二级	20
        public string SocLowAlarmLevel2 { get; set; }   //	SOC过低告警二级	21
        public string SocDifferenceTooLargeAlarmLevel2 { get; set; }    //	SOC差异过大告警二级	22
        public string InsulationLowAlarmLevel2 { get; set; }    //	绝缘过低告警二级	23
        public string CellOvervoltageAlarmLevel3 { get; set; }  //	单体过压告警三级	24
        public string CellUndervoltageAlarmLevel3 { get; set; } //	单体欠压告警三级	25
        public string CellOvertemperatureAlarmLevel3 { get; set; }  //	单体过温告警三级	26
        public string CellLowTemperatureAlarmLevel3 { get; set; }   //	单体低温告警三级	27
        public string CellVoltageDifferenceAlarmLevel3 { get; set; }    //	单体压差告警三级	28
        public string ChargeOvercurrentAlarmLevel3 { get; set; }    //	充电过流告警三级	29
        public string DischargeOvercurrentAlarmLevel3 { get; set; } //	放电过流告警三级	30
        public string SocLowAlarmLevel3 { get; set; }   //	SOC过低告警三级	31
        public string SocDifferenceTooLargeAlarmLevel3 { get; set; }    //	SOC差异过大告警三级	32
        public string InsulationLowAlarmLevel3 { get; set; }    //	绝缘过低告警三级	33
        public string CellTemperatureLimitAlarm { get; set; }   //	电芯温度极限告警	34
        public string CellVoltageLimitAlarm { get; set; }   //	电芯电压极限告警	35
        public string InterClusterCirculationAlarmLevel1 { get; set; }  //	簇间环流1级告警	36
        public string InterClusterCirculationAlarmLevel2 { get; set; }  //	簇间环流2级告警	37
        public string InterClusterCirculationAlarmLevel3 { get; set; }  //	簇间环流3级告警	38
        public string InterClusterCurrentDifferenceAlarmLevel1 { get; set; }    //	簇间电流差1级告警	39
        public string InterClusterCurrentDifferenceAlarmLevel2 { get; set; }    //	簇间电流差2级告警	40
        public string InterClusterCurrentDifferenceAlarmLevel3 { get; set; }    //	簇间电流差3级告警	41
        public string GroupTerminalOvervoltageAlarmLevel1 { get; set; } //	组端过压1级告警	42
        public string GroupTerminalOvervoltageAlarmLevel2 { get; set; } //	组端过压2级告警	43
        public string GroupTerminalOvervoltageAlarmLevel3 { get; set; } //	组端过压3级告警	44
        public string GroupTerminalUndervoltageAlarmLevel1 { get; set; }    //	组端欠压1级告警	45
        public string GroupTerminalUndervoltageAlarmLevel2 { get; set; }    //	组端欠压2级告警	46
        public string GroupTerminalUndervoltageAlarmLevel3 { get; set; }    //	组端欠压3级告警	47
        public string PoleOvertemperatureAlarmLevel1 { get; set; }  //	极柱过温1级告警	48
        public string PoleOvertemperatureAlarmLevel2 { get; set; }  //	极柱过温2级告警	49
        public string PoleOvertemperatureAlarmLevel3 { get; set; }  //	极柱过温3级告警	50
        public string AfeTemperatureSensorCableAbnormal { get; set; }   //	AFE温感排线异常	51
        public string AfeVoltageCableAbnormal { get; set; } //	AFE电压排线异常	52
        public string BatteryClusterCommunicationAlarm { get; set; }    //	与电池簇通信告警	53
        public string MasterSlaveCommunicationAlarm { get; set; }   //	主从通讯告警	54
        public string RelayStickingAlarm { get; set; }  //	继电器粘连告警	65
        public string BatteryLimitFault { get; set; }   //	电池极限故障	66
        public string FuseFault { get; set; }   //	熔丝故障	55
        public string CircuitBreakerFault { get; set; } //	断路器故障	56
        public string AirConditionerFault { get; set; } //	空调故障	57
        public string FirefightingEquipmentFault { get; set; }  //	消防设备故障	58
        public string FireAlarm { get; set; }   //	消防火警	59
        public string FireSprinkler { get; set; }   //	消防喷洒	60
        public string AfeFault { get; set; }    //	AFE故障	61
        public string HighVoltageAbnormal { get; set; } //	高压异常	62
        public string PreChargeAlarm { get; set; }  //	预充告警	63
        public string OpenCircuitFault { get; set; }    //	开路故障	64
        public string AlarmStatusReserved { get; set; } = String.Empty; //	告警+状态保留	继续~100
        public string TotalVoltage { get; set; }    //	总压	101
        public string TotalCurrent { get; set; }    //	总电流	102
        public string Soc { get; set; } //	SOC	103
        public string Soh { get; set; } //	SOH	104
        public string Soe { get; set; } //	SOE	105
        public string RatedTotalVoltage { get; set; }   //	额定总压	106
        public string RatedCapacity { get; set; }   //	额定容量	107
        public string RemainingCapacity { get; set; }   //	剩余容量	108
        public string RatedEnergy { get; set; } //	额定电量	109
        public string RemainingEnergy { get; set; } //	剩余电量	110
        public string TotalSlavesBmu { get; set; }  //	从机总数(BMU)	111
        public string OnlineSlavesBmu { get; set; } //	在线从机总数(BMU)	112
        public string TotalBatteries { get; set; }  //	电池总数	113
        public string OnlineBatteries { get; set; } //	在线电池总数	114
        public string TotalTemperatureSensors { get; set; } //	温感总数	115
        public string OnlineTemperatureSensors { get; set; }    //	在线温感总数	116
        public string MaxAllowableDischargeCurrent { get; set; }    //	最大允许放电电流	117
        public string MaxAllowableDischargePower { get; set; }  //	最大允许放电功率	118
        public string MaxAllowableChargeCurrent { get; set; }   //	最大允许充电电流	119
        public string MaxAllowableChargePower { get; set; } //	最大允许充电功率	120
        public string PositiveInsulationResistance { get; set; }    //	正极绝缘阻值	121
        public string NegativeInsulationResistance { get; set; }    //	负极绝缘阻值	122
        public string CellAverageVoltage { get; set; }  //	单体平均电压	123
        public string CellMaxVoltageDifference { get; set; }    //	单体最大压差	124
        public string HighestCellVoltage { get; set; }  //	最高单体电压	125
        public string HighestCellVoltageSlaveId { get; set; }   //	最高单体电压从机号	126
        public string HighestCellVoltageId { get; set; }    //	最高单体电压编号	127
        public string LowestCellVoltage { get; set; }   //	最低单体电压	128
        public string LowestCellVoltageSlaveId { get; set; }    //	最低单体电压从机号	129
        public string LowestCellVoltageId { get; set; } //	最低单体电压编号	130
        public string CellAverageTemperature { get; set; }  //	单体平均温度	131
        public string MaxTemperatureDifference { get; set; }    //	最大温差	132
        public string HighestCellTemperature { get; set; }  //	最高单体温度	133
        public string HighestCellTemperatureSlaveId { get; set; }   //	最高单体温度从机号	134
        public string HighestCellTemperatureId { get; set; }    //	最高单体温度编号	135
        public string LowestCellTemperature { get; set; }   //	最低单体温度	136
        public string LowestCellTemperatureSlaveId { get; set; }    //	最低单体温度从机号	137
        public string LowestCellTemperatureId { get; set; } //	最低单体温度编号	138
        public string DailyChargeCapacity { get; set; } //	日充电容量	139
        public string DailyChargeEnergy { get; set; }   //	日充电电量	140
        public string DailyDischargeCapacity { get; set; }  //	日放电容量	141
        public string DailyDischargeEnergy { get; set; }    //	日放电电量	142
        public string DailyChargeTime { get; set; } //	日充电时间	143
        public string DailyDischargeTime { get; set; }  //	日放电时间	144
        public string CumulativeChargeCapacity { get; set; }    //	累计充电容量	145
        public string CumulativeChargeEnergy { get; set; }  //	累计充电电量	146
        public string CumulativeDischargeCapacity { get; set; } //	累计放电容量	147
        public string CumulativeDischargeEnergy { get; set; }   //	累计放电电量	148
        public string CumulativeChargeTime { get; set; }    //	累计充电时间	149
        public string CumulativeDischargeTime { get; set; } //	累计放电时间	150
        public string BcuOperatingStatus { get; set; }  //	BCU工作状态	151
        public string FaultWord1 { get; set; }  //	故障字1	152
        public string FaultWord2 { get; set; }  //	故障字2	153
        public string FaultWord3 { get; set; }  //	故障字3	154
        public string FaultWord4 { get; set; }  //	故障字4	155
        public string FaultWord5 { get; set; }  //	故障字5	156
        public string FaultWord6 { get; set; }  //	故障字6	157
        public string FaultWord7 { get; set; }  //	故障字7	158
        public string FaultWord8 { get; set; }  //	故障字8	159
        public string FaultWord9 { get; set; }  //	故障字9	160
        public string Pack1ToPack30BalancingStatus { get; set; } = String.Empty;    //	1#PACK~30#PACK均衡状态	161-190
        public string BatteryVoltage1ToBattery420 { get; set; } = String.Empty;   //	1号~420号电池电压	191~610
        public string BatteryTemperature1ToBattery200 { get; set; } //	1号~200号电池温度	611~810
        public string Reserved { get; set; } = String.Empty;    //	预留	811~1000
        public string HighVoltagePowerOffCommand { get; set; }  //	高压下电指令	1001
        public string ReservedThresholdAndParameters { get; set; } = String.Empty; //	预留阈值及参数	1001~1999



    }
}
