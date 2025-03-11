using BMS.Models.CustomerManagement;
using BMS.Models.ProjectManagement;
using BMS.Service;
using BMS.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BMS.Controllers {
    public class CustomerManagementController : Controller {
        private readonly ICustomerManagementService _customerManagementService;
        public CustomerManagementController(ICustomerManagementService customerManagementService) {
            _customerManagementService = customerManagementService;
        }


        public IActionResult Index() {
            List<CustomerListViewModel> customerListViewModel = new List<CustomerListViewModel>();
            var allCustomer = _customerManagementService.GetAllCustomerInfos();
            foreach (var c in allCustomer) {
                customerListViewModel.Add(new CustomerListViewModel() {
                    CustomerId = c.Id,
                    CustomerName = c.CustomerName,
                    CreateTime = c.CreateTime,
                    ProjectCount = c.ProjectInfos.Count,
                    DeviceCount = c.ProjectInfos.Sum(s => s.DeviceCount)
                });
            }
            return View(customerListViewModel);
        }

        [HttpGet]
        public IActionResult AddCustomerInfo() {
            return View();
        }

        [HttpPost]
        public IActionResult AddCustomerInfo([FromForm] CustomerInfo model) {
            try {
                //电站名称存在，返回
                var infos = _customerManagementService.GetAllCustomerInfos();
                if (infos.FindAll(s => s.CustomerName == model.CustomerName).Count > 0) {
                    return Ok(new { status = 500, message = "客户已经存在了" });
                } else {
                    if (_customerManagementService.AddCustomerInfos(model)) {
                        return Ok(new { status = 200, message = "客户创建成功" });
                    } else {
                        return BadRequest("客户创建失败");
                    }
                }

            } catch (Exception ex) {
                return BadRequest("项目新建失败");
            }
        }

        [HttpPost]
        public async Task<IActionResult> BindProjectToCustomer([FromBody] ProjectBindToCustomerModel model) {
            if (_customerManagementService.BindProjectToCustomer(model.customerId, model.projectIds)) {
                return Ok(new { message = "绑定成功" });
            } else {
                return BadRequest("绑定失败");
            }
        }
    }
}
