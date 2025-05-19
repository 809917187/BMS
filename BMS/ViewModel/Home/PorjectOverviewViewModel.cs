namespace BMS.ViewModel.Home {
    public class PorjectOverviewViewModel {
        public int PorjectCount { get; set; } = 0;
        public int TotalDeviceCount { get; set; } = 0;
        public int ChargeDeviceCount { get; set; } = 0;
        public int ActicateDeviceCount { get; set; } = 0;
        public int OnlineDeviceCount { get; set; } = 0;
        public int AlarmCount { get; set; } = 0;
        public int CumulativeChargeEnergy { get; set; }
        public int CumulativeDischargeEnergy { get; set; }

    }
}
