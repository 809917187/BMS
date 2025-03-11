using BMS.Models.CustomerManagement;
using BMS.Models.GroupManagement;
using BMS.Service;
using BMS.ViewModel;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace BMS.Controllers {
    public class GroupManagementController : Controller {
        private readonly IGroupManagementService _groupManagementService;
        public GroupManagementController(IGroupManagementService groupManagementService) {
            _groupManagementService = groupManagementService;
        }

        public IActionResult Index() {
            List<GroupListViewModel> groupListViewModels = new List<GroupListViewModel>();
            var groups = _groupManagementService.GetAllGroupInfos();
            foreach (GroupInfo groupInfo in groups) {
                groupListViewModels.Add(new GroupListViewModel() {
                    GroupName = groupInfo.GroupName,
                    ProjectCount = groupInfo.ProjectInfos.Count,
                    DeviceCount = groupInfo.ProjectInfos.Sum(s => s.DeviceCount),
                    CreateTime = groupInfo.CreateTime,
                    GroupId = groupInfo.Id
                });
            }
            return View(groupListViewModels);
        }

        [HttpGet]
        public IActionResult AddGroup() {
            return View();
        }

        [HttpPost]
        public IActionResult AddGroup([FromForm] GroupInfo model) {
            try {
                //电站名称存在，返回
                var infos = _groupManagementService.GetAllGroupInfos();
                if (infos.FindAll(s => s.GroupName == model.GroupName).Count > 0) {
                    return Ok(new { status = 500, message = "分组已经存在了" });
                } else {
                    if (_groupManagementService.AddGroup(model)) {
                        return Ok(new { status = 200, message = "分组创建成功" });
                    } else {
                        return BadRequest("分组创建失败");
                    }
                }

            } catch (Exception ex) {
                return BadRequest("项目新建失败");
            }
        }


        [HttpPost]
        public async Task<IActionResult> BindProjectToGroup([FromBody] ProjectBindToGroupModel model) {
            if (_groupManagementService.BindProjectToGroup(model.groupId, model.projectIds)) {
                return Ok(new { message = "绑定成功" });
            } else {
                return BadRequest("绑定失败");
            }
        }
    }
}
