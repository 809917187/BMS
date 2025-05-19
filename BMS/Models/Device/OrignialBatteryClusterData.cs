namespace BMS.Models.Device {
    public class OrignialBatteryClusterData {
        public string Sn { get; set; } = string.Empty;
        public DateTime UploadTime { get; set; }
        public string DeviceType { get; set; } = string.Empty;
        public string DeviceName { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public int[] PointData { get; set; }

    }
}
