using BMS.Models;
using BMS.Models.Device;
using BMS.Service;
using BMS.ViewModel;
using BMS.ViewModel.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace BMS.Controllers {
    public class HomeController : Controller {
        private readonly IProjectManagementService _projectManagementService;
        private readonly ICustomerManagementService _customerManagementService;
        private readonly IGroupManagementService _groupManagementService;
        private readonly IDeviceManagementService _deviceManagementService;
        private readonly IMemoryCache _cache;

        public HomeController(IProjectManagementService projectManagementService, ICustomerManagementService customerManagementService,
            IMemoryCache cache, IGroupManagementService groupManagementService, IDeviceManagementService deviceManagementService) {
            _projectManagementService = projectManagementService;
            _customerManagementService = customerManagementService;
            _groupManagementService = groupManagementService;
            _deviceManagementService = deviceManagementService;
            _cache = cache;
        }

        private void SetCacheData() {
            // 如果缓存不存在，从数据库查询
            List<BatteryClusterInfo> batteryClusterInfos = _deviceManagementService.GetLatestBatteryClusterInfos();
            List<DeviceInfo> deviceInfos = _deviceManagementService.GetAllDeviceInfo();
            // 设置缓存，10分钟后自动失效
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetPriority(CacheItemPriority.NeverRemove); // 设置为永不过期

            _cache.Set("BatteryClusterCache", batteryClusterInfos, cacheOptions);
            _cache.Set("DeviceInfosCache", deviceInfos, cacheOptions);
        }

        public IActionResult Index() {
            SetCacheData();
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.projectInfos = _projectManagementService.GetAllProjectInfo();
            homeViewModel.customerInfos = _customerManagementService.GetAllCustomerInfos();
            homeViewModel.groupInfos = _groupManagementService.GetAllGroupInfos();
            return View(homeViewModel);
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult GetProjectOverviewByProject([FromBody] List<int> selectedProjectIds) {
            var projectInfos = _projectManagementService.GetAllProjectInfo().FindAll(s => selectedProjectIds.Contains(s.Id)).ToList();

            PorjectOverviewViewModel ret = new PorjectOverviewViewModel();
            ret.PorjectCount = projectInfos.Count();
            ret.TotalDeviceCount = projectInfos.Sum(s => s.DeviceInfos.Count);

            return Ok(ret); // 返回 200 状态码 + JSON 数据
        }

        [HttpPost]
        public IActionResult GetWorkingStatusChartByProject([FromBody] List<int> selectedProjectIds) {
            var _deviceInfos = _cache.Get("DeviceInfosCache") as List<DeviceInfo>;
            var _batteryClusterInfos = _cache.Get("BatteryClusterCache") as List<BatteryClusterInfo>;
            if (_deviceInfos == null || _batteryClusterInfos == null) {
                return BadRequest(string.Empty);
            }

            var allDeviceSNs = _deviceInfos.Where(s => selectedProjectIds.Contains(s.projectInfo.Id)).Select(s => s.BatterySeriesNumber);
            var latestData = _batteryClusterInfos.Where(s => allDeviceSNs.Contains(s.Sn));
            var result = new List<object>
                {
                    new { status = "在线", count = latestData.Count(d => d.DeviceOnline ) },
                    new { status = "离线", count = latestData.Count(d => !d.DeviceOnline ) },
                    new { status = "充电", count = latestData.Count(d => d.DeviceEnable) },
                    new { status = "空闲", count = latestData.Count(d => !d.DeviceEnable) }
                };
            return Ok(result);

        }

        [HttpPost]
        public IActionResult GetSOCStatusChartByProject([FromBody] List<int> selectedProjectIds) {
            var _deviceInfos = _cache.Get("DeviceInfosCache") as List<DeviceInfo>;
            var _batteryClusterInfos = _cache.Get("BatteryClusterCache") as List<BatteryClusterInfo>;
            if (_deviceInfos == null || _batteryClusterInfos == null) {
                return BadRequest(string.Empty);
            }

            var allDeviceSNs = _deviceInfos.Where(s => selectedProjectIds.Contains(s.projectInfo.Id)).Select(s => s.BatterySeriesNumber);
            var latestData = _batteryClusterInfos.Where(s => allDeviceSNs.Contains(s.Sn));
            var result = new List<object>
            {
                new { status = ">80%", count = latestData.Count(d => Convert.ToDouble(d.Soc)>0.8 ) },
                new { status = "20%~80%", count = latestData.Count(d => Convert.ToDouble(d.Soc)>=0.2&& Convert.ToDouble(d.Soc)<=0.8) },
                new { status = "<20%", count = latestData.Count(d => Convert.ToDouble(d.Soc)<0.2) }
            };
            return Ok(result);

        }

        [HttpPost]
        public IActionResult GetAlarmStatusChartByProject([FromBody] List<int> selectedProjectIds) {
            var _deviceInfos = _cache.Get("DeviceInfosCache") as List<DeviceInfo>;
            var _batteryClusterInfos = _cache.Get("BatteryClusterCache") as List<BatteryClusterInfo>;
            if (_deviceInfos == null || _batteryClusterInfos == null) {
                return BadRequest(string.Empty);
            }

            var allDeviceSNs = _deviceInfos.Where(s => selectedProjectIds.Contains(s.projectInfo.Id)).Select(s => s.BatterySeriesNumber);
            var latestData = _batteryClusterInfos.Where(s => allDeviceSNs.Contains(s.Sn));
            var result = new List<object>
            {
                new { status = "一级告警", count = 1 },
                new { status = "二级告警", count = 2 },
                new { status = "三级告警", count = 3 }
            };
            return Ok(result);

        }


        [HttpPost]
        public IActionResult GetLast24HourStatusChartByProject([FromBody] List<int> selectedProjectIds) {
            var _deviceInfos = _cache.Get("DeviceInfosCache") as List<DeviceInfo>;
            var _batteryClusterInfos = _cache.Get("BatteryClusterCache") as List<BatteryClusterInfo>;
            if (_deviceInfos == null || _batteryClusterInfos == null) {
                return BadRequest(string.Empty);
            }

            var allDeviceSNs = _deviceInfos.Where(s => selectedProjectIds.Contains(s.projectInfo.Id)).Select(s => s.BatterySeriesNumber);
            var latestData = _batteryClusterInfos.Where(s => allDeviceSNs.Contains(s.Sn));
            var now = DateTime.Now;

            var data = Enumerable.Range(0, 24).Select(i => new
            {
                hour = now.AddHours(-i).ToString("HH时"),
                charging = Random.Shared.Next(5, 15),
                discharging = Random.Shared.Next(3, 10),
                idle = Random.Shared.Next(2, 8),
                offline = Random.Shared.Next(1, 5)
            }).Reverse().ToList();
            return Ok(data);

        }
    }
}