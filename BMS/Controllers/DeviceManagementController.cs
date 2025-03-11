using BMS.Common;
using BMS.Models.Device;
using BMS.Service;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Globalization;

namespace BMS.Controllers {
    public class DeviceManagementController : Controller {
        private readonly IDeviceManagementService _deviceManagementService;

        public DeviceManagementController(IDeviceManagementService deviceManagementService) {
            _deviceManagementService = deviceManagementService;
        }

        public IActionResult Index() {
            var ret = _deviceManagementService.GetAllDeviceInfo();
            return View(ret);
        }

        [HttpPost]
        public async Task<IActionResult> DeviceBindToProject([FromBody] DeviceBindToProjectModel model) {
            if (_deviceManagementService.DeviceBindToProject(model.deviceId, model.projectId)) {
                return Ok(new { message = "绑定成功" });
            } else {
                return BadRequest("绑定失败");
            }
        }

        public IActionResult DownloadTemplate() {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "Device Information Import.xlsx");

            // 返回文件作为下载
            return File(System.IO.File.ReadAllBytes(filePath), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Template.xlsx");
        }

        [HttpPost]
        public IActionResult ImportDeviceInfo(IFormFile file) {
            if (file == null || file.Length == 0) {
                return Json(new { success = false, message = "未选择文件" });
            }

            try {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                List<DeviceInfo> deviceInfos = new List<DeviceInfo>();
                // 使用 EPPlus 读取 Excel 文件
                using (var stream = new MemoryStream()) {
                    file.CopyTo(stream);
                    using (var package = new ExcelPackage(stream)) {
                        // 获取工作表
                        var worksheet = package.Workbook.Worksheets[0];

                        // 读取 Excel 内容（假设第一列是设备 ID，第二列是设备名称）
                        var devices = new List<string>(); // 存储设备信息
                        for (int row = 2; row <= worksheet.Dimension.End.Row; row++) // 从第二行开始读取
                        {
                            if (DateTime.TryParseExact(worksheet.Cells[row, 5].Text, Utility.TimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result)) {
                                DeviceInfo deviceInfo = new DeviceInfo();
                                deviceInfo.BMSSeriesNumber = worksheet.Cells[row, 2].Text;
                                deviceInfo.BatterySeriesNumber = worksheet.Cells[row, 3].Text;
                                deviceInfo.CarSeriesNumber = worksheet.Cells[row, 4].Text;
                                deviceInfo.ActivationTime = result;
                                deviceInfos.Add(deviceInfo);
                            }

                        }

                        if (_deviceManagementService.AddDeviceInfo(deviceInfos)) {
                            return Json(new { success = true, message = "设备信息导入成功", devices = devices });
                        } else {
                            return Json(new { success = false, message = "导入信息失败: " });
                        }
                    }
                }
            } catch (Exception ex) {
                return Json(new { success = false, message = "导入信息失败: " + ex.Message });
            }
        }
    }
}
