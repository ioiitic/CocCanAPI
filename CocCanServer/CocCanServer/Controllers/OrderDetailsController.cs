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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDetailDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<OrderDetailDTO>> Create([FromBody] CreateOrderDetailDTO createOrderDetailDTO)
        {
            if (createOrderDetailDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var _newOrderDetail = await _orderDetailService.CreateOrderDetailAsync(createOrderDetailDTO);

            if (_newOrderDetail.Success == false && _newOrderDetail.Message == "Exist")
            {
                return Ok(_newOrderDetail);
            }


            if (_newOrderDetail.Success == false && _newOrderDetail.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when adding order detail {createOrderDetailDTO}");
                return StatusCode(500, ModelState);
            }

            if (_newOrderDetail.Success == false && _newOrderDetail.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when adding order detail {createOrderDetailDTO}");
                return StatusCode(500, ModelState);
            }
            return Ok(_newOrderDetail);
        }

        [HttpPatch("{id:Guid}", Name = "UpdateOrderDetail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOrderDetail(Guid id, [FromBody] OrderDetailDTO orderDetailDTO)
        {
            if (orderDetailDTO == null || orderDetailDTO.Id != id)
            {
                return BadRequest(ModelState);
            }


            var _updateOrderDetail = await _orderDetailService.UpdateOrderDetailAsync(orderDetailDTO);

            if (_updateOrderDetail.Success == false && _updateOrderDetail.Message == "NotFound")
            {
                return Ok(_updateOrderDetail);
            }

            if (_updateOrderDetail.Success == false && _updateOrderDetail.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when updating order detail {orderDetailDTO}");
                return StatusCode(500, ModelState);
            }

            if (_updateOrderDetail.Success == false && _updateOrderDetail.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when updating order detail {orderDetailDTO}");
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


            if (_deleteOrderDetail.Success == false && _deleteOrderDetail.Data == "NotFound")
            {
                ModelState.AddModelError("", "Order Detail Not found");
                return StatusCode(404, ModelState);
            }

            if (_deleteOrderDetail.Success == false && _deleteOrderDetail.Data == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in Repository when deleting order detail");
                return StatusCode(500, ModelState);
            }

            if (_deleteOrderDetail.Success == false && _deleteOrderDetail.Data == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in service layer when deleting order detail");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
