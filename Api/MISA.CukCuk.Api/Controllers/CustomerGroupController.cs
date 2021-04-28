using Microsoft.AspNetCore.Mvc;
using MISA.Core.Interfaces.Services;
using System.Linq;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/customergroups")]
    [ApiController]
    public class CustomerGroupController : ControllerBase
    {
        ICustomerGroupService _customerGroupService;

        public CustomerGroupController(ICustomerGroupService customerGroupService)
        {
            _customerGroupService = customerGroupService;
        }

        /// <summary>
        /// Lấy toàn bộ nhóm khách hàng.
        /// </summary>
        /// <returns>Danh sách khách hàng.</returns>
        /// <response code="200">Có dữ liệu trả về.</response>
        /// <response code="204">Không có dữ liệu.</response>
        /// <response code="500">Có lỗi từ server.</response>
        /// CreatedBy: dbhuan (28/04/2021)
        [HttpGet]
        public IActionResult GetCustomerGroups()
        {
            var customerGroups = _customerGroupService.GetCustomerGroups();
            if (customerGroups.Any())
            {
                return Ok(customerGroups);
            }
            return NoContent();
        }

    }
}
