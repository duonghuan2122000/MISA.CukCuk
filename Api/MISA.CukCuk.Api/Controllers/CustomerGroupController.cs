using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Services;
using System;
using System.Linq;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/customer-groups")]
    [ApiController]
    public class CustomerGroupController : BaseController<CustomerGroup>
    {
        ICustomerGroupService _customerGroupService;

        public CustomerGroupController(ICustomerGroupService customerGroupService): base(customerGroupService)
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

        /// <summary>
        /// Private
        /// </summary>
        [NonAction]
        public override IActionResult Get([FromRoute] Guid entityId)
        {
            return base.Get(entityId);
        }

        /// <summary>
        /// Private
        /// </summary>
        [NonAction]
        public override IActionResult Post([FromBody] CustomerGroup t)
        {
            return base.Post(t);
        }

        /// <summary>
        /// private
        /// </summary>
        [NonAction]
        public override IActionResult Put([FromBody] CustomerGroup t)
        {
            return base.Put(t);
        }

        /// <summary>
        /// private
        /// </summary>
        [NonAction]
        public override IActionResult Delete([FromRoute] Guid entityId)
        {
            return base.Delete(entityId);
        }

        /// <summary>
        /// private
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [NonAction]
        public override IActionResult Delete([FromBody] CustomerId customerId)
        {
            return base.Delete(customerId);
        }
    }
}
