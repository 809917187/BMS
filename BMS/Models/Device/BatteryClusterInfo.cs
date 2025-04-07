using System.ComponentModel.DataAnnotations;

namespace BMS.Models.Device {
    public class BatteryClusterInfo {
        [Display(AutoGenerateField = false)]
        public int Id { get; set; }
        [Display(Name = "时间")]
        public DateTime UploadTime { get; set; }
        public string DeviceType { get; set; } = string.Empty;
        public string DeviceName { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        [Display(Name = "SN")]
        public string Sn { get; set; } = string.Empty;
        [Display(Name = "设备启用")]
        public bool DeviceEnable { get; set; }    //	设备启用	0
        [Display(Name = "设备在线")]
        public bool DeviceOnline { get; set; }    //	设备在线	1
        [Display(Name = "总告警")]
        public string TotalAlarm { get; set; }  //	总告警	2
        [Display(Name = "总故障")]
        public string TotalFault { get; set; }  //	总故障	3
        [Display(Name = "单体过压告警一级")]
        public string CellOvervoltageAlarmLevel1 { get; set; }  //	单体过压告警一级	4
        [Display(Name = "单体欠压告警一级")]
        public string CellUndervoltageAlarmLevel1 { get; set; } //	单体欠压告警一级	5
        [Display(Name = "单体过温告警一级")]
        public string CellOvertemperatureAlarmLevel1 { get; set; }  //	单体过温告警一级	6
        [Display(Name = "单体低温告警一级")]
        public string CellLowTemperatureAlarmLevel1 { get; set; }   //	单体低温告警一级	7
        [Display(Name = "单体压差告警一级")]
        public string CellVoltageDifferenceAlarmLevel1 { get; set; }    //	单体压差告警一级	8
        [Display(Name = "充电过流告警一级")]
        public string ChargeOvercurrentAlarmLevel1 { get; set; }    //	充电过流告警一级	9
        [Display(Name = "放电过流告警一级")]
        public string DischargeOvercurrentAlarmLevel1 { get; set; } //	放电过流告警一级	10
        [Display(Name = "SOC过低告警一级")]
        public string SocLowAlarmLevel1 { get; set; }   //	SOC过低告警一级	11
        [Display(Name = "SOC差异过大告警一级")]
        public string SocDifferenceTooLargeAlarmLevel1 { get; set; }    //	SOC差异过大告警一级	12
        [Display(Name = "绝缘过低告警一级")]
        public string InsulationLowAlarmLevel1 { get; set; }    //	绝缘过低告警一级	13
        [Display(Name = "单体过压告警二级")]
        public string CellOvervoltageAlarmLevel2 { get; set; }  //	单体过压告警二级	14.
        [Display(Name = "单体欠压告警二级")]
        public string CellUndervoltageAlarmLevel2 { get; set; } //	单体欠压告警二级	15
        [Display(Name = "单体过温告警二级")]
        public string CellOvertemperatureAlarmLevel2 { get; set; }  //	单体过温告警二级	16
        [Display(Name = "单体低温告警二级")]
        public string CellLowTemperatureAlarmLevel2 { get; set; }   //	单体低温告警二级	17
        [Display(Name = "单体压差告警二级")]
        public string CellVoltageDifferenceAlarmLevel2 { get; set; }    //	单体压差告警二级	18
        [Display(Name = "充电过流告警二级")]
        public string ChargeOvercurrentAlarmLevel2 { get; set; }    //	充电过流告警二级	19
        [Display(Name = "放电过流告警二级")]
        public string DischargeOvercurrentAlarmLevel2 { get; set; } //	放电过流告警二级	20
        [Display(Name = "SOC过低告警二级")]
        public string SocLowAlarmLevel2 { get; set; }   //	SOC过低告警二级	21
        [Display(Name = "SOC差异过大告警二级")]
        public string SocDifferenceTooLargeAlarmLevel2 { get; set; }    //	SOC差异过大告警二级	22
        [Display(Name = "绝缘过低告警二级")]
        public string InsulationLowAlarmLevel2 { get; set; }    //	绝缘过低告警二级	23
        [Display(Name = "单体过压告警三级")]
        public string CellOvervoltageAlarmLevel3 { get; set; }  //	单体过压告警三级	24
        [Display(Name = "单体欠压告警三级")]
        public string CellUndervoltageAlarmLevel3 { get; set; } //	单体欠压告警三级	25
        [Display(Name = "单体过温告警三级")]
        public string CellOvertemperatureAlarmLevel3 { get; set; }  //	单体过温告警三级	26
        [Display(Name = "单体低温告警三级")]
        public string CellLowTemperatureAlarmLevel3 { get; set; }   //	单体低温告警三级	27
        [Display(Name = "单体压差告警三级")]
        public string CellVoltageDifferenceAlarmLevel3 { get; set; }    //	单体压差告警三级	28
        [Display(Name = "充电过流告警三级")]
        public string ChargeOvercurrentAlarmLevel3 { get; set; }    //	充电过流告警三级	29
        [Display(Name = "放电过流告警三级")]
        public string DischargeOvercurrentAlarmLevel3 { get; set; } //	放电过流告警三级	30
        [Display(Name = "SOC过低告警三级")]
        public string SocLowAlarmLevel3 { get; set; }   //	SOC过低告警三级	31
        [Display(Name = "SOC差异过大告警三级")]
        public string SocDifferenceTooLargeAlarmLevel3 { get; set; }    //	SOC差异过大告警三级	32
        [Display(Name = "绝缘过低告警三级")]
        public string InsulationLowAlarmLevel3 { get; set; }    //	绝缘过低告警三级	33
        [Display(Name = "电芯温度极限告警")]
        public string CellTemperatureLimitAlarm { get; set; }   //	电芯温度极限告警	34
        [Display(Name = "电芯电压极限告警")]
        public string CellVoltageLimitAlarm { get; set; }   //	电芯电压极限告警	35
        [Display(Name = "簇间环流1级告警")]
        public string InterClusterCirculationAlarmLevel1 { get; set; }  //	簇间环流1级告警	36
        [Display(Name = "簇间环流2级告警")]
        public string InterClusterCirculationAlarmLevel2 { get; set; }  //	簇间环流2级告警	37
        [Display(Name = "簇间环流3级告警")]
        public string InterClusterCirculationAlarmLevel3 { get; set; }  //	簇间环流3级告警	38
        [Display(Name = "簇间电流差1级告警")]
        public string InterClusterCurrentDifferenceAlarmLevel1 { get; set; }    //	簇间电流差1级告警	39
        [Display(Name = "簇间电流差2级告警")]
        public string InterClusterCurrentDifferenceAlarmLevel2 { get; set; }    //	簇间电流差2级告警	40
        [Display(Name = "簇间电流差3级告警")]
        public string InterClusterCurrentDifferenceAlarmLevel3 { get; set; }    //	簇间电流差3级告警	41
        [Display(Name = "组端过压1级告警")]
        public string GroupTerminalOvervoltageAlarmLevel1 { get; set; } //	组端过压1级告警	42
        [Display(Name = "组端过压2级告警")]
        public string GroupTerminalOvervoltageAlarmLevel2 { get; set; } //	组端过压2级告警	43
        [Display(Name = "组端过压3级告警")]
        public string GroupTerminalOvervoltageAlarmLevel3 { get; set; } //	组端过压3级告警	44
        [Display(Name = "组端欠压1级告警")]
        public string GroupTerminalUndervoltageAlarmLevel1 { get; set; }    //	组端欠压1级告警	45
        [Display(Name = "组端欠压2级告警")]
        public string GroupTerminalUndervoltageAlarmLevel2 { get; set; }    //	组端欠压2级告警	46
        [Display(Name = "组端欠压3级告警")]
        public string GroupTerminalUndervoltageAlarmLevel3 { get; set; }    //	组端欠压3级告警	47
        [Display(Name = "极柱过温1级告警")]
        public string PoleOvertemperatureAlarmLevel1 { get; set; }  //	极柱过温1级告警	48
        [Display(Name = "极柱过温2级告警")]
        public string PoleOvertemperatureAlarmLevel2 { get; set; }  //	极柱过温2级告警	49
        [Display(Name = "极柱过温3级告警")]
        public string PoleOvertemperatureAlarmLevel3 { get; set; }  //	极柱过温3级告警	50
        [Display(Name = "AFE温感排线异常")]
        public string AfeTemperatureSensorCableAbnormal { get; set; }   //	AFE温感排线异常	51
        [Display(Name = "AFE电压排线异常")]
        public string AfeVoltageCableAbnormal { get; set; } //	AFE电压排线异常	52
        [Display(Name = "与电池簇通信告警")]
        public string BatteryClusterCommunicationAlarm { get; set; }    //	与电池簇通信告警	53
        [Display(Name = "主从通讯告警")]
        public string MasterSlaveCommunicationAlarm { get; set; }   //	主从通讯告警	54
        [Display(Name = "继电器粘连告警")]
        public string RelayStickingAlarm { get; set; }  //	继电器粘连告警	65
        [Display(Name = "电池极限故障")]
        public string BatteryLimitFault { get; set; }   //	电池极限故障	66
        [Display(Name = "熔丝故障")]
        public string FuseFault { get; set; }   //	熔丝故障	55
        [Display(Name = "断路器故障")]
        public string CircuitBreakerFault { get; set; } //	断路器故障	56
        [Display(Name = "空调故障")]
        public string AirConditionerFault { get; set; } //	空调故障	57
        [Display(Name = "消防设备故障")]
        public string FirefightingEquipmentFault { get; set; }  //	消防设备故障	58
        [Display(Name = "消防火警")]
        public string FireAlarm { get; set; }   //	消防火警	59
        [Display(Name = "消防喷洒")]
        public string FireSprinkler { get; set; }   //	消防喷洒	60
        [Display(Name = "AFE故障")]
        public string AfeFault { get; set; }    //	AFE故障	61
        [Display(Name = "高压异常")]
        public string HighVoltageAbnormal { get; set; } //	高压异常	62
        [Display(Name = "预充告警")]
        public string PreChargeAlarm { get; set; }  //	预充告警	63
        [Display(Name = "开路故障")]
        public string OpenCircuitFault { get; set; }    //	开路故障	64
        [Display(AutoGenerateField = false)]
        public string AlarmStatusReserved { get; set; } = String.Empty; //	告警+状态保留	继续~100
        [Display(Name = "总压")]
        public string TotalVoltage { get; set; }    //	总压	101
        [Display(Name = "总电流")]
        public string TotalCurrent { get; set; }    //	总电流	102
        [Display(Name = "SOC")]
        public string Soc { get; set; } //	SOC	103
        [Display(Name = "SOH")]
        public string Soh { get; set; } //	SOH	104
        [Display(Name = "SOE")]
        public string Soe { get; set; } //	SOE	105
        [Display(Name = "额定总压")]
        public string RatedTotalVoltage { get; set; }   //	额定总压	106
        [Display(Name = "额定容量")]
        public string RatedCapacity { get; set; }   //	额定容量	107
        [Display(Name = "剩余容量")]
        public string RemainingCapacity { get; set; }   //	剩余容量	108
        [Display(Name = "额定电量")]
        public string RatedEnergy { get; set; } //	额定电量	109
        [Display(Name = "剩余电量")]
        public string RemainingEnergy { get; set; } //	剩余电量	110
        [Display(Name = "从机总数(BMU)")]
        public string TotalSlavesBmu { get; set; }  //	从机总数(BMU)	111
        [Display(Name = "在线从机总数(BMU)")]
        public string OnlineSlavesBmu { get; set; } //	在线从机总数(BMU)	112
        [Display(Name = "电池总数")]
        public string TotalBatteries { get; set; }  //	电池总数	113
        [Display(Name = "在线电池总数")]
        public string OnlineBatteries { get; set; } //	在线电池总数	114
        [Display(Name = "温感总数")]
        public string TotalTemperatureSensors { get; set; } //	温感总数	115
        [Display(Name = "在线温感总数")]
        public string OnlineTemperatureSensors { get; set; }    //	在线温感总数	116
        [Display(Name = "最大允许放电电流")]
        public string MaxAllowableDischargeCurrent { get; set; }    //	最大允许放电电流	117
        [Display(Name = "最大允许放电功率")]
        public string MaxAllowableDischargePower { get; set; }  //	最大允许放电功率	118
        [Display(Name = "最大允许充电电流")]
        public string MaxAllowableChargeCurrent { get; set; }   //	最大允许充电电流	119
        [Display(Name = "最大允许充电功率")]
        public string MaxAllowableChargePower { get; set; } //	最大允许充电功率	120
        [Display(Name = "正极绝缘阻值")]
        public string PositiveInsulationResistance { get; set; }    //	正极绝缘阻值	121
        [Display(Name = "负极绝缘阻值")]
        public string NegativeInsulationResistance { get; set; }    //	负极绝缘阻值	122
        [Display(Name = "单体平均电压")]
        public string CellAverageVoltage { get; set; }  //	单体平均电压	123
        [Display(Name = "单体最大压差")]
        public string CellMaxVoltageDifference { get; set; }    //	单体最大压差	124
        [Display(Name = "最高单体电压")]
        public string HighestCellVoltage { get; set; }  //	最高单体电压	125
        [Display(Name = "最高单体电压从机号")]
        public string HighestCellVoltageSlaveId { get; set; }   //	最高单体电压从机号	126
        [Display(Name = "最高单体电压编号")]
        public string HighestCellVoltageId { get; set; }    //	最高单体电压编号	127
        [Display(Name = "最低单体电压")]
        public string LowestCellVoltage { get; set; }   //	最低单体电压	128
        [Display(Name = "最低单体电压从机号")]
        public string LowestCellVoltageSlaveId { get; set; }    //	最低单体电压从机号	129
        [Display(Name = "最低单体电压编号")]
        public string LowestCellVoltageId { get; set; } //	最低单体电压编号	130
        [Display(Name = "单体平均温度")]
        public string CellAverageTemperature { get; set; }  //	单体平均温度	131
        [Display(Name = "最大温差")]
        public string MaxTemperatureDifference { get; set; }    //	最大温差	132
        [Display(Name = "最高单体温度")]
        public string HighestCellTemperature { get; set; }  //	最高单体温度	133
        [Display(Name = "最高单体温度从机号")]
        public string HighestCellTemperatureSlaveId { get; set; }   //	最高单体温度从机号	134
        [Display(Name = "最高单体温度编号")]
        public string HighestCellTemperatureId { get; set; }    //	最高单体温度编号	135
        [Display(Name = "最低单体温度")]
        public string LowestCellTemperature { get; set; }   //	最低单体温度	136
        [Display(Name = "最低单体温度从机号")]
        public string LowestCellTemperatureSlaveId { get; set; }    //	最低单体温度从机号	137
        [Display(Name = "最低单体温度编号")]
        public string LowestCellTemperatureId { get; set; } //	最低单体温度编号	138
        [Display(Name = "日充电容量")]
        public string DailyChargeCapacity { get; set; } //	日充电容量	139
        [Display(Name = "日充电电量")]
        public string DailyChargeEnergy { get; set; }   //	日充电电量	140
        [Display(Name = "日放电容量")]
        public string DailyDischargeCapacity { get; set; }  //	日放电容量	141
        [Display(Name = "日放电电量")]
        public string DailyDischargeEnergy { get; set; }    //	日放电电量	142
        [Display(Name = "日充电时间")]
        public string DailyChargeTime { get; set; } //	日充电时间	143
        [Display(Name = "日放电时间")]
        public string DailyDischargeTime { get; set; }  //	日放电时间	144
        [Display(Name = "累计充电容量")]
        public string CumulativeChargeCapacity { get; set; }    //	累计充电容量	145
        [Display(Name = "累计充电电量")]
        public string CumulativeChargeEnergy { get; set; }  //	累计充电电量	146
        [Display(Name = "累计放电容量")]
        public string CumulativeDischargeCapacity { get; set; } //	累计放电容量	147
        [Display(Name = "累计放电电量")]
        public string CumulativeDischargeEnergy { get; set; }   //	累计放电电量	148
        [Display(Name = "累计充电时间")]
        public string CumulativeChargeTime { get; set; }    //	累计充电时间	149
        [Display(Name = "累计放电时间")]
        public string CumulativeDischargeTime { get; set; } //	累计放电时间	150
        [Display(Name = "BCU工作状态")]
        public string BcuOperatingStatus { get; set; }  //	BCU工作状态	151
        [Display(AutoGenerateField = false)]
        public string FaultWord1 { get; set; }  //	故障字1	152
        [Display(AutoGenerateField = false)]
        public string FaultWord2 { get; set; }  //	故障字2	153
        [Display(AutoGenerateField = false)]
        public string FaultWord3 { get; set; }  //	故障字3	154
        [Display(AutoGenerateField = false)]
        public string FaultWord4 { get; set; }  //	故障字4	155
        [Display(AutoGenerateField = false)]
        public string FaultWord5 { get; set; }  //	故障字5	156
        [Display(AutoGenerateField = false)]
        public string FaultWord6 { get; set; }  //	故障字6	157
        [Display(AutoGenerateField = false)]
        public string FaultWord7 { get; set; }  //	故障字7	158
        [Display(AutoGenerateField = false)]
        public string FaultWord8 { get; set; }  //	故障字8	159
        [Display(AutoGenerateField = false)]
        public string FaultWord9 { get; set; }  //	故障字9	160
        [Display(Name = "1#PACK~30#PACK均衡状态")]
        public string Pack1ToPack30BalancingStatus { get; set; } = String.Empty;    //	1#PACK~30#PACK均衡状态	161-190
        [Display(Name = "1号~420号电池电压")]
        public string BatteryVoltage1ToBattery420 { get; set; } = String.Empty;   //	1号~420号电池电压	191~610
        [Display(Name = "1号~200号电池温度")]
        public string BatteryTemperature1ToBattery200 { get; set; } //	1号~200号电池温度	611~810
        [Display(AutoGenerateField = false)]
        public string Reserved { get; set; } = String.Empty;    //	预留	811~1000
        [Display(Name = "高压下电指令")]
        public string HighVoltagePowerOffCommand { get; set; }  //	高压下电指令	1001
        [Display(AutoGenerateField = false)]
        public string ReservedThresholdAndParameters { get; set; } = String.Empty; //	预留阈值及参数	1001~1999



    }
}
