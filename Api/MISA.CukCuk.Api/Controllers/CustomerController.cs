using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Services;
using System;
using System.Linq;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/customers")]
    [ApiController]
    public class CustomerController : BaseController<Customer>
    {
        ICustomerService _customerService;

        public CustomerController(ICustomerService customerService): base(customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Lấy danh sách khách hàng có lọc và phân trang.
        /// </summary>
        /// <param name="customerFilter">Điều kiện lọc danh sách khách hàng.</param>
        /// <returns>Danh sách khách hàng.</returns>
        /// <response code="200">Có dữ liệu trả về.</response>
        /// <response code="204">Không có dữ liệu.</response>
        /// CreatedBy: dbhuan (19/04/2021)
        [HttpGet]
        public IActionResult GetCustomers([FromQuery] CustomerFilter customerFilter)
        {
            var paging = _customerService.GetCustomers(customerFilter);

            // Xử lý kết quả trả về cho client.
            if (paging.data.Any())
            {
                return Ok(paging);
            }

            return NoContent();
        }
    }
}
