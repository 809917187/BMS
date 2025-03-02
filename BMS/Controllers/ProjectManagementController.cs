using BMS.Models.ProjectManagement;
using BMS.Service;
using Microsoft.AspNetCore.Mvc;

namespace BMS.Controllers {
    public class ProjectManagementController : Controller {
        private readonly IProjectManagementService _projectManagementService;
        public ProjectManagementController(IProjectManagementService projectManagementService) {
            _projectManagementService = projectManagementService;
        }


        public IActionResult Index() {
            return View(_projectManagementService.GetAllProjectInfo());
        }

        [HttpGet]
        public IActionResult AddProject() {
            return View();
        }

        [HttpPost]
        public IActionResult AddProject([FromForm] ProjectInfo model) {
            try {
                //电站名称存在，返回
                var projectInfos = _projectManagementService.GetAllProjectInfo();
                if (projectInfos.FindAll(s => s.CustomerProjectNumber == model.CustomerProjectNumber).Count > 0) {
                    return Ok(new { status = 500, message = "客户项目序列号已经存在了" });
                } else {
                    if (_projectManagementService.AddProjectInfo(model)) {
                        return Ok(new { status = 200, message = "项目创建成功" });
                    } else {
                        return BadRequest("项目新建失败");
                    }

                }

            } catch (Exception ex) {
                return BadRequest("项目新建失败");
            }
        }

        [HttpGet]
        public IActionResult GetProjectList(int deviceId) {
            var allProjectInfos = _projectManagementService.GetAllProjectInfo();

            var bindProject = _projectManagementService.GetBindedProjectByDeviceId(deviceId);

            if (bindProject != null) {
                allProjectInfos.Find(s => s.Id == bindProject.Id).IsSelected = true;
            }

            return Ok(allProjectInfos);
        }
    }
}
