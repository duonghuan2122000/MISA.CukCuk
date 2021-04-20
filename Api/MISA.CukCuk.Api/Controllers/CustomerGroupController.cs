using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
