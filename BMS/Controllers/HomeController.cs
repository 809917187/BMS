using BMS.Models;
using BMS.Service;
using BMS.ViewModel;
using BMS.ViewModel.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BMS.Controllers {
    public class HomeController : Controller {
        private readonly IProjectManagementService _projectManagementService;
        private readonly ICustomerManagementService _customerManagementService;
        private readonly IGroupManagementService _groupManagementService;

        public HomeController(IProjectManagementService projectManagementService, ICustomerManagementService customerManagementService, IGroupManagementService groupManagementService) {
            _projectManagementService = projectManagementService;
            _customerManagementService = customerManagementService;
            _groupManagementService = groupManagementService;
        }

        public IActionResult Index() {
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
    }
}