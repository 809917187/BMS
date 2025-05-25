using BMS.Models;
using BMS.Models.Device;
using BMS.Models.Home;
using BMS.Models.ProjectManagement;
using BMS.Service;
using BMS.ViewModel;
using BMS.ViewModel.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;

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


        /*
         GetProjectInfosByTypeAndIds查到选中的project
        根据project从GetAllDeviceByProject查到Device的信息

         
         
         */

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

        public IActionResult DeviceDetails(string sn) {
            var mode = _deviceManagementService.GetBatteryClusterInfosBySn(sn);
            return View(mode);
        }


        public IActionResult RealTimeStatus(string sn) {
            var model = _deviceManagementService.GetLatestBatteryClusterInfosBySn(sn);
            if (model == null) {
                return View("Error");
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<ProjectInfo> GetProjectInfosByTypeAndIds(SelectedInfo selectedInfo) {
            List<ProjectInfo> projectInfos = new List<ProjectInfo>();

            if (selectedInfo == null) {
                return projectInfos;
            }
            List<int> projectIds = new List<int>();
            if (selectedInfo.type == 1) {
                projectIds = selectedInfo.selectedIds.ToList();
            } else if (selectedInfo.type == 2) {
                projectIds = _projectManagementService.GetBindedProjectIdsByCustomerIds(selectedInfo.selectedIds);
            } else if (selectedInfo.type == 3) {
                projectIds = _projectManagementService.GetBindedProjectIdsByGroupIds(selectedInfo.selectedIds);
            }
            projectInfos = _projectManagementService.GetAllProjectInfo().FindAll(s => projectIds.Contains(s.Id)).ToList();
            return projectInfos;
        }

        private List<DeviceInfo> GetAllDeviceByProject(List<ProjectInfo> projectInfos) {
            return projectInfos.SelectMany(s => s.DeviceInfos).GroupBy(s => s.Sn).Select(g => g.First()).ToList();
        }

        private List<String> GetAllDeviceSNByTypeAndIds(SelectedInfo selectedInfo) {
            var projectInfos = this.GetProjectInfosByTypeAndIds(selectedInfo);
            return this.GetAllDeviceByProject(projectInfos).Select(S => S.Sn).ToList();
        }

        [HttpPost]
        public IActionResult GetProjectOverview([FromBody] SelectedInfo selectedInfo) {
            List<ProjectInfo> projectInfos = this.GetProjectInfosByTypeAndIds(selectedInfo);
            List<DeviceInfo> deviceInfos = this.GetAllDeviceByProject(projectInfos);

            PorjectOverviewViewModel ret = new PorjectOverviewViewModel();
            ret.PorjectCount = projectInfos.Count();
            ret.TotalDeviceCount = deviceInfos.Count;

            var _deviceInfos = _cache.Get("DeviceInfosCache") as List<DeviceInfo>;
            var _batteryClusterInfos = _cache.Get("BatteryClusterCache") as List<BatteryClusterInfo>;
            if (_deviceInfos == null || _batteryClusterInfos == null) {
                return BadRequest(string.Empty);
            }
            var allDeviceSNs = this.GetAllDeviceSNByTypeAndIds(selectedInfo);
            var latestData = _batteryClusterInfos.Where(s => allDeviceSNs.Contains(s.Sn));

            ret.CumulativeChargeEnergy = latestData.Sum(S => S.CumulativeChargeEnergy).ToString("f1");
            ret.CumulativeDischargeEnergy = latestData.Sum(S => S.CumulativeDischargeEnergy).ToString("f1");


            return Ok(ret);
        }



        [HttpPost]
        public IActionResult GetWorkingStatusChart([FromBody] SelectedInfo selectedInfo) {
            var _deviceInfos = _cache.Get("DeviceInfosCache") as List<DeviceInfo>;
            var _batteryClusterInfos = _cache.Get("BatteryClusterCache") as List<BatteryClusterInfo>;
            if (_deviceInfos == null || _batteryClusterInfos == null) {
                return BadRequest(string.Empty);
            }
            var allDeviceSNs = this.GetAllDeviceSNByTypeAndIds(selectedInfo);
            var latestData = _batteryClusterInfos.Where(s => allDeviceSNs.Contains(s.Sn));
            var result = new List<object>
                {
                    new { status = "离线", count = latestData.Count(d => !d.DeviceOnline ) },
                    new { status = "待机", count = latestData.Count(d => d.BcuOperatingStatus==1) },
                    new { status = "充电", count = latestData.Count(d => d.BcuOperatingStatus==2) },
                    new { status = "放电", count = latestData.Count(d => d.BcuOperatingStatus==3) }
                };
            return Ok(result);

        }

        [HttpPost]
        public IActionResult GetSummaryAnalysisChart([FromBody] SelectedInfo selectedInfo) {
            var _deviceInfos = _cache.Get("DeviceInfosCache") as List<DeviceInfo>;
            if (_deviceInfos == null) {
                return BadRequest(string.Empty);
            }
            var allDeviceSNs = this.GetAllDeviceSNByTypeAndIds(selectedInfo);
            List<BatteryClusterInfo> data = _deviceManagementService.GetDailyBatteryClusterInfos(allDeviceSNs);
            if (selectedInfo.summaryAnalysisType == 1) {
                return Ok(data.GroupBy(s => s.Sn).Select(g => new {
                    sn = g.Key,
                    data = g.OrderBy(x => x.UploadTime).Select(x => new {
                        time = x.UploadTime.ToString("HH:mm:ss"),
                        value = x.HighestCellVoltage
                    })
                }));
            }else if (selectedInfo.summaryAnalysisType == 2) {
                return Ok(data.GroupBy(s => s.Sn).Select(g => new {
                    sn = g.Key,
                    data = g.OrderBy(x => x.UploadTime).Select(x => new {
                        time = x.UploadTime.ToString("HH:mm:ss"),
                        value = x.CellAverageTemperature
                    })
                }));
            }


            return BadRequest(string.Empty);

        }

        [HttpPost]
        public IActionResult GetDeviceList([FromBody] SelectedInfo selectedInfo) {
            var _deviceInfos = _cache.Get("DeviceInfosCache") as List<DeviceInfo>;
            var _batteryClusterInfos = _cache.Get("BatteryClusterCache") as List<BatteryClusterInfo>;
            if (_deviceInfos == null || _batteryClusterInfos == null) {
                return BadRequest(string.Empty);
            }
            var allDeviceSNs = this.GetAllDeviceSNByTypeAndIds(selectedInfo);
            var data = _deviceInfos.Where(s => allDeviceSNs.Contains(s.Sn));

            return Ok(data);

        }

        [HttpPost]
        public IActionResult GetSOCStatusChart([FromBody] SelectedInfo selectedInfo) {
            var _deviceInfos = _cache.Get("DeviceInfosCache") as List<DeviceInfo>;
            var _batteryClusterInfos = _cache.Get("BatteryClusterCache") as List<BatteryClusterInfo>;
            if (_deviceInfos == null || _batteryClusterInfos == null) {
                return BadRequest(string.Empty);
            }
            var allDeviceSNs = this.GetAllDeviceSNByTypeAndIds(selectedInfo);
            var latestData = _batteryClusterInfos.Where(s => allDeviceSNs.Contains(s.Sn));
            var result = new List<object>
            {
                new { status = ">80%", count = latestData.Count(d => Convert.ToDouble(d.Soc)>80 ) },
                new { status = "20%~80%", count = latestData.Count(d => Convert.ToDouble(d.Soc)>=20&& Convert.ToDouble(d.Soc)<=80) },
                new { status = "<20%", count = latestData.Count(d => Convert.ToDouble(d.Soc)<20) }
            };
            return Ok(result);

        }

        [HttpPost]
        public IActionResult GetAlarmStatusChart([FromBody] SelectedInfo selectedInfo) {
            var _deviceInfos = _cache.Get("DeviceInfosCache") as List<DeviceInfo>;
            var _batteryClusterInfos = _cache.Get("BatteryClusterCache") as List<BatteryClusterInfo>;
            if (_deviceInfos == null || _batteryClusterInfos == null) {
                return BadRequest(string.Empty);
            }
            var allDeviceSNs = this.GetAllDeviceSNByTypeAndIds(selectedInfo);
            var latestData = _batteryClusterInfos.Where(s => allDeviceSNs.Contains(s.Sn));

            int alarm1 = 0;
            int alarm2 = 0;
            int alarm3 = 0;
            foreach (BatteryClusterInfo batteryClusterInfo in latestData) {
                var alarmPros = typeof(BatteryClusterInfo).GetProperties()
                    .Where(p => p.GetCustomAttribute<BMS.AttributeTag.AlarmAttribute>() != null)
                    .Select(p => new {
                        AlarmIndex = p.GetCustomAttribute<BMS.AttributeTag.AlarmAttribute>().Index,
                        AlarmValue = p.GetValue(batteryClusterInfo)
                    }).Where(x => Convert.ToInt32(x.AlarmValue) == 1).ToList();
                foreach (var prop in alarmPros) {
                    if (prop.AlarmIndex == 1) {
                        alarm1++;
                    } else if (prop.AlarmIndex == 2) {
                        alarm2++;
                    } else {
                        alarm3++;
                    }
                }
            }


            var result = new List<object>
            {
                new { status = "轻微告警", count = alarm1 },
                new { status = "一般告警", count = alarm2 },
                new { status = "严重告警", count = alarm3 },
            };
            return Ok(result);

        }


        [HttpPost]
        public IActionResult GetLast24HourStatusChart([FromBody] SelectedInfo selectedInfo) {
            var _deviceInfos = _cache.Get("DeviceInfosCache") as List<DeviceInfo>;
            var _batteryClusterInfos = _cache.Get("BatteryClusterCache") as List<BatteryClusterInfo>;
            if (_deviceInfos == null || _batteryClusterInfos == null) {
                return BadRequest(string.Empty);
            }
            var allDeviceSNs = this.GetAllDeviceSNByTypeAndIds(selectedInfo);
            var latestData = _batteryClusterInfos.Where(s => allDeviceSNs.Contains(s.Sn));
            var now = DateTime.Now;

            var data = Enumerable.Range(0, 24).Select(i => new {
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