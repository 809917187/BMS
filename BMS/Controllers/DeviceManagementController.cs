using BMS.Models.Device;
using BMS.Service;
using Microsoft.AspNetCore.Mvc;

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
    }
}
