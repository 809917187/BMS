using BMS.AttributeTag;
using System.ComponentModel.DataAnnotations;

namespace BMS.Models.Device {
    public class BatteryClusterInfo {
        [Display(Name = "SN"), NotPointData]
        public string Sn { get; set; } = string.Empty;
        [Display(Name = "时间"), NotPointData]
        public DateTime UploadTime { get; set; }
        [NotPointData]
        public string DeviceType { get; set; } = string.Empty;
        [NotPointData]
        public string DeviceName { get; set; } = string.Empty;
        [NotPointData]
        public string DeviceId { get; set; } = string.Empty;
        [Display(Name = "设备启用"), PointIndex(0)]
        public bool DeviceEnable { get; set; }//设备启用0
        [Display(Name = "设备在线"), PointIndex(1)]
        public bool DeviceOnline { get; set; }//设备在线1
        [Display(Name = "总告警"), PointIndex(2)]
        public float TotalAlarm { get; set; }//总告警2
        [Display(Name = "总故障"), PointIndex(3)]
        public float TotalFault { get; set; }//总故障3
        [Display(Name = "单体过压告警一级"), PointIndex(4)]
        public float CellOvervoltageAlarmLevel1 { get; set; }//单体过压告警一级4
        [Display(Name = "单体欠压告警一级"), PointIndex(5)]
        public float CellUndervoltageAlarmLevel1 { get; set; }//单体欠压告警一级5
        [Display(Name = "单体过温告警一级"), PointIndex(6)]
        public float CellOvertemperatureAlarmLevel1 { get; set; }//单体过温告警一级6
        [Display(Name = "单体低温告警一级"), PointIndex(7)]
        public float CellLowTemperatureAlarmLevel1 { get; set; }//单体低温告警一级7
        [Display(Name = "单体压差告警一级"), PointIndex(8)]
        public float CellVoltageDifferenceAlarmLevel1 { get; set; }//单体压差告警一级8
        [Display(Name = "充电过流告警一级"), PointIndex(9)]
        public float ChargeOvercurrentAlarmLevel1 { get; set; }//充电过流告警一级9
        [Display(Name = "放电过流告警一级"), PointIndex(10)]
        public float DischargeOvercurrentAlarmLevel1 { get; set; }//放电过流告警一级10
        [Display(Name = "SOC过低告警一级"), PointIndex(11)]
        public float SocLowAlarmLevel1 { get; set; }//SOC过低告警一级11
        [Display(Name = "SOC差异过大告警一级"), PointIndex(12)]
        public float SocDifferenceTooLargeAlarmLevel1 { get; set; }//SOC差异过大告警一级12
        [Display(Name = "绝缘过低告警一级"), PointIndex(13)]
        public float InsulationLowAlarmLevel1 { get; set; }//绝缘过低告警一级13
        [Display(Name = "单体过压告警二级"), PointIndex(14)]
        public float CellOvervoltageAlarmLevel2 { get; set; }//单体过压告警二级14
        [Display(Name = "单体欠压告警二级"), PointIndex(15)]
        public float CellUndervoltageAlarmLevel2 { get; set; }//单体欠压告警二级15
        [Display(Name = "单体过温告警二级"), PointIndex(16)]
        public float CellOvertemperatureAlarmLevel2 { get; set; }//单体过温告警二级16
        [Display(Name = "单体低温告警二级"), PointIndex(17)]
        public float CellLowTemperatureAlarmLevel2 { get; set; }//单体低温告警二级17
        [Display(Name = "单体压差告警二级"), PointIndex(18)]
        public float CellVoltageDifferenceAlarmLevel2 { get; set; }//单体压差告警二级18
        [Display(Name = "充电过流告警二级"), PointIndex(19)]
        public float ChargeOvercurrentAlarmLevel2 { get; set; }//充电过流告警二级19
        [Display(Name = "放电过流告警二级"), PointIndex(20)]
        public float DischargeOvercurrentAlarmLevel2 { get; set; }//放电过流告警二级20
        [Display(Name = "SOC过低告警二级"), PointIndex(21)]
        public float SocLowAlarmLevel2 { get; set; }//SOC过低告警二级21
        [Display(Name = "SOC差异过大告警二级"), PointIndex(22)]
        public float SocDifferenceTooLargeAlarmLevel2 { get; set; }//SOC差异过大告警二级22
        [Display(Name = "绝缘过低告警二级"), PointIndex(23)]
        public float InsulationLowAlarmLevel2 { get; set; }//绝缘过低告警二级23
        [Display(Name = "单体过压告警三级"), PointIndex(24)]
        public float CellOvervoltageAlarmLevel3 { get; set; }//单体过压告警三级24
        [Display(Name = "单体欠压告警三级"), PointIndex(25)]
        public float CellUndervoltageAlarmLevel3 { get; set; }//单体欠压告警三级25
        [Display(Name = "单体过温告警三级"), PointIndex(26)]
        public float CellOvertemperatureAlarmLevel3 { get; set; }//单体过温告警三级26
        [Display(Name = "单体低温告警三级"), PointIndex(27)]
        public float CellLowTemperatureAlarmLevel3 { get; set; }//单体低温告警三级27
        [Display(Name = "单体压差告警三级"), PointIndex(28)]
        public float CellVoltageDifferenceAlarmLevel3 { get; set; }//单体压差告警三级28
        [Display(Name = "充电过流告警三级"), PointIndex(29)]
        public float ChargeOvercurrentAlarmLevel3 { get; set; }//充电过流告警三级29
        [Display(Name = "放电过流告警三级"), PointIndex(30)]
        public float DischargeOvercurrentAlarmLevel3 { get; set; }//放电过流告警三级30
        [Display(Name = "SOC过低告警三级"), PointIndex(31)]
        public float SocLowAlarmLevel3 { get; set; }//SOC过低告警三级31
        [Display(Name = "SOC差异过大告警三级"), PointIndex(32)]
        public float SocDifferenceTooLargeAlarmLevel3 { get; set; }//SOC差异过大告警三级32
        [Display(Name = "绝缘过低告警三级"), PointIndex(33)]
        public float InsulationLowAlarmLevel3 { get; set; }//绝缘过低告警三级33
        [Display(Name = "电芯温度极限告警"), PointIndex(34)]
        public float CellTemperatureLimitAlarm { get; set; }//电芯温度极限告警34
        [Display(Name = "电芯电压极限告警"), PointIndex(35)]
        public float CellVoltageLimitAlarm { get; set; }//电芯电压极限告警35
        [Display(Name = "簇间环流1级告警"), PointIndex(36)]
        public float InterClusterCirculationAlarmLevel1 { get; set; }//簇间环流1级告警36
        [Display(Name = "簇间环流2级告警"), PointIndex(37)]
        public float InterClusterCirculationAlarmLevel2 { get; set; }//簇间环流2级告警37
        [Display(Name = "簇间环流3级告警"), PointIndex(38)]
        public float InterClusterCirculationAlarmLevel3 { get; set; }//簇间环流3级告警38
        [Display(Name = "簇间电流差1级告警"), PointIndex(39)]
        public float InterClusterCurrentDifferenceAlarmLevel1 { get; set; }//簇间电流差1级告警39
        [Display(Name = "簇间电流差2级告警"), PointIndex(40)]
        public float InterClusterCurrentDifferenceAlarmLevel2 { get; set; }//簇间电流差2级告警40
        [Display(Name = "簇间电流差3级告警"), PointIndex(41)]
        public float InterClusterCurrentDifferenceAlarmLevel3 { get; set; }//簇间电流差3级告警41
        [Display(Name = "组端过压1级告警"), PointIndex(42)]
        public float GroupTerminalOvervoltageAlarmLevel1 { get; set; }//组端过压1级告警42
        [Display(Name = "组端过压2级告警"), PointIndex(43)]
        public float GroupTerminalOvervoltageAlarmLevel2 { get; set; }//组端过压2级告警43
        [Display(Name = "组端过压3级告警"), PointIndex(44)]
        public float GroupTerminalOvervoltageAlarmLevel3 { get; set; }//组端过压3级告警44
        [Display(Name = "组端欠压1级告警"), PointIndex(45)]
        public float GroupTerminalUndervoltageAlarmLevel1 { get; set; }//组端欠压1级告警45
        [Display(Name = "组端欠压2级告警"), PointIndex(46)]
        public float GroupTerminalUndervoltageAlarmLevel2 { get; set; }//组端欠压2级告警46
        [Display(Name = "组端欠压3级告警"), PointIndex(47)]
        public float GroupTerminalUndervoltageAlarmLevel3 { get; set; }//组端欠压3级告警47
        [Display(Name = "极柱过温1级告警"), PointIndex(48)]
        public float PoleOvertemperatureAlarmLevel1 { get; set; }//极柱过温1级告警48
        [Display(Name = "极柱过温2级告警"), PointIndex(49)]
        public float PoleOvertemperatureAlarmLevel2 { get; set; }//极柱过温2级告警49
        [Display(Name = "极柱过温3级告警"), PointIndex(50)]
        public float PoleOvertemperatureAlarmLevel3 { get; set; }//极柱过温3级告警50
        [Display(Name = "AFE温感排线异常"), PointIndex(51)]
        public float AfeTemperatureSensorCableAbnormal { get; set; }//AFE温感排线异常51
        [Display(Name = "AFE电压排线异常"), PointIndex(52)]
        public float AfeVoltageCableAbnormal { get; set; }//AFE电压排线异常52
        [Display(Name = "与电池簇通信告警"), PointIndex(53)]
        public float BatteryClusterCommunicationAlarm { get; set; }//与电池簇通信告警53
        [Display(Name = "主从通讯告警"), PointIndex(54)]
        public float MasterSlaveCommunicationAlarm { get; set; }//主从通讯告警54
        [Display(Name = "继电器粘连告警"), PointIndex(65)]
        public float RelayStickingAlarm { get; set; }//继电器粘连告警65
        [Display(Name = "电池极限故障"), PointIndex(66)]
        public float BatteryLimitFault { get; set; }//电池极限故障66
        [Display(Name = "熔丝故障"), PointIndex(55)]
        public float FuseFault { get; set; }//熔丝故障55
        [Display(Name = "断路器故障"), PointIndex(56)]
        public float CircuitBreakerFault { get; set; }//断路器故障56
        [Display(Name = "空调故障"), PointIndex(57)]
        public float AirConditionerFault { get; set; }//空调故障57
        [Display(Name = "消防设备故障"), PointIndex(58)]
        public float FirefightingEquipmentFault { get; set; }//消防设备故障58
        [Display(Name = "消防火警"), PointIndex(59)]
        public float FireAlarm { get; set; }//消防火警59
        [Display(Name = "消防喷洒"), PointIndex(60)]
        public float FireSprinkler { get; set; }//消防喷洒60
        [Display(Name = "AFE故障"), PointIndex(61)]
        public float AfeFault { get; set; }//AFE故障61
        [Display(Name = "高压异常"), PointIndex(62)]
        public float HighVoltageAbnormal { get; set; }//高压异常62
        [Display(Name = "预充告警"), PointIndex(63)]
        public float PreChargeAlarm { get; set; }//预充告警63
        [Display(Name = "开路故障"), PointIndex(64)]
        public float OpenCircuitFault { get; set; }//开路故障64
        [Display(AutoGenerateField = false), PointRange(65, 100)]
        public float[] AlarmStatusReserved { get; set; } //告警+状态保留继续~100
        [Display(Name = "总压"), PointIndex(101)]
        public float TotalVoltage { get; set; }//总压101
        [Display(Name = "总电流"), PointIndex(102)]
        public float TotalCurrent { get; set; }//总电流102
        [Display(Name = "SOC"), PointIndex(103)]
        public float Soc { get; set; }//SOC103
        [Display(Name = "SOH"), PointIndex(104)]
        public float Soh { get; set; }//SOH104
        [Display(Name = "SOE"), PointIndex(105)]
        public float Soe { get; set; }//SOE105
        [Display(Name = "额定总压"), PointIndex(106)]
        public float RatedTotalVoltage { get; set; }//额定总压106
        [Display(Name = "额定容量"), PointIndex(107)]
        public float RatedCapacity { get; set; }//额定容量107
        [Display(Name = "剩余容量"), PointIndex(108)]
        public float RemainingCapacity { get; set; }//剩余容量108
        [Display(Name = "额定电量"), PointIndex(109)]
        public float RatedEnergy { get; set; }//额定电量109
        [Display(Name = "剩余电量"), PointIndex(110)]
        public float RemainingEnergy { get; set; }//剩余电量110
        [Display(Name = "从机总数(BMU)"), PointIndex(111)]
        public float TotalSlavesBmu { get; set; }//从机总数(BMU)111
        [Display(Name = "在线从机总数(BMU)"), PointIndex(112)]
        public float OnlineSlavesBmu { get; set; }//在线从机总数(BMU)112
        [Display(Name = "电池总数"), PointIndex(113)]
        public float TotalBatteries { get; set; }//电池总数113
        [Display(Name = "在线电池总数"), PointIndex(114)]
        public float OnlineBatteries { get; set; }//在线电池总数114
        [Display(Name = "温感总数"), PointIndex(115)]
        public float TotalTemperatureSensors { get; set; }//温感总数115
        [Display(Name = "在线温感总数"), PointIndex(116)]
        public float OnlineTemperatureSensors { get; set; }//在线温感总数116
        [Display(Name = "最大允许放电电流"), PointIndex(117)]
        public float MaxAllowableDischargeCurrent { get; set; }//最大允许放电电流117
        [Display(Name = "最大允许放电功率"), PointIndex(118)]
        public float MaxAllowableDischargePower { get; set; }//最大允许放电功率118
        [Display(Name = "最大允许充电电流"), PointIndex(119)]
        public float MaxAllowableChargeCurrent { get; set; }//最大允许充电电流119
        [Display(Name = "最大允许充电功率"), PointIndex(120)]
        public float MaxAllowableChargePower { get; set; }//最大允许充电功率120
        [Display(Name = "正极绝缘阻值"), PointIndex(121)]
        public float PositiveInsulationResistance { get; set; }//正极绝缘阻值121
        [Display(Name = "负极绝缘阻值"), PointIndex(122)]
        public float NegativeInsulationResistance { get; set; }//负极绝缘阻值122
        [Display(Name = "单体平均电压"), PointIndex(123)]
        public float CellAverageVoltage { get; set; }//单体平均电压123
        [Display(Name = "单体最大压差"), PointIndex(124)]
        public float CellMaxVoltageDifference { get; set; }//单体最大压差124
        [Display(Name = "最高单体电压"), PointIndex(125)]
        public float HighestCellVoltage { get; set; }//最高单体电压125
        [Display(Name = "最高单体电压从机号"), PointIndex(126)]
        public float HighestCellVoltageSlaveId { get; set; }//最高单体电压从机号126
        [Display(Name = "最高单体电压编号"), PointIndex(127)]
        public float HighestCellVoltageId { get; set; }//最高单体电压编号127
        [Display(Name = "最低单体电压"), PointIndex(128)]
        public float LowestCellVoltage { get; set; }//最低单体电压128
        [Display(Name = "最低单体电压从机号"), PointIndex(129)]
        public float LowestCellVoltageSlaveId { get; set; }//最低单体电压从机号129
        [Display(Name = "最低单体电压编号"), PointIndex(130)]
        public float LowestCellVoltageId { get; set; }//最低单体电压编号130
        [Display(Name = "单体平均温度"), PointIndex(131)]
        public float CellAverageTemperature { get; set; }//单体平均温度131
        [Display(Name = "最大温差"), PointIndex(132)]
        public float MaxTemperatureDifference { get; set; }//最大温差132
        [Display(Name = "最高单体温度"), PointIndex(133)]
        public float HighestCellTemperature { get; set; }//最高单体温度133
        [Display(Name = "最高单体温度从机号"), PointIndex(134)]
        public float HighestCellTemperatureSlaveId { get; set; }//最高单体温度从机号134
        [Display(Name = "最高单体温度编号"), PointIndex(135)]
        public float HighestCellTemperatureId { get; set; }//最高单体温度编号135
        [Display(Name = "最低单体温度"), PointIndex(136)]
        public float LowestCellTemperature { get; set; }//最低单体温度136
        [Display(Name = "最低单体温度从机号"), PointIndex(137)]
        public float LowestCellTemperatureSlaveId { get; set; }//最低单体温度从机号137
        [Display(Name = "最低单体温度编号"), PointIndex(138)]
        public float LowestCellTemperatureId { get; set; }//最低单体温度编号138
        [Display(Name = "日充电容量"), PointIndex(139)]
        public float DailyChargeCapacity { get; set; }//日充电容量139
        [Display(Name = "日充电电量"), PointIndex(140)]
        public float DailyChargeEnergy { get; set; }//日充电电量140
        [Display(Name = "日放电容量"), PointIndex(141)]
        public float DailyDischargeCapacity { get; set; }//日放电容量141
        [Display(Name = "日放电电量"), PointIndex(142)]
        public float DailyDischargeEnergy { get; set; }//日放电电量142
        [Display(Name = "日充电时间"), PointIndex(143)]
        public float DailyChargeTime { get; set; }//日充电时间143
        [Display(Name = "日放电时间"), PointIndex(144)]
        public float DailyDischargeTime { get; set; }//日放电时间144
        [Display(Name = "累计充电容量"), PointIndex(145)]
        public float CumulativeChargeCapacity { get; set; }//累计充电容量145
        [Display(Name = "累计充电电量"), PointIndex(146)]
        public float CumulativeChargeEnergy { get; set; }//累计充电电量146
        [Display(Name = "累计放电容量"), PointIndex(147)]
        public float CumulativeDischargeCapacity { get; set; }//累计放电容量147
        [Display(Name = "累计放电电量"), PointIndex(148)]
        public float CumulativeDischargeEnergy { get; set; }//累计放电电量148
        [Display(Name = "累计充电时间"), PointIndex(149)]
        public float CumulativeChargeTime { get; set; }//累计充电时间149
        [Display(Name = "累计放电时间"), PointIndex(150)]
        public float CumulativeDischargeTime { get; set; }//累计放电时间150
        [Display(Name = "BCU工作状态"), PointIndex(151)]
        public float BcuOperatingStatus { get; set; }//BCU工作状态151
        [Display(AutoGenerateField = false), PointIndex(152)]
        public float FaultWord1 { get; set; }//故障字1152
        [Display(AutoGenerateField = false), PointIndex(153)]
        public float FaultWord2 { get; set; }//故障字2153
        [Display(AutoGenerateField = false), PointIndex(154)]
        public float FaultWord3 { get; set; }//故障字3154
        [Display(AutoGenerateField = false), PointIndex(155)]
        public float FaultWord4 { get; set; }//故障字4155
        [Display(AutoGenerateField = false), PointIndex(156)]
        public float FaultWord5 { get; set; }//故障字5156
        [Display(AutoGenerateField = false), PointIndex(157)]
        public float FaultWord6 { get; set; }//故障字6157
        [Display(AutoGenerateField = false), PointIndex(158)]
        public float FaultWord7 { get; set; }//故障字7158
        [Display(AutoGenerateField = false), PointIndex(159)]
        public float FaultWord8 { get; set; }//故障字8159
        [Display(AutoGenerateField = false), PointIndex(160)]
        public float FaultWord9 { get; set; }//故障字9160
        [Display(Name = "1#PACK~30#PACK均衡状态"), PointRange(161, 190)]
        public float[] Pack1ToPack30BalancingStatus { get; set; }//1#PACK~30#PACK均衡状态161-190
        [Display(Name = "1号~420号电池电压"), PointRange(191, 610)]
        public float[] BatteryVoltage1ToBattery420 { get; set; } //1号~420号电池电压191~610
        [Display(Name = "1号~200号电池温度"), PointRange(611, 810)]
        public float[] BatteryTemperature1ToBattery200 { get; set; }//1号~200号电池温度611~810
        [Display(AutoGenerateField = false), PointRange(811, 1000)]
        public float[] Reserved { get; set; }//预留811~1000*/
        [Display(Name = "高压下电指令"), PointIndex(1001)]
        public float HighVoltagePowerOffCommand { get; set; }//高压下电指令1001
        [Display(AutoGenerateField = false), PointRange(1001, 1999)]
        public float[] ReservedThresholdAndParameters { get; set; } //预留阈值及参数1001~1999




    }
}
