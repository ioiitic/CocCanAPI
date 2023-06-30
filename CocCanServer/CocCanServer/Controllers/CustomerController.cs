using CocCanService.DTOs.Customer;
using CocCanService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using System;

namespace CocCanAPI.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class CustomersController : ControllerBase
        {
            private readonly ICustomerService _CustomerService;

            public CustomersController(ICustomerService CustomerService)
            {
                _CustomerService = CustomerService;
            }

            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerDTO>))]
            public async Task<IActionResult> GetAll(string filter, string range, string sort)
            {
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            HttpContext.Response.Headers.Add("Content-Range", "customers 0-1/2");
            var Customers = await _CustomerService.GetAllCustomersAsync();
                if (Customers.Status == false && Customers.Title == "Error")
                {
                    foreach (string error in Customers.ErrorMessages)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return StatusCode(500, ModelState);
                }
                return Ok(Customers.Data);
            }

            [HttpPost]
            [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDTO))]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            [ProducesDefaultResponseType]
            public async Task<ActionResult<CustomerDTO>> CreateCustomer([FromBody] CreateCustomerDTO createCustomerDTO)
            {
                if (createCustomerDTO == null)
                {
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid) { return BadRequest(ModelState); }

                var _newCustomer = await _CustomerService.CreateCustomerAsync(createCustomerDTO);

                if (_newCustomer.Status == false && _newCustomer.Title == "Error")
                {
                    foreach (string error in _newCustomer.ErrorMessages)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return StatusCode(500, ModelState);
                }
                return Ok(_newCustomer.Data);
            }

            [HttpPut("{id:Guid}", Name = "UpdateCustomer")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] UpdateCustomerDTO updateCustomerDTO)
            {
                if (updateCustomerDTO == null)
                {
                    return BadRequest(ModelState);
                }

                var _updateCustomer = await _CustomerService.UpdateCustomerAsync(id, updateCustomerDTO);

                if (_updateCustomer.Status == false && _updateCustomer.Title == "Error")
                {
                    foreach (string error in _updateCustomer.ErrorMessages)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return StatusCode(500, ModelState);
                }

                return Ok(_updateCustomer);
            }

            [HttpDelete("{id:Guid}", Name = "DeleteCustomer")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
            [ProducesResponseType(StatusCodes.Status409Conflict)] //Can not be removed 
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> DeleteCustomer(Guid id)
            {

                var _deleteCustomer = await _CustomerService.SoftDeleteCustomerAsync(id);

                if (_deleteCustomer.Status == false && _deleteCustomer.Title == "Error")
                {
                    foreach (string error in _deleteCustomer.ErrorMessages)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return StatusCode(500, ModelState);
                }

                return NoContent();

            }
        }
    
}
