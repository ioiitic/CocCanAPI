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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var order = await _orderService.GetAllOrdersAsync();
            return Ok(order);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<OrderDTO>> Create([FromBody] CreateOrderDTO createOrderDTO)
        {
            if (createOrderDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var _newOrder = await _orderService.CreateOrderAsync(createOrderDTO);

            if (_newOrder.Success == false && _newOrder.Message == "Exist")
            {
                return Ok(_newOrder);
            }


            if (_newOrder.Success == false && _newOrder.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when adding order {createOrderDTO}");
                return StatusCode(500, ModelState);
            }

            if (_newOrder.Success == false && _newOrder.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when adding order {createOrderDTO}");
                return StatusCode(500, ModelState);
            }
            return Ok(_newOrder);
        }

        [HttpPatch("{id:Guid}", Name = "UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderDTO orderDTO)
        {
            if (orderDTO == null || orderDTO.Id != id)
            {
                return BadRequest(ModelState);
            }


            var _updateOrder = await _orderService.UpdateOrderAsync(orderDTO);

            if (_updateOrder.Success == false && _updateOrder.Message == "NotFound")
            {
                return Ok(_updateOrder);
            }

            if (_updateOrder.Success == false && _updateOrder.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when updating order {orderDTO}");
                return StatusCode(500, ModelState);
            }

            if (_updateOrder.Success == false && _updateOrder.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when updating order {orderDTO}");
                return StatusCode(500, ModelState);
            }

            return Ok(_updateOrder);
        }

        [HttpDelete("{id:Guid}", Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status409Conflict)] //Can not be removed 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {

            var _deleteOrder = await _orderService.SoftDeleteOrderAsync(id);


            if (_deleteOrder.Success == false && _deleteOrder.Data == "NotFound")
            {
                ModelState.AddModelError("", "Order Not found");
                return StatusCode(404, ModelState);
            }

            if (_deleteOrder.Success == false && _deleteOrder.Data == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in Repository when deleting order");
                return StatusCode(500, ModelState);
            }

            if (_deleteOrder.Success == false && _deleteOrder.Data == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in service layer when deleting order");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
