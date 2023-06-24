using CocCanService.DTOs.OrderDetail;
using CocCanService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using CocCanService.DTOs.Order;

namespace CocCanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailsController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderDetailDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var orderDetail = await _orderDetailService.GetAllOrderDetailsAsync();
            return Ok(orderDetail);
        }

        [HttpPost("{Guid:id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDetailDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<OrderDetailDTO>> Create(Guid id, [FromBody] List<CreateOrderDetailDTO> createOrderDetailDTOList)
        {
            if (createOrderDetailDTOList == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var _newOrderDetail = await _orderDetailService.CreateOrderDetailAsync(id, createOrderDetailDTOList);

            if (_newOrderDetail.Status == false && _newOrderDetail.Title == "RepoError")
            {
                foreach (string error in _newOrderDetail.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_newOrderDetail.Status == false && _newOrderDetail.Title == "Error")
            {
                foreach (string error in _newOrderDetail.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }
            return Ok(_newOrderDetail);
        }

        [HttpPatch("{id:Guid}", Name = "UpdateOrderDetail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOrderDetail(Guid id, [FromBody] UpdateOrderDetailDTO updateOrderDetailDTO)
        {
            var _updateOrderDetail = await _orderDetailService.UpdateOrderDetailAsync(id, updateOrderDetailDTO);

            if (_updateOrderDetail.Status == false && _updateOrderDetail.Title == "RepoError")
            {
                foreach (string error in _updateOrderDetail.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_updateOrderDetail.Status == false && _updateOrderDetail.Title == "Error")
            {
                foreach (string error in _updateOrderDetail.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }
            return Ok(_updateOrderDetail);
        }

        [HttpDelete("{id:Guid}", Name = "DeleteOrderDetail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status409Conflict)] //Can not be removed 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {

            var _deleteOrderDetail = await _orderDetailService.SoftDeleteOrderDetailAsync(id);

            if (_deleteOrderDetail.Status == false && _deleteOrderDetail.Title == "RepoError")
            {
                foreach (string error in _deleteOrderDetail.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_deleteOrderDetail.Status == false && _deleteOrderDetail.Title == "Error")
            {
                foreach (string error in _deleteOrderDetail.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
