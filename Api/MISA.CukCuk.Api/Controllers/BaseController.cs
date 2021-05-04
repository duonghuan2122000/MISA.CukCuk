using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Services;
using System;

namespace MISA.CukCuk.Api.Controllers
{
    [ApiController]
    public class BaseController<T> : ControllerBase where T : class
    {
        IBaseService<T> _baseService;

        public BaseController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Lấy thông tin một thực thể theo id.
        /// </summary>
        /// <param name="entityId">id của thực thể cần lấy thông tin.</param>
        /// <returns>Thông tin một thực thể.</returns>
        /// <response code="200">Có dữ liệu trả về.</response>
        /// <response code="204">Không có dữ liệu.</response>
        /// CreatedBy: dbhuan (19/04/2021)
        [HttpGet("{entityId}")]
        public virtual IActionResult Get([FromRoute] Guid entityId)
        {
            var entity = _baseService.Get(entityId);
            if (entity != null)
            {
                return Ok(entity);
            }
            return NoContent();
        }

        /// <summary>
        /// Thực hiện thêm mới một thực thể vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="t">Thông tin thực thể.</param>
        /// <returns>Số thực thể được thêm vào là 1</returns>
        /// <response code="201">Thêm mới thành công.</response>
        /// <response code="204">Thêm mới thất bại.</response>
        /// <response code="400">Có lỗi từ phía client.</response>
        /// <response code="500">Có lỗi từ phía server.</response>
        /// CreatedBy: dbhuan (19/04/2021)
        [HttpPost]
        public virtual IActionResult Post([FromBody] T t)
        {
            var res = _baseService.Insert(t);
            if (res > 0)
            {
                return StatusCode(201, res);
            }
            return NoContent();
        }

        /// <summary>
        /// Thực hiện cập nhật một thực thể vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="t">Thông tin thực thể.</param>
        /// <returns>Số thực thể được thêm vào là 1</returns>
        /// <response code="201">Thêm mới thành công.</response>
        /// <response code="204">Thêm mới thất bại.</response>
        /// <response code="400">Có lỗi từ phía client.</response>
        /// <response code="500">Có lỗi từ phía server.</response>
        /// CreatedBy: dbhuan (19/04/2021)
        [HttpPut]
        public virtual IActionResult Put([FromBody] T t)
        {
            var res = _baseService.Update(t);
            if (res > 0)
            {
                return StatusCode(201, res);
            }
            return NoContent();
        }

        /// <summary>
        /// Thực hiện xóa một thực thể.
        /// </summary>
        /// <param name="entityId">id thực thể cần xóa.</param>
        /// <returns>Số thực thể bị ảnh hưởng</returns>
        /// <response code="200">Xóa thành công.</response>
        /// <response code="204">Xóa thất bại.</response>
        /// <response code="400">Có lỗi từ phía client.</response>
        /// <response code="500">Có lỗi từ phía server.</response>
        [HttpDelete("{entityId}")]
        public virtual IActionResult Delete([FromRoute] Guid entityId)
        {
            var res = _baseService.Delete(entityId);
            if (res > 0)
            {
                return Ok(res);
            }
            return NoContent();
        }

        /// <summary>
        /// Xóa nhiều thực thể T.
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: dbhuan (04/05/2021)
        [HttpDelete]
        public virtual IActionResult Delete([FromBody] CustomerId customerId)
        {
            var res = _baseService.Delete(customerId.ids);
            if (res > 0)
            {
                return Ok(res);
            }
            return NoContent();
        }
    }
}
