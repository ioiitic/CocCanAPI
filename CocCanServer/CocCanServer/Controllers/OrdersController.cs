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
        public async Task<IActionResult> GetAll(string filter, string range, string sort)
        {
            var order = await _orderService.GetAllOrdersAsync(filter,range,sort);
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            HttpContext.Response.Headers.Add("Content-Range", "orders 0-1/2");
            return Ok(order.Data);
        }
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderDTO>))]
        public async Task<IActionResult> GetAllOrderByOrderId(Guid id)
        {
<<<<<<< HEAD
            var order = await _orderService.GetAllOrdersByCustomerIdAsync(id);
            return Ok(order.Data);
=======
            var order = await _orderService.GetOrderByIdAsync(id);
            return Ok(order);
>>>>>>> NT4
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

            if (_newOrder.Status == false && _newOrder.Title == "RepoError")
            {
                foreach (string error in _newOrder.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_newOrder.Status == false && _newOrder.Title == "Error")
            {
                foreach (string error in _newOrder.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }
            return Ok(_newOrder.Data);
        }

        [HttpPut("{id:Guid}", Name = "UpdateOrder")]
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

            if (_updateOrder.Status == false && _updateOrder.Title == "RepoError")
            {
                foreach (string error in _updateOrder.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_updateOrder.Status == false && _updateOrder.Title == "Error")
            {
                foreach (string error in _updateOrder.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            return Ok(_updateOrder.Data);
        }

        [HttpDelete("{id:Guid}", Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status409Conflict)] //Can not be removed 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {

            var _deleteOrder = await _orderService.SoftDeleteOrderAsync(id);

            if (_deleteOrder.Status == false && _deleteOrder.Title == "RepoError")
            {
                foreach (string error in _deleteOrder.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_deleteOrder.Status == false && _deleteOrder.Title == "Error")
            {
                foreach (string error in _deleteOrder.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
    }
}
