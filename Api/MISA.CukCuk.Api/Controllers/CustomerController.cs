using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Services;
using System;
using System.Linq;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Lấy danh sách khách hàng có lọc và phân trang.
        /// </summary>
        /// <param name="customerGroupId">Id nhóm khách hàng cần lọc.</param>
        /// <param name="page">Trang hiện tại.</param>
        /// <param name="pageSize">Số khách hàng trên một trang.</param>
        /// <param name="fullName">Lọc theo tên khách hàng.</param>
        /// <param name="phoneNumber">Lọc theo số điện thoại.</param>
        /// <returns>Danh sách khách hàng.</returns>
        /// <response code="200">Có dữ liệu trả về.</response>
        /// <response code="204">Không có dữ liệu.</response>
        /// CreatedBy: dbhuan (19/04/2021)
        [HttpGet]
        public IActionResult GetCustomers([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string fullName = "", [FromQuery] string phoneNumber = "", [FromQuery] Guid? customerGroupId = null)
        {
            var paging = _customerService.GetCustomers(page, pageSize, fullName, phoneNumber, customerGroupId);

            // Xử lý kết quả trả về cho client.
            if (paging.data.Any())
            {
                return Ok(paging);
            }

            return NoContent();
        }

        /// <summary>
        /// Lấy thông tin một khách hàng theo id.
        /// </summary>
        /// <param name="customerId">id của khách hàng cần lấy thông tin.</param>
        /// <returns>Thông tin một khách hàng.</returns>
        /// <response code="200">Có dữ liệu trả về.</response>
        /// <response code="204">Không có dữ liệu.</response>
        /// CreatedBy: dbhuan (19/04/2021)
        [HttpGet("{customerId}")]
        public IActionResult GetCustomer([FromRoute] Guid customerId)
        {
            var customer = _customerService.GetCustomer(customerId);

            if (customer != null)
            {
                return Ok(customer);
            }
            return NoContent();
        }

        /// <summary>
        /// Thực hiện thêm mới một khách hàng vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="customer">Thông tin khách hàng.</param>
        /// <returns>Số khách hàng được thêm vào là 1</returns>
        /// <response code="201">Thêm mới thành công.</response>
        /// <response code="204">Thêm mới thất bại.</response>
        /// <response code="400">Có lỗi từ phía client.</response>
        /// <response code="500">Có lỗi từ phía server.</response>
        /// CreatedBy: dbhuan (19/04/2021)
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            var res = _customerService.Insert(customer);
            if (res > 0)
            {
                return StatusCode(201, res);
            }
            return NoContent();
        }

        /// <summary>
        /// Thực hiện cập nhật thông tin một khách hàng vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="customer">Thông tin khách hàng.</param>
        /// <returns>Số khách hàng được thêm vào là 1</returns>
        /// <response code="201">Cập nhật thành công.</response>
        /// <response code="204">Cập nhật thất bại.</response>
        /// <response code="400">Có lỗi từ phía client.</response>
        /// <response code="500">Có lỗi từ phía server.</response>
        /// CreatedBy: dbhuan (19/04/2021)
        [HttpPut]
        public IActionResult Put([FromBody] Customer customer)
        {
            var res = _customerService.Update(customer);
            if (res > 0)
            {
                return StatusCode(201, res);
            }
            return NoContent();
        }

        /// <summary>
        /// Thực hiện xóa một khách hàng.
        /// </summary>
        /// <param name="customerId">id khách hàng cần xóa.</param>
        /// <returns>Số khách hàng bị ảnh hưởng</returns>
        /// <response code="200">Xóa thành công.</response>
        /// <response code="204">Xóa thất bại.</response>
        /// <response code="400">Có lỗi từ phía client.</response>
        /// <response code="500">Có lỗi từ phía server.</response>
        [HttpDelete]
        public IActionResult Delete([FromRoute] Guid customerId)
        {
            var res = _customerService.Delete(customerId);
            if (res > 0)
            {
                return StatusCode(201, res);
            }
            return NoContent();
        }
    }
}
